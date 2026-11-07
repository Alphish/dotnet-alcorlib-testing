namespace Alphicsh.Alcorlib.Testing.Integration.Services.Scope;

public static class ScopeTestServices
{
    public interface IValueManager
    {
        string GetOrInit(string value);
    }

    public abstract class BaseValueManager : IValueManager
    {
        private string? Value { get; set; }

        public string GetOrInit(string value)
        {
            Value ??= value;
            return Value;
        }
    }

    // ---------
    // Singleton
    // ---------

    public interface ISingletonManager : IValueManager { }
    public class SingletonManager : BaseValueManager, ISingletonManager { }

    // ------
    // Scoped
    // ------

    public interface IScopedManager : IValueManager { }
    public class ScopedManager : BaseValueManager, IScopedManager { }

    // ---------
    // Transient
    // ---------

    public interface ITransientManager : IValueManager { }
    public class TransientManager : BaseValueManager, ITransientManager { }
}
