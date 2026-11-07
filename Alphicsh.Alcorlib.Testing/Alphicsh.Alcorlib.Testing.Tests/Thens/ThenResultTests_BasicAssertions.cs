using Alphicsh.Alcorlib.Testing.Thens;

namespace Alphicsh.Alcorlib.Testing.Tests.Thens;

public partial class ThenResultTests
{
    // ---------
    // Set/Unset
    // ---------

    [Fact]
    public void ShouldBeUnsetShouldPassWhenUnset()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        AssertCheckPasses(() => thenResult.ShouldBeUnset());
    }

    [Fact]
    public void ShouldBeUnsetShouldFailWhenGivenResult()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        thenResult.Accept(123);
        AssertCheckFails(() => thenResult.ShouldBeUnset());
    }

    [Fact]
    public void ShouldBeSetShouldFailWhenUnset()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        AssertCheckFails(() => thenResult.ShouldBeSet());
    }

    [Fact]
    public void ShouldBeSetShouldPassWhenGivenResult()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        thenResult.Accept(123);
        AssertCheckPasses(() => thenResult.ShouldBeSet());
    }

    // ------------
    // Null/NotNull
    // ------------

    [Fact]
    public void ShouldBeNullShouldFailWhenUnset()
    {
        var thenResult = new ThenResult<string?>("ThenLorem");
        AssertCheckFails(() => thenResult.ShouldBeNull());
    }

    [Fact]
    public void ShouldBeNullShouldPassWhenGivenNull()
    {
        var thenResult = new ThenResult<string?>("ThenLorem");
        thenResult.Accept(null);
        AssertCheckPasses(() => thenResult.ShouldBeNull());
    }

    [Fact]
    public void ShouldBeNullShouldFailWhenGivenNotNull()
    {
        var thenResult = new ThenResult<string?>("ThenLorem");
        thenResult.Accept("Ipsum");
        AssertCheckFails(() => thenResult.ShouldBeNull());
    }

    [Fact]
    public void ShouldNotBeNullShouldFailWhenUnset()
    {
        var thenResult = new ThenResult<string?>("ThenLorem");
        AssertCheckFails(() => thenResult.ShouldNotBeNull());
    }

    [Fact]
    public void ShouldNotBeNullShouldFailWhenGivenNull()
    {
        var thenResult = new ThenResult<string?>("ThenLorem");
        thenResult.Accept(null);
        AssertCheckFails(() => thenResult.ShouldNotBeNull());
    }

    [Fact]
    public void ShouldNotBeNullShouldPassWhenGivenNotNull()
    {
        var thenResult = new ThenResult<string?>("ThenLorem");
        thenResult.Accept("Ipsum");
        AssertCheckPasses(() => thenResult.ShouldNotBeNull());
    }

    // -----------------
    // To Be or to NotBe
    // -----------------

    [Fact]
    public void ShouldBeShouldFailWhenUnset()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        AssertCheckFails(() => thenResult.ShouldBe(123));
    }

    [Fact]
    public void ShouldBeShouldPassWhenGivenSameValue()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        thenResult.Accept(123);
        AssertCheckPasses(() => thenResult.ShouldBe(123));
    }

    [Fact]
    public void ShouldBeShouldFailWhenGivenDifferentValue()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        thenResult.Accept(123);
        AssertCheckFails(() => thenResult.ShouldBe(456));
    }

    [Fact]
    public void ShouldNotBeShouldFailWhenUnset()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        AssertCheckFails(() => thenResult.ShouldNotBe(123));
    }

    [Fact]
    public void ShouldNotBeShouldFailWhenGivenSameValue()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        thenResult.Accept(123);
        AssertCheckFails(() => thenResult.ShouldNotBe(123));
    }

    [Fact]
    public void ShouldNotBeShouldPassWhenGivenDifferentValue()
    {
        var thenResult = new ThenResult<int>("ThenLorem");
        thenResult.Accept(123);
        AssertCheckPasses(() => thenResult.ShouldNotBe(456));
    }
}
