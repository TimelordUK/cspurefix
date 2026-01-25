using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport;
using PureFix.Transport.Recovery;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;

namespace PureFix.Test.ModularTypes.Env.TradeCapture
{
    internal class TradeCaptureTestLogRecovery : FixLogRecovery
    {
        public string Path { get; set; } = Fix44PathHelper.TradeCaptureRecoveryPath;

        public TradeCaptureTestLogRecovery(IFixLogParser parser, ILogFactory logFactory, IFixConfig config, IFixMsgStore msgStore)
            : base(parser, logFactory, config, msgStore)
        {
        }

        protected override FileInfo GetFixFileInfo()
        {
            return new FileInfo(Path);
        }
    }
}
