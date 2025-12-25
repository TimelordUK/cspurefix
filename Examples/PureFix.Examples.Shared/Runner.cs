using Arrow.Threading.Tasks;
using PureFix.Transport.Session;
using PureFix.Types;


namespace PureFix.Examples.Shared;

public static class Runner
{
    /// <summary>
    /// Runs both acceptor and initiator sessions (for local testing).
    /// Logging and store are configured via JSON config.
    /// </summary>
    public static async Task Run(string acceptorConfigPath, string initiatorConfigPath, string dictRootPath, Func<IFixConfig, BaseAppDI> makeHost)
    {
        var acceptor = Start(acceptorConfigPath, dictRootPath, makeHost);
        await Task.Delay(500); // Let acceptor start listening
        var initiator = Start(initiatorConfigPath, dictRootPath, makeHost);

        var tasks = new Task[] { acceptor, initiator };
        Task.WaitAll(tasks);
    }

    /// <summary>
    /// Runs both acceptor and initiator sessions with legacy parameters.
    /// </summary>
    [Obsolete("Use overload without clock parameter - clock is now created per-host")]
    public static async Task Run(string acceptorConfigPath, string initiatorConfigPath, string dictRootPath, IFixClock clock, Func<IFixClock, IFixConfig, BaseAppDI> makeHost, string? storeDirectory = null, byte? logDelimiter = null)
    {
        var acceptor = Start(acceptorConfigPath, dictRootPath, clock, makeHost, storeDirectory, logDelimiter);
        await Task.Delay(500);
        var initiator = Start(initiatorConfigPath, dictRootPath, clock, makeHost, storeDirectory, logDelimiter);
        Task.WaitAll(acceptor, initiator);
    }

    /// <summary>
    /// Runs a single session (client-only mode for connecting to external servers).
    /// Logging and store are configured via JSON config.
    /// </summary>
    public static async Task RunSingle(string configPath, string dictRootPath, Func<IFixConfig, BaseAppDI> makeHost)
    {
        var task = Start(configPath, dictRootPath, makeHost);
        await task;
    }

    /// <summary>
    /// Runs a single session with legacy parameters.
    /// </summary>
    [Obsolete("Use overload without clock parameter - clock is now created per-host")]
    public static async Task RunSingle(string configPath, string dictRootPath, IFixClock clock, Func<IFixClock, IFixConfig, BaseAppDI> makeHost, string? storeDirectory = null, byte? logDelimiter = null)
    {
        var task = Start(configPath, dictRootPath, clock, makeHost, storeDirectory, logDelimiter);
        await task;
    }

    private static Task Start(string configPath, string dictRootPath, Func<IFixConfig, BaseAppDI> makeHost)
    {
        if (string.IsNullOrEmpty(configPath)) return Task.CompletedTask;

        var config = FixApp.MakeConfig(configPath, dictRootPath);
        var host = makeHost(config);
        var t1 = FixApp.Run(host);
        return t1;
    }

    private static Task Start(string configPath, string dictRootPath, IFixClock clock, Func<IFixClock, IFixConfig, BaseAppDI> makeHost, string? storeDirectory, byte? logDelimiter)
    {
        if (string.IsNullOrEmpty(configPath)) return Task.CompletedTask;

#pragma warning disable CS0618 // Using obsolete method for backward compatibility
        var config = FixApp.MakeConfig(configPath, dictRootPath, storeDirectory, logDelimiter);
#pragma warning restore CS0618
        var host = makeHost(clock, config);
        return FixApp.Run(host);
    }
}
