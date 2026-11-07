using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Xunit;

namespace Alphicsh.Alcorlib.Testing.Services;

public abstract class ServiceProviderFixture : IAsyncLifetime
{
    private IServiceProvider Provider { get; set; } = default!;

    // -----
    // Setup
    // -----

    public virtual ValueTask InitializeAsync()
    {
        var collection = StubServiceCollection();
        ConfigureService(collection);
        Provider = CreateServiceProvider(collection);
        return ValueTask.CompletedTask;
    }

    protected virtual IServiceCollection StubServiceCollection()
    {
        return new ServiceCollection();
    }

    protected abstract void ConfigureService(IServiceCollection collection);

    protected virtual IServiceProvider CreateServiceProvider(IServiceCollection collection)
    {
        return collection.BuildServiceProvider();
    }

    // ------
    // Access
    // ------

    public virtual IServiceScope CreateTestScope()
    {
        return Provider.CreateScope();
    }

    // --------
    // Teardown
    // --------

    public virtual ValueTask DisposeAsync()
    {
        return Provider is IAsyncDisposable disposableProvider
            ? disposableProvider.DisposeAsync()
            : ValueTask.CompletedTask;
    }
}
