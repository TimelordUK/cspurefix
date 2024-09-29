using Arrow.Threading.Tasks;
using PureFix.Transport.Session;
using PureFix.Transport.SocketTransport;
using PureFix.Transport;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFIix.Test.Env;

namespace PureFix.ConsoleApp
{
    internal class FixApp
    {
        public string Name { get; private set; }
        public IFixConfig? Config { get; private set; }

        private static IFixConfig MakeConfig(ILogFactory factory, string json)
        {
            var config = FixConfig.MakeConfigFromPaths(factory, Fix44PathHelper.DataDictRootPath, Path.Join(Fix44PathHelper.SessionRootPath, json));
            return config;
        }

        public FixApp(string json)
        {
            Name = json;
        }

        public async Task Run(ILogFactory factory, IFixClock clock)
        {
            Config = MakeConfig(factory, Name);
            var queue = new AsyncWorkQueue();
            var app = new TradeCaptureDI(queue, factory, clock, Config);
            var entity = app.Resolve<ITcpEntity>();
            var cts = new CancellationTokenSource();

            if (entity != null)
            {
                var t = entity.Start(cts.Token);
                await t;
            }
        }
    }
}
