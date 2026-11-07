namespace Alphicsh.Alcorlib.Testing.Thens;

public class ThenException<TException> : ThenResult<TException> where TException : Exception
{
    public ThenException(string name) : base(name) { }
    public ThenException(string name, TException result) : base(name, result) { }

    private ThenString? _message;
    public ThenString Message => _message ??= IsSet ? new ThenString($"{Name} Message", Result.Message) : new ThenString($"{Name} Message");

    public override void Accept(TException value)
    {
        base.Accept(value);
        _message?.Accept(value.Message);
    }
}

public class ThenException : ThenException<Exception>
{
    public ThenException(string name) : base(name) { }
    public ThenException(string name, Exception result) : base(name, result) { }
}
