using Xunit.Sdk;
using Xunit.v3;

namespace Alphicsh.Alcorlib.Testing.Integration.Services.Scope;

public class ScopeTestMethodOrderer : ITestMethodOrderer
{
    public IReadOnlyCollection<TTestMethod?> OrderTestMethods<TTestMethod>(IReadOnlyCollection<TTestMethod?> testMethods)
        where TTestMethod : notnull, ITestMethod
    {
        return testMethods
            .OrderBy(testMethod => testMethod?.MethodName ?? "-")
            .ToList();
    }
}
