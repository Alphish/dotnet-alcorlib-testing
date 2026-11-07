namespace Alphicsh.Alcorlib.Testing.Scenarios;

public class ScenarioSetupAttribute : BaseScenarioMethodAttribute
{
    public string? SetupKey { get; init; }

    public override string ScenarioRole => "Setup";
    public override string ScenarioOrder => "A";

    public override IReadOnlyCollection<KeyValuePair<string, string>> GetTraits()
    {
        var baseTraits = base.GetTraits();
        if (SetupKey == null)
            return baseTraits;

        var totalTraits = baseTraits.ToList();
        totalTraits.Add(KeyValuePair.Create("SetupKey", SetupKey));
        return totalTraits;
    }
}
