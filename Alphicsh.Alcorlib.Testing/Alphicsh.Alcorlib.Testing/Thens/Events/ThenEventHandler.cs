using Xunit;
using Xunit.Sdk;

namespace Alphicsh.Alcorlib.Testing.Thens.Events;

public class ThenEventHandler<TArgs>
{
    public string Name { get; }
    private Queue<ReceivedEvent<TArgs>> ReceivedEvents { get; } = new Queue<ReceivedEvent<TArgs>>();

    public ThenEventHandler(string name)
    {
        Name = name;
    }

    public void Handle(object? sender, TArgs args)
    {
        var data = ReceivedEvent.Of(sender, args);
        ReceivedEvents.Enqueue(data);
    }

    public ThenEventHandler<TArgs> ShouldHandleNoEvents()
    {
        if (ReceivedEvents.TryDequeue(out var data))
            throw new XunitException($"{Name} was expected to receive an event, but got {data.Args} from {data.Sender ?? "null sender"}.");

        return this;
    }

    public ThenEventHandler<TArgs> ShouldHandleNoMoreEvents()
        => ShouldHandleNoEvents();

    public ThenEventHandler<TArgs> ShouldHandleEvent()
    {
        if (!ReceivedEvents.TryDequeue(out var data))
            throw new XunitException($"{Name} was expected to receive an event, but got none.");

        return this;
    }

    public ThenEventHandler<TArgs> ShouldHandleEvent(object? sender, TArgs args)
    {
        if (!ReceivedEvents.TryDequeue(out var data))
            throw new XunitException($"{Name} was expected to receive {args} from {sender ?? "null sender"}, but got no event.");
        
        Assert.True(object.Equals(args, data.Args), $"{Name} was expected to receive {args}, but got {data.Args} instead.");
        Assert.True(object.Equals(sender, data.Sender), $"{Name} was expected to receive event from {sender ?? "null sender"}, but got it from {data.Sender ?? "null sender"} instead.");
        return this;
    }
    
    public ThenEventHandler<TArgs> ShouldHandleArgs(TArgs args)
    {
        if (!ReceivedEvents.TryDequeue(out var data))
            throw new XunitException($"{Name} was expected to receive {args}, but got no event.");
        
        Assert.True(object.Equals(args, data.Args), $"{Name} was expected to receive {args}, but got {data.Args} instead.");
        return this;
    }
}
