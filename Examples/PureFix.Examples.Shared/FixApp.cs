using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Transport.SocketTransport;
using PureFix.Transport.Store;
using PureFix.Types;

namespace PureFix.Examples.Shared;

public class FixApp
{
    public string Name => Config.Name();
    public IFixConfig Config { get; }

    /// <summary>
    /// Creates a FIX config from paths.
    /// Store factory is auto-created from the "store" section in the JSON config.
    /// </summary>
    /// <param name="sessionConfigPath">Path to session JSON config</param>
    /// <param name="dictRootPath">Root path for dictionaries</param>
    public static IFixConfig MakeConfig(string sessionConfigPath, string dictRootPath)
    {
        return FixConfig.MakeConfigFromPaths(dictRootPath, sessionConfigPath);
    }

    /// <summary>
    /// Creates a FIX config from paths with explicit store directory override.
    /// Store directory parameter overrides the "store" section in JSON config.
    /// </summary>
    [Obsolete("Store directory should be configured in JSON config 'store' section")]
    public static IFixConfig MakeConfig(string sessionConfigPath, string dictRootPath, string? storeDirectory)
    {
        return MakeConfig(sessionConfigPath, dictRootPath, storeDirectory, null);
    }

    /// <summary>
    /// Creates a FIX config from paths with explicit store directory and log delimiter override.
    /// </summary>
    [Obsolete("Store directory and log delimiter should be configured in JSON config")]
    public static IFixConfig MakeConfig(string sessionConfigPath, string dictRootPath, string? storeDirectory, byte? logDelimiter)
    {
        var config = FixConfig.MakeConfigFromPaths(dictRootPath, sessionConfigPath);

        if (config is FixConfig fixConfig)
        {
            // Override store factory if explicitly specified
            if (!string.IsNullOrEmpty(storeDirectory))
            {
                fixConfig.SessionStoreFactory = new FileSessionStoreFactory(storeDirectory);
            }

            if (logDelimiter.HasValue)
            {
                fixConfig.LogDelimiter = logDelimiter.Value;
            }
        }

        return config;
    }

    public FixApp(IFixConfig config)
    {
        Config = config;
    }

    public static async Task Run(BaseAppDI app)
    {
        var entity = app.Resolve<ITcpEntity>();
        var cts = new CancellationTokenSource();

        if (entity != null)
        {
            var t = entity.Start(cts.Token);
            await t;
        }
    }
}
