using Xunit.v3;

namespace Alphicsh.Alcorlib.Testing.Scenarios;

public interface IScenarioMethodOrderer : ITestMethodOrderer
{
    IReadOnlySet<string> FoundMethods { get; }
    IReadOnlyCollection<IReadOnlyCollection<string>> TestOrderGroups { get; }
}
