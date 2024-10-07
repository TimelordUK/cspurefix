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
        public static async Task Run(CommandOptions options)
        {
            var clock = new RealtimeClock();
            
            FixApp acceptor = new(options.Acceptor);
            var t1 = acceptor.Run(clock);            
            await Task.Factory.StartNew(async () =>
            {
                FixApp initiator = new(options.Initiator);
                await Task.Delay(500);
                await initiator.Run(clock);
            });
            
            await t1;            
        }
    }
}
