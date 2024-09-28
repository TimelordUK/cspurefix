using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Transport.Ascii;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.FIX44.QuickFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env.Skeleton
{
    internal class TestAsciiSkeleton : BaseApp
    {
        readonly FixMessageFactory m_msg_factory = new();

        public TestAsciiSkeleton(IFixConfig config, IMessageTransport transport, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, AsyncWorkQueue q, IFixClock clock) : base(config, transport, fixMessageFactory, parser, encoder, q, clock)
        {
            m_logReceivedMessages = true;
            var me = config?.Description?.Application?.Name ?? "initiator";
        }

        protected override async Task OnApplicationMsg(string msgType, IMessageView view)
        {
            var res = await m_msgStore.Put(FixMsgStoreRecord.ToMsgStoreRecord(view));
            m_logger.Info($"store state {res}");
        }

        protected override bool OnLogon(IMessageView view, string user, string password)
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
