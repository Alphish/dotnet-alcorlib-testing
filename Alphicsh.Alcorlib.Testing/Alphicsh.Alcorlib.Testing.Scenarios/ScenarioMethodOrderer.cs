using Xunit.Sdk;

namespace Alphicsh.Alcorlib.Testing.Scenarios;

public class ScenarioMethodOrderer : IScenarioMethodOrderer
{
    public IReadOnlySet<string> FoundMethods { get; set; } = default!;
    public IReadOnlyCollection<IReadOnlyCollection<string>> TestOrderGroups { get; set; } = default!;

    public IReadOnlyCollection<TTestMethod?> OrderTestMethods<TTestMethod>(IReadOnlyCollection<TTestMethod?> testMethods) where TTestMethod : notnull, ITestMethod
    {
        var scenarioMethods = testMethods
            .Select(testMethod => testMethod != null ? testMethod! : throw new ArgumentNullException())
            .ToList();

        FoundMethods = scenarioMethods
            .Select(testMethod => testMethod.MethodName)
            .ToHashSet();

        var methodsGrouping = scenarioMethods
            .GroupBy(testMethod => testMethod.GetScenarioOrder())
            .OrderBy(group => group.Key)
            .ToList();

        TestOrderGroups = methodsGrouping
            .Select(group => group.Select(testMethod => testMethod.UniqueID).ToList())
            .ToList();

        return methodsGrouping.SelectMany(group => group).ToList();
    }
}
