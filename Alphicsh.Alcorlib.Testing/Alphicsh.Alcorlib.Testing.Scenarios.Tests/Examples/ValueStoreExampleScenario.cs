namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Examples;

public class ValueStoreExampleScenario : BaseScenario
{
    private const string GuidKey = "TEST_GUID";
    private static Dictionary<Guid, string> TestDictionary { get; } = new Dictionary<Guid, string>();

    public ValueStoreExampleScenario(ScenarioFixture scenarioFixture) : base(scenarioFixture) { }

    [ScenarioStep(01)]
    public void Step01_AddSomeValue()
    {
        var guid = Guid.NewGuid();
        TestDictionary.Add(guid, "ExampleValue");
        Assert.Single(TestDictionary);

        ScenarioFixture.SetValue(GuidKey, guid);
    }

    [ScenarioStep(02)]
    public void Step02_CheckTheValue()
    {
        var guid = ScenarioFixture.GetValue<Guid>(GuidKey);
        Assert.Equal("ExampleValue", TestDictionary[guid]);
    }
}
