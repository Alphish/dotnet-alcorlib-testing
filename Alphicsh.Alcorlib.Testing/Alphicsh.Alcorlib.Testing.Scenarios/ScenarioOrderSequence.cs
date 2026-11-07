namespace Alphicsh.Alcorlib.Testing.Scenarios;

public class ScenarioOrderSequence(string input) : IComparable<ScenarioOrderSequence>, IEquatable<ScenarioOrderSequence>
{
    public string Input { get; } = input;
    public IReadOnlyList<IComparable> Segments { get; } = input.Split(',').Select(ParseElement).ToList();

    private static IComparable ParseElement(string str)
        => int.TryParse(str, out var result) ? result : str;

    public override bool Equals(object? obj)
        => obj is ScenarioOrderSequence sequence && sequence.Input == Input;

    public bool Equals(ScenarioOrderSequence? other)
        => other != null && other.Input == Input;

    public override int GetHashCode()
        => HashCode.Combine(Input);

    public int CompareTo(ScenarioOrderSequence? other)
    {
        if (other == null)
            return -1;

        for (var i = 0; i < Segments.Count; i++)
        {
            if (i >= other.Segments.Count)
                return 1;

            var stepResult = Segments[i].CompareTo(other.Segments[i]);
            if (stepResult != 0)
                return stepResult;
        }

        return other.Segments.Count > Segments.Count ? -1 : 0;
    }

    public override string ToString() => Input;
}
