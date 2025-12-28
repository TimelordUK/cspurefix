using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Ascii;
using PureFix.Transport.Recovery;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.FIX44;

namespace PureFix.Examples.Skeleton
{
    internal class TestAsciiSkeleton : BaseApp
    {
        private readonly FixMessageFactory m_msg_factory = new();

        public TestAsciiSkeleton(IFixConfig config, IFixLogRecovery? fixLogRecovery, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, AsyncWorkQueue q, IFixClock clock) : base(config, fixLogRecovery, logFactory, fixMessageFactory, parser, encoder, q, clock)
        {
            m_logReceivedMessages = true;
        }

        protected override Task OnApplicationMsg(string msgType, IMessageView view)
        {
            m_logger.Info($"OnApplicationMsg receives {msgType}");
            var m = m_msg_factory.ToFixMessage(view);
            if (m != null)
            {
                m_logger.Info($"{JsonHelper.ToJson(m, m.GetType())}");
            }
            return Task.CompletedTask;
        }

        protected override bool OnLogon(IMessageView view, string? user, string? password)
        {
            var msg = m_msg_factory.ToFixMessage(view);
            m_logger.Info($"peer logs in user {user}");
            return true;
        }

        protected override Task OnReady(IMessageView view)
        {
            m_logger.Info("OnReady");
            return Task.CompletedTask;
        }

        protected override void OnStopped(Exception error)
        {
            m_logger.Info("OnStopped");
        }
    }
}
