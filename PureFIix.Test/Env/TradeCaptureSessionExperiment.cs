using Arrow.Threading.Tasks;
using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Types;
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
            var factory = new TradeCaptureSessionFactory();
            Initiator = new TradeCaptureRuntimeContainer(InitiatorConfig, factory, Queue, Clock);
            Acceptor = new TradeCaptureRuntimeContainer(AcceptorConfig, factory, Queue, Clock);

            Initiator.ConnectTo(Acceptor);
            Acceptor.ConnectTo(Initiator);
        }
    }
}
