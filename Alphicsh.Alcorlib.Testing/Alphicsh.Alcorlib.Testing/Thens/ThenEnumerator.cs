using Xunit;

namespace Alphicsh.Alcorlib.Testing.Thens;

public class ThenEnumerator<TItem>
{
    public string Name { get; }
    private IEnumerator<TItem> Enumerator { get; }
    private bool HasRemainingItems { get; set; }

    public ThenEnumerator(string name, IEnumerable<TItem> items)
    {
        Name = name;
        Enumerator = items.GetEnumerator();
        HasRemainingItems = Enumerator.MoveNext();
    }

    public ThenEnumerator<TItem> ShouldYield(TItem expected)
    {
        if (!HasRemainingItems)
            Assert.Fail($"{Name} was expected to have another item {expected}, but no more items were yielded.");

        Assert.True(object.Equals(expected, Enumerator.Current), $"{Name} was expected to be {expected}, but it's {Enumerator.Current} instead.");
        HasRemainingItems = Enumerator.MoveNext();
        return this;
    }

    public ThenEnumerator<TItem> ShouldEnd()
    {
        if (HasRemainingItems)
            Assert.Fail($"{Name} was expected to have no more items, but {Enumerator.Current} was yielded instead.");

        return this;
    }
}
