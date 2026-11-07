using Alphicsh.Alcorlib.Testing.Thens.Events;

namespace Alphicsh.Alcorlib.Testing.Tests.Thens.Events;

public class ThenEventHandlerTests : BaseResultTests
{
    private event EventHandler<string>? TestEvent; 
    
    [Fact]
    public void ShouldBeCreatedWithGivenName()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        Assert.Equal("EventHandler", handler.Name);
    }
    
    // -------------
    // Passing cases
    // -------------

    [Fact]
    public void ShouldBeCreatedWithNoHandledEvents()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        AssertCheckPasses(() => handler.ShouldHandleNoEvents());
    }

    [Fact]
    public void ShouldHandleSingleEventAndAssertExistence()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        TestEvent += handler.Handle;
        TestEvent.Invoke("sender", "OK");

        AssertCheckPasses(() => handler.ShouldHandleEvent().ShouldHandleNoMoreEvents());
    }
    
    [Fact]
    public void ShouldHandleSingleEventAndAssertSenderWithArgs()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        TestEvent += handler.Handle;
        TestEvent.Invoke("sender", "OK");

        AssertCheckPasses(() => handler.ShouldHandleEvent("sender", "OK").ShouldHandleNoMoreEvents());
    }
    
    [Fact]
    public void ShouldHandleSingleEventAndAssertArgs()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        TestEvent += handler.Handle;
        TestEvent.Invoke("sender", "OK");

        AssertCheckPasses(() => handler.ShouldHandleArgs("OK").ShouldHandleNoMoreEvents());
    }
    
    [Fact]
    public void ShouldHandleMultipleEvents()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        TestEvent += handler.Handle;
        TestEvent.Invoke("sender", "one");
        TestEvent.Invoke("sender", "two");
        TestEvent.Invoke("sender", "three");

        AssertCheckPasses(() => handler
            .ShouldHandleEvent()
            .ShouldHandleEvent("sender", "two")
            .ShouldHandleArgs("three")
            .ShouldHandleNoMoreEvents()
        );
    }
    
    // --------
    // Failures
    // --------

    [Fact]
    public void ShouldFailDueToUnexpectedEvent()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        TestEvent += handler.Handle;
        TestEvent.Invoke("sender", "OK");
        
        AssertCheckFails(() => handler.ShouldHandleNoEvents());
    }
    
    [Fact]
    public void ShouldFailDueToUnexpectedAdditionalEvent()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        TestEvent += handler.Handle;
        TestEvent.Invoke("sender", "one");
        TestEvent.Invoke("sender", "two");

        handler.ShouldHandleEvent();
        AssertCheckFails(() => handler.ShouldHandleNoMoreEvents());
    }

    [Fact]
    public void ShouldFailExistenceCheckDueToMissingEvent()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        TestEvent += handler.Handle;
        
        AssertCheckFails(() => handler.ShouldHandleEvent());
    }
    
    [Fact]
    public void ShouldFailEventCheckDueToMissingEvent()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        TestEvent += handler.Handle;
        
        AssertCheckFails(() => handler.ShouldHandleEvent("sender", "OK"));
    }
    
    [Fact]
    public void ShouldFailEventCheckDueToInvalidSender()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        TestEvent += handler.Handle;
        TestEvent?.Invoke("senderer", "OK");
        
        AssertCheckFails(() => handler.ShouldHandleEvent("sender", "OK"));
    }
    
    [Fact]
    public void ShouldFailEventCheckDueToInvalidArgs()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        TestEvent += handler.Handle;
        TestEvent?.Invoke("sender", "NOTOK");
        
        AssertCheckFails(() => handler.ShouldHandleEvent("sender", "OK"));
    }
    
    [Fact]
    public void ShouldFailArgsCheckDueToMissingEvent()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        TestEvent += handler.Handle;
        
        AssertCheckFails(() => handler.ShouldHandleArgs("OK"));
    }

    [Fact]
    public void ShouldFailArgsCheckDueToInvalidArgs()
    {
        var handler = new ThenEventHandler<string>("EventHandler");
        TestEvent += handler.Handle;
        TestEvent?.Invoke("sender", "NOTOK");
        
        AssertCheckFails(() => handler.ShouldHandleArgs("OK"));
    }
}
