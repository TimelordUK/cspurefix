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
                while (false == Initiator.TokenSource.IsCancellationRequested && iteration < 100)
                {
                    await Task.Delay(20);
                    if (!triggered && stopCondition())
                    {
                        await stopAction();
                        triggered = true;
                    }
                    ++iteration;
                }
            });
            var tasks = new Task[] { t1, t2 };
            var res = Task.WaitAny(tasks, TimeSpan.FromSeconds(5));
            
            Initiator.Dump();
            Console.WriteLine();
            Acceptor.Dump();

            Initiator.CheckSeq(Acceptor.Config.Description.SenderCompID);
            Acceptor.CheckSeq(Initiator.Config.Description.SenderCompID);           
            
            Queue.Dispose();
            // check we did not fault from an exception in session.
            Assert.Multiple(() =>
            {
                Assert.That(t1.IsFaulted, Is.False);
                Assert.That(t2.IsFaulted, Is.False);
              
                Assert.That(t1.Exception, Is.Null);
                Assert.That(t2.Exception, Is.Null);
                Assert.That(triggered, Is.True);
            }); 
        }
    }

    internal class SessionExperiment : BaseSessionExperiment
    {
        public SessionExperiment(TestEntity testEntity)
        {
            Clock = testEntity.Clock;
            InitiatorConfig = testEntity.GetTestInitiatorConfig();
            AcceptorConfig = testEntity.GetTestAcceptorConfig();
            Queue = new AsyncWorkQueue();

            Initiator = new RuntimeContainer(InitiatorConfig, Queue, Clock);
            Acceptor = new RuntimeContainer(AcceptorConfig, Queue, Clock);

            Initiator.ConnectTo(Acceptor);
            Acceptor.ConnectTo(Initiator);
        }
    }
}
