using Arrow.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using PureFix.Test.Env.Experiment;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Env.TradeCapture
{
    internal class TradeCaptureSessionExperiment : BaseSessionExperiment
    {
        public TradeCaptureSessionExperiment(TestEntity testEntity) : base(testEntity)
        {
            InitiatorConfig = testEntity.GetTestInitiator52Config();
            AcceptorConfig = testEntity.GetTestAcceptor52Config();
            var initiatorHost = new TradeCaptureDIContainer(Queue, Clock, InitiatorConfig);
            var acceptorHost = new TradeCaptureDIContainer(Queue, Clock, AcceptorConfig);
            Connect(initiatorHost.AppHost, acceptorHost.AppHost);
        }
    }
}
