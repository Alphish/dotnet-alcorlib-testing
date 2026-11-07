using Alphicsh.Alcorlib.Testing.Thens;
using Alphicsh.Alcorlib.Testing.Whens;
using Xunit.Sdk;

namespace Alphicsh.Alcorlib.Testing.Tests.Whens;

public class WhenFunctionTests
{
    [Fact]
    public void ShouldHaveGivenName()
    {
        var whenFunction = new WhenFunction<string>("WhenLorem", () => "CORRECT");
        Assert.Equal("WhenLorem", whenFunction.Name);
    }

    // ----------------
    // Expected success
    // ----------------

    [Fact]
    public void ShouldHandleExecutionSuccess()
    {
        var thenSuccess = new ThenResult<bool>("ExecutionSuccess");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => "CORRECT").LinkSuccess(thenSuccess);

        whenFunction.IsExecuted();
        thenSuccess.ShouldBe(true);
    }

    [Fact]
    public void ShouldThrowOnExecutionFailure()
    {
        var whenFunction = new WhenFunction<string>("WhenLorem", () => { throw new InvalidOperationException(); });
        Action executionAction = () => whenFunction.IsExecuted();
        Assert.Throws<InvalidOperationException>(executionAction);
    }

    // ----------------
    // Expected failure
    // ----------------

    [Fact]
    public void ShouldRejectExpectedFailureSuccess()
    {
        var whenFunction = new WhenFunction<string>("WhenLorem", () => "CORRECT");
        Action executionAction = () => whenFunction.Fails();
        Assert.Throws<FailException>(executionAction);
    }

    [Fact]
    public void ShouldHandleExpectedFailure()
    {
        var thenSuccess = new ThenResult<bool>("ExecutionSuccess");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => { throw new InvalidOperationException(); }).LinkSuccess(thenSuccess);

        whenFunction.Fails();
        thenSuccess.ShouldBe(false);
    }

    // -------
    // Attempt
    // -------

    [Fact]
    public void ShouldHandleAttemptSuccess()
    {
        var thenSuccess = new ThenResult<bool>("ExecutionSuccess");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => "CORRECT").LinkSuccess(thenSuccess);

        whenFunction.IsAttempted();
        thenSuccess.ShouldBe(true);
    }

    [Fact]
    public void ShouldHandleAttemptFailure()
    {
        var thenSuccess = new ThenResult<bool>("ExecutionSuccess");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => { throw new InvalidOperationException(); }).LinkSuccess(thenSuccess);

        whenFunction.IsAttempted();
        thenSuccess.ShouldBe(false);
    }

    // ---------------
    // Linking success
    // ---------------

    // Untyped output

    [Fact]
    public void ShouldHandleLinkedOutputOnSuccess()
    {
        var thenResult = new ThenResult<string>("FunctionOutput");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => "CORRECT")
            .LinkOutput(thenResult);

        whenFunction.IsAttempted();
        thenResult.ShouldBe("CORRECT");
    }

    [Fact]
    public void ShouldIgnoreLinkedOutputOnFailure()
    {
        var thenResult = new ThenResult<string>("FunctionOutput");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => throw new InvalidOperationException())
            .LinkOutput(thenResult);

        whenFunction.IsAttempted();
        thenResult.ShouldBeUnset();
    }

    // Typed output

    [Fact]
    public void ShouldHandleLinkedTypedOutputOnSuccess()
    {
        var resultList = new List<int>();
        var thenList = new ThenResult<List<int>>("OutputList");
        var whenFunction = new WhenFunction<ICollection<int>>("WhenLorem", () => resultList)
            .LinkOutput(thenList);

        whenFunction.IsAttempted();
        thenList.ShouldBe(resultList);
    }

    [Fact]
    public void ShouldIgnoreLinkedTypedOutputOnFailure()
    {
        var thenList = new ThenResult<List<int>>("OutputList");
        var whenFunction = new WhenFunction<ICollection<int>>("WhenLorem", () => { throw new InvalidOperationException(); })
            .LinkOutput(thenList);

        whenFunction.IsAttempted();
        thenList.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedTypedOutputOnlyOfThrownType()
    {
        var resultList = new List<int>();
        var thenList = new ThenResult<List<int>>("OutputList");
        var thenSet = new ThenResult<HashSet<int>>("OutputSet");
        var whenFunction = new WhenFunction<ICollection<int>>("WhenLorem", () => resultList)
            .LinkOutput(thenList)
            .LinkOutput(thenSet);

        whenFunction.IsAttempted();
        thenList.ShouldBe(resultList);
        thenSet.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedTypedOutputOfDerivedType()
    {
        var resultList = new List<int>();
        var thenList = new ThenResult<IList<int>>("OutputList");
        var whenFunction = new WhenFunction<ICollection<int>>("WhenLorem", () => resultList)
            .LinkOutput(thenList);

        whenFunction.IsAttempted();
        thenList.ShouldBe(resultList);
    }

    // Untyped output selectors

    [Fact]
    public void ShouldHandleLinkedOutputSelectorOnSuccess()
    {
        var thenResultLength = new ThenResult<int>("ExecutionResultLength");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => "CORRECT")
            .LinkOutput(thenResultLength, result => result.Length);

        whenFunction.IsAttempted();
        thenResultLength.ShouldBe(7);
    }

    [Fact]
    public void ShouldIgnoreLinkedOutputSelectorOnFailure()
    {
        var thenResultLength = new ThenResult<int>("ExecutionResultLength");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => throw new InvalidOperationException())
            .LinkOutput(thenResultLength, result => result.Length);

        whenFunction.IsAttempted();
        thenResultLength.ShouldBeUnset();
    }

    // Typed output selector

    [Fact]
    public void ShouldHandleLinkedTypedOutputSelectorOnSuccess()
    {
        var thenListCount = new ThenResult<int>("OutputListCount");
        var whenFunction = new WhenFunction<ICollection<int>>("WhenLorem", () => new List<int> { 1, 2, 3 })
            .LinkOutput(thenListCount, (List<int> output) => output.Count);

        whenFunction.IsAttempted();
        thenListCount.ShouldBe(3);
    }

    [Fact]
    public void ShouldIgnoreLinkedTypedOutputSelectorOnFailure()
    {
        var thenList = new ThenResult<int>("OutputListCount");
        var whenFunction = new WhenFunction<ICollection<int>>("WhenLorem", () => { throw new InvalidOperationException(); })
            .LinkOutput(thenList, (List<int> output) => output.Count);

        whenFunction.IsAttempted();
        thenList.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedTypedOutputSelectorOnlyOfThrownType()
    {
        var thenListCount = new ThenResult<int>("OutputListCount");
        var thenSetCount = new ThenResult<int>("OutputSetCount");
        var whenFunction = new WhenFunction<ICollection<int>>("WhenLorem", () => new List<int> { 1, 2, 3 })
            .LinkOutput(thenListCount, (List<int> output) => output.Count)
            .LinkOutput(thenSetCount, (HashSet<int> output) => output.Count);

        whenFunction.IsAttempted();
        thenListCount.ShouldBe(3);
        thenSetCount.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedTypedOutputSelectorOfDerivedType()
    {
        var thenListCount = new ThenResult<int>("OutputListCount");
        var whenFunction = new WhenFunction<ICollection<int>>("WhenLorem", () => new List<int> { 1, 2, 3 })
            .LinkOutput(thenListCount, (IList<int> output) => output.Count);

        whenFunction.IsAttempted();
        thenListCount.ShouldBe(3);
    }

    // Miscellaneous

    [Fact]
    public void ShouldHandleStackedSuccessProcessingOnSuccess()
    {
        var thenResult = new ThenResult<string>("ExecutionResult");
        var thenResultLength = new ThenResult<int>("ExecutionResultLength");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => "CORRECT")
            .LinkOutput(thenResult)
            .LinkOutput(thenResultLength, result => result.Length);

        whenFunction.IsAttempted();
        thenResult.ShouldBe("CORRECT");
        thenResultLength.ShouldBe(7);
    }

    // -----------------
    // Linking exception
    // -----------------

    // Untyped exceptions

    [Fact]
    public void ShouldIgnoreLinkedExceptionOnSuccess()
    {
        var thenException = new ThenException("ThrownException");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => "CORRECT")
            .LinkException(thenException);

        whenFunction.IsAttempted();
        thenException.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedExceptionOnFailure()
    {
        var thenException = new ThenException("ThrownException");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => { throw new InvalidOperationException(); })
            .LinkException(thenException);

        whenFunction.IsAttempted();
        thenException.ShouldHaveExactType<InvalidOperationException>();
    }

    // Typed exceptions

    [Fact]
    public void ShouldIgnoreLinkedTypedExceptionOnSuccess()
    {
        var thenException = new ThenException<InvalidOperationException>("ThrownException");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => "CORRECT")
            .LinkException(thenException);

        whenFunction.IsAttempted();
        thenException.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedTypedExceptionOnFailure()
    {
        var thenException = new ThenException<InvalidOperationException>("ThrownException");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => { throw new InvalidOperationException(); })
            .LinkException(thenException);

        whenFunction.IsAttempted();
        thenException.ShouldHaveExactType<InvalidOperationException>();
    }

    [Fact]
    public void ShouldHandleLinkedTypedExceptionOnlyOfThrownType()
    {
        var thenInvalidOperationException = new ThenException<InvalidOperationException>("ThrownInvalidOperationException");
        var thenArgumentException = new ThenException<ArgumentException>("ThrownArgumentException");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => { throw new InvalidOperationException(); })
            .LinkException(thenInvalidOperationException)
            .LinkException(thenArgumentException);

        whenFunction.IsAttempted();
        thenInvalidOperationException.ShouldHaveExactType<InvalidOperationException>();
        thenArgumentException.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedTypedExceptionOfDerivedType()
    {
        var thenException = new ThenException<ArgumentException>("ThrownException");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => { throw new ArgumentNullException(); })
            .LinkException(thenException);

        whenFunction.IsAttempted();
        thenException.ShouldHaveExactType<ArgumentNullException>();
    }

    // Untyped exception selectors

    [Fact]
    public void ShouldIgnoreLinkedExceptionSelectorOnSuccess()
    {
        var thenExceptionMessage = new ThenResult<string>("ThrownExceptionMessage");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => "CORRECT")
            .LinkException(thenExceptionMessage, (exception) => exception.Message);

        whenFunction.IsAttempted();
        thenExceptionMessage.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedExceptionSelectorOnFailure()
    {
        var thenExceptionMessage = new ThenResult<string>("ThrownExceptionMessage");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => { throw new InvalidOperationException("You shouldn't do that."); })
            .LinkException(thenExceptionMessage, (exception) => exception.Message);

        whenFunction.IsAttempted();
        thenExceptionMessage.ShouldBe("You shouldn't do that.");
    }

    // Typed exception selectors

    [Fact]
    public void ShouldIgnoreLinkedTypedExceptionSelectorOnSuccess()
    {
        var thenInvalidParamName = new ThenResult<string?>("InvalidParamName");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => "CORRECT")
            .LinkException(thenInvalidParamName, (ArgumentException exception) => exception.ParamName);

        whenFunction.IsAttempted();
        thenInvalidParamName.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedTypedExceptionSelectorOnFailure()
    {
        var thenInvalidParamName = new ThenResult<string?>("InvalidParamName");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => { throw new ArgumentException("The value is bad.", "badParam"); })
            .LinkException(thenInvalidParamName, (ArgumentException exception) => exception.ParamName);

        whenFunction.IsAttempted();
        thenInvalidParamName.ShouldBe("badParam");
    }

    [Fact]
    public void ShouldHandleLinkedTypedExceptionSelectorOnlyOfThrownType()
    {
        var thenInvalidOperationMessage = new ThenResult<string>("ThrownExceptionMessage");
        var thenInvalidParamName = new ThenResult<string?>("InvalidParamName");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => { throw new InvalidOperationException("You shouldn't do that."); })
            .LinkException(thenInvalidOperationMessage, (InvalidOperationException exception) => exception.Message)
            .LinkException(thenInvalidParamName, (ArgumentException exception) => exception.ParamName);

        whenFunction.IsAttempted();
        thenInvalidOperationMessage.ShouldBe("You shouldn't do that.");
        thenInvalidParamName.ShouldBeUnset();
    }

    [Fact]
    public void ShouldHandleLinkedTypedExceptionSelectorOfDerivedType()
    {
        var thenInvalidParamName = new ThenResult<string?>("InvalidParamName");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => { throw new ArgumentNullException("badParam"); })
            .LinkException(thenInvalidParamName, (ArgumentException exception) => exception.ParamName);

        whenFunction.IsAttempted();
        thenInvalidParamName.ShouldBe("badParam");
    }

    // Miscellaneous

    [Fact]
    public void ShouldHandleStackedExceptionProcessingOnFailure()
    {
        var thenSuccess = new ThenResult<bool>("ExecutionSuccess");
        var thenException = new ThenException("ThrownException");
        var thenInvalidParamName = new ThenResult<string?>("InvalidParamName");
        var whenFunction = new WhenFunction<string>("WhenLorem", () => { throw new ArgumentNullException("badParam"); })
            .LinkSuccess(thenSuccess)
            .LinkException(thenException)
            .LinkException(thenInvalidParamName, (ArgumentException exception) => exception.ParamName);

        whenFunction.IsAttempted();
        thenSuccess.ShouldBe(false);
        thenException.ShouldHaveExactType<ArgumentNullException>();
        thenInvalidParamName.ShouldBe("badParam");
    }
}
