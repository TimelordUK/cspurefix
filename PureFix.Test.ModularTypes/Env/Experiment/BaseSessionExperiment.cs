using Arrow.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport.Session;
using PureFix.Types;
using System.Diagnostics;


namespace PureFix.Test.ModularTypes.Env.Experiment
{
    internal abstract class BaseSessionExperiment
    {
        protected BaseSessionExperiment(TestEntity testEntity)
        {
            Clock = testEntity.Clock;
            Queue = new AsyncWorkQueue();
        }
        public IFixConfig InitiatorConfig { get; protected set; } = null!;
        public IFixConfig AcceptorConfig { get; protected set; } = null!;
        public RuntimeContainer Initiator { get; protected set; } = null!;
        public RuntimeContainer Acceptor { get; protected set; } = null!;
        public IFixClock Clock { get; protected set; }
        public AsyncWorkQueue Queue { get; protected set; }

        public void NoResetSeqNumFlag()
        {
            Initiator.NoResetSeqNumFlag();
            Acceptor.NoResetSeqNumFlag();
        }
        
        protected void Connect(IHost initiator, IHost acceptor)
        {
            Initiator = new RuntimeContainer(initiator);
            Acceptor = new RuntimeContainer(acceptor);
            Initiator.ConnectTo(Acceptor);
            Acceptor.ConnectTo(Initiator);
        }

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
            var tasks = new[] { t1, t2 };
            var res = Task.WaitAll(tasks, TimeSpan.FromSeconds(5));

#if DEBUG
            Initiator.Dump();
            Console.WriteLine();
            Acceptor.Dump();
#endif

            Assert.Multiple(() =>
            {
                Assert.That(Acceptor.Config.Description, Is.Not.Null);
                Assert.That(Initiator.Config.Description, Is.Not.Null);
                Debug.Assert(Acceptor.Config.Description != null);
                Debug.Assert(Acceptor.Config.Description.SenderCompID != null);
                Initiator.CheckSeq(Acceptor.Config.Description.SenderCompID);
                Debug.Assert(Initiator.Config.Description != null);
                Debug.Assert(Initiator.Config.Description.SenderCompID != null);
                Acceptor.CheckSeq(Initiator.Config.Description.SenderCompID);
            });
 
            // check we have processed all messages sent from remote peer in sequence and
            // have not re-ordered in particular a batch sent from server
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

                Assert.That(t1.IsFaulted, Is.False);
                Assert.That(t2.IsFaulted, Is.False);

                Assert.That(t1.Exception, Is.Null);
                Assert.That(t2.Exception, Is.Null);
                Assert.That(triggered, Is.True);
            });
        }
    }
}
