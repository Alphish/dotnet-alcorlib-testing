using Alphicsh.Alcorlib.Testing.Scenarios.Tests.Examples;
using Alphicsh.Alcorlib.Testing.Scenarios.Tests.Systems.Fixture;
using Xunit.Sdk;

namespace Alphicsh.Alcorlib.Testing.Scenarios.Tests.Systems;

public class ScenarioFixtureTests
{
    // ------------
    // Flow control
    // ------------

    [Fact]
    public void CorrectFlow_ShouldHandleBasicExample()
    {
        var tests = DummyTestCollection.FromTestClass<BasicExampleScenario>();
        var setup = tests.GetByName(nameof(BasicExampleScenario.BasicSetup));
        var step = tests.GetByName(nameof(BasicExampleScenario.BasicCheck));
        var teardown = tests.GetByName(nameof(BasicExampleScenario.BasicTeardown));

        var fixture = new ScenarioFixture();
        PrepareAndCompletePassingTest(fixture, setup);
        PrepareAndCompletePassingTest(fixture, step);
        PrepareAndCompletePassingTest(fixture, teardown);
    }

    [Fact]
    public void CorrectFlow_ShouldHandleNoSetupTeardownExample()
    {
        var tests = DummyTestCollection.FromTestClass<ValueStoreExampleScenario>();
        var step1 = tests.GetByName(nameof(ValueStoreExampleScenario.Step01_AddSomeValue));
        var step2 = tests.GetByName(nameof(ValueStoreExampleScenario.Step02_CheckTheValue));

        var fixture = new ScenarioFixture();
        PrepareAndCompletePassingTest(fixture, step1);
        PrepareAndCompletePassingTest(fixture, step2);
    }

    [Fact]
    public void IncompleteFlow_ShouldPreventRunningScenarioPartially()
    {
        IReadOnlyCollection<string> includedMethods = [nameof(BasicExampleScenario.BasicCheck)];
        var tests = DummyTestCollection.FromTestClass<BasicExampleScenario>(includedMethods);
        var step = tests.GetByName(nameof(BasicExampleScenario.BasicCheck));

        var fixture = new ScenarioFixture();
        Action testAction = () => fixture.PrepareTest(step);
        var assertionFailure = Assert.Throws<FailException>(testAction);
        Assert.Contains("Missing stages names: BasicSetup, BasicTeardown", assertionFailure.Message);
    }

    [Fact]
    public void FailingFlow_ShouldSkipStepsAfterFailedSetup()
    {
        var tests = DummyTestCollection.FromTestClass<BasicExampleScenario>();
        var setup = tests.GetByName(nameof(BasicExampleScenario.BasicSetup));
        var step = tests.GetByName(nameof(BasicExampleScenario.BasicCheck));
        var teardown = tests.GetByName(nameof(BasicExampleScenario.BasicTeardown));

        var fixture = new ScenarioFixture();
        PrepareAndCompleteFailingTest(fixture, setup);
        PrepareAndCatchSkippingTest(fixture, step);

        // the teardown doesn't depend on setup in this case, so it passes
        PrepareAndCompletePassingTest(fixture, teardown);
    }

    [Fact]
    public void FailingFlow_ShouldSkipDependentTeardownsAfterFailingAllSetups()
    {
        var tests = DummyTestCollection.FromTestClass<DependentTeardownScenario>();
        var setupA = tests.GetByName(nameof(DependentTeardownScenario.SetupA));
        var setupB = tests.GetByName(nameof(DependentTeardownScenario.SetupB));
        var step = tests.GetByName(nameof(DependentTeardownScenario.BasicCheck));
        var teardownNone = tests.GetByName(nameof(DependentTeardownScenario.IndependentTeardown));
        var teardownA = tests.GetByName(nameof(DependentTeardownScenario.ADependentTeardown));
        var teardownAB = tests.GetByName(nameof(DependentTeardownScenario.ABDependentTeardown));

        var fixture = new ScenarioFixture();
        PrepareAndCompleteFailingTest(fixture, setupA);
        PrepareAndCompleteFailingTest(fixture, setupB);

        PrepareAndCatchSkippingTest(fixture, step);

        PrepareAndCompletePassingTest(fixture, teardownNone);
        PrepareAndCatchSkippingTest(fixture, teardownA);
        PrepareAndCatchSkippingTest(fixture, teardownAB);
    }

    [Fact]
    public void FailingFlow_ShouldSkipDependentTeardownsAfterFailingSomeSetups()
    {
        var tests = DummyTestCollection.FromTestClass<DependentTeardownScenario>();
        var setupA = tests.GetByName(nameof(DependentTeardownScenario.SetupA));
        var setupB = tests.GetByName(nameof(DependentTeardownScenario.SetupB));
        var step = tests.GetByName(nameof(DependentTeardownScenario.BasicCheck));
        var teardownNone = tests.GetByName(nameof(DependentTeardownScenario.IndependentTeardown));
        var teardownA = tests.GetByName(nameof(DependentTeardownScenario.ADependentTeardown));
        var teardownAB = tests.GetByName(nameof(DependentTeardownScenario.ABDependentTeardown));

        var fixture = new ScenarioFixture();
        PrepareAndCompletePassingTest(fixture, setupA);
        PrepareAndCompleteFailingTest(fixture, setupB);

        PrepareAndCatchSkippingTest(fixture, step);

        PrepareAndCompletePassingTest(fixture, teardownNone);
        PrepareAndCompletePassingTest(fixture, teardownA);
        PrepareAndCatchSkippingTest(fixture, teardownAB);
    }

    // -----------
    // Value store
    // -----------

    [Fact]
    public void ValueStore_ShouldSetAndGetValueCorrectly()
    {
        var fixture = new ScenarioFixture();
        fixture.SetValue("TEST", "Lorem");
        
        var retrieved = fixture.GetValue<string>("TEST");
        Assert.Equal("Lorem", retrieved);
    }

    [Fact]
    public void ValueStore_ShouldSetAndGetNullCorrectly()
    {
        var fixture = new ScenarioFixture();
        fixture.SetValue("TEST", null);

        var retrieved = fixture.GetValue<string>("TEST");
        Assert.Null(retrieved);
    }

    [Fact]
    public void ValueStore_ShouldFailToGetMissingValue()
    {
        var fixture = new ScenarioFixture();
        Action retrieveAttempt = () => fixture.GetValue<string>("TEST");
        Assert.Throws<TrueException>(retrieveAttempt);
    }

    [Fact]
    public void ValueStore_ShouldFailToGetMistypedValue()
    {
        var fixture = new ScenarioFixture();
        fixture.SetValue("TEST", 123);

        Action retrieveAttempt = () => fixture.GetValue<string>("TEST");
        Assert.Throws<IsTypeException>(retrieveAttempt);
    }

    // -------
    // Helpers
    // -------

    private void PrepareAndCompletePassingTest(ScenarioFixture fixture, DummyTest test)
    {
        try
        {
            fixture.PrepareTest(test);
            fixture.CompleteTest(test, TestResult.Passed);
        }
        catch (SkipException)
        {
            Assert.Fail("Expected a test completing with the Passed result, but it got skipped.");
        }
    }

    private void PrepareAndCompleteFailingTest(ScenarioFixture fixture, DummyTest test)
    {
        try
        {
            fixture.PrepareTest(test);
            fixture.CompleteTest(test, TestResult.Failed);
        }
        catch (SkipException)
        {
            Assert.Fail("Expected a test completing with the Failed result, but it got skipped.");
        }
    }

    private void PrepareAndCatchSkippingTest(ScenarioFixture fixture, DummyTest test)
    {
        try
        {
            fixture.PrepareTest(test);
            Assert.Fail("Expected the scenario fixture to skip step without setup, but it didn't.");
        }
        catch (SkipException)
        {
            // using try-catch directly, because Assert.Throws<SkipException> won't catch it for some reason
        }
        fixture.CompleteTest(test, TestResult.Skipped);
    }
}
