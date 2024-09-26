using Arrow.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
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
