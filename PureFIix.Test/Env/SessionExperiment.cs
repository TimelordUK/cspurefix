using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    internal class SessionExperiment
    {
        public IFixConfig InitiatorConfig { get; private set; }
        public IFixConfig AcceptorConfig { get; private set; }
        public RuntimeContainer Initiator { get; private set; }
        public RuntimeContainer Acceptor { get; private set; }
        public IFixClock Clock { get; private set; }

        public SessionExperiment(TestEntity testEntity)
        {
            Clock = new TestClock();
            InitiatorConfig = testEntity.GetTestInitiatorConfig();
            AcceptorConfig = testEntity.GetTestInitiatorConfig();

            Initiator = new RuntimeContainer(InitiatorConfig, Clock);
            Acceptor = new RuntimeContainer(AcceptorConfig, Clock);

            Initiator.ConnectTo(Acceptor);
            Acceptor.ConnectTo(Initiator);
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
                    await Task.Delay(100);
                    if (!stopped && stopCondition())
                    {
                        await stopAction();
                        stopped = true;
                    }
                }
            });
            var res = Task.WaitAny(t1, t2);
        }
    }
}
