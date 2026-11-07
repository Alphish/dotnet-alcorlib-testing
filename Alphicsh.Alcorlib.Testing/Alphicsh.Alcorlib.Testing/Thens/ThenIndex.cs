using Xunit;
using Xunit.Sdk;

namespace Alphicsh.Alcorlib.Testing.Thens;

public class ThenIndex<TKey, TValue>
    where TKey : notnull
{
    public string Name { get; }
    private IReadOnlyDictionary<TKey, TValue> Entries { get; }
    private HashSet<TKey> RemainingKeys { get; }

    public ThenIndex(string name, IEnumerable<KeyValuePair<TKey, TValue>> entries)
    {
        Name = name;
        Entries = entries.ToDictionary(kvp => kvp.Key, kvp => kvp.Value);
        RemainingKeys = Entries.Keys.ToHashSet();
    }

    public ThenIndex<TKey, TValue> ShouldHaveEntry(TKey key, TValue expected)
    {
        if (!Entries.TryGetValue(key, out var actual))
            Assert.Fail($"{Name} was expected to have an entry with key {key}, but no such entry was present.");

        RemainingKeys.Remove(key);
        Assert.True(object.Equals(expected, actual), $"{Name} entry of {key} was expected to be {expected}, but it's {actual} instead.");
        return this;
    }

    public ThenIndex<TKey, TValue> ShouldHaveNoMoreEntries()
    {
        if (RemainingKeys.Count > 0)
        {
            var keysString = string.Join(", ", RemainingKeys);
            throw new XunitException($"{Name} was expected to have no more entries, but there are still {RemainingKeys.Count} unchecked entries.\nUnchecked keys: {keysString}");
        }
        return this;
    }
}
