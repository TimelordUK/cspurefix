using Arrow.Threading.Tasks;
using PureFIix.Test.Env.Skeleton;
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
    internal class SessionExperiment : BaseSessionExperiment
    {
        public SessionExperiment(TestEntity testEntity)
        {
            Clock = testEntity.Clock;
            InitiatorConfig = testEntity.GetTestInitiatorConfig();
            AcceptorConfig = testEntity.GetTestAcceptorConfig();
            Queue = new AsyncWorkQueue();
            var initiatorHost = new SkeletonDIContainer(Queue, Clock, InitiatorConfig);
            var acceptorHost = new SkeletonDIContainer(Queue, Clock, AcceptorConfig);
            Initiator = new RuntimeContainer(initiatorHost.AppHost);
            Acceptor = new RuntimeContainer(acceptorHost.AppHost);

            Initiator.ConnectTo(Acceptor);
            Acceptor.ConnectTo(Initiator);
        }
    }
}
