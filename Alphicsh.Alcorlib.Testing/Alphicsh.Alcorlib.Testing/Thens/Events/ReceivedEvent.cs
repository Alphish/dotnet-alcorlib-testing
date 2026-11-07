namespace Alphicsh.Alcorlib.Testing.Thens.Events;

public class ReceivedEvent<TArgs>(object? sender, TArgs args)
{
    public object? Sender { get; } = sender;
    public TArgs Args { get; } = args;
}

public static class ReceivedEvent
{
    public static ReceivedEvent<TArgs> Of<TArgs>(object? sender, TArgs args)
        =>  new ReceivedEvent<TArgs>(sender, args);
}
