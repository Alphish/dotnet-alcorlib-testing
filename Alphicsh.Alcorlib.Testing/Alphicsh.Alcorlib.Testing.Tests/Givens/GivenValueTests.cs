using Alphicsh.Alcorlib.Testing.Givens;

namespace Alphicsh.Alcorlib.Testing.Tests.Givens;

public class GivenValueTests
{
    [Fact]
    public void ShouldHaveGivenName()
    {
        var givenValue = new GivenValue<int>("GivenLorem");
        Assert.Equal("GivenLorem", givenValue.Name);
    }

    [Fact]
    public void ShouldStartUnsetAndWithDefaultValue()
    {
        var givenValue = new GivenValue<int>("GivenLorem");
        Assert.Equal(default(int), givenValue.Value);
        Assert.False(givenValue.IsSet);
    }

    [Fact]
    public void ShouldUsePassedValue()
    {
        var givenValue = new GivenValue<int>("GivenLorem");
        givenValue.Of(123);
        Assert.Equal(123, givenValue.Value);
        Assert.True(givenValue.IsSet);
    }

    [Fact]
    public void ShouldPreventSettingValueTwice()
    {
        var givenValue = new GivenValue<int>("GivenLorem");
        givenValue.Of(123);

        Action testAction = () => givenValue.Of(456);
        Assert.Throws<InvalidOperationException>(testAction);
    }

    [Fact]
    public void ShouldActAsInnerType()
    {
        var givenValue = new GivenValue<int>("GivenLorem");
        givenValue.Of(123);
        var result = givenValue + 456;
        Assert.Equal(579, result);
    }

    [Fact]
    public void ShouldHaveCorrectStringRepresentationWhenUnset()
    {
        var givenValue = new GivenValue<int>("GivenLorem");
        Assert.Equal("GivenLorem = <unset>", givenValue.ToString());
    }

    [Fact]
    public void ShouldHaveCorrectStringRepresentationWhenSet()
    {
        var givenValue = new GivenValue<int>("GivenLorem");
        givenValue.Of(123);
        Assert.Equal("GivenLorem = 123", givenValue.ToString());
    }
}
