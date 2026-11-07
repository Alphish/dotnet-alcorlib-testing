using System.Reflection;
using Xunit;
using Xunit.v3;

namespace Alphicsh.Alcorlib.Testing.Scenarios;

public abstract class BaseScenarioMethodAttribute : FactAttribute, IScenarioAttribute, IBeforeAfterTestAttribute
{
    public abstract string ScenarioRole { get; }
    public abstract string ScenarioOrder { get; }

    public virtual IReadOnlyCollection<KeyValuePair<string, string>> GetTraits()
    {
        return [
            KeyValuePair.Create(nameof(ScenarioRole), ScenarioRole),
            KeyValuePair.Create(nameof(ScenarioOrder), ScenarioOrder),
        ];
    }

    public virtual void Before(MethodInfo methodUnderTest, IXunitTest test)
    {
        var orderer = test.TestCase.TestClass.TestCaseOrderer;
        var scenario = ExtractScenarioFixture();
        scenario.PrepareTest(test);
    }

    public virtual void After(MethodInfo methodUnderTest, IXunitTest test)
    {
        var scenario = ExtractScenarioFixture();
        scenario.CompleteTest(test, TestContext.Current.TestState!.Result);
    }

    protected virtual IScenarioFixture ExtractScenarioFixture()
    {
        var instance = TestContext.Current.TestClassInstance;
        Assert.True(instance != null, $"Cannot perform a scenario setup without a test class instance.");

        var testScenario = instance as ITestScenario;
        Assert.True(testScenario != null, $"The test class instance must implement {nameof(ITestScenario)} interface to apply the scenario setup.");
        return testScenario.ScenarioFixture;
    }
}
