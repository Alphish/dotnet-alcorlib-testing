using Xunit.Sdk;

namespace Alphicsh.Alcorlib.Testing.Scenarios;

public static class ScenarioExtensions
{
    public static ScenarioOrderSequence GetScenarioOrder(this ITestMethod testMethod)
    {
        var scenarioOrderKey = nameof(IScenarioAttribute.ScenarioOrder);
        if (!testMethod.Traits.TryGetValue(scenarioOrderKey, out var order))
            throw new ArgumentException($"The test method {testMethod.MethodName} does not have the {scenarioOrderKey} trait.");

        if (order.Count != 1)
            throw new ArgumentException($"The test method {testMethod.MethodName} should have a single {scenarioOrderKey} trait, but it has {order.Count}.");

        return new ScenarioOrderSequence(order.First());
    }

    public static string GetScenarioRole(this ITest test)
    {
        var scenarioRoleKey = nameof(IScenarioAttribute.ScenarioRole);
        if (!test.Traits.TryGetValue(scenarioRoleKey, out var roles))
            throw new ArgumentException($"The test {test.TestDisplayName} does not have the {scenarioRoleKey} trait.");

        if (roles.Count != 1)
            throw new ArgumentException($"The test {test.TestDisplayName} should have a single {scenarioRoleKey} trait, but it has {roles.Count}.");

        return roles.First();
    }

    public static bool HasScenarioRole(this ITest test, string role)
        => role.Equals(GetScenarioRole(test));

    public static IReadOnlyCollection<string> GetSetupKeys(this ITest test)
    {
        var setupKeyTrait = nameof(ScenarioSetupAttribute.SetupKey);
        return test.Traits.TryGetValue(setupKeyTrait, out var setupKeys) ? setupKeys : new List<string>();
    }
}
