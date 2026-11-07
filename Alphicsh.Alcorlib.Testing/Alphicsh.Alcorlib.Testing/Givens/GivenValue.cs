namespace Alphicsh.Alcorlib.Testing.Givens;

public class GivenValue<TValue>
{
    public string Name { get; }
    public TValue Value { get; protected set; } = default!;
    public bool IsSet { get; protected set; } = false;

    public GivenValue(string name)
    {
        Name = name;
    }

    public static implicit operator TValue(GivenValue<TValue> given) => given.Value;

    public virtual void Of(TValue value)
    {
        SetValue(value);
    }

    protected void SetValue(TValue value)
    {
        if (IsSet)
            throw new InvalidOperationException($"The given value of '{Name}' has been already set.");

        Value = value;
        IsSet = true;
    }

    public override string ToString()
    {
        var valueString = IsSet ? (Value?.ToString() ?? "null") : "<unset>";
        return $"{Name} = {valueString}";
    }
}
