using Microsoft.Extensions.Logging;

namespace SushiShop.Services;

internal class LoggerService<T>: ILoggerService<T>
    {
        private readonly IFileService _fileService;
        private readonly string _nameSpace;
        private readonly string _currentTime = DateTime.Now.ToLongTimeString();
        
        public LoggerService(IFileService fileService)
        {
            _fileService = fileService;
            _nameSpace = typeof(T).FullName;
        }

        private string CastMessage(string message, LogLevel level, short traceId, Exception? exception)
        {
            return exception != null
                ? $"{_currentTime}|{traceId}|{_nameSpace}|{level.ToString()}|{message}{exception}"
                : $"{_currentTime}|{traceId}|{_nameSpace}|{level.ToString()}|{message}";
        }

        public void Log(LogLevel level, string message, short traceId = 0, Exception? exception = null)
        {
            var response = CastMessage(message, level, traceId, exception);
            switch (level)
            {
                case LogLevel.Information:
                    LogInformation(response);
                    break;
                case LogLevel.Debug:
                    LogDebug(response);
                    break;
                case LogLevel.Error:
                    LogError(response);
                    break;
            }
        }


        public void LogInformation(string message)
        {
            _fileService.WriteLog(message);
        }
        public void LogDebug(string message)
        {
            _fileService.WriteLog(message);
        }
        
        public void LogError(string message)
        {
            _fileService.WriteLog(message);
        }
    }