using Arrow.Threading.Tasks;
using PureFix.Transport.Session;
using PureFix.Types;


namespace PureFix.Examples.Shared;

public static class Runner
{
    /// <summary>
    /// Runs both acceptor and initiator sessions (for local testing)
    /// </summary>
    public static async Task Run(string acceptorConfigPath, string initiatorConfigPath, string dictRootPath, IFixClock clock, Func<IFixClock, IFixConfig, BaseAppDI> makeHost, string? storeDirectory = null, byte? logDelimiter = null)
    {
        var acceptor = Start(acceptorConfigPath, dictRootPath, clock, makeHost, storeDirectory, logDelimiter);
        await Task.Delay(500); // Let acceptor start listening
        var initiator = Start(initiatorConfigPath, dictRootPath, clock, makeHost, storeDirectory, logDelimiter);

        var tasks = new Task[] { acceptor, initiator };
        Task.WaitAll(tasks);
    }

    /// <summary>
    /// Runs a single session (client-only mode for connecting to external servers)
    /// </summary>
    /// <param name="configPath">Path to session config JSON</param>
    /// <param name="dictRootPath">Root path for dictionaries</param>
    /// <param name="clock">Clock implementation</param>
    /// <param name="makeHost">Factory for creating the app host</param>
    /// <param name="storeDirectory">Optional: directory for QuickFix-compatible file store</param>
    /// <param name="logDelimiter">Optional: delimiter for FIX log (null=Pipe, SOH for production)</param>
    public static async Task RunSingle(string configPath, string dictRootPath, IFixClock clock, Func<IFixClock, IFixConfig, BaseAppDI> makeHost, string? storeDirectory = null, byte? logDelimiter = null)
    {
        var task = Start(configPath, dictRootPath, clock, makeHost, storeDirectory, logDelimiter);
        await task;
    }

    private static Task Start(string configPath, string dictRootPath, IFixClock clock, Func<IFixClock, IFixConfig, BaseAppDI> makeHost, string? storeDirectory = null, byte? logDelimiter = null)
    {
        if (string.IsNullOrEmpty(configPath)) return Task.CompletedTask;

        var config = FixApp.MakeConfig(configPath, dictRootPath, storeDirectory, logDelimiter);
        var host = makeHost(clock, config);
        var t1 = FixApp.Run(host);
        return t1;
    }
}
