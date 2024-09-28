using Arrow.Threading.Tasks;
using PureFIix.Test.Env;
using PureFix.ConsoleApp;
using PureFix.Dictionary.Definition;
using PureFix.Transport;
using PureFix.Transport.SocketTransport;
using PureFix.Types;
using Serilog;
using Serilog.Core;
using Microsoft.Extensions.DependencyInjection;

namespace PureFix.ConsoleApp
{
    public class Runner()
    {
        public async Task Run()
        {
            var factory = new ConsoleLogFactory();
            var initiatorConfig = FixConfig.MakeConfigFromPaths(factory, Fix44PathHelper.DataDictRootPath, Path.Join(Fix44PathHelper.SessionRootPath, "test-qf44-initiator.json"));
            var acceptorConfig = FixConfig.MakeConfigFromPaths(factory, Fix44PathHelper.DataDictRootPath, Path.Join(Fix44PathHelper.SessionRootPath, "test-qf44-acceptor.json"));

            var qInitiator = new AsyncWorkQueue();
            var qAcceptor = new AsyncWorkQueue();
            var clock = new RealtimeClock();
            var diInitiator = new TradeCaptureDI(qInitiator, clock, initiatorConfig);
            var diAcceptor = new TradeCaptureDI(qAcceptor, clock, acceptorConfig);

            var initiator = diInitiator.AppHost.Services.GetService<ITcpEntity>();
            var acceptor = diAcceptor.AppHost.Services.GetService<ITcpEntity>();

            var cts = new CancellationTokenSource();

            if (initiator != null && acceptor != null)
            {
                var t2 = acceptor.Start(cts.Token);
                await Task.Delay(500);
                var t1 = initiator.Start(cts.Token);
                Task.WaitAll(t1, t2);
            }
        }
    }
}
