using Alphicsh.Alcorlib.Testing.Thens;

namespace Alphicsh.Alcorlib.Testing.Tests.Thens;

public class ThenDictionaryTests : BaseResultTests
{
    // --------
    // Creation
    // --------

    [Fact]
    public void ShouldBeCreatedUnsetAndWithDefaultResult()
    {
        var thenResult = new ThenDictionary<string, int>("ThenLorem");
        Assert.Equal("ThenLorem", thenResult.Name);
        Assert.Equal(null!, thenResult.Result);
        Assert.False(thenResult.IsSet);
    }

    [Fact]
    public void ShouldBeCreatedWithGivenResult()
    {
        var thenResult = new ThenDictionary<string, int>("ThenLorem", new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
        });
        Assert.Equal("ThenLorem", thenResult.Name);
        Assert.Equal(1, thenResult.Result["one"]);
        Assert.Equal(2, thenResult.Result["two"]);
        Assert.Equal(3, thenResult.Result["three"]);
        Assert.True(thenResult.IsSet);
    }

    [Fact]
    public void ShouldBeCreatedStaticallyWithGivenResult()
    {
        var thenResult = ThenDictionary.Of("ThenLorem", new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
        });
        Assert.Equal("ThenLorem", thenResult.Name);
        Assert.Equal(1, thenResult.Result["one"]);
        Assert.Equal(2, thenResult.Result["two"]);
        Assert.Equal(3, thenResult.Result["three"]);
        Assert.True(thenResult.IsSet);
    }

    // --------
    // Indexing
    // --------

    [Fact]
    public void ShouldIndexEmptyCollection()
    {
        var thenResult = new ThenDictionary<string, int>("ThenLorem", new Dictionary<string, int>());
        AssertCheckPasses(() => thenResult.Indexed().ShouldHaveNoMoreEntries());
    }

    [Fact]
    public void ShouldIndexSingleEntryDictionary()
    {
        var thenResult = ThenDictionary.Of("ThenLorem", new Dictionary<string, int>
        {
            ["one"] = 1,
        });
        AssertCheckPasses(() => thenResult.Indexed()
            .ShouldHaveEntry("one", 1)
            .ShouldHaveNoMoreEntries()
        );
    }

    [Fact]
    public void ShouldIndexMultiEntryDictionary()
    {
        var thenResult = ThenDictionary.Of("ThenLorem", new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
        });
        AssertCheckPasses(() => thenResult.Indexed()
            .ShouldHaveEntry("one", 1)
            .ShouldHaveEntry("two", 2)
            .ShouldHaveEntry("three", 3)
            .ShouldHaveNoMoreEntries()
        );
    }

    [Fact]
    public void ShouldIndexEntriesOutOfOrder()
    {
        var thenResult = ThenDictionary.Of("ThenLorem", new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
        });
        AssertCheckPasses(() => thenResult.Indexed()
            .ShouldHaveEntry("two", 2)
            .ShouldHaveEntry("one", 1)
            .ShouldHaveEntry("three", 3)
            .ShouldHaveNoMoreEntries()
        );
    }

    [Fact]
    public void ShouldIndexSameEntriesManyTimes()
    {
        var thenResult = ThenDictionary.Of("ThenLorem", new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
        });
        AssertCheckPasses(() => thenResult.Indexed()
            .ShouldHaveEntry("two", 2)
            .ShouldHaveEntry("one", 1)
            .ShouldHaveEntry("three", 3)
            .ShouldHaveEntry("two", 2)
            .ShouldHaveEntry("one", 1)
            .ShouldHaveNoMoreEntries()
        );
    }

    [Fact]
    public void ShouldFailToEndBeforeAnyEntry()
    {
        var thenResult = ThenDictionary.Of("ThenLorem", new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
        });
        AssertCheckFails(() => thenResult.Indexed().ShouldHaveNoMoreEntries());
    }

    [Fact]
    public void ShouldFailToEndBeforeSomeEntries()
    {
        var thenResult = ThenDictionary.Of("ThenLorem", new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
        });
        AssertCheckFails(() => thenResult.Indexed()
            .ShouldHaveEntry("one", 1)
            .ShouldHaveEntry("two", 2)
            .ShouldHaveEntry("one", 1)
            .ShouldHaveNoMoreEntries()
        );
    }

    [Fact]
    public void ShouldFailToConfirmUnknownKey()
    {
        var thenResult = ThenDictionary.Of("ThenLorem", new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
        });
        AssertCheckFails(() => thenResult.Indexed().ShouldHaveEntry("four", 4));
    }

    [Fact]
    public void ShouldFailToConfirmUnexpectedValue()
    {
        var thenResult = ThenDictionary.Of("ThenLorem", new Dictionary<string, int>
        {
            ["one"] = 1,
            ["two"] = 2,
            ["three"] = 3,
        });
        AssertCheckFails(() => thenResult.Indexed().ShouldHaveEntry("one", 11));
    }
}
