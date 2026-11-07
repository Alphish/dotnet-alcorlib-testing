namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Flow.Cases;

public class SetupBasicFailureCase : ScenarioFlowCase
{
    public SetupBasicFailureCase(ScenarioFixture scenarioFixture) : base(scenarioFixture)
    {
    }

    [ScenarioSetup] public void Setup() => SimulateFailed();
    [ScenarioStep(01)] public void Step1() => SimulatePassed();
    [ScenarioStep(02)] public void Step2() => SimulatePassed();
    [ScenarioStep(03)] public void Step3() => SimulatePassed();
    [ScenarioTeardown] public void Teardown() => SimulatePassed();
}
