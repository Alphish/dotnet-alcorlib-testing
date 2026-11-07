using System.Reflection;
using Xunit.Sdk;
using Xunit.v3;

namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Systems.Fixture;

public class DummyTestClass : IXunitTestClass
{
    public static DummyTestClass Create(Type classType, IScenarioMethodOrderer? orderer = null)
        => new DummyTestClass() { Class = classType, ScenarioOrderer = orderer };

    public Type Class { get; init; } = default!;
    public IScenarioMethodOrderer? ScenarioOrderer { get; init; } = default!;
    public ITestMethodOrderer? TestMethodOrderer => ScenarioOrderer;

    public IReadOnlyCollection<IBeforeAfterTestAttribute> BeforeAfterTestAttributes => throw new NotImplementedException();
    public IReadOnlyCollection<Type> ClassFixtureTypes => throw new NotImplementedException();
    public IReadOnlyCollection<ConstructorInfo>? Constructors => throw new NotImplementedException();
    public IReadOnlyCollection<MethodInfo> Methods => throw new NotImplementedException();
    public IXunitTestCollection TestCollection => throw new NotImplementedException();
    public string TestClassName => throw new NotImplementedException();
    public string? TestClassNamespace => throw new NotImplementedException();
    public string TestClassSimpleName => throw new NotImplementedException();
    public IReadOnlyDictionary<string, IReadOnlyCollection<string>> Traits => throw new NotImplementedException();
    public string UniqueID => throw new NotImplementedException();
    public ITestCaseOrderer? TestCaseOrderer => throw new NotImplementedException();
    ITestCollection ITestClass.TestCollection => TestCollection;
    public int MetadataToken => throw new NotImplementedException();
    public bool DisableParallelization => throw new NotImplementedException();
    ICoreTestCollection ICoreTestClass.TestCollection => TestCollection;
}
