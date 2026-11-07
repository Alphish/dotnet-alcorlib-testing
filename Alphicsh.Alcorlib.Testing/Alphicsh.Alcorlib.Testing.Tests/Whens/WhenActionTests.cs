using Alphicsh.Alcorlib.Testing.Thens;
using Alphicsh.Alcorlib.Testing.Whens;
using Xunit.Sdk;

namespace Alphicsh.Alcorlib.Testing.Tests.Whens;

public class WhenActionTests
{
    [Fact]
    public void ShouldHaveGivenName()
    {
        var whenAction = new WhenAction("WhenLorem", () => { });
        Assert.Equal("WhenLorem", whenAction.Name);
    }

    // ----------------
    // Expected success
    // ----------------

    [Fact]
    public void ShouldHandleExecutionSuccess()
    {
        var thenSuccess = new ThenResult<bool>("ExecutionSuccess");
        var whenAction = new WhenAction("WhenLorem", () => { }).LinkSuccess(thenSuccess);

        whenAction.IsExecuted();
        thenSuccess.ShouldBe(true);
    }

    [Fact]
    public void ShouldThrowOnExecutionFailure()
    {
        var whenAction = new WhenAction("WhenLorem", () => { throw new InvalidOperationException(); });
        Action executionAction = () => whenAction.IsExecuted();
        Assert.Throws<InvalidOperationException>(executionAction);
    }

    // ----------------
    // Expected failure
    // ----------------

    [Fact]
    public void ShouldRejectExpectedFailureSuccess()
    {
        var whenAction = new WhenAction("WhenLorem", () => { });
        Action executionAction = () => whenAction.Fails();
        Assert.Throws<FailException>(executionAction);
    }

    [Fact]
    public void ShouldHandleExpectedFailure()
    {
        var thenSuccess = new ThenResult<bool>("ExecutionSuccess");
        var whenAction = new WhenAction("WhenLorem", () => { throw new InvalidOperationException(); }).LinkSuccess(thenSuccess);

        whenAction.Fails();
        thenSuccess.ShouldBe(false);
    }

    // -------
    // Attempt
    // -------

    [Fact]
    public void ShouldHandleAttemptSuccess()
    {
        var thenSuccess = new ThenResult<bool>("ExecutionSuccess");
        var whenAction = new WhenAction("WhenLorem", () => { }).LinkSuccess(thenSuccess);

        whenAction.IsAttempted();
        thenSuccess.ShouldBe(true);
    }

    [Fact]
    public void ShouldHandleAttemptFailure()
    {
        var thenSuccess = new ThenResult<bool>("ExecutionSuccess");
        var whenAction = new WhenAction("WhenLorem", () => { throw new InvalidOperationException(); }).LinkSuccess(thenSuccess);

        whenAction.IsAttempted();
        thenSuccess.ShouldBe(false);
    }

    // ---------------
    // Linking success
    // ---------------

    [Fact]
    public void ShouldProcessLinkedSuccessSelectorOnSuccess()
    {
        var expectedResult = "WRONG";
        var thenResult = new ThenString("ExecutionResult");
        var whenAction = new WhenAction("WhenLorem", () => { expectedResult = "CORRECT"; })
            .LinkSuccess(thenResult, () => expectedResult);

        whenAction.IsAttempted();
        thenResult.ShouldBe("CORRECT");
    }

    [Fact]
    public void ShouldIgnoreLinkedSuccessSelectorOnFailure()
    {
        var expectedResult = "WRONG";
        var thenResult = new ThenString("ExecutionResult");
        var whenAction = new WhenAction("WhenLorem", () => { throw new InvalidOperationException(); })
            .LinkSuccess(thenResult, () => expectedResult);

        whenAction.IsAttempted();
        thenResult.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleStackedSuccessProcessingOnSuccess()
    {
        var expectedResult = "WRONG";
        var thenSuccess = new ThenResult<bool>("ExecutionSuccess");
        var thenResult = new ThenString("ExecutionResult");
        var whenAction = new WhenAction("WhenLorem", () => { expectedResult = "CORRECT"; })
            .LinkSuccess(thenSuccess)
            .LinkSuccess(thenResult, () => expectedResult);

        whenAction.IsAttempted();
        thenSuccess.ShouldBe(true);
        thenResult.ShouldBe("CORRECT");
    }

    // -----------------
    // Linking exception
    // -----------------

    // Untyped exceptions

    [Fact]
    public void ShouldIgnoreLinkedExceptionOnSuccess()
    {
        var thenException = new ThenException("ThrownException");
        var whenAction = new WhenAction("WhenLorem", () => { })
            .LinkException(thenException);

        whenAction.IsAttempted();
        thenException.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedExceptionOnFailure()
    {
        var thenException = new ThenException("ThrownException");
        var whenAction = new WhenAction("WhenLorem", () => { throw new InvalidOperationException(); })
            .LinkException(thenException);

        whenAction.IsAttempted();
        thenException.ShouldHaveExactType<InvalidOperationException>();
    }

    // Typed exceptions

    [Fact]
    public void ShouldIgnoreLinkedTypedExceptionOnSuccess()
    {
        var thenException = new ThenException<InvalidOperationException>("ThrownException");
        var whenAction = new WhenAction("WhenLorem", () => { })
            .LinkException(thenException);

        whenAction.IsAttempted();
        thenException.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedTypedExceptionOnFailure()
    {
        var thenException = new ThenException<InvalidOperationException>("ThrownException");
        var whenAction = new WhenAction("WhenLorem", () => { throw new InvalidOperationException(); })
            .LinkException(thenException);

        whenAction.IsAttempted();
        thenException.ShouldHaveExactType<InvalidOperationException>();
    }

    [Fact]
    public void ShouldHandleLinkedTypedExceptionOnlyOfThrownType()
    {
        var thenInvalidOperationException = new ThenException<InvalidOperationException>("ThrownInvalidOperationException");
        var thenArgumentException = new ThenException<ArgumentException>("ThrownArgumentException");
        var whenAction = new WhenAction("WhenLorem", () => { throw new InvalidOperationException(); })
            .LinkException(thenInvalidOperationException)
            .LinkException(thenArgumentException);

        whenAction.IsAttempted();
        thenInvalidOperationException.ShouldHaveExactType<InvalidOperationException>();
        thenArgumentException.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedTypedExceptionOfDerivedType()
    {
        var thenException = new ThenException<ArgumentException>("ThrownException");
        var whenAction = new WhenAction("WhenLorem", () => { throw new ArgumentNullException(); })
            .LinkException(thenException);

        whenAction.IsAttempted();
        thenException.ShouldHaveExactType<ArgumentNullException>();
    }

    // Untyped exception selectors

    [Fact]
    public void ShouldIgnoreLinkedExceptionSelectorOnSuccess()
    {
        var thenExceptionMessage = new ThenString("ThrownExceptionMessage");
        var whenAction = new WhenAction("WhenLorem", () => { })
            .LinkException(thenExceptionMessage, (exception) => exception.Message);

        whenAction.IsAttempted();
        thenExceptionMessage.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedExceptionSelectorOnFailure()
    {
        var thenExceptionMessage = new ThenString("ThrownExceptionMessage");
        var whenAction = new WhenAction("WhenLorem", () => { throw new InvalidOperationException("You shouldn't do that."); })
            .LinkException(thenExceptionMessage, (exception) => exception.Message);

        whenAction.IsAttempted();
        thenExceptionMessage.ShouldBe("You shouldn't do that.");
    }

    // Typed exception selectors

    [Fact]
    public void ShouldIgnoreLinkedTypedExceptionSelectorOnSuccess()
    {
        var thenInvalidParamName = new ThenString("InvalidParamName");
        var whenAction = new WhenAction("WhenLorem", () => { })
            .LinkException(thenInvalidParamName, (ArgumentException exception) => exception.ParamName);

        whenAction.IsAttempted();
        thenInvalidParamName.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedTypedExceptionSelectorOnFailure()
    {
        var thenInvalidParamName = new ThenString("InvalidParamName");
        var whenAction = new WhenAction("WhenLorem", () => { throw new ArgumentException("The value is bad.", "badParam"); })
            .LinkException(thenInvalidParamName, (ArgumentException exception) => exception.ParamName);

        whenAction.IsAttempted();
        thenInvalidParamName.ShouldBe("badParam");
    }

    [Fact]
    public void ShouldHandleLinkedTypedExceptionSelectorOnlyOfThrownType()
    {
        var thenInvalidOperationMessage = new ThenString("ThrownExceptionMessage");
        var thenInvalidParamName = new ThenString("InvalidParamName");
        var whenAction = new WhenAction("WhenLorem", () => { throw new InvalidOperationException("You shouldn't do that."); })
            .LinkException(thenInvalidOperationMessage, (InvalidOperationException exception) => exception.Message)
            .LinkException(thenInvalidParamName, (ArgumentException exception) => exception.ParamName);

        whenAction.IsAttempted();
        thenInvalidOperationMessage.ShouldBe("You shouldn't do that.");
        thenInvalidParamName.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedTypedExceptionSelectorOfDerivedType()
    {
        var thenInvalidParamName = new ThenString("InvalidParamName");
        var whenAction = new WhenAction("WhenLorem", () => { throw new ArgumentNullException("badParam"); })
            .LinkException(thenInvalidParamName, (ArgumentException exception) => exception.ParamName);

        whenAction.IsAttempted();
        thenInvalidParamName.ShouldBe("badParam");
    }

    // Miscellaneous

    [Fact]
    public void ShouldHandleStackedExceptionProcessingOnFailure()
    {
        var thenSuccess = new ThenResult<bool>("ExecutionSuccess");
        var thenException = new ThenException("ThrownException");
        var thenInvalidParamName = new ThenString("InvalidParamName");
        var whenAction = new WhenAction("WhenLorem", () => { throw new ArgumentNullException("badParam"); })
            .LinkSuccess(thenSuccess)
            .LinkException(thenException)
            .LinkException(thenInvalidParamName, (ArgumentException exception) => exception.ParamName);

        whenAction.IsAttempted();
        thenSuccess.ShouldBe(false);
        thenException.ShouldHaveExactType<ArgumentNullException>();
        thenInvalidParamName.ShouldBe("badParam");
    }
}
