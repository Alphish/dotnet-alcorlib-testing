namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Systems;

public class ScenarioOrderSequenceTests
{
    [Fact]
    public void ShouldCreateNumericSequenceCorrectly()
    {
        var sequence = new ScenarioOrderSequence("1,2,9");
        Assert.Equal("1,2,9", sequence.Input);
        Assert.Equal("1,2,9", sequence.ToString());

        Assert.Equal(3, sequence.Segments.Count);
        Assert.StrictEqual(sequence.Segments[0], 1);
        Assert.StrictEqual(sequence.Segments[1], 2);
        Assert.StrictEqual(sequence.Segments[2], 9);
    }

    [Fact]
    public void ShouldCreateStringSequenceCorrectly()
    {
        var sequence = new ScenarioOrderSequence("Lorem,Ipsum,Dolor");
        Assert.Equal("Lorem,Ipsum,Dolor", sequence.Input);
        Assert.Equal("Lorem,Ipsum,Dolor", sequence.ToString());

        Assert.Equal(3, sequence.Segments.Count);
        Assert.StrictEqual(sequence.Segments[0], "Lorem");
        Assert.StrictEqual(sequence.Segments[1], "Ipsum");
        Assert.StrictEqual(sequence.Segments[2], "Dolor");
    }

    [Fact]
    public void ShouldCreateMixedSequenceCorrectly()
    {
        var sequence = new ScenarioOrderSequence("A,1,B");
        Assert.Equal("A,1,B", sequence.Input);
        Assert.Equal("A,1,B", sequence.ToString());
        
        Assert.Equal(3, sequence.Segments.Count);
        Assert.StrictEqual(sequence.Segments[0], "A");
        Assert.StrictEqual(sequence.Segments[1], 1);
        Assert.StrictEqual(sequence.Segments[2], "B");
    }

    [Fact]
    public void ShouldCompareMixedSegmentTypes()
    {
        var first = new ScenarioOrderSequence("A,2");

        // ensuring that 10 is placed after 2, despite "10" being earlier than "2" in string sorting
        var second = new ScenarioOrderSequence("A,10");
        Assert.True(first.CompareTo(second) < 0);
        Assert.True(second.CompareTo(first) > 0);

        // ensuring that B earlier in the segment decides the later order, despite later 1 being less than 2 and 10 alike
        var third = new ScenarioOrderSequence("B,1");
        Assert.True(first.CompareTo(third) < 0);
        Assert.True(third.CompareTo(first) > 0);
        Assert.True(second.CompareTo(third) < 0);
        Assert.True(third.CompareTo(second) > 0);

    }

    [Fact]
    public void ShouldEquateSameInputSequences()
    {
        var sequence = new ScenarioOrderSequence("A,10");
        var otherSequence = new ScenarioOrderSequence("A,10");
        Assert.True(sequence.CompareTo(otherSequence) == 0);
        Assert.True(otherSequence.CompareTo(sequence) == 0);
    }

    [Fact]
    public void ShouldNotEquateAnotherType()
    {
        var sequence = new ScenarioOrderSequence("Lorem");
        Assert.False(sequence.Equals("Lorem"));
    }

    [Fact]
    public void ShouldCompareItselfBeforeNull()
    {
        var sequence = new ScenarioOrderSequence("A,1,B");
        Assert.True(sequence.CompareTo(null) < 0);
    }

    [Fact]
    public void ShouldPlaceSubsequenceBeforeFullSequence()
    {
        var first = new ScenarioOrderSequence("A");
        var second = new ScenarioOrderSequence("A,10");
        Assert.True(first.CompareTo(second) < 0);
        Assert.True(second.CompareTo(first) > 0);
    }
}
