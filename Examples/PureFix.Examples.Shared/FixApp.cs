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
    /// </summary>
    /// <param name="sessionConfigPath">Path to session JSON config</param>
    /// <param name="dictRootPath">Root path for dictionaries</param>
    /// <param name="storeDirectory">Optional: directory for file-based session store (QuickFix-compatible)</param>
    public static IFixConfig MakeConfig(string sessionConfigPath, string dictRootPath, string? storeDirectory = null)
    {
        var config = FixConfig.MakeConfigFromPaths(dictRootPath, sessionConfigPath);

        // Configure session store factory
        if (config is FixConfig fixConfig)
        {
            if (!string.IsNullOrEmpty(storeDirectory))
            {
                // Use file-based store for persistence (QuickFix-compatible)
                fixConfig.SessionStoreFactory = new FileSessionStoreFactory(storeDirectory);
            }
            else
            {
                // Default to in-memory store
                fixConfig.SessionStoreFactory = new MemorySessionStoreFactory();
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
