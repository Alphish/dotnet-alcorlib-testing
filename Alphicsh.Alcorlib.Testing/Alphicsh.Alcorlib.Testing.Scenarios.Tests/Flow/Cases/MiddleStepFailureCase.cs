namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Flow.Cases;

public class MiddleStepFailureCase : ScenarioFlowCase
{
    public MiddleStepFailureCase(ScenarioFixture scenarioFixture) : base(scenarioFixture)
    {
    }

    [ScenarioSetup] public void Setup() => SimulatePassed();
    [ScenarioStep(01)] public void Step1() => SimulatePassed();
    [ScenarioStep(02)] public void Step2() => SimulatePassed();
    [ScenarioStep(03)] public void Step3() => SimulateFailed();
    [ScenarioStep(04)] public void Step4() => SimulatePassed();
    [ScenarioStep(05)] public void Step5() => SimulatePassed();
    [ScenarioTeardown] public void Teardown() => SimulatePassed();
}
