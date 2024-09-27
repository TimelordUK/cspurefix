using Arrow.Threading.Tasks;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    internal abstract class BaseSessionExperiment
    {
        public IFixConfig InitiatorConfig { get; protected set; }
        public IFixConfig AcceptorConfig { get; protected set; }
        public RuntimeContainer Initiator { get; protected set; }
        public RuntimeContainer Acceptor { get; protected set; }
        public IFixClock Clock { get; protected set; }
        public AsyncWorkQueue Queue { get; protected set; }

        public bool OnReady()
        {
            return Initiator.OnReady() && Acceptor.OnReady();
        }

        public async Task Run(Func<bool> stopCondition, Func<Task> stopAction)
        {
            var t2 = Acceptor.Run();
            var t1 = Initiator.Run();

            bool triggered = false;
            int iteration = 0;
            await Task.Factory.StartNew(async () =>
            {
                while (false == Initiator.TokenSource.IsCancellationRequested && false == Acceptor.TokenSource.IsCancellationRequested)
                {
                    await Task.Delay(20);
                    if (!triggered && stopCondition())
                    {
                        await stopAction();
                        triggered = true;
                    }
                    ++iteration;
                    if (iteration > 500)
                    {
                        break;
                    }
                }
            });
            var tasks = new Task[] { t1, t2 };
            var res = Task.WaitAll(tasks, TimeSpan.FromSeconds(5));

#if DEBUG
            Initiator.Dump();
            Console.WriteLine();
            Acceptor.Dump();
#endif
            // check we have processed all messages sent from remote peer in sequence and
            // have not re-ordered in particular a batch sent from server.
            Initiator.CheckSeq(Acceptor.Config.Description.SenderCompID);
            Acceptor.CheckSeq(Initiator.Config.Description.SenderCompID);

            // should not have sent a resend request 
            var initiatorResends = Initiator.ResendRequestCount();
            var acceptorResends = Acceptor.ResendRequestCount();

            var initiatorLogonCount = Initiator.LogonCount();
            var acceptorLogonCount = Acceptor.LogonCount();

            var initiatorLogoutCount = Initiator.LogoutCount();
            var acceptorLogoutCount = Acceptor.LogoutCount();

            Queue.Dispose();
            // check we did not fault from an exception in session.
            Assert.Multiple(() =>
            {
                Assert.That(initiatorResends, Is.EqualTo(0));
                Assert.That(acceptorResends, Is.EqualTo(0));

                Assert.That(initiatorLogonCount, Is.EqualTo(2));
                Assert.That(acceptorLogonCount, Is.EqualTo(2));

                //Assert.That(initiatorLogoutCount, Is.EqualTo(2));
                //Assert.That(acceptorLogoutCount, Is.EqualTo(2));

                Assert.That(t1.IsFaulted, Is.False);
                Assert.That(t2.IsFaulted, Is.False);

                Assert.That(t1.Exception, Is.Null);
                Assert.That(t2.Exception, Is.Null);
                Assert.That(triggered, Is.True);
            });
        }
    }
}
