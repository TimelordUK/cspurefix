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
        public string Name => Config.Name();
        public IFixConfig Config { get; }

        public static IFixConfig MakeConfig(string json)
        {
            var config = FixConfig.MakeConfigFromPaths(Fix44PathHelper.DataDictRootPath, Path.Join(Fix44PathHelper.SessionRootPath, json));
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
}
