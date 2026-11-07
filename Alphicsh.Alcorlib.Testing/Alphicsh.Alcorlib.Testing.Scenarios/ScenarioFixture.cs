using System.Reflection;
using Xunit;
using Xunit.Internal;
using Xunit.v3;

namespace Alphicsh.Alcorlib.Testing.Scenarios;

public class ScenarioFixture : IScenarioFixture
{
    private bool IsInitialized { get; set; }
    private Queue<HashSet<string>> PendingTests { get; } = new Queue<HashSet<string>>();
    private HashSet<string> CompletedSetupKeys { get; } = new HashSet<string>();

    private Dictionary<string, object?> StoredValues { get; } = new Dictionary<string, object?>();

    // ------------
    // Flow control
    // ------------

    public void PrepareTest(IXunitTest test)
    {
        if (!IsInitialized)
            InitializeFixture(test);

        switch (test.GetScenarioRole())
        {
            case "Step":
                var isStepUpcoming = PendingTests.TryPeek(out var upcomingSteps) && upcomingSteps.Contains(test.TestCase.TestMethod.UniqueID);
                Assert.SkipUnless(isStepUpcoming, "The required previous steps were not executed or failed, skipping.");
                return;

            case "Teardown":
                var priorSetupsCompleted = test.GetSetupKeys().All(CompletedSetupKeys.Contains);
                Assert.SkipUnless(priorSetupsCompleted, "One or more of prior setups were not completed, skipping.");
                return;
        }
    }

    private void InitializeFixture(IXunitTest test)
    {
        var scenarioAttributeType = typeof(IScenarioAttribute);
        var scenarioClass = test.TestMethod.Method.DeclaringType!;
        var expectedMethodNames = scenarioClass
            .GetMethods(BindingFlags.Public | BindingFlags.Instance)
            .Where(method => method.CustomAttributes.Any(attribute => attribute.AttributeType.IsAssignableTo(scenarioAttributeType)))
            .Select(method => method.Name)
            .ToList();

        var orderer = test.TestCase.TestClass.TestMethodOrderer as IScenarioMethodOrderer;
        var foundMethods = orderer!.FoundMethods;
        var missingMethods = expectedMethodNames.Where(method => !foundMethods.Contains(method)).ToList();
        var missingMethodsString = string.Join(", ", missingMethods);
        if (foundMethods.Count < expectedMethodNames.Count)
            Assert.Fail($"The entire scenario must be executed at once, but some test stages weren't scheduled.\nMissing stages names: {missingMethodsString}");

        foreach (var group in orderer.TestOrderGroups)
        {
            PendingTests.Enqueue(group.ToHashSet());
        }

        IsInitialized = true;
    }

    public void CompleteTest(IXunitTest test, TestResult result)
    {
        // exiting early if the test wasn't completed successfully
        if (result != TestResult.Passed)
            return;

        // removing the completed test from pending tests
        foreach (var testGroup in PendingTests)
        {
            if (testGroup.Remove(test.TestCase.TestMethod.UniqueID))
                break;
        }

        // if all tests from the earliest group are completed
        // advance to the next group
        if (PendingTests.TryPeek(out var steps) && steps.Count == 0)
            PendingTests.Dequeue();

        // mark completed setup keys, if any
        if (test.HasScenarioRole("Setup"))
        {
            foreach (var key in test.GetSetupKeys())
            {
                CompletedSetupKeys.Add(key);
            }
        }
    }

    // -----------------
    // Values management
    // -----------------

    public object? GetValue(string key)
    {
        Assert.True(StoredValues.TryGetValue(key, out var value), $"Expected the scenario to contain the '{key}' value but found none.");
        return value;
    }

    public T? GetValue<T>(string key)
    {
        var value = GetValue(key);
        return object.Equals(value, default(T)) ? default(T) : Assert.IsType<T>(value);
    }

    public void SetValue(string key, object? value)
       => StoredValues[key] = value;
}
