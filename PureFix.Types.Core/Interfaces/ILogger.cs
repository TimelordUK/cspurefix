using System;

namespace PureFix.Types
{
    public enum LogLevel
    {
        Debug,
        Info,
        Warn,
        Error
    }

    public interface ILogger
    {
        bool IsEnabled(LogLevel level);

        void Debug(string message);
        void Debug<T>(string template, T arg);
        void Debug<T1, T2>(string template, T1 arg1, T2 arg2);
        void Debug<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3);
        /// <summary>
        /// Debug log with variadic args (less efficient - allocates array).
        /// Use sparingly for complex diagnostic logging.
        /// </summary>
        void DebugV(string template, params object?[] args);

        void Info(string message);
        void Info<T>(string template, T arg);
        void Info<T1, T2>(string template, T1 arg1, T2 arg2);
        void Info<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3);
        /// <summary>
        /// Info log with variadic args (less efficient - allocates array).
        /// Use sparingly for complex diagnostic logging.
        /// </summary>
        void InfoV(string template, params object?[] args);

        void Warn(string message);
        void Warn<T>(string template, T arg);
        void Warn<T1, T2>(string template, T1 arg1, T2 arg2);
        void Warn<T1, T2, T3>(string template, T1 arg1, T2 arg2, T3 arg3);
        /// <summary>
        /// Warn log with variadic args (less efficient - allocates array).
        /// Use sparingly for complex diagnostic logging.
        /// </summary>
        void WarnV(string template, params object?[] args);

        void Error(Exception ex, string? message = null);
        void Error<T>(Exception ex, string template, T arg);
        void Error<T1, T2>(Exception ex, string template, T1 arg1, T2 arg2);
    }
}
