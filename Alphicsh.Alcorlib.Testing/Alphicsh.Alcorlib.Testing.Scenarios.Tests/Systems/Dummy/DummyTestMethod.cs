using System.Reflection;
using Xunit.Sdk;
using Xunit.v3;

namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Systems.Fixture;

public class DummyTestMethod : IXunitTestMethod
{
    public static DummyTestMethod Create(string name)
    {
        return new DummyTestMethod()
        {
            Method = null!,
            MethodName = name,
            TestClass = null!,
            BeforeAfterTestAttributes = null!,
        };
    }

    public static DummyTestMethod Create(MethodInfo method, IXunitTestClass testClass)
    {
        return new DummyTestMethod()
        {
            Method = method,
            MethodName = method.Name,
            TestClass = testClass,
            BeforeAfterTestAttributes = method.GetCustomAttributes().OfType<IBeforeAfterTestAttribute>().ToList(),
        };
    }

    public MethodInfo Method { get; init; } = default!;
    public string MethodName { get; init; } = default!;
    public IXunitTestClass TestClass { get; init; } = default!;
    public IReadOnlyCollection<IBeforeAfterTestAttribute> BeforeAfterTestAttributes { get; init; } = default!;

    // Traits

    public IReadOnlyDictionary<string, IReadOnlyCollection<string>> Traits => InnerTraits;
    private Dictionary<string, IReadOnlyCollection<string>> InnerTraits { get; }
        = new Dictionary<string, IReadOnlyCollection<string>>();

    public DummyTestMethod WithTrait(string key, IReadOnlyCollection<string> values)
    {
        InnerTraits.Add(key, values);
        return this;
    }

    // ------
    // Unused
    // ------

    public IReadOnlyCollection<IDataAttribute> DataAttributes => throw new NotImplementedException();
    public IReadOnlyCollection<IFactAttribute> FactAttributes => throw new NotImplementedException();
    public bool IsGenericMethodDefinition => throw new NotImplementedException();
    public IReadOnlyCollection<ParameterInfo> Parameters => throw new NotImplementedException();
    public Type ReturnType => throw new NotImplementedException();
    public object?[] TestMethodArguments => throw new NotImplementedException();
    public int? MethodArity => throw new NotImplementedException();
    public string UniqueID { get; } = Guid.NewGuid().ToString();
    ITestClass ITestMethod.TestClass => TestClass;

    public int MetadataToken => throw new NotImplementedException();
    public bool DisableParallelization => throw new NotImplementedException();
    int ICoreTestMethod.MethodArity => throw new NotImplementedException();
    public ITestCaseOrderer? TestCaseOrderer => throw new NotImplementedException();
    ICoreTestClass ICoreTestMethod.TestClass => TestClass;

    public string GetDisplayName(string baseDisplayName, string? label, object?[]? testMethodArguments, Type[]? methodGenericTypes)
    {
        throw new NotImplementedException();
    }

    public MethodInfo MakeGenericMethod(Type[] genericTypes)
    {
        throw new NotImplementedException();
    }

    public Type[]? ResolveGenericTypes(object?[] arguments)
    {
        throw new NotImplementedException();
    }

    public object?[] ResolveMethodArguments(object?[] arguments)
    {
        throw new NotImplementedException();
    }
}
