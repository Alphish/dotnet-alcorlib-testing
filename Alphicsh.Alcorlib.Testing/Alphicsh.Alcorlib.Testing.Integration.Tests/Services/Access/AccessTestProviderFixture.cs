using Alphicsh.Alcorlib.Testing.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Alphicsh.Alcorlib.Testing.Integration.Services.Access;

public class AccessTestProviderFixture : ServiceProviderFixture
{
    protected override void ConfigureService(IServiceCollection collection)
    {
        collection
            .AddSingleton<TestServices.ILoremService, TestServices.LoremService>()
            .AddSingleton<TestServices.IIpsumService, TestServices.IpsumService>()
            .AddSingleton<TestServices.INumberService, TestServices.AnswerToEverythingService>()
            .AddSingleton<TestServices.INumberService, TestServices.ZeroService>()
            .AddKeyedSingleton<TestServices.INumberService, TestServices.AnswerToEverythingService>("ANSWER")
            .AddKeyedSingleton<TestServices.INumberService, TestServices.ZeroService>("ZERO")
            .AddKeyedSingleton<TestServices.IColorService, TestServices.RedService>("WARM")
            .AddKeyedSingleton<TestServices.IColorService, TestServices.YellowService>("WARM")
            .AddKeyedSingleton<TestServices.IColorService, TestServices.BlueService>("COOL");
    }
}
