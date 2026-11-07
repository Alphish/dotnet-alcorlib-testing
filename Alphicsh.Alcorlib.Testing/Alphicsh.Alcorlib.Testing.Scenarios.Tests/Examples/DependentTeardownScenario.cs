namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Examples;

public class DependentTeardownScenario : BaseScenario
{
    public DependentTeardownScenario(ScenarioFixture scenarioFixture) : base(scenarioFixture) { }

    [ScenarioSetup(SetupKey = "A")]
    public void SetupA() { }

    [ScenarioSetup(SetupKey = "B")]
    public void SetupB() { }

    [ScenarioStep(01)]
    public void BasicCheck()
    {
    }

    [ScenarioTeardown]
    public void IndependentTeardown() { }

    [ScenarioTeardown(SetupKeys = "A")]
    public void ADependentTeardown() { }

    [ScenarioTeardown(SetupKeys = "A,B")]
    public void ABDependentTeardown() { }
}
