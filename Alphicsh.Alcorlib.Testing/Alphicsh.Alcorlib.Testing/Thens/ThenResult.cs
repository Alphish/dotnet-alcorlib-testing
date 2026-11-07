using Xunit;
using Xunit.Sdk;

namespace Alphicsh.Alcorlib.Testing.Thens;

public static class ThenResult
{
    public static ThenResult<TResult> Of<TResult>(string name, TResult result)
        => new ThenResult<TResult>(name, result);
}

public class ThenResult<TResult>
{
    public string Name { get; }
    public TResult Result { get; protected set; } = default!;
    public bool IsSet { get; protected set; }

    public ThenResult(string name)
    {
        Name = name;
        IsSet = false;
    }

    public ThenResult(string name, TResult result)
    {
        Name = name;
        Result = result;
        IsSet = true;
    }

    public virtual void Accept(TResult value)
    {
        SetValue(value);
    }

    protected void SetValue(TResult value)
    {
        if (IsSet)
            throw new InvalidOperationException($"The result value of '{Name}' has been already set.");

        Result = value;
        IsSet = true;
    }

    public override string ToString()
    {
        var valueString = IsSet ? (Result?.ToString() ?? "null") : "<unset>";
        return $"{Name} = {valueString}";
    }

    // ----------------
    // Basic assertions
    // ----------------

    public ThenResult<TResult> ShouldBeUnset()
    {
        Assert.False(IsSet, $"{Name} was expected to have its value unset, but the value is set.");
        return this;
    }

    public ThenResult<TResult> ShouldBeSet()
    {
        Assert.True(IsSet, $"{Name} was expected to have its value set, but it remains unset.");
        return this;
    }

    public ThenResult<TResult> ShouldBeNull()
    {
        ShouldBeSet();
        Assert.True(Result == null, $"{Name} was expected to be null, but it's not.");
        return this;
    }

    public ThenResult<TResult> ShouldNotBeNull()
    {
        ShouldBeSet();
        Assert.False(Result == null, $"{Name} was expected to be other than null, but it's null.");
        return this;
    }

    public ThenResult<TResult> ShouldBe(TResult expected)
    {
        ShouldBeSet();
        Assert.True(object.Equals(expected, Result), $"{Name} was expected to be {expected}, but it's {Result} instead.");
        return this;
    }

    public ThenResult<TResult> ShouldNotBe(TResult unexpected)
    {
        ShouldBeSet();
        Assert.False(object.Equals(unexpected, Result), $"{Name} was expected not to be {unexpected}, but it is.");
        return this;
    }

    // ---------------
    // Type assertions
    // ---------------

    // Instance of

    public ThenResult<TResult> ShouldBeInstanceOf(Type expected)
    {
        ShouldBeSet();
        if (!expected.IsInstanceOfType(Result))
            throw new XunitException($"{Name} was expected to be assignable to {expected.FullName}, but it's {Result?.GetType().FullName ?? "null"} instead.");

        return this;
    }

    public ThenResult<TExpected> ShouldBeInstanceOf<TExpected>()
    {
        ShouldBeSet();
        if (Result is not TExpected typedResult)
            throw new XunitException($"{Name} was expected to be assignable to {typeof(TExpected).FullName}, but it's {Result?.GetType().FullName ?? "null"} instead.");

        return new ThenResult<TExpected>(Name, typedResult);
    }

    public ThenResult<TResult> ShouldNotBeInstanceOf(Type unexpected)
    {
        ShouldBeSet();
        if (unexpected.IsInstanceOfType(Result))
            throw new XunitException($"{Name} was expected not be assignable to {unexpected.FullName}, but it is.");

        return this;
    }

    public ThenResult<TResult> ShouldNotBeInstanceOf<TUnexpected>()
    {
        return ShouldNotBeInstanceOf(typeof(TUnexpected));
    }

    // Exact type

    public ThenResult<TResult> ShouldHaveExactType(Type expected)
    {
        ShouldBeSet();
        if (Result?.GetType() != expected)
            throw new XunitException($"{Name} was expected to be of exact type {expected.FullName}, but it's {Result?.GetType().FullName ?? "null"} instead.");

        return this;
    }

    public ThenResult<TExpected> ShouldHaveExactType<TExpected>()
    {
        ShouldBeSet();
        if (Result is not TExpected typedResult || Result?.GetType() != typeof(TExpected))
            throw new XunitException($"{Name} was expected to be of exact type {typeof(TExpected).FullName}, but it's {Result?.GetType().FullName ?? "null"} instead.");

        return new ThenResult<TExpected>(Name, typedResult);
    }

    public ThenResult<TResult> ShouldNotHaveExactType(Type unexpected)
    {
        ShouldBeSet();
        if (Result?.GetType() == unexpected)
            throw new XunitException($"{Name} was expected not be of exact type {unexpected.FullName}, but it is.");

        return this;
    }

    public ThenResult<TResult> ShouldNotHaveExactType<TUnexpected>()
    {
        return ShouldNotHaveExactType(typeof(TUnexpected));
    }
}
