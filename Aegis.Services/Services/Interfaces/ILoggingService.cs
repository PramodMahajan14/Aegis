namespace Aegis.Services.Services
{
    public interface ILoggingService
    {
        void LogInfo(string? message, params object[]? args);
        void LogError(Exception? ex,string? name, params object[]? args);
    }
}