using Alphicsh.Alcorlib.Testing.Thens;

namespace Alphicsh.Alcorlib.Testing.Whens;

public class WhenFunction<TOutput> : WhenCall<WhenFunction<TOutput>>
{
    public Func<TOutput> Function { get; }
    private Action<TOutput>? OutputHandler { get; set; }

    public WhenFunction(string name, Func<TOutput> function) : base(name)
    {
        Function = function;
    }

    // ---------
    // Execution
    // ---------

    protected override void HandleCall()
    {
        var output = Function();
        OutputHandler?.Invoke(output);
    }

    protected override void ExecuteCall()
    {
        Function();
    }

    // -------
    // Outputs
    // -------

    public WhenFunction<TOutput> LinkSuccess(ThenResult<bool> resultWrapper)
    {
        OutputHandler += (output) => resultWrapper.Accept(true);
        ExceptionHandler += (exception) => resultWrapper.Accept(false);
        return this;
    }

    public WhenFunction<TOutput> LinkOutput(ThenResult<TOutput> resultWrapper)
    {
        OutputHandler += resultWrapper.Accept;
        return this;
    }

    public WhenFunction<TOutput> LinkOutput<TDerived>(ThenResult<TDerived> typedResultWrapper)
        where TDerived : TOutput
    {
        OutputHandler += (output) =>
        {
            if (output is TDerived typedOutput)
                typedResultWrapper.Accept(typedOutput);
        };
        return this;
    }

    public WhenFunction<TOutput> LinkOutput<TResult>(ThenResult<TResult> resultWrapper, Func<TOutput, TResult> resultSelector)
    {
        OutputHandler += (output) => resultWrapper.Accept(resultSelector(output));
        return this;
    }

    public WhenFunction<TOutput> LinkOutput<TDerived, TResult>(ThenResult<TResult> resultWrapper, Func<TDerived, TResult> resultSelector)
        where TDerived : TOutput
    {
        OutputHandler += (output) =>
        {
            if (output is TDerived typedOutput)
                resultWrapper.Accept(resultSelector(typedOutput));
        };
        return this;
    }
}
