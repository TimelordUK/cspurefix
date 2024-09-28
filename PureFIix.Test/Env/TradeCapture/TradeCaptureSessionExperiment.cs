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

namespace PureFIix.Test.Env.TradeCapture
{
    internal class TradeCaptureSessionExperiment : BaseSessionExperiment
    {
        public TradeCaptureSessionExperiment(TestEntity testEntity)
        {
            Clock = testEntity.Clock;
            InitiatorConfig = testEntity.GetTestInitiator52Config();
            AcceptorConfig = testEntity.GetTestAcceptor52Config();
            Queue = new AsyncWorkQueue();
            var initiatorHost = new TradeCaptureDIContainer(Queue, Clock, InitiatorConfig);
            var acceptorHost = new TradeCaptureDIContainer(Queue, Clock, AcceptorConfig);
            Initiator = new TradeCaptureRuntimeContainer(initiatorHost.AppHost);
            Acceptor = new TradeCaptureRuntimeContainer(acceptorHost.AppHost);

            Initiator.ConnectTo(Acceptor);
            Acceptor.ConnectTo(Initiator);
        }
    }
}
