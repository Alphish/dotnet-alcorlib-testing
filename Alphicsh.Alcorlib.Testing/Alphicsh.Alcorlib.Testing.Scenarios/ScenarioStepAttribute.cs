namespace Alphicsh.Alcorlib.Testing.Scenarios;

public class ScenarioStepAttribute(int order) : BaseScenarioMethodAttribute
{
    public int Order { get; } = order;

    public override string ScenarioRole => "Step";
    public override string ScenarioOrder => $"B,{Order}";
}
