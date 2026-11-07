namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Examples;

public class BasicExampleScenario : BaseScenario
{
    private static bool TestOngoing { get; set; }

    public BasicExampleScenario(ScenarioFixture scenarioFixture) : base(scenarioFixture) { }

    [ScenarioSetup]
    public void BasicSetup()
    {
        TestOngoing = true;
    }

    [ScenarioStep(01)]
    public void BasicCheck()
    {
        Assert.True(TestOngoing);
    }

    [ScenarioTeardown]
    public void BasicTeardown()
    {
        TestOngoing = false;
    }
}
