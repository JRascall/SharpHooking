namespace Hooks
{
    public interface IHookingLogger
    {
        void LogDebug(string text);
        void LogInformation(string text);
        void LogWarning(string text);
        void LogError(string text);
    }
}