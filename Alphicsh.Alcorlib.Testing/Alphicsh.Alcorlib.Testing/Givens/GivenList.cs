namespace Alphicsh.Alcorlib.Testing.Givens;

public class GivenList<TItem> : GivenValue<List<TItem>>
{
    protected bool IsConfirmedEmpty { get; set; }

    public GivenList(string name) : base(name)
    {
        Value = new List<TItem>();
    }

    public virtual GivenList<TItem> With(TItem item)
    {
        AddItem(item);
        return this;
    }

    public virtual void Add(TItem item)
        => AddItem(item);

    protected void AddItem(TItem item)
    {
        if (IsConfirmedEmpty)
            throw new InvalidOperationException($"The given list of '{Name}' cannot have items.");

        Value.Add(item);

        // prevent replacing with another list after adding any item
        IsSet = true;
    }

    public virtual GivenList<TItem> WithNoItems()
    {
        if (Value.Count > 0)
            throw new InvalidOperationException($"Cannot mark given list of '{Name}' as having no items, because it already has items.");

        IsConfirmedEmpty = true;
        IsSet = true; // prevent replacing with another list after confirming empty
        return this;
    }
}
