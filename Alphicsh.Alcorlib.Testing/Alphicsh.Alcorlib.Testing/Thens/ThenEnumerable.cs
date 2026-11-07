namespace Alphicsh.Alcorlib.Testing.Thens;

public static class ThenEnumerable
{
    public static ThenEnumerable<TItem> Of<TItem>(string name, IEnumerable<TItem> items)
        => new ThenEnumerable<TItem>(name, items);
}

public class ThenEnumerable<TItem> : ThenResult<IEnumerable<TItem>>
{
    public ThenEnumerable(string name) : base(name) { }
    public ThenEnumerable(string name, IEnumerable<TItem> result) : base(name, result) { }

    public ThenEnumerator<TItem> Enumerated()
    {
        ShouldBeSet();
        return new ThenEnumerator<TItem>(Name, Result);
    }
}
