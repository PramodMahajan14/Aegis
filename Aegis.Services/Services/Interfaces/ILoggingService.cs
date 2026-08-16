namespace Aegis.Services.Services
{
    public interface ILoggingService
    {
       void LogInfo(string? message, params object[]? args);
        void LogDebug(string? message, params object[]? args);
        void LogWarning(string? message, params object[]? args);
        void LogError(Exception? ex, string? message, params object[]? args);
        void LogError(string? message, params object[]? args);
        void LogCritical(string? message, params object[]? args);
        public IDisposable? BeginScope(Dictionary<string, object> dict);
    }
}