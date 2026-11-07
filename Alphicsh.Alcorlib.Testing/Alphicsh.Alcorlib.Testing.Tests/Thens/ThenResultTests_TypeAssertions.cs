using Alphicsh.Alcorlib.Testing.Thens;

namespace Alphicsh.Alcorlib.Testing.Tests.Thens;

public partial class ThenResultTests
{
    // ------------------------------
    // ShouldBeInstanceOf non-generic
    // ------------------------------

    [Fact]
    public void ShouldBeInstanceOfShouldFailWhenUnset()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        AssertCheckFails(() => thenResult.ShouldBeInstanceOf(typeof(ArgumentException)));
    }

    [Fact]
    public void ShouldBeInstanceOfShouldPassWhenGivenSameType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentException());
        AssertCheckPasses(() => thenResult.ShouldBeInstanceOf(typeof(ArgumentException)));
    }

    [Fact]
    public void ShouldBeInstanceOfShouldFailWhenGivenSeparateType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new InvalidOperationException());
        AssertCheckFails(() => thenResult.ShouldBeInstanceOf(typeof(ArgumentException)));
    }

    [Fact]
    public void ShouldBeInstanceOfShouldPassWhenGivenDerivedType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentNullException());
        AssertCheckPasses(() => thenResult.ShouldBeInstanceOf(typeof(ArgumentException)));
    }

    [Fact]
    public void ShouldBeInstanceOfShouldFailWhenGivenBaseType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentException());
        AssertCheckFails(() => thenResult.ShouldBeInstanceOf(typeof(ArgumentNullException)));
    }

    // --------------------------
    // ShouldBeInstanceOf generic
    // --------------------------

    [Fact]
    public void ShouldBeInstanceOfGenericShouldFailWhenUnset()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        AssertCheckFails(() => thenResult.ShouldBeInstanceOf<ArgumentException>());
    }

    [Fact]
    public void ShouldBeInstanceOfGenericShouldPassWhenGivenSameType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentException());
        AssertCheckPasses(() => thenResult.ShouldBeInstanceOf<ArgumentException>());
    }

    [Fact]
    public void ShouldBeInstanceOfGenericShouldFailWhenGivenSeparateType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new InvalidOperationException());
        AssertCheckFails(() => thenResult.ShouldBeInstanceOf<ArgumentException>());
    }

    [Fact]
    public void ShouldBeInstanceOfGenericShouldPassWhenGivenDerivedType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentNullException());
        AssertCheckPasses(() => thenResult.ShouldBeInstanceOf<ArgumentException>());
    }

    [Fact]
    public void ShouldBeInstanceOfGenericShouldFailWhenGivenBaseType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentException());
        AssertCheckFails(() => thenResult.ShouldBeInstanceOf<ArgumentNullException>());
    }

    [Fact]
    public void ShouldBeInstanceOfGenericShouldReturnSpecificTypeResult()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentNullException());
        Assert.IsType<ThenResult<ArgumentException>>(thenResult.ShouldBeInstanceOf<ArgumentException>());
    }

    // ---------------------
    // ShouldNotBeInstanceOf
    // ---------------------

    [Fact]
    public void ShouldNotBeInstanceOfToShouldFailWhenUnset()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        AssertCheckFails(() => thenResult.ShouldNotBeInstanceOf<ArgumentException>());
    }

    [Fact]
    public void ShouldNotBeAssignableToShouldFailWhenGivenSameType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentException());
        AssertCheckFails(() => thenResult.ShouldNotBeInstanceOf<ArgumentException>());
    }

    [Fact]
    public void ShouldNotBeAssignableToShouldFailWhenGivenDerivedType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentNullException());
        AssertCheckFails(() => thenResult.ShouldNotBeInstanceOf<ArgumentException>());
    }

    [Fact]
    public void ShouldNotBeAssignableToShouldPassWhenGivenBaseType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentException());
        AssertCheckFails(() => thenResult.ShouldBeInstanceOf<ArgumentNullException>());
    }

    [Fact]
    public void ShouldNotBeAssignableToShouldPassWhenGivenSeparateType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new InvalidOperationException());
        AssertCheckPasses(() => thenResult.ShouldNotBeInstanceOf<ArgumentException>());
    }

    // ------------------------
    // ShouldBeType non-generic
    // ------------------------

    [Fact]
    public void ShouldHaveExactTypeShouldFailWhenUnset()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        AssertCheckFails(() => thenResult.ShouldHaveExactType(typeof(ArgumentException)));
    }

    [Fact]
    public void ShouldHaveExactTypeShouldPassWhenGivenSameType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentException());
        AssertCheckPasses(() => thenResult.ShouldHaveExactType(typeof(ArgumentException)));
    }

    [Fact]
    public void ShouldHaveExactTypeShouldFailWhenGivenSeparateType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new InvalidOperationException());
        AssertCheckFails(() => thenResult.ShouldHaveExactType(typeof(ArgumentException)));
    }

    [Fact]
    public void ShouldHaveExactTypeShouldFailWhenGivenDerivedType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentNullException());
        AssertCheckFails(() => thenResult.ShouldHaveExactType(typeof(ArgumentException)));
    }

    // --------------------
    // ShouldBeType generic
    // --------------------

    [Fact]
    public void ShouldHaveExactTypeGenericShouldFailWhenUnset()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        AssertCheckFails(() => thenResult.ShouldHaveExactType<ArgumentException>());
    }

    [Fact]
    public void ShouldHaveExactTypeGenericShouldPassWhenGivenSameType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentException());
        AssertCheckPasses(() => thenResult.ShouldHaveExactType<ArgumentException>());
    }

    [Fact]
    public void ShouldHaveExactTypeGenericShouldFailWhenGivenSeparateType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new InvalidOperationException());
        AssertCheckFails(() => thenResult.ShouldHaveExactType<ArgumentException>());
    }

    [Fact]
    public void ShouldHaveExactTypeGenericShouldFailWhenGivenDerivedType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentNullException());
        AssertCheckFails(() => thenResult.ShouldHaveExactType<ArgumentException>());
    }

    [Fact]
    public void ShouldHaveExactTypeGenericShouldReturnSpecificTypeResult()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentException());
        Assert.IsType<ThenResult<ArgumentException>>(thenResult.ShouldHaveExactType<ArgumentException>());
    }

    // ---------------
    // ShouldNotBeType
    // ---------------

    [Fact]
    public void ShouldNotHaveExactTypeShouldFailWhenUnset()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        AssertCheckFails(() => thenResult.ShouldNotHaveExactType<ArgumentException>());
    }

    [Fact]
    public void ShouldNotHaveExactTypeShouldFailWhenGivenSameType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentException());
        AssertCheckFails(() => thenResult.ShouldNotHaveExactType<ArgumentException>());
    }

    [Fact]
    public void ShouldNotHaveExactTypeShouldPassWhenGivenSeparateType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new InvalidOperationException());
        AssertCheckPasses(() => thenResult.ShouldNotHaveExactType<ArgumentException>());
    }

    [Fact]
    public void ShouldNotHaveExactTypeShouldPassWhenGivenDerivedType()
    {
        var thenResult = new ThenResult<Exception>("ThenLorem");
        thenResult.Accept(new ArgumentNullException());
        AssertCheckPasses(() => thenResult.ShouldNotHaveExactType<ArgumentException>());
    }
}
