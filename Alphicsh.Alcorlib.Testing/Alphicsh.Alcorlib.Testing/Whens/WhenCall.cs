using Alphicsh.Alcorlib.Testing.Thens;
using Xunit;

namespace Alphicsh.Alcorlib.Testing.Whens;

public abstract class WhenCall<TCall> where TCall : WhenCall<TCall>
{
    public string Name { get; }
    protected Action<Exception>? ExceptionHandler { get; set; }

    protected WhenCall(string name)
    {
        Name = name;
    }

    // ---------
    // Execution
    // ---------

    public void IsExecuted()
    {
        HandleCall();
    }

    public void Fails()
    {
        try
        {
            ExecuteCall();
        }
        catch (Exception ex)
        {
            ExceptionHandler?.Invoke(ex);
            return;
        }

        // if it didn't return from the catch block, it means the execution failed to fail
        Assert.Fail($"Expected {Name} to fail but it executed without issues.");
    }

    public void IsAttempted()
    {
        try
        {
            HandleCall();
        }
        catch (Exception ex)
        {
            ExceptionHandler?.Invoke(ex);
        }
    }

    protected abstract void HandleCall();
    protected abstract void ExecuteCall();

    // ----------
    // Exceptions
    // ----------

    public TCall LinkException(ThenResult<Exception> exceptionWrapper)
    {
        ExceptionHandler += exceptionWrapper.Accept;
        return (this as TCall)!;
    }

    public TCall LinkException<TException>(ThenResult<TException> typedExceptionWrapper)
        where TException : Exception
    {
        ExceptionHandler += (exception) =>
        {
            if (exception is TException typedException)
                typedExceptionWrapper.Accept(typedException);
        };
        return (this as TCall)!;
    }

    public TCall LinkException<TResult>(ThenResult<TResult> resultWrapper, Func<Exception, TResult> resultSelector)
    {
        ExceptionHandler += (exception) => resultWrapper.Accept(resultSelector(exception));
        return (this as TCall)!;
    }

    public TCall LinkException<TException, TResult>(ThenResult<TResult> resultWrapper, Func<TException, TResult> resultSelector)
        where TException : Exception
    {
        ExceptionHandler += (exception) =>
        {
            if (exception is TException typedException)
                resultWrapper.Accept(resultSelector(typedException));
        };
        return (this as TCall)!;
    }
}
