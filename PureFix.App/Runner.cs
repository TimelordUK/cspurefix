using Arrow.Threading.Tasks;
using PureFIix.Test.Env;
using PureFix.ConsoleApp;
using PureFix.Dictionary.Definition;
using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Transport.SocketTransport;
using PureFix.Types;
using Serilog;
using Serilog.Core;

namespace PureFix.ConsoleApp
{
    public class Runner()
    {
        static ILogFactory m_factory = new ConsoleLogFactory();
      
        internal class FixApp
        {
            public string Name { get; private set; }
            public IFixConfig Config { get; private set; }

            private IFixConfig MakeConfig(string json)
            {
                var config = FixConfig.MakeConfigFromPaths(m_factory, Fix44PathHelper.DataDictRootPath, Path.Join(Fix44PathHelper.SessionRootPath, json));
                return config;
            }

            public FixApp(string json)
            {
                Name = json;
                Config = MakeConfig(json);
            }

            public async Task Run(IFixClock clock)
            {
                var qInitiator = new AsyncWorkQueue();
                var qAcceptor = new AsyncWorkQueue();

                var app = new TradeCaptureDI(qAcceptor, clock, Config);
                var entity = app.Resolve<ITcpEntity>();
                var cts = new CancellationTokenSource();

                if (entity != null)
                {
                    var t = entity.Start(cts.Token);
                    await t;
                }
            }
        }

        public async Task Run()
        {
            var clock = new RealtimeClock();
            FixApp initiator = new("test-qf44-initiator.json");
            FixApp acceptor = new("test-qf44-acceptor.json");
            var t1 = acceptor.Run(clock);
            await Task.Delay(500);
            var t2 = initiator.Run(clock); 
            Task.WaitAll(t1, t2);            
        }
    }
}
