namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Flow.Cases;

public class AllGoodCase : ScenarioFlowCase
{
    public AllGoodCase(ScenarioFixture scenarioFixture) : base(scenarioFixture)
    {
    }

    [ScenarioSetup] public void Setup() => SimulatePassed();
    [ScenarioStep(01)] public void Step1() => SimulatePassed();
    [ScenarioStep(02)] public void Step2() => SimulatePassed();
    [ScenarioStep(03)] public void Step3() => SimulatePassed();
    [ScenarioTeardown] public void Teardown() => SimulatePassed();
}
