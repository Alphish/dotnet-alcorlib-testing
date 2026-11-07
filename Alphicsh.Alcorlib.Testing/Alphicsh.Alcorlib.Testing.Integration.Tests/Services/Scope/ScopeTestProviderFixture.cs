using Alphicsh.Alcorlib.Testing.Services;
using Microsoft.Extensions.DependencyInjection;

namespace Alphicsh.Alcorlib.Testing.Integration.Services.Scope;

public class ScopeTestProviderFixture : ServiceProviderFixture
{
    protected override void ConfigureService(IServiceCollection collection)
    {
        collection
            .AddSingleton<ScopeTestServices.ISingletonManager, ScopeTestServices.SingletonManager>()
            .AddScoped<ScopeTestServices.IScopedManager, ScopeTestServices.ScopedManager>()
            .AddTransient<ScopeTestServices.ITransientManager, ScopeTestServices.TransientManager>();
    }
}
