using Alphicsh.Alcorlib.Testing.Scenarios.Tests.Systems.Fixture;

namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Systems;

public class ScenarioExtensionsTests
{
    // --------------
    // Scenario order
    // --------------

    [Fact]
    public void GetScenarioOrder_ShouldRetrieveValidOrder()
    {
        var test = DummyTest.Create("Valid").WithTrait(nameof(IScenarioAttribute.ScenarioOrder), ["S.1"]);
        var result = test.TestCase.TestMethod.GetScenarioOrder();
        Assert.Equal(new ScenarioOrderSequence("S.1"), result);
    }

    [Fact]
    public void GetScenarioOrder_ShouldRejectMissingOrder()
    {
        var test = DummyTest.Create("NoOrder");
        var exception = Assert.Throws<ArgumentException>(() => test.TestCase.TestMethod.GetScenarioOrder());
        var expected = $"does not have the {nameof(IScenarioAttribute.ScenarioOrder)} trait";
        Assert.Contains(expected, exception.Message);
    }

    [Fact]
    public void GetScenarioOrder_ShouldRejectEmptyOrders()
    {
        var test = DummyTest.Create("EmptyOrders").WithTrait(nameof(IScenarioAttribute.ScenarioOrder), []);
        var exception = Assert.Throws<ArgumentException>(() => test.TestCase.TestMethod.GetScenarioOrder());
        var expected = $"should have a single {nameof(IScenarioAttribute.ScenarioOrder)} trait, but it has 0";
        Assert.Contains(expected, exception.Message);
    }

    [Fact]
    public void GetScenarioOrder_ShouldRejectMultipleOrders()
    {
        var test = DummyTest.Create("ManyOrders").WithTrait(nameof(IScenarioAttribute.ScenarioOrder), ["Lorem", "Ipsum"]);
        var exception = Assert.Throws<ArgumentException>(() => test.TestCase.TestMethod.GetScenarioOrder());
        var expected = $"should have a single {nameof(IScenarioAttribute.ScenarioOrder)} trait, but it has 2";
        Assert.Contains(expected, exception.Message);
    }

    // -------------
    // Scenario role
    // -------------

    [Fact]
    public void GetScenarioRole_ShouldRetrieveValidRole()
    {
        var test = DummyTest.Create("Valid").WithTrait(nameof(IScenarioAttribute.ScenarioRole), ["Example"]);
        var result = test.GetScenarioRole();
        Assert.Equal("Example", result);
    }

    [Fact]
    public void GetScenarioRole_ShouldRejectMissingRole()
    {
        var test = DummyTest.Create("NoRole");
        var exception = Assert.Throws<ArgumentException>(() => test.GetScenarioRole());
        var expected = $"does not have the {nameof(IScenarioAttribute.ScenarioRole)} trait";
        Assert.Contains(expected, exception.Message);
    }

    [Fact]
    public void GetScenarioRole_ShouldRejectEmptyRoles()
    {
        var test = DummyTest.Create("EmptyRoles").WithTrait(nameof(IScenarioAttribute.ScenarioRole), []);
        var exception = Assert.Throws<ArgumentException>(() => test.GetScenarioRole());
        var expected = $"should have a single {nameof(IScenarioAttribute.ScenarioRole)} trait, but it has 0";
        Assert.Contains(expected, exception.Message);
    }

    [Fact]
    public void GetScenarioRole_ShouldRejectMultipleRoles()
    {
        var test = DummyTest.Create("ManyRoles").WithTrait(nameof(IScenarioAttribute.ScenarioRole), ["Lorem", "Ipsum"]);
        var exception = Assert.Throws<ArgumentException>(() => test.GetScenarioRole());
        var expected = $"should have a single {nameof(IScenarioAttribute.ScenarioRole)} trait, but it has 2";
        Assert.Contains(expected, exception.Message);
    }

    [Fact]
    public void HasScenarioRole_ShouldReturnTrueForMatchingRole()
    {
        var test = DummyTest.Create("SetupScenario").WithTrait(nameof(IScenarioAttribute.ScenarioRole), ["Setup"]);
        Assert.True(test.HasScenarioRole("Setup"));
    }

    [Fact]
    public void HasScenarioRole_ShouldReturnFalseForOtherRole()
    {
        var test = DummyTest.Create("SetupScenario").WithTrait(nameof(IScenarioAttribute.ScenarioRole), ["Teardown"]);
        Assert.False(test.HasScenarioRole("Setup"));
    }

    // ----------
    // Setup keys
    // ----------

    [Fact]
    public void GetSetupKeys_ShouldReturnEmptyGivenNoValues()
    {
        var test = DummyTest.Create("BasicSetup");
        Assert.Equal(test.GetSetupKeys(), []);
    }

    [Fact]
    public void GetSetupKeys_ShouldReturnGivenSingleKey()
    {
        var test = DummyTest.Create("LoremSetup").WithTrait(nameof(ScenarioSetupAttribute.SetupKey), ["Lorem"]);
        Assert.Equal(test.GetSetupKeys(), ["Lorem"]);
    }

    [Fact]
    public void GetSetupKeys_ShouldReturnGivenMultipleKeys()
    {
        var test = DummyTest.Create("MultiTeardown").WithTrait(nameof(ScenarioSetupAttribute.SetupKey), ["Lorem", "Ipsum"]);
        Assert.Equal(test.GetSetupKeys(), ["Lorem", "Ipsum"]);
    }
}
