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

        public void LogInfo(string? message,params object[]? args)
        {
            using(LogContext.PushProperty("LogLevel","Infromation"));
            if(args != null)
            {
                _logger.LogInformation(message,args);
            }
            else
            {
                _logger.LogInformation(message);
            }
        }

        public void LogError(Exception? ex,string? message,params object[]? args)
        {
            using(LogContext.PushProperty("LogLevel","Error"));
            if(args != null)
            {
                _logger.LogError(ex,message,args);
            }
            else
            {
                _logger.LogError(ex,message);
            }
        }
    }
}