using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport;
using PureFix.Transport.Recovery;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;

namespace PureFix.Test.ModularTypes.Env.Skeleton
{
    internal class SkeletonTestLogRecovery : FixLogRecovery
    {
        public string Path { get; set; } = Fix44PathHelper.SkeletonRecoveryPath;

        public SkeletonTestLogRecovery(IFixLogParser parser, ILogFactory logFactory, IFixConfig config, IFixMsgStore msgStore)
            : base(parser, logFactory, config, msgStore)
        {
        }

        protected override FileInfo GetFixFileInfo()
        {
            return new FileInfo(Path);
        }
    }
}
