using Xunit.SimpleRunner;

namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Flow;

public class ScenarioMethodResult
{
    public required string MethodName { get; init; }
    public required TestResult Result { get; init; }

    public static ScenarioMethodResult FromResult(TestFinishedInfo info, TestResult result)
    {
        return new ScenarioMethodResult { MethodName = info.MethodName!, Result = result };
    }

    public override string ToString()
    {
        return $"{MethodName}: {Result}";
    }
}
