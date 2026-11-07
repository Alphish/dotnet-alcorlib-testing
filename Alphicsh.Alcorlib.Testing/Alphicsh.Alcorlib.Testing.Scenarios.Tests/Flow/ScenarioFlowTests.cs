using Alphicsh.Alcorlib.Testing.Scenarios.Tests.Flow.Cases;

namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Flow;

public static class TestExecution
{
    public static bool IsTestExecution { get; set; } = false;
}

public class ScenarioFlowTests
{
    // ---------------
    // Basic successes
    // ---------------

    [Fact]
    public async Task AllGoodScenario_ShouldPass()
    {
        var simulator = new ScenarioSimulator();
        var result = await simulator.RunScenario<AllGoodCase>();
        result.ShouldHaveEntry(nameof(AllGoodCase.Setup), TestResult.Passed)
            .ShouldHaveEntry(nameof(AllGoodCase.Step1), TestResult.Passed)
            .ShouldHaveEntry(nameof(AllGoodCase.Step2), TestResult.Passed)
            .ShouldHaveEntry(nameof(AllGoodCase.Step3), TestResult.Passed)
            .ShouldHaveEntry(nameof(AllGoodCase.Teardown), TestResult.Passed)
            .ShouldHaveNoMoreEntries();
    }

    [Fact]
    public async Task NoSetupSuccess_ShouldPass()
    {
        var simulator = new ScenarioSimulator();
        var result = await simulator.RunScenario<NoSetupSuccessCase>();
        result.ShouldHaveEntry(nameof(NoSetupSuccessCase.Step1), TestResult.Passed)
            .ShouldHaveEntry(nameof(NoSetupSuccessCase.Step2), TestResult.Passed)
            .ShouldHaveEntry(nameof(NoSetupSuccessCase.Step3), TestResult.Passed)
            .ShouldHaveNoMoreEntries();
    }

    // ---------------------------
    // Failures at different spots
    // ---------------------------

    [Fact]
    public async Task MiddleStepFailure_ShouldSkipStepsAfterFailureButKeepTeardown()
    {
        var simulator = new ScenarioSimulator();
        var result = await simulator.RunScenario<MiddleStepFailureCase>();
        result.ShouldHaveEntry(nameof(MiddleStepFailureCase.Setup), TestResult.Passed)
            .ShouldHaveEntry(nameof(MiddleStepFailureCase.Step1), TestResult.Passed)
            .ShouldHaveEntry(nameof(MiddleStepFailureCase.Step2), TestResult.Passed)
            .ShouldHaveEntry(nameof(MiddleStepFailureCase.Step3), TestResult.Failed)
            .ShouldHaveEntry(nameof(MiddleStepFailureCase.Step4), TestResult.Skipped)
            .ShouldHaveEntry(nameof(MiddleStepFailureCase.Step5), TestResult.Skipped)
            .ShouldHaveEntry(nameof(MiddleStepFailureCase.Teardown), TestResult.Passed)
            .ShouldHaveNoMoreEntries();
    }

    [Fact]
    public async Task SetupBasicFailure_ShouldSkipStepsButTeardownAfterFailedSetup()
    {
        var simulator = new ScenarioSimulator();
        var result = await simulator.RunScenario<SetupBasicFailureCase>();
        result.ShouldHaveEntry(nameof(SetupBasicFailureCase.Setup), TestResult.Failed)
            .ShouldHaveEntry(nameof(SetupBasicFailureCase.Step1), TestResult.Skipped)
            .ShouldHaveEntry(nameof(SetupBasicFailureCase.Step2), TestResult.Skipped)
            .ShouldHaveEntry(nameof(SetupBasicFailureCase.Step3), TestResult.Skipped)
            .ShouldHaveEntry(nameof(SetupBasicFailureCase.Teardown), TestResult.Passed)
            .ShouldHaveNoMoreEntries();
    }

    [Fact]
    public async Task SetupNamedFailure_ShouldSkipTeardownsReferringToFailedSetups()
    {
        var simulator = new ScenarioSimulator();
        var result = await simulator.RunScenario<SetupNamedFailureCase>();
        result.ShouldHaveEntry(nameof(SetupNamedFailureCase.SetupGood), TestResult.Passed)
            .ShouldHaveEntry(nameof(SetupNamedFailureCase.SetupBad), TestResult.Failed)
            .ShouldHaveEntry(nameof(SetupNamedFailureCase.Step1), TestResult.Skipped)
            .ShouldHaveEntry(nameof(SetupNamedFailureCase.TeardownGood), TestResult.Passed)
            .ShouldHaveEntry(nameof(SetupNamedFailureCase.TeardownBad), TestResult.Skipped)
            .ShouldHaveEntry(nameof(SetupNamedFailureCase.TeardownGoodBad), TestResult.Skipped)
            .ShouldHaveNoMoreEntries();
    }

    [Fact]
    public async Task TeardownFailure_ShouldPerformAllStepsUpToTeardown()
    {
        var simulator = new ScenarioSimulator();
        var result = await simulator.RunScenario<TeardownFailureCase>();
        result.ShouldHaveEntry(nameof(TeardownFailureCase.Setup), TestResult.Passed)
            .ShouldHaveEntry(nameof(TeardownFailureCase.Step1), TestResult.Passed)
            .ShouldHaveEntry(nameof(TeardownFailureCase.Step2), TestResult.Passed)
            .ShouldHaveEntry(nameof(TeardownFailureCase.Step3), TestResult.Passed)
            .ShouldHaveEntry(nameof(TeardownFailureCase.Teardown), TestResult.Failed)
            .ShouldHaveNoMoreEntries();
    }

    // --------------
    // Parallel steps
    // --------------

    [Fact]
    public async Task ParallelStepsSuccess_ShouldPass()
    {
        var simulator = new ScenarioSimulator();
        var result = await simulator.RunScenario<ParallelStepsSuccessCase>();
        result.ShouldHaveEntry(nameof(ParallelStepsSuccessCase.Setup), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsSuccessCase.Step1a), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsSuccessCase.Step1b), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsSuccessCase.Step1c), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsSuccessCase.Step2a), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsSuccessCase.Step2b), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsSuccessCase.Step2c), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsSuccessCase.Step3a), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsSuccessCase.Step3b), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsSuccessCase.Step3c), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsSuccessCase.Teardown), TestResult.Passed)
            .ShouldHaveNoMoreEntries();
    }

    [Fact]
    public async Task ParallelStepsCompleteFailure_ShouldFailAllParallelStepsWithoutSkipping()
    {
        var simulator = new ScenarioSimulator();
        var result = await simulator.RunScenario<ParallelStepsCompleteFailureCase>();
        result.ShouldHaveEntry(nameof(ParallelStepsCompleteFailureCase.Setup), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsCompleteFailureCase.Step1a), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsCompleteFailureCase.Step1b), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsCompleteFailureCase.Step1c), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsCompleteFailureCase.Step2a), TestResult.Failed)
            .ShouldHaveEntry(nameof(ParallelStepsCompleteFailureCase.Step2b), TestResult.Failed)
            .ShouldHaveEntry(nameof(ParallelStepsCompleteFailureCase.Step2c), TestResult.Failed)
            .ShouldHaveEntry(nameof(ParallelStepsCompleteFailureCase.Step3a), TestResult.Skipped)
            .ShouldHaveEntry(nameof(ParallelStepsCompleteFailureCase.Step3b), TestResult.Skipped)
            .ShouldHaveEntry(nameof(ParallelStepsCompleteFailureCase.Step3c), TestResult.Skipped)
            .ShouldHaveEntry(nameof(ParallelStepsCompleteFailureCase.Teardown), TestResult.Passed)
            .ShouldHaveNoMoreEntries();
    }

    [Fact]
    public async Task ParallelStepsPartialFailure_ShouldFailSomeParallelStepsWithoutSkipping()
    {
        var simulator = new ScenarioSimulator();
        var result = await simulator.RunScenario<ParallelStepsPartialFailureCase>();
        result.ShouldHaveEntry(nameof(ParallelStepsPartialFailureCase.Setup), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsPartialFailureCase.Step1a), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsPartialFailureCase.Step1b), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsPartialFailureCase.Step1c), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsPartialFailureCase.Step2a), TestResult.Failed)
            .ShouldHaveEntry(nameof(ParallelStepsPartialFailureCase.Step2b), TestResult.Passed)
            .ShouldHaveEntry(nameof(ParallelStepsPartialFailureCase.Step2c), TestResult.Failed)
            .ShouldHaveEntry(nameof(ParallelStepsPartialFailureCase.Step3a), TestResult.Skipped)
            .ShouldHaveEntry(nameof(ParallelStepsPartialFailureCase.Step3b), TestResult.Skipped)
            .ShouldHaveEntry(nameof(ParallelStepsPartialFailureCase.Step3c), TestResult.Skipped)
            .ShouldHaveEntry(nameof(ParallelStepsPartialFailureCase.Teardown), TestResult.Passed)
            .ShouldHaveNoMoreEntries();
    }
}
