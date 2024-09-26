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
          
            var t1 = Initiator.Run();
            var t2 = Acceptor.Run();
            bool stopped = false;
            await Task.Factory.StartNew(async () =>
            {
                while (!Initiator.TokenSource.IsCancellationRequested)
                {
                    await Task.Delay(20);
                    if (!stopped && stopCondition())
                    {
                        await stopAction();
                        stopped = true;
                    }
                }
            });
            var res = Task.WaitAny(t1, t2);
            Queue.Dispose();
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

    internal class TradeCaptureSessionExperiment : BaseSessionExperiment
    {
        public TradeCaptureSessionExperiment(TestEntity testEntity)
        {
            Clock = testEntity.Clock;
            InitiatorConfig = testEntity.GetTestInitiator52Config();
            AcceptorConfig = testEntity.GetTestAcceptor52Config();
            Queue = new AsyncWorkQueue();

            Initiator = new TradeCaptureRuntimeContainer(InitiatorConfig, Queue, Clock);
            Acceptor = new TradeCaptureRuntimeContainer(AcceptorConfig, Queue, Clock);

            Initiator.ConnectTo(Acceptor);
            Acceptor.ConnectTo(Initiator);
        }
    }
}
