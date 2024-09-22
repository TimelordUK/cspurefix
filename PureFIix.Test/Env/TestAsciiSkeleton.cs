using Microsoft.VisualStudio.TestPlatform.ObjectModel;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Transport.Ascii;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    internal class TestAsciiSkeleton : AsciiSession
    {
        private ILogger m_logger;
        private ILogger m_fixLog;
        public TestAsciiSkeleton(IFixConfig config, IMessageTransport transport, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixClock clock) : base(config, transport, fixMessageFactory, parser, encoder, clock)
        {
            m_logReceivedMessages = true;
            var me = config?.Description?.Application?.Name ?? "initiator";
            m_fixLog = config.LogFactory.MakePlainLogger($"csfix.{me}.txt");
            m_logger = config.LogFactory.MakeLogger($"csfix.{me}.app");
        }

        protected override async Task OnApplicationMsg(string msgType, MsgView view)
        {
            var res = await m_msgStore.Put(FixMsgStoreRecord.ToMsgStoreRecord(view));
            m_logger.Info($"store state {res}");
        }

        protected override void OnDecoded(string msgType, string txt)
        {
            m_fixLog.Info(txt);
            
        }

        protected override void OnEncoded(string msgType, string txt)
        {
            m_fixLog.Info(txt);
        }

        protected override bool OnLogon(MsgView view, string user, string password)
        {
            m_logger.Info($"peer logs in user {user}");
            return true;
        }

        protected override void OnReady(MsgView view)
        {
            m_logger.Info("OnReady");
        }

        protected override void OnStopped(Exception error)
        {
            m_logger.Info("OnStopped");
        }
    }
}
