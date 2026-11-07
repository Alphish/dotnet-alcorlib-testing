using Xunit.v3;

namespace Alphicsh.Alcorlib.Testing.Scenarios;

public interface IScenarioAttribute : ITraitAttribute
{
    string ScenarioRole { get; }
    string ScenarioOrder { get; }
}
