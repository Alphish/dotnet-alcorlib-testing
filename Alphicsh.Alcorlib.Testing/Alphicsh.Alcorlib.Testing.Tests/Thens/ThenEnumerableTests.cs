using Alphicsh.Alcorlib.Testing.Thens;

namespace Alphicsh.Alcorlib.Testing.Tests.Thens;

public class ThenEnumerableTests : BaseResultTests
{
    // --------
    // Creation
    // --------

    [Fact]
    public void ShouldBeCreatedUnsetAndWithDefaultResult()
    {
        var thenResult = new ThenEnumerable<int>("ThenLorem");
        Assert.Equal("ThenLorem", thenResult.Name);
        Assert.Equal(null!, thenResult.Result);
        Assert.False(thenResult.IsSet);
    }

    [Fact]
    public void ShouldBeCreatedWithGivenResult()
    {
        var thenResult = new ThenEnumerable<int>("ThenLorem", [123, 456, 789]);
        Assert.Equal("ThenLorem", thenResult.Name);
        Assert.True(thenResult.Result.SequenceEqual([123, 456, 789]));
        Assert.True(thenResult.IsSet);
    }

    [Fact]
    public void ShouldBeCreatedStaticallyWithGivenResult()
    {
        var thenResult = ThenEnumerable.Of("ThenLorem", [123, 456, 789]);
        Assert.Equal("ThenLorem", thenResult.Name);
        Assert.True(thenResult.Result.SequenceEqual([123, 456, 789]));
        Assert.True(thenResult.IsSet);
    }

    // -----------
    // Enumeration
    // -----------

    [Fact]
    public void ShouldEnumerateEmptyCollection()
    {
        var thenResult = new ThenEnumerable<int>("ThenLorem", []);
        AssertCheckPasses(() => thenResult.Enumerated().ShouldEnd());
    }

    [Fact]
    public void ShouldEnumerateSingleItemCollection()
    {
        var thenResult = new ThenEnumerable<int>("ThenLorem", [123]);
        AssertCheckPasses(() => thenResult.Enumerated()
            .ShouldYield(123)
            .ShouldEnd()
        );
    }

    [Fact]
    public void ShouldEnumerateMultiItemCollection()
    {
        var thenResult = new ThenEnumerable<int>("ThenLorem", [123, 456, 789]);
        AssertCheckPasses(() => thenResult.Enumerated()
            .ShouldYield(123)
            .ShouldYield(456)
            .ShouldYield(789)
            .ShouldEnd()
        );
    }

    [Fact]
    public void ShouldFailToEndBeforeFirstCollectionItem()
    {
        var thenResult = new ThenEnumerable<int>("ThenLorem", [123, 456, 789]);
        AssertCheckFails(() => thenResult.Enumerated().ShouldEnd());
    }

    [Fact]
    public void ShouldFailToEndBeforeLaterCollectionItem()
    {
        var thenResult = new ThenEnumerable<int>("ThenLorem", [123, 456, 789]);
        AssertCheckFails(() => thenResult.Enumerated()
            .ShouldYield(123)
            .ShouldYield(456)
            .ShouldEnd()
        );
    }

    [Fact]
    public void ShouldFailToYieldEmptyCollectionItem()
    {
        var thenResult = new ThenEnumerable<int>("ThenLorem", []);
        AssertCheckFails(() => thenResult.Enumerated()
            .ShouldYield(123)
            .ShouldEnd()
        );
    }

    [Fact]
    public void ShouldFailToYieldPastFinalCollectionItem()
    {
        var thenResult = new ThenEnumerable<int>("ThenLorem", [123, 456]);
        AssertCheckFails(() => thenResult.Enumerated()
            .ShouldYield(123)
            .ShouldYield(456)
            .ShouldYield(789)
            .ShouldEnd()
        );
    }

    [Fact]
    public void ShouldFailToYieldFirstItemTwice()
    {
        var thenResult = new ThenEnumerable<int>("ThenLorem", [123, 456, 789]);
        AssertCheckFails(() => thenResult.Enumerated()
            .ShouldYield(123)
            .ShouldYield(123)
            .ShouldEnd()
        );
    }

    [Fact]
    public void ShouldFailToYieldIncorrectItem()
    {
        var thenResult = new ThenEnumerable<int>("ThenLorem", [123, 456, 789]);
        AssertCheckFails(() => thenResult.Enumerated()
            .ShouldYield(246)
            .ShouldEnd()
        );
    }

    [Fact]
    public void ShouldFailToYieldOutOfOrder()
    {
        var thenResult = new ThenEnumerable<int>("ThenLorem", [123, 456, 789]);
        AssertCheckFails(() => thenResult.Enumerated()
            .ShouldYield(789)
            .ShouldYield(456)
            .ShouldYield(123)
            .ShouldEnd()
        );
    }
}
