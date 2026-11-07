namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Flow.Cases;

public class ParallelStepsCompleteFailureCase : ScenarioFlowCase
{
    public ParallelStepsCompleteFailureCase(ScenarioFixture scenarioFixture) : base(scenarioFixture)
    {
    }

    [ScenarioSetup] public void Setup() => SimulatePassed();

    [ScenarioStep(01)] public void Step1a() => SimulatePassed();
    [ScenarioStep(01)] public void Step1b() => SimulatePassed();
    [ScenarioStep(01)] public void Step1c() => SimulatePassed();

    [ScenarioStep(02)] public void Step2a() => SimulateFailed();
    [ScenarioStep(02)] public void Step2b() => SimulateFailed();
    [ScenarioStep(02)] public void Step2c() => SimulateFailed();

    [ScenarioStep(03)] public void Step3a() => SimulatePassed();
    [ScenarioStep(03)] public void Step3b() => SimulatePassed();
    [ScenarioStep(03)] public void Step3c() => SimulatePassed();

    [ScenarioTeardown] public void Teardown() => SimulatePassed();
}
