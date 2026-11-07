using Xunit;
using Xunit.v3;

namespace Alphicsh.Alcorlib.Testing.Scenarios;

public interface IScenarioFixture
{
    void PrepareTest(IXunitTest test);
    void CompleteTest(IXunitTest test, TestResult result);

    object? GetValue(string key);
    T? GetValue<T>(string key);
    void SetValue(string key, object? value);
}
