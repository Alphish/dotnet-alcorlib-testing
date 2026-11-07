namespace Alphicsh.Alcorlib.Testing.Scenarios;

public class ScenarioTeardownAttribute : BaseScenarioMethodAttribute
{
    public string? SetupKeys { get; init; }

    public override string ScenarioRole => "Teardown";
    public override string ScenarioOrder => "Z";

    public override IReadOnlyCollection<KeyValuePair<string, string>> GetTraits()
    {
        var baseTraits = base.GetTraits();
        if (SetupKeys == null)
            return baseTraits;

        var totalTraits = baseTraits.ToList();
        foreach (var key in SetupKeys.Split(','))
        {
            totalTraits.Add(KeyValuePair.Create("SetupKey", key));
        }
        return totalTraits;
    }
}
