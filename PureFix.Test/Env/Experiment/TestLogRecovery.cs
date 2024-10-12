using PureFix.Transport.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Types;

namespace PureFix.Test.Env.Experiment
{
    internal class TestLogRecovery : FixLogRecovery
    {
        public TestLogRecovery(IFixLogParser parser, ILogFactory logFactory, IFixConfig config, IFixMsgStore msgStore) : base(parser, logFactory, config, msgStore)
        {
        }

        protected override FileInfo GetFixFileInfo()
        {
            return new FileInfo(Fix44PathHelper.SkeletonRecoveryPath);
        }
    }
}
