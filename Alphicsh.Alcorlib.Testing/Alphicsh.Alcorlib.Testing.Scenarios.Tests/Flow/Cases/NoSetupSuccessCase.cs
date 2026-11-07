namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Flow.Cases;

public class NoSetupSuccessCase : ScenarioFlowCase
{
    public NoSetupSuccessCase(ScenarioFixture scenarioFixture) : base(scenarioFixture)
    {
    }

    [ScenarioStep(01)] public void Step1() => SimulatePassed();
    [ScenarioStep(02)] public void Step2() => SimulatePassed();
    [ScenarioStep(03)] public void Step3() => SimulatePassed();
}
