using Alphicsh.Alcorlib.Testing.Thens;

namespace Alphicsh.Alcorlib.Testing.Whens;

public class WhenAction : WhenCall<WhenAction>
{
    public Action Action { get; }
    private Action? SuccessHandler { get; set; }

    public WhenAction(string name, Action action) : base(name)
    {
        Action = action;
    }

    // ---------
    // Execution
    // ---------

    protected override void HandleCall()
    {
        Action();
        SuccessHandler?.Invoke();
    }

    protected override void ExecuteCall()
    {
        Action();
    }

    // -------
    // Outputs
    // -------

    public WhenAction LinkSuccess(ThenResult<bool> resultWrapper)
    {
        SuccessHandler += () => resultWrapper.Accept(true);
        ExceptionHandler += (exception) => resultWrapper.Accept(false);
        return this;
    }

    public WhenAction LinkSuccess<TResult>(ThenResult<TResult> resultWrapper, Func<TResult> resultGenerator)
    {
        SuccessHandler += () => resultWrapper.Accept(resultGenerator());
        return this;
    }
}
