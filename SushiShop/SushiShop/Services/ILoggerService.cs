using Microsoft.Extensions.Logging;

namespace SushiShop.Services
{
    internal interface ILoggerService<T>
    {
        void Log(LogLevel level, string message, short traceId = 0, Exception? exception = null);
        void LogInformation(string message);
        void LogDebug(string message);
        void LogError(string message);
    }
}