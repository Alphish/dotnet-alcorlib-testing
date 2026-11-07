using Alphicsh.Alcorlib.Testing.Thens;

namespace Alphicsh.Alcorlib.Testing.Tests.Thens;

public class ThenExceptionTests
{
    // -------
    // Message
    // -------

    [Fact]
    public void ShouldExposeMessageWithoutSetting()
    {
        var thenException = new ThenException("ThenException");
        thenException.Message.ShouldBeUnset();
    }

    [Fact]
    public void ShouldExposeMessageThenAcceptIt()
    {
        var thenException = new ThenException("ThenException");
        thenException.Accept(new InvalidOperationException("This is wrong."));
        thenException.Message.ShouldContain("This is wrong");
    }

    [Fact]
    public void ShouldAcceptExceptionThenExposeMessage()
    {
        var thenException = new ThenException("ThenException");
        thenException.Message.ShouldBeUnset();
        thenException.Accept(new InvalidOperationException("This is wrong."));
        thenException.Message.ShouldContain("This is wrong");
    }

    [Fact]
    public void ShouldBeCreatedWithExceptionThenExposeMessage()
    {
        var thenException = new ThenException("ThenException", new InvalidOperationException("This is wrong."));
        thenException.Message.ShouldContain("This is wrong");
    }
}
