namespace Alphicsh.Alcorlib.Testing.Integration.Services.Access;

public static class TestServices
{
    // Basic services

    public interface ILoremService
    {
        string GetLorem();
    }

    public class LoremService : ILoremService
    {
        public string GetLorem() => "Lorem";
    }

    public interface IIpsumService
    {
        string GetIpsum();
    }

    public class IpsumService : IIpsumService
    {
        public string GetIpsum() => "Ipsum";
    }

    // Number services

    public interface INumberService
    {
        int GetNumber();
    }

    public class AnswerToEverythingService : INumberService
    {
        public int GetNumber() => 42;
    }

    public class ZeroService : INumberService
    {
        public int GetNumber() => 0;
    }

    // Color services

    public interface IColorService
    {
        string GetColor();
    }

    public class RedService : IColorService
    {
        public string GetColor() => "Red";
    }

    public class YellowService : IColorService
    {
        public string GetColor() => "Yellow";
    }

    public class BlueService : IColorService
    {
        public string GetColor() => "Blue";
    }

    // Non-existent

    public interface INonExistentService
    {
        string GetExistence();
    }
}
