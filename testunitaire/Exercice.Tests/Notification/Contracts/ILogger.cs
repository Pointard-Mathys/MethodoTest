namespace Notification.Contracts;

public interface ILogger
{
    /// <summary>
    /// Logs an informational message.
    /// </summary>
    /// <param name="message">The message to be logged as information.</param>
    void LogInfo(string message);

    /// <summary>
    /// Logs an error message.
    /// </summary>
    /// <param name="message">The message to be logged as an error.</param>
    void LogError(string message);

    /// <summary>
    /// Logs a warning message.
    /// </summary>
    /// <param name="message">The message to be logged as a warning.</param>
    void LogWarning(string message);
}