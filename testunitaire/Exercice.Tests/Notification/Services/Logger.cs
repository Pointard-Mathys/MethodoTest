using Notification.Contracts;
namespace Notification.Services;

public class Logger : ILogger
{
    public List<string> Logs { get; } = new();
    private readonly string _logFilePath;

    public Logger(string logFilePath = "log.txt")
    {
        _logFilePath = logFilePath;
    }

    public void LogInfo(string message)
    {
        Log("[INFO]", message);
    }

    public void LogError(string message)
    {
        Log("[ERROR]", message);
    }

    public void LogWarning(string message)
    {
        Log("[WARN]", message);
    }

    private void Log(string level, string message)
    {
        var logEntry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} {level} {message}";
        Logs.Add(logEntry);
        Console.WriteLine(logEntry);
        File.AppendAllText(_logFilePath, logEntry + Environment.NewLine);
    }
}