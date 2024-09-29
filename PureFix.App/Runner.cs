using Arrow.Threading.Tasks;
using PureFIix.Test.Env;
using PureFix.ConsoleApp;
using PureFix.Dictionary.Definition;
using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Transport.SocketTransport;
using PureFix.Types;

namespace PureFix.ConsoleApp
{
    public class Runner()
    {
        public async Task Run()
        {
            var clock = new RealtimeClock();
            var factory = new ConsoleLogFactory();
            FixApp acceptor = new("test-qf44-acceptor.json");
            var t1 = acceptor.Run(factory, clock);            
            await Task.Factory.StartNew(async () =>
            {
                FixApp initiator = new("test-qf44-initiator.json");
                await Task.Delay(500);
                await initiator.Run(factory, clock);
            });
            
            await t1;            
        }
    }
}
