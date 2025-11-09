using Arrow.Threading.Tasks;
using PureFix.Transport.Session;
using PureFix.Types;

namespace PureFix.Examples.Shared;

public static class Runner
{
    public static async Task Run(string acceptorConfigPath, string initiatorConfigPath, string dictRootPath, IFixClock clock, Func<IFixClock, IFixConfig, BaseAppDI> makeHost)
    {
        var acceptor = Start(acceptorConfigPath, dictRootPath, clock, makeHost);
        await Task.Delay(500); // Let acceptor start listening
        var initiator = Start(initiatorConfigPath, dictRootPath, clock, makeHost);

        var tasks = new Task[] { acceptor, initiator };
        Task.WaitAll(tasks);
    }

    private static Task Start(string configPath, string dictRootPath, IFixClock clock, Func<IFixClock, IFixConfig, BaseAppDI> makeHost)
    {
        if (string.IsNullOrEmpty(configPath)) return Task.CompletedTask;

        var config = FixApp.MakeConfig(configPath, dictRootPath);
        FixApp fixApp = new(config);
        var host = makeHost(clock, config);
        var t1 = FixApp.Run(host);
        return t1;
    }
}
