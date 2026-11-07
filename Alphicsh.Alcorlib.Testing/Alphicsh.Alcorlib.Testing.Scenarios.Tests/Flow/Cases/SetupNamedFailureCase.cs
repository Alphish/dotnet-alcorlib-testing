namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Flow.Cases;

public class SetupNamedFailureCase : ScenarioFlowCase
{
    public SetupNamedFailureCase(ScenarioFixture scenarioFixture) : base(scenarioFixture)
    {
    }

    [ScenarioSetup(SetupKey = "good")] public void SetupGood() => SimulatePassed();
    [ScenarioSetup(SetupKey = "bad")] public void SetupBad() => SimulateFailed();
    [ScenarioStep(01)] public void Step1() => SimulatePassed();
    [ScenarioTeardown(SetupKeys = "good")] public void TeardownGood() => SimulatePassed();
    [ScenarioTeardown(SetupKeys = "bad")] public void TeardownBad() => SimulatePassed();
    [ScenarioTeardown(SetupKeys = "good,bad")] public void TeardownGoodBad() => SimulatePassed();
}
