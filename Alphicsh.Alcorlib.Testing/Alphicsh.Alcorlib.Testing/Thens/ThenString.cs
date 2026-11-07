using Xunit;

namespace Alphicsh.Alcorlib.Testing.Thens;

public class ThenString : ThenResult<string?>
{
    public ThenString(string name) : base(name)
    {
    }

    public ThenString(string name, string? result) : base(name, result)
    {
    }

    // ---------
    // Emptiness
    // ---------

    public ThenString ShouldBeEmpty()
    {
        ShouldBeSet();
        Assert.True(Result == "", $"{Name} was expected to be an empty string but wasn't.");
        return this;
    }

    public ThenString ShouldBeNullOrEmpty()
    {
        ShouldBeSet();
        Assert.True(string.IsNullOrEmpty(Result), $"{Name} was expected to be null or empty but wasn't.");
        return this;
    }

    public ThenString ShouldBeNonEmpty()
    {
        ShouldBeSet();
        Assert.True(!string.IsNullOrEmpty(Result), $"{Name} was expected to be a non-empty string, but was null or empty.");
        return this;
    }

    public ThenString ShouldBeNullOrWhiteSpace()
    {
        ShouldBeSet();
        Assert.True(string.IsNullOrWhiteSpace(Result), $"{Name} was expected to be null or whitespace but wasn't.");
        return this;
    }

    public ThenString ShouldBeNonWhiteSpace()
    {
        ShouldBeSet();
        Assert.True(!string.IsNullOrWhiteSpace(Result), $"{Name} was expected to be a non-whitespace string, but was null or whitespace.");
        return this;
    }

    // ----------
    // Substrings
    // ----------

    public ThenString ShouldContain(string substring)
    {
        ShouldBeSet();
        Assert.True(Result != null && Result.Contains(substring), $"{Name} was expected to contain '{substring}', but it didn't.");
        return this;
    }

    public ThenString ShouldNotContain(string substring)
    {
        ShouldBeSet();
        Assert.False(Result != null && Result.Contains(substring), $"{Name} was expected not to contain '{substring}', but it did.");
        return this;
    }
}
