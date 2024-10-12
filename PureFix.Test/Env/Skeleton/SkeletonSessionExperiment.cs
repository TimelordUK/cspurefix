using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Test.Env;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Test.Env.Experiment;

namespace PureFix.Test.Env.Skeleton
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
