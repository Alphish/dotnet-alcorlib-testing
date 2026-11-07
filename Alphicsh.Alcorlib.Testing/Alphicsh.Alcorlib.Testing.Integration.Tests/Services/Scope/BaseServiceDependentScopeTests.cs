using Alphicsh.Alcorlib.Testing.Scenarios;
using Alphicsh.Alcorlib.Testing.Services;

namespace Alphicsh.Alcorlib.Testing.Integration.Services.Scope;

[TestMethodOrderer(typeof(ScenarioMethodOrderer))]
[TestClass(DisableParallelization = true)]
public class BaseServiceDependentScopeTests
    : BaseServiceDependentTest<ScopeTestProviderFixture>, IClassFixture<ScopeTestProviderFixture>, IClassFixture<ScenarioFixture>, ITestScenario
{
    public IScenarioFixture ScenarioFixture { get; }

    public BaseServiceDependentScopeTests(ScopeTestProviderFixture providerFixture, ScenarioFixture scenarioFixture)
        : base(providerFixture)
    {
        ScenarioFixture = scenarioFixture;
    }

    // This test suite checks the proper behaviour of singleton/scoped/transient services:
    // - a singleton service is reused across all tests
    // - each test gets a new scoped provider, so a scoped service is reused only within the same test
    // - a transient service will make a new instance for each GetTestService call
    // 
    // For brevity, each test uses CheckTestService helper method, which:
    // - acquires a service instance (new or reused) with GetTestService
    // - uses GetOrInit to enter a value into a new service instance, or retrieves a prior value from a reused instance
    // - asserts that the received GetOrInit result is correct
    //
    // The received value being the same as entered indicates a new instance; otherwise it's a reused instance.
    // There are three tests (initial, follow up, end), each with two sets of test service checks.
    // The expected behaviour is:
    // - the singleton will accept and return the entered value the first time,
    //   then it will reuse the initial value for all the remaining checks across all tests
    // - the scoped service will accept and return the entered value for each first check in a test,
    //   but it will reuse this value in the second check
    //   (indicating the scope changing between tests, but not within an individual test)
    // - the transient service will keep accepting and returning the entered value for all checks,
    //   because a transient service provides a new instance even within the same scope

    [ScenarioStep(01)]
    public void Step01_ShouldDeliverInitialScopedServices()
    {
        // the initial singleton uses the INIT1 entered value
        CheckTestService<ScopeTestServices.ISingletonManager>(entered: "INIT1", received: "INIT1");
        // the initial scoped service uses the INIT1 entered value
        CheckTestService<ScopeTestServices.IScopedManager>(entered: "INIT1", received: "INIT1");
        // the initial transient service uses the INIT1 entered value
        CheckTestService<ScopeTestServices.ITransientManager>(entered: "INIT1", received: "INIT1");

        // the reused singleton from the ongoing scope receives the prior INIT1 value
        CheckTestService<ScopeTestServices.ISingletonManager>(entered: "INIT2", received: "INIT1");
        // the reused scoped service from the ongoing scope receives the prior INIT1 value
        CheckTestService<ScopeTestServices.IScopedManager>(entered: "INIT2", received: "INIT1");
        // the transient service is not reused and uses the INIT2 entered value
        CheckTestService<ScopeTestServices.ITransientManager>(entered: "INIT2", received: "INIT2");
    }

    [ScenarioStep(02)]
    public void Step02_ShouldDeliverFollowUpScopedServices()
    {
        // the reused singleton from the first test receives the prior INIT1 value
        CheckTestService<ScopeTestServices.ISingletonManager>(entered: "FOLLOW1", received: "INIT1");
        // the new test scope demands a new scoped service, which uses the FOLLOW1 entered value
        CheckTestService<ScopeTestServices.IScopedManager>(entered: "FOLLOW1", received: "FOLLOW1");
        // the transient service is not reused and uses the FOLLOW1 entered value
        CheckTestService<ScopeTestServices.ITransientManager>(entered: "FOLLOW1", received: "FOLLOW1");

        // the reused singleton from the first test receives the prior INIT1 value
        CheckTestService<ScopeTestServices.ISingletonManager>(entered: "FOLLOW2", received: "INIT1");
        // the reused scoped service from the ongoing scope receives the prior FOLLOW1 value
        CheckTestService<ScopeTestServices.IScopedManager>(entered: "FOLLOW2", received: "FOLLOW1");
        // the transient service is not reused and uses the FOLLOW2 entered value
        CheckTestService<ScopeTestServices.ITransientManager>(entered: "FOLLOW2", received: "FOLLOW2");
    }

    [ScenarioStep(03)]
    public void Step03_ShouldDeliverFinalScopedServices()
    {
        // the reused singleton from the first test receives the prior INIT1 value
        CheckTestService<ScopeTestServices.ISingletonManager>(entered: "FINAL1", received: "INIT1");
        // the new test scope demands a new scoped service, which uses the FINAL1 entered value
        CheckTestService<ScopeTestServices.IScopedManager>(entered: "FINAL1", received: "FINAL1");
        // the transient service is not reused and uses the FINAL1 entered value
        CheckTestService<ScopeTestServices.ITransientManager>(entered: "FINAL1", received: "FINAL1");

        // the reused singleton from the first test receives the prior INIT1 value
        CheckTestService<ScopeTestServices.ISingletonManager>(entered: "FINAL2", received: "INIT1");
        // the reused scoped instance from the ongoing scope receives the prior FINAL1 value
        CheckTestService<ScopeTestServices.IScopedManager>(entered: "FINAL2", received: "FINAL1");
        // the transient service is not reused and uses the FINAL2 entered value
        CheckTestService<ScopeTestServices.ITransientManager>(entered: "FINAL2", received: "FINAL2");
    }

    // ------
    // Helper
    // ------

    private void CheckTestService<TService>(string entered, string received)
        where TService : notnull, ScopeTestServices.IValueManager
    {
        var service = GetTestService<TService>();
        var result = service.GetOrInit(entered);
        Assert.Equal(received, result);
    }
}
