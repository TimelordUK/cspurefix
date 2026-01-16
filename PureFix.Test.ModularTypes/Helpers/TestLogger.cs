using System.Text.RegularExpressions;
using PureFix.Types;


namespace PureFix.Test.ModularTypes.Helpers
{
    internal partial class TestLogger : ILogger
    {
        public enum LogFormatTypes
        {
            App,
            Plain
        }

        private LogLevel MinLevel { get; set; } = LogLevel.Debug;
        public LogTrace Log { get; set; }
        public IReadOnlyList<string> Entries()
        {
            return Log.GetEntries();
        }
        private string Me { get; }
        private IFixClock Clock { get; }
        private LogFormatTypes LogFormat { get; set; }
        public TestLogger(string name, LogTrace file, LogFormatTypes format, IFixClock clock)
        {
            LogFormat = format;
            Me = name;
            Clock = clock;
            Log = file;
        }

        public bool IsEnabled(LogLevel level) => level >= MinLevel;

        public void Debug(string message) => AddEntry("D", LogLevel.Debug, message);
        public void Debug<T>(string template, T arg) => AddEntry("D", LogLevel.Debug, template, arg);
        public void Debug<T1, T2>(string template, T1 arg1, T2 arg2) => AddEntry("D", LogLevel.Debug, template, arg1, arg2);
        public void Debug<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3) => AddEntry("D", LogLevel.Debug, template, arg1, arg2, arg3);
        public void DebugV(string template, params object?[] args) => AddEntryV("D", LogLevel.Debug, template, args);

        public void Info(string message) => AddEntry("I", LogLevel.Info, message);
        public void Info<T>(string template, T arg) => AddEntry("I", LogLevel.Info, template, arg);
        public void Info<T1, T2>(string template, T1 arg1, T2 arg2) => AddEntry("I", LogLevel.Info, template, arg1, arg2);
        public void Info<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3) => AddEntry("I", LogLevel.Info, template, arg1, arg2, arg3);
        public void InfoV(string template, params object?[] args) => AddEntryV("I", LogLevel.Info, template, args);

        public void Warn(string message) => AddEntry("W", LogLevel.Warn, message);
        public void Warn<T>(string template, T arg) => AddEntry("W", LogLevel.Warn, template, arg);
        public void Warn<T1, T2>(string template, T1 arg1, T2 arg2) => AddEntry("W", LogLevel.Warn, template, arg1, arg2);
        public void Warn<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3) => AddEntry("W", LogLevel.Warn, template, arg1, arg2, arg3);
        public void WarnV(string template, params object?[] args) => AddEntryV("W", LogLevel.Warn, template, args);

        public void Error(Exception ex, string? message = null) => AddEntry("E", LogLevel.Error, message ?? ex.Message);
        public void Error<T>(Exception ex, string template, T arg) => AddEntry("E", LogLevel.Error, template, arg);
        public void Error<T1, T2>(Exception ex, string template, T1 arg1, T2 arg2) => AddEntry("E", LogLevel.Error, template, arg1, arg2);

        private void AddEntry(string levelCode, LogLevel level, string msg)
        {
            if (!IsEnabled(level)) return;
            AddFormattedEntry(levelCode, msg);
        }

        private void AddEntry<T>(string levelCode, LogLevel level, string template, T arg)
        {
            if (!IsEnabled(level)) return;
            AddFormattedEntry(levelCode, FormatTemplate(template, arg));
        }

        private void AddEntry<T1, T2>(string levelCode, LogLevel level, string template, T1 arg1, T2 arg2)
        {
            if (!IsEnabled(level)) return;
            AddFormattedEntry(levelCode, FormatTemplate(template, arg1, arg2));
        }

        private void AddEntry<T1, T2, T3>(string levelCode, LogLevel level, string template, T1 arg1, T2 arg2, T3 arg3)
        {
            if (!IsEnabled(level)) return;
            AddFormattedEntry(levelCode, FormatTemplate(template, arg1, arg2, arg3));
        }

        private void AddEntryV(string levelCode, LogLevel level, string template, object?[] args)
        {
            if (!IsEnabled(level)) return;
            AddFormattedEntry(levelCode, FormatTemplateV(template, args));
        }

        private void AddFormattedEntry(string levelCode, string msg)
        {
            switch (LogFormat)
            {
                case LogFormatTypes.App:
                    Log.Add($"{levelCode}:[{Me,-25}] [{Clock.Current.ToLongTimeString()}] [{Environment.CurrentManagedThreadId}] {msg}");
                    break;

                default:
                    Log.Add(msg);
                    break;
            }
        }

        private static string FormatTemplate<T>(string template, T arg)
        {
            return TemplatePlaceholder().Replace(template, arg?.ToString() ?? "null", 1);
        }

        private static string FormatTemplate<T1, T2>(string template, T1 arg1, T2 arg2)
        {
            var result = TemplatePlaceholder().Replace(template, arg1?.ToString() ?? "null", 1);
            return TemplatePlaceholder().Replace(result, arg2?.ToString() ?? "null", 1);
        }

        private static string FormatTemplate<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3)
        {
            var result = TemplatePlaceholder().Replace(template, arg1?.ToString() ?? "null", 1);
            result = TemplatePlaceholder().Replace(result, arg2?.ToString() ?? "null", 1);
            return TemplatePlaceholder().Replace(result, arg3?.ToString() ?? "null", 1);
        }

        private static string FormatTemplateV(string template, object?[] args)
        {
            var result = template;
            foreach (var arg in args)
            {
                result = TemplatePlaceholder().Replace(result, arg?.ToString() ?? "null", 1);
            }
            return result;
        }

        [GeneratedRegex(@"\{[^}]+\}")]
        private static partial Regex TemplatePlaceholder();

        public override string ToString()
        {
            return $"{Me} [{Log.Count}] {Log.LastOrDefault()}";
        }
    }
}
