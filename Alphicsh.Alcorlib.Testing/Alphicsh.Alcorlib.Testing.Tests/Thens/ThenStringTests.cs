using Alphicsh.Alcorlib.Testing.Thens;

namespace Alphicsh.Alcorlib.Testing.Tests.Thens;

public class ThenStringTests : BaseResultTests
{
    // ---------
    // Emptiness
    // ---------

    // ShouldBeEmpty

    [Fact]
    public void ShouldBeEmptyShouldAcceptEmptyString()
    {
        var thenResult = new ThenString("Result", "");
        AssertCheckPasses(() => thenResult.ShouldBeEmpty());
    }

    [Fact]
    public void ShouldBeEmptyShouldRejectNull()
    {
        var thenResult = new ThenString("Result", null);
        AssertCheckFails(() => thenResult.ShouldBeEmpty());
    }

    [Fact]
    public void ShouldBeEmptyShouldRejectWhiteSpace()
    {
        var thenResult = new ThenString("Result", "  \n  ");
        AssertCheckFails(() => thenResult.ShouldBeEmpty());
    }

    [Fact]
    public void ShouldBeEmptyShouldRejectProperString()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckFails(() => thenResult.ShouldBeEmpty());
    }

    // ShouldBeNullOrEmpty

    [Fact]
    public void ShouldBeNullOrEmptyShouldAcceptEmptyString()
    {
        var thenResult = new ThenString("Result", "");
        AssertCheckPasses(() => thenResult.ShouldBeNullOrEmpty());
    }

    [Fact]
    public void ShouldBeNullOrEmptyShouldAcceptNull()
    {
        var thenResult = new ThenString("Result", null);
        AssertCheckPasses(() => thenResult.ShouldBeNullOrEmpty());
    }

    [Fact]
    public void ShouldBeNullOrEmptyShouldRejectWhiteSpace()
    {
        var thenResult = new ThenString("Result", "  \n  ");
        AssertCheckFails(() => thenResult.ShouldBeNullOrEmpty());
    }

    [Fact]
    public void ShouldBeNullOrEmptyShouldRejectProperString()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckFails(() => thenResult.ShouldBeNullOrEmpty());
    }

    // ShouldBeNonEmpty

    [Fact]
    public void ShouldBeNonEmptyShouldRejectEmptyString()
    {
        var thenResult = new ThenString("Result", "");
        AssertCheckFails(() => thenResult.ShouldBeNonEmpty());
    }

    [Fact]
    public void ShouldBeNonEmptyShouldRejectNull()
    {
        var thenResult = new ThenString("Result", null);
        AssertCheckFails(() => thenResult.ShouldBeNonEmpty());
    }

    [Fact]
    public void ShouldBeNonEmptyShouldAcceptWhiteSpace()
    {
        var thenResult = new ThenString("Result", "  \n  ");
        AssertCheckPasses(() => thenResult.ShouldBeNonEmpty());
    }

    [Fact]
    public void ShouldBeNonEmptyShouldAcceptProperString()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckPasses(() => thenResult.ShouldBeNonEmpty());
    }

    // ShouldBeNullOrWhiteSpace

    [Fact]
    public void ShouldBeNullOrWhiteSpaceShouldAcceptEmptyString()
    {
        var thenResult = new ThenString("Result", "");
        AssertCheckPasses(() => thenResult.ShouldBeNullOrWhiteSpace());
    }

    [Fact]
    public void ShouldBeNullOrWhiteSpaceShouldAcceptNull()
    {
        var thenResult = new ThenString("Result", null);
        AssertCheckPasses(() => thenResult.ShouldBeNullOrWhiteSpace());
    }

    [Fact]
    public void ShouldBeNullOrWhiteSpaceShouldAcceptWhiteSpace()
    {
        var thenResult = new ThenString("Result", "  \n  ");
        AssertCheckPasses(() => thenResult.ShouldBeNullOrWhiteSpace());
    }

    [Fact]
    public void ShouldBeNullOrWhiteSpaceShouldRejectProperString()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckFails(() => thenResult.ShouldBeNullOrWhiteSpace());
    }

    // ShouldBeNonWhiteSpace

    [Fact]
    public void ShouldBeNonWhiteSpaceShouldRejectEmptyString()
    {
        var thenResult = new ThenString("Result", "");
        AssertCheckFails(() => thenResult.ShouldBeNonWhiteSpace());
    }

    [Fact]
    public void ShouldBeNonWhiteSpaceShouldRejectNull()
    {
        var thenResult = new ThenString("Result", null);
        AssertCheckFails(() => thenResult.ShouldBeNonWhiteSpace());
    }

    [Fact]
    public void ShouldBeNonWhiteSpaceShouldRejectWhiteSpace()
    {
        var thenResult = new ThenString("Result", "  \n  ");
        AssertCheckFails(() => thenResult.ShouldBeNonWhiteSpace());
    }

    [Fact]
    public void ShouldBeNonWhiteSpaceShouldAcceptProperString()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckPasses(() => thenResult.ShouldBeNonWhiteSpace());
    }

    // ----------
    // Substrings
    // ----------

    // ShouldContain

    [Fact]
    public void ShouldContainShouldMatchEmptyStringWithEmptyString()
    {
        var thenResult = new ThenString("Result", "");
        AssertCheckPasses(() => thenResult.ShouldContain(""));
    }

    [Fact]
    public void ShouldContainShouldMatchEmptyStringWithProperString()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckPasses(() => thenResult.ShouldContain(""));
    }

    [Fact]
    public void ShouldContainShouldMatchStringWithItself()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckPasses(() => thenResult.ShouldContain("Lorem"));
    }

    [Fact]
    public void ShouldContainShouldMatchStringStart()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckPasses(() => thenResult.ShouldContain("Lo"));
    }

    [Fact]
    public void ShouldContainShouldMatchStringEnd()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckPasses(() => thenResult.ShouldContain("rem"));
    }

    [Fact]
    public void ShouldContainShouldMatchStringMiddle()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckPasses(() => thenResult.ShouldContain("ore"));
    }

    [Fact]
    public void ShouldContainShouldNotMatchNonSubstring()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckFails(() => thenResult.ShouldContain("sum"));
    }

    // ShouldNotContain

    [Fact]
    public void ShouldNotContainShouldRejectEmptyStringWithEmptyString()
    {
        var thenResult = new ThenString("Result", "");
        AssertCheckFails(() => thenResult.ShouldNotContain(""));
    }

    [Fact]
    public void ShouldNotContainShouldRejectEmptyStringWithProperString()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckFails(() => thenResult.ShouldNotContain(""));
    }

    [Fact]
    public void ShouldNotContainShouldRejectStringWithItself()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckFails(() => thenResult.ShouldNotContain("Lorem"));
    }

    [Fact]
    public void ShouldNotContainShouldRejectStringStart()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckFails(() => thenResult.ShouldNotContain("Lo"));
    }

    [Fact]
    public void ShouldNotContainShouldRejectStringEnd()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckFails(() => thenResult.ShouldNotContain("rem"));
    }

    [Fact]
    public void ShouldNotContainShouldRejectStringMiddle()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckFails(() => thenResult.ShouldNotContain("ore"));
    }

    [Fact]
    public void ShouldNotContainShouldAcceptNonSubstring()
    {
        var thenResult = new ThenString("Result", "Lorem");
        AssertCheckPasses(() => thenResult.ShouldNotContain("sum"));
    }
}
