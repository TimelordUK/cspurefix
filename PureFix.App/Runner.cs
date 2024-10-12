using Arrow.Threading.Tasks;
using PureFix.ConsoleApp;
using PureFix.Dictionary.Definition;
using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Types;

namespace PureFix.ConsoleApp
{
    public class Runner
    {
        internal static async Task Run(CommandOptions options, Func<IFixClock, IFixConfig, BaseAppDI> makeHost)
        {           
            var clock = new RealtimeClock(); 
            var acceptor = Start(options.Acceptor, clock, makeHost);
            await Task.Delay(500);
            var initiator = Start(options.Initiator, clock, makeHost);
            var tasks = new Task[] { acceptor, initiator };
            Task.WaitAll(tasks);            
        }

        private static Task Start(string json, IFixClock clock, Func<IFixClock, IFixConfig, BaseAppDI> makeHost)
        {
            if (string.IsNullOrEmpty(json)) return Task.CompletedTask;
            var config = FixApp.MakeConfig(json);
            FixApp fixApp = new(config);
            var host = makeHost(clock, config);
            var t1 = FixApp.Run(host);
            return t1;
        }
    }
}
