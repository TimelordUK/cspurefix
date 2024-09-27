using Arrow.Threading.Tasks;
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
            var factory = new SkeletonSessionFactory();
            Initiator = new RuntimeContainer(InitiatorConfig, factory, Queue, Clock);
            Acceptor = new RuntimeContainer(AcceptorConfig, factory, Queue, Clock);

            Initiator.ConnectTo(Acceptor);
            Acceptor.ConnectTo(Initiator);
        }
    }
}
