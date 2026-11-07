using System.Globalization;

namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Flow;

public class ScenarioFlowCase : BaseScenario
{
    public ScenarioFlowCase(ScenarioFixture scenarioFixture) : base(scenarioFixture)
    {
    }

    public void SimulatePassed()
    {
        // do nothing
    }

    public void SimulateFailed()
    {
        if (CultureInfo.CurrentCulture.Name == "")
            return;

        Assert.Fail("Failure simulated.");
    }
}
