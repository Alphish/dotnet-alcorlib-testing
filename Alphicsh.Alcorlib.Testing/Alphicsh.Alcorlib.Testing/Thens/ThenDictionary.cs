namespace Alphicsh.Alcorlib.Testing.Thens;

public static class ThenDictionary
{
    public static ThenDictionary<TKey, TValue> Of<TKey, TValue>(string name, IDictionary<TKey, TValue> items)
        where TKey : notnull
    {
        return new ThenDictionary<TKey, TValue>(name, items);
    }
}

public class ThenDictionary<TKey, TValue> : ThenResult<IDictionary<TKey, TValue>>
    where TKey : notnull
{
    public ThenDictionary(string name) : base(name) { }
    public ThenDictionary(string name, IDictionary<TKey, TValue> result) : base(name, result) { }

    public ThenIndex<TKey, TValue> Indexed()
    {
        ShouldBeSet();
        return new ThenIndex<TKey, TValue>(Name, Result);
    }
}
