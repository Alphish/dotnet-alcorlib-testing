using System.Reflection;
using Xunit.Sdk;
using Xunit.v3;

namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Systems.Fixture;

public class DummyTest : IXunitTest
{
    public static DummyTest Create(string displayName)
        => new DummyTest() { TestCase = DummyTestCase.Create(displayName) };

    public static DummyTest FromTestMethod(MethodInfo method, IXunitTestClass testClass)
    {
        return new DummyTest() { TestCase = DummyTestCase.Create(method, testClass) };
    }

    public required IXunitTestCase TestCase { get; init; }
    public string TestDisplayName => TestCase.TestCaseDisplayName;
    public IXunitTestMethod TestMethod => TestCase.TestMethod;
    public IReadOnlyDictionary<string, IReadOnlyCollection<string>> Traits => TestCase.Traits;

    public DummyTest WithTrait(string key, IReadOnlyCollection<string> values)
    {
        (TestCase as DummyTestCase)!.WithTrait(key, values);
        return this;
    }

    // ------
    // Unused
    // ------

    public bool Explicit => throw new NotImplementedException();
    public string? SkipReason => throw new NotImplementedException();
    public Type? SkipType => throw new NotImplementedException();
    public string? SkipUnless => throw new NotImplementedException();
    public string? SkipWhen => throw new NotImplementedException();
    public object?[] TestMethodArguments => throw new NotImplementedException();
    public int Timeout => throw new NotImplementedException();
    public string UniqueID => throw new NotImplementedException();
    ITestCase ITest.TestCase => TestCase;
    public bool DisableParallelization => throw new NotImplementedException();
    ICoreTestCase ICoreTest.TestCase => TestCase;
    public string? TestLabel => throw new NotImplementedException();
}
