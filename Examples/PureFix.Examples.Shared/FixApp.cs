using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Transport.SocketTransport;
using PureFix.Types;

namespace PureFix.Examples.Shared;

public class FixApp
{
    public string Name => Config.Name();
    public IFixConfig Config { get; }

    public static IFixConfig MakeConfig(string sessionConfigPath, string dictRootPath)
    {
        var config = FixConfig.MakeConfigFromPaths(dictRootPath, sessionConfigPath);
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
