using Alphicsh.Alcorlib.Testing.Thens;

namespace Alphicsh.Alcorlib.Testing.Tests.Thens;

public partial class ThenResultTests
{
    [Fact]
    public void ShouldBeCreatedUnsetAndWithDefaultResult()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        Assert.Equal("ThenLorem", thenResult.Name);
        Assert.Equal(default(int), thenResult.Result);
        Assert.False(thenResult.IsSet);
    }

    [Fact]
    public void ShouldBeCreatedWithGivenResult()
    {
        var thenResult = new ThenResult<int>("ThenLorem", 123);
        Assert.Equal("ThenLorem", thenResult.Name);
        Assert.Equal(123, thenResult.Result);
        Assert.True(thenResult.IsSet);
    }

    [Fact]
    public void ShouldBeCreatedStaticallyWithGivenResult()
    {
        var thenResult = ThenResult.Of("ThenLorem", 123);
        Assert.Equal("ThenLorem", thenResult.Name);
        Assert.Equal(123, thenResult.Result);
        Assert.True(thenResult.IsSet);
    }

    [Fact]
    public void ShouldSetPassedResult()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        thenResult.Accept(123);
        Assert.Equal(123, thenResult.Result);
        Assert.True(thenResult.IsSet);
    }

    [Fact]
    public void ShouldPreventSettingValueTwice()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        thenResult.Accept(123);

        Action testAction = () => thenResult.Accept(456);
        Assert.Throws<InvalidOperationException>(testAction);
    }

    [Fact]
    public void ShouldHaveCorrectStringRepresentationWhenUnset()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        Assert.Equal("ThenLorem = <unset>", thenResult.ToString());
    }

    [Fact]
    public void ShouldHaveCorrectStringRepresentationWhenSet()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        thenResult.Accept(123);
        Assert.Equal("ThenLorem = 123", thenResult.ToString());
    }
}
