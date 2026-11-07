using Xunit.Sdk;

namespace Alphicsh.Alcorlib.Testing.Tests.Thens;

public class BaseResultTests
{
    protected void AssertCheckPasses<TCheck>(Func<TCheck> check)
    {
        try
        {
            check();
        }
        catch (XunitException)
        {
            Assert.Fail($"Expected the check to pass but it didn't.");
        }
    }

    protected void AssertCheckFails<TCheck>(Func<TCheck> check)
    {
        Assert.ThrowsAny<XunitException>(() => check());
    }
}
