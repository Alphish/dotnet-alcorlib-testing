using Alphicsh.Alcorlib.Testing.Thens;
using Xunit.SimpleRunner;

namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Flow;

public class ScenarioSimulator
{
    public Task<ThenIndex<string, TestResult>> RunScenario<T>(string cultureCode = "en-US")
        => RunScenario(typeof(T), cultureCode);

    public async Task<ThenIndex<string, TestResult>> RunScenario(Type scenarioType, string cultureCode = "en-US")
    {
        var result = new List<ScenarioMethodResult>();

        var runnerOptions = new AssemblyRunnerOptions(scenarioType.Assembly.Location) { Culture = cultureCode };
        runnerOptions.Filters.AddIncludedClassFilter(scenarioType.FullName!);
        runnerOptions.OnTestFinished = (testInfo) =>
        {
            switch (testInfo)
            {
                case TestPassedInfo:
                    result.Add(ScenarioMethodResult.FromResult(testInfo, TestResult.Passed));
                    break;

                case TestFailedInfo:
                    result.Add(ScenarioMethodResult.FromResult(testInfo, TestResult.Failed));
                    break;

                case TestSkippedInfo:
                    result.Add(ScenarioMethodResult.FromResult(testInfo, TestResult.Skipped));
                    break;

                default:
                    throw new NotSupportedException($"Could not handle test info of type {testInfo.GetType().Name}");
            }
        };
        var simulatorRunner = new AssemblyRunner(runnerOptions);

        await simulatorRunner.Run();

        var resultsByMethod = result.ToDictionary(entry => entry.MethodName, entry => entry.Result);
        return new ThenDictionary<string, TestResult>("TestResults", resultsByMethod).Indexed();
    }
}
