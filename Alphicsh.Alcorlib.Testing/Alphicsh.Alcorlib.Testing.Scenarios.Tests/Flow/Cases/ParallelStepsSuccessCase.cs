namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Flow.Cases;

public class ParallelStepsSuccessCase : ScenarioFlowCase
{
    public ParallelStepsSuccessCase(ScenarioFixture scenarioFixture) : base(scenarioFixture)
    {
    }

    [ScenarioSetup] public void Setup() => SimulatePassed();

    [ScenarioStep(01)] public void Step1a() => SimulatePassed();
    [ScenarioStep(01)] public void Step1b() => SimulatePassed();
    [ScenarioStep(01)] public void Step1c() => SimulatePassed();

    [ScenarioStep(02)] public void Step2a() => SimulatePassed();
    [ScenarioStep(02)] public void Step2b() => SimulatePassed();
    [ScenarioStep(02)] public void Step2c() => SimulatePassed();

    [ScenarioStep(03)] public void Step3a() => SimulatePassed();
    [ScenarioStep(03)] public void Step3b() => SimulatePassed();
    [ScenarioStep(03)] public void Step3c() => SimulatePassed();

    [ScenarioTeardown] public void Teardown() => SimulatePassed();
}
