using PureFix.Types;


namespace PureFix.Examples.Shared;

public class ConsoleLogger : ILogger
{
    private readonly string _name;
    private readonly IFixClock _clock;

    public ConsoleLogger(string name, IFixClock clock)
    {
        _name = name;
        _clock = clock;
    }

    public void Info(string messageTemplate)
    {
        WriteLog("INFO", messageTemplate, ConsoleColor.White);
    }

    public void Warn(string messageTemplate)
    {
        WriteLog("WARN", messageTemplate, ConsoleColor.Yellow);
    }

    public void Debug(string messageTemplate)
    {
        WriteLog("DEBUG", messageTemplate, ConsoleColor.Gray);
    }

    public void Error(Exception ex)
    {
        WriteLog("ERROR", ex.ToString(), ConsoleColor.Red);
    }

    private void WriteLog(string level, string message, ConsoleColor color)
    {
        var timestamp = _clock.Current.ToString("HH:mm:ss.fff");
        var threadId = Environment.CurrentManagedThreadId;

        lock (Console.Out)
        {
            var original = Console.ForegroundColor;
            Console.ForegroundColor = color;
            Console.WriteLine($"[{timestamp}] [{_name}] [{level}] [{threadId}] {message}");
            Console.ForegroundColor = original;
        }
    }
}
