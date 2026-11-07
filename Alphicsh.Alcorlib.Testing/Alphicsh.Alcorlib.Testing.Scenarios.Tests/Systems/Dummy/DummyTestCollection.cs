using System.Collections;
using System.Reflection;

namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Systems.Fixture;

public class DummyTestCollection(IEnumerable<DummyTest> tests) : IReadOnlyCollection<DummyTest>
{
    private List<DummyTest> Tests { get; } = tests.ToList();
    private Dictionary<string, DummyTest> TestsByName { get; } = tests.ToDictionary(test => test.TestDisplayName);

    public static DummyTestCollection FromTestClass(Type testClassType, IReadOnlyCollection<string>? methodNames = null)
    {
        var orderer = new ScenarioMethodOrderer();
        var testClass = DummyTestClass.Create(testClassType, orderer);
        var methodNamesSet = methodNames?.ToHashSet();
        var tests = testClassType.GetMethods()
            .Where(method => method.GetCustomAttributes().OfType<IScenarioAttribute>().Any())
            .Where(method => methodNamesSet == null || methodNamesSet.Contains(method.Name))
            .Select(method => DummyTest.FromTestMethod(method, testClass))
            .ToList();

        var testMethods = tests.Select(test => test.TestCase.TestMethod).ToList();
        orderer.OrderTestMethods(testMethods);
        return new DummyTestCollection(tests);
    }

    public static DummyTestCollection FromTestClass<T>(IReadOnlyCollection<string>? methodNames = null)
        => FromTestClass(typeof(T), methodNames);

    public int Count => Tests.Count;
    public IEnumerator<DummyTest> GetEnumerator() => Tests.GetEnumerator();
    IEnumerator IEnumerable.GetEnumerator() => Tests.GetEnumerator();

    public DummyTest GetByName(string name) => TestsByName[name];
}
