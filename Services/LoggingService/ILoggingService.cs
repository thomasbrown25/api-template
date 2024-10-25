

namespace personal_trainer_api.Services.LoggingService
{
    public interface ILoggingService
    {
        void LogTrace(string message);
        void LogException(Exception? exception);
        void LogDataExchange(string messageSource, string messageTarget, string methodCall, string messagePayload);
    }
}