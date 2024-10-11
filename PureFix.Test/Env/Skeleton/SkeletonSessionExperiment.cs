using Arrow.Threading.Tasks;
using PureFIix.Test.Env.Skeleton;
using PureFix.Buffer;
using PureFix.Test.Env;
using PureFix.Test.Env.Experiment;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
