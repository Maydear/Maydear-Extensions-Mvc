using System;

namespace Microsoft.Extensions.Logging
{
    /// <summary>
    /// 
    /// </summary>
    internal static class LoggingExtensions
    {
        private static readonly Action<ILogger, string, Exception> _MaydearValidationFailed;
        private static readonly Action<ILogger, Exception> _MaydearValidationSucceeded;
        private static readonly Action<ILogger, Exception> _errorProcessingMessage;

        static LoggingExtensions()
        {
            _MaydearValidationFailed = LoggerMessage.Define<string>(
                eventId: 1,
                logLevel: LogLevel.Information,
                formatString: "Failed to validate the token {Token}.");
            _MaydearValidationSucceeded = LoggerMessage.Define(
                eventId: 2,
                logLevel: LogLevel.Information,
                formatString: "Successfully validated the token.");
            _errorProcessingMessage = LoggerMessage.Define(
                eventId: 3,
                logLevel: LogLevel.Error,
                formatString: "Exception occurred while processing message.");
        }

        public static void MaydearValidationFailed(this ILogger logger, string token, Exception ex)
        {
            _MaydearValidationFailed(logger, token, ex);
        }

        public static void MaydearValidationSucceeded(this ILogger logger)
        {
            _MaydearValidationSucceeded(logger, null);
        }

        public static void ErrorProcessingMessage(this ILogger logger, Exception ex)
        {
            _errorProcessingMessage(logger, ex);
        }
    }
}
