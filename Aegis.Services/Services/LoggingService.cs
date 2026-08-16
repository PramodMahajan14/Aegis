using Serilog.Context;

namespace Aegis.Services.Services
{
    public class LoggingService : ILoggingService
    {
        private readonly ILogger<LoggingService> _logger;
        public LoggingService(ILogger<LoggingService> logger)
        {
            _logger = logger;
        }

        public void LogInfo(string? message, params object[]? args)
        {
            using (LogContext.PushProperty("LogLevel", "Infromation")) ;
            if (args != null)
            {
                _logger.LogInformation(message, args);
            }
            else
            {
                _logger.LogInformation(message);
            }
        }

        public void LogError(Exception? ex, string? message, params object[]? args)
        {
            using (LogContext.PushProperty("LogLevel", "Error")) ;
            if (args != null)
            {
                _logger.LogError(ex, message, args);
            }
            else
            {
                _logger.LogError(ex, message);
            }
        }

        public void LogError(string? message,params object[]? args)
        {
            using (LogContext.PushProperty("LogLevel","Error"));
            if(args != null)
            {
                _logger.LogError(message,args);
            }
            else
            {
                _logger.LogError(message);
            }
            
        }

        public void LogDebug(string? message, params object[]? args)
        {
            using (LogContext.PushProperty("LogLevel", "Information"))
                if (args != null)
                {
                    _logger.LogDebug(message, args);
                }
                else
                {
                    _logger.LogDebug(message);
                }
        }

        public void LogWarning(string? message, params object[]? args)
        {
            using (LogContext.PushProperty("LogLevel", "Warning"))
                if (args != null)
                {
                    _logger.LogWarning(message, args);
                }
                else
                {
                    _logger.LogWarning(message);
                }
        }

        public void LogCritical(string? message, params object[]? args)
        {
            using (LogContext.PushProperty("LogLevel", "Critical"))
                if (args != null)
                {
                    _logger.LogCritical(message, args);
                }
                else
                {
                    _logger.LogCritical(message);
                }
        }

        public IDisposable? BeginScope(Dictionary<string, object> dict)
        {
            return _logger.BeginScope(dict);
        }


    }
}