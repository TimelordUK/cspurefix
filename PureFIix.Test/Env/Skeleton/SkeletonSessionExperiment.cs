using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env.Skeleton
{
    internal class SkeletonSessionExperiment : BaseSessionExperiment
    {
        public SkeletonSessionExperiment(TestEntity testEntity) : base(testEntity)
        {
            InitiatorConfig = testEntity.GetTestInitiatorConfig();
            AcceptorConfig = testEntity.GetTestAcceptorConfig();
            var initiatorHost = new SkeletonDIContainer(Queue, Clock, InitiatorConfig);
            var acceptorHost = new SkeletonDIContainer(Queue, Clock, AcceptorConfig);
            Connect(initiatorHost.AppHost, acceptorHost.AppHost);           
        }
    }
}
