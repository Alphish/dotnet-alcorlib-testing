using System.Reflection;
using Xunit.Sdk;
using Xunit.v3;

namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Systems.Fixture;

public class DummyTestCase : IXunitTestCase
{
    public static DummyTestCase Create(string displayName)
    {
        var method = DummyTestMethod.Create(displayName);
        return new DummyTestCase { TestCaseDisplayName = displayName, TestMethod = method };
    }

    public static DummyTestCase Create(MethodInfo method, IXunitTestClass testClass)
    {
        var testCase = new DummyTestCase
        {
            TestMethod = DummyTestMethod.Create(method, testClass),
            TestCaseDisplayName = method.Name
        };

        var scenarioAttribute = method.GetCustomAttributes().OfType<IScenarioAttribute>().Single();
        foreach (var traitGroup in scenarioAttribute.GetTraits().GroupBy(trait => trait.Key))
        {
            // scenario traits are expected to have only a single entry
            (testCase.TestMethod as DummyTestMethod)!.WithTrait(traitGroup.Key, traitGroup.Select(trait => trait.Value).ToList());
        }
        return testCase;
    }

    public required string TestCaseDisplayName { get; init; }
    public IXunitTestMethod TestMethod { get; init; } = default!;
    public IXunitTestClass TestClass => TestMethod.TestClass;
    public string TestMethodName => TestMethod.MethodName;
    public string UniqueID { get; } = Guid.NewGuid().ToString();

    // Traits

    private Dictionary<string, IReadOnlyCollection<string>> InnerTraits { get; }
        = new Dictionary<string, IReadOnlyCollection<string>>();

    public IReadOnlyDictionary<string, IReadOnlyCollection<string>> Traits => TestMethod.Traits;

    public DummyTestCase WithTrait(string key, IReadOnlyCollection<string> values)
    {
        (TestMethod as DummyTestMethod)!.WithTrait(key, values);
        return this;
    }

    // Rest

    public Type[]? SkipExceptions => throw new NotImplementedException();
    public string? SkipReason => throw new NotImplementedException();
    public Type? SkipType => throw new NotImplementedException();
    public string? SkipUnless => throw new NotImplementedException();
    public string? SkipWhen => throw new NotImplementedException();
    public int TestClassMetadataToken => throw new NotImplementedException();
    public string TestClassName => throw new NotImplementedException();
    public string TestClassSimpleName => throw new NotImplementedException();
    public IXunitTestCollection TestCollection => throw new NotImplementedException();
    public int TestMethodMetadataToken => throw new NotImplementedException();
    public string[] TestMethodParameterTypesVSTest => throw new NotImplementedException();
    public string TestMethodReturnTypeVSTest => throw new NotImplementedException();
    public int Timeout => throw new NotImplementedException();
    public bool Explicit => throw new NotImplementedException();
    public string? SourceFilePath => throw new NotImplementedException();
    public int? SourceLineNumber => throw new NotImplementedException();
    public string? TestClassNamespace => throw new NotImplementedException();
    public int? TestMethodArity => throw new NotImplementedException();
    ITestClass? ITestCase.TestClass => TestClass;
    ITestCollection ITestCase.TestCollection => TestCollection;
    ITestMethod? ITestCase.TestMethod => TestMethod;
    int? ITestCaseMetadata.TestClassMetadataToken => TestClassMetadataToken;
    int? ITestCaseMetadata.TestMethodMetadataToken => TestMethodMetadataToken;
    public bool DisableParallelization => throw new NotImplementedException();
    int ICoreTestCase.TestMethodArity => throw new NotImplementedException();
    ICoreTestClass ICoreTestCase.TestClass => TestClass;
    ICoreTestCollection ICoreTestCase.TestCollection => TestCollection;
    ICoreTestMethod ICoreTestCase.TestMethod => TestMethod;

    public ValueTask<IReadOnlyCollection<IXunitTest>> CreateTests()
    {
        return ValueTask.FromResult<IReadOnlyCollection<IXunitTest>>([
            new DummyTest() { TestCase = this }
            ]);
    }

    public void PostInvoke()
    {
        throw new NotImplementedException();
    }

    public void PreInvoke()
    {
        throw new NotImplementedException();
    }
}
