using Xunit;

namespace Alphicsh.Alcorlib.Testing.Scenarios;

[TestMethodOrderer(typeof(ScenarioMethodOrderer))]
[TestClass(DisableParallelization = true)]
public abstract class BaseScenario(ScenarioFixture scenarioFixture) : IClassFixture<ScenarioFixture>, ITestScenario
{
    public IScenarioFixture ScenarioFixture { get; } = scenarioFixture;
}
