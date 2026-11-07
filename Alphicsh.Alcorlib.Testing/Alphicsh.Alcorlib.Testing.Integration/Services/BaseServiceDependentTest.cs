using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;

namespace Alphicsh.Alcorlib.Testing.Services;

public abstract class BaseServiceDependentTest<TProviderFixture> : IDisposable
    where TProviderFixture : ServiceProviderFixture
{
    protected TProviderFixture ProviderFixture { get; }
    protected IServiceScope ProviderScope { get; }
    protected IServiceProvider Provider => ProviderScope.ServiceProvider;

    public BaseServiceDependentTest(TProviderFixture fixture)
    {
        ProviderFixture = fixture;
        ProviderScope = ProviderFixture.CreateTestScope();
    }

    protected IEnumerable<TService> GetTestServices<TService>()
        => Provider.GetServices<TService>();

    protected TService GetTestService<TService>() where TService : notnull
        => Provider.GetRequiredService<TService>();

    protected TService? TryGetTestService<TService>()
        => Provider.GetService<TService>();

    protected IEnumerable<TService> GetKeyedTestServices<TService>(object? key)
        => Provider.GetKeyedServices<TService>(key);

    protected TService GetKeyedTestService<TService>(object? key) where TService : notnull
        => Provider.GetRequiredKeyedService<TService>(key);

    protected TService? TryGetKeyedTestService<TService>(object? key)
        => Provider.GetKeyedService<TService>(key);

    public virtual void Dispose()
    {
        ProviderScope.Dispose();
    }
}
