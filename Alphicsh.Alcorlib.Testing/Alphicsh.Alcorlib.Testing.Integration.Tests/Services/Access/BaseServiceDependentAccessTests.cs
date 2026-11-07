using Alphicsh.Alcorlib.Testing.Services;

namespace Alphicsh.Alcorlib.Testing.Integration.Services.Access;

public class BaseServiceDependentAccessTests
    : BaseServiceDependentTest<AccessTestProviderFixture>, IClassFixture<AccessTestProviderFixture>
{
    public BaseServiceDependentAccessTests(AccessTestProviderFixture fixture) : base(fixture)
    {
    }

    // ---------------
    // Simple services
    // ---------------

    [Fact]
    public void SimpleServiceRequiredAccess_ShouldGetService()
    {
        var loremService = GetTestService<TestServices.ILoremService>();
        Assert.Equal("Lorem", loremService.GetLorem());

        var ipsumService = GetTestService<TestServices.IIpsumService>();
        Assert.Equal("Ipsum", ipsumService.GetIpsum());
    }

    [Fact]
    public void SimpleServiceOptionalAccess_ShouldGetService()
    {
        var loremService = TryGetTestService<TestServices.ILoremService>();
        Assert.NotNull(loremService);
        Assert.Equal("Lorem", loremService.GetLorem());

        var ipsumService = TryGetTestService<TestServices.IIpsumService>();
        Assert.NotNull(ipsumService);
        Assert.Equal("Ipsum", ipsumService.GetIpsum());
    }

    [Fact]
    public void SimpleServiceListAccess_ShouldGetSingleItem()
    {
        var loremServices = GetTestServices<TestServices.ILoremService>().ToList();
        Assert.Single(loremServices);
        Assert.Equal("Lorem", loremServices[0].GetLorem());

        var ipsumServices = GetTestServices<TestServices.IIpsumService>().ToList();
        Assert.Single(ipsumServices);
        Assert.Equal("Ipsum", ipsumServices[0].GetIpsum());
    }

    // -----------------------
    // Strictly keyed services
    // -----------------------

    [Fact]
    public void StrictlyKeyedServicesListAccess_ShouldGetAll()
    {
        // note: this only works because number services were registered as raw singletons on top of keyed singletons

        var numberServices = GetTestServices<TestServices.INumberService>().ToList();
        Assert.Equal(2, numberServices.Count);

        var receivedNumbers = numberServices.Select(service => service.GetNumber()).ToList();
        Assert.Contains(0, receivedNumbers); // zero service
        Assert.Contains(42, receivedNumbers); // answer to everything service
    }

    [Fact]
    public void StrictlyKeyedServicesRequiredKeyedAccess_ShouldGetService()
    {
        var zeroService = GetKeyedTestService<TestServices.INumberService>("ZERO");
        Assert.Equal(0, zeroService.GetNumber());

        var answerToEverythingService = GetKeyedTestService<TestServices.INumberService>("ANSWER");
        Assert.Equal(42, answerToEverythingService.GetNumber());
    }

    [Fact]
    public void StrictlyKeyedServicesOptionalKeyedAccess_ShouldGetService()
    {
        var zeroService = TryGetKeyedTestService<TestServices.INumberService>("ZERO");
        Assert.NotNull(zeroService);
        Assert.Equal(0, zeroService.GetNumber());

        var answerToEverythingService = TryGetKeyedTestService<TestServices.INumberService>("ANSWER");
        Assert.NotNull(answerToEverythingService);
        Assert.Equal(42, answerToEverythingService.GetNumber());
    }

    [Fact]
    public void UnknownKeyRequiredKeyedAccess_ShouldThrow()
    {
        Action unknownKeyServiceAccess = () => GetKeyedTestService<TestServices.INumberService>("UNKNOWN");
        Assert.Throws<InvalidOperationException>(unknownKeyServiceAccess);
    }

    [Fact]
    public void UnknownKeyOptionalKeyedAccess_ShouldThrow()
    {
        var unknownKeyService = TryGetKeyedTestService<TestServices.INumberService>("UNKNOWN");
        Assert.Null(unknownKeyService);
    }

    // -------------------
    // Shared key services
    // -------------------

    [Fact]
    public void SharedKeyServicesListKeyedAccess_ShouldGetMatches()
    {
        var warmColorServices = GetKeyedTestServices<TestServices.IColorService>("WARM").ToList();
        Assert.Equal(2, warmColorServices.Count);

        var warmColors = warmColorServices.Select(service => service.GetColor()).ToList();
        Assert.Contains("Red", warmColors);
        Assert.Contains("Yellow", warmColors);

        var coolColorServices = GetKeyedTestServices<TestServices.IColorService>("COOL").ToList();
        Assert.Single(coolColorServices);

        var coolColors = coolColorServices.Select(service => service.GetColor()).ToList();
        Assert.Contains("Blue", coolColors);
    }

    [Fact]
    public void UnknownKeyListKeyedAccess_ShouldGetEmpty()
    {
        var unknownColorService = GetKeyedTestServices<TestServices.IColorService>("UNKNOWN");
        Assert.Empty(unknownColorService);
    }

    // --------------------
    // Non-existent service
    // --------------------

    [Fact]
    public void NonexistentServiceRequiredAccess_ShouldThrow()
    {
        Action notImplementedServiceAccess = () => GetTestService<TestServices.INonExistentService>();
        Assert.Throws<InvalidOperationException>(notImplementedServiceAccess);
    }

    [Fact]
    public void NonexistentServiceOptionalAccess_ShouldGetNull()
    {
        var notImplementedService = TryGetTestService<TestServices.INonExistentService>();
        Assert.Null(notImplementedService);
    }

    [Fact]
    public void NonexistentServiceListAccess_ShouldGetEmpty()
    {
        var notImplementedService = GetTestServices<TestServices.INonExistentService>();
        Assert.Empty(notImplementedService);
    }

    [Fact]
    public void NonexistentServiceRequiredKeyedAccess_ShouldThrow()
    {
        Action notImplementedServiceAccess = () => GetKeyedTestService<TestServices.INonExistentService>("NONE");
        Assert.Throws<InvalidOperationException>(notImplementedServiceAccess);
    }

    [Fact]
    public void NonexistentServiceOptionalKeyedAccess_ShouldGetNull()
    {
        var notImplementedService = TryGetKeyedTestService<TestServices.INonExistentService>("NONE");
        Assert.Null(notImplementedService);
    }

    [Fact]
    public void NonexistentServiceListKeyedAccess_ShouldGetEmpty()
    {
        var notImplementedService = GetKeyedTestServices<TestServices.INonExistentService>("NONE");
        Assert.Empty(notImplementedService);
    }
}
