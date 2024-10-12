using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using PureFix.Transport.Recovery;

namespace PureFix.Transport.Ascii
{
    public abstract class BaseApp : AsciiSession
    {
        protected readonly ILogger m_logger;
        protected readonly ILogger m_fixLog;
        public (ILogger appLog, ILogger fixLog) Logs => (m_logger, m_fixLog);
        public string FixLogName { get; }
        public string AppLogName { get; }
        private IFixLogRecovery Recovery { get; }

        protected BaseApp(IFixConfig config, IFixLogRecovery fixLogRecovery, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixMsgStore msgStore, AsyncWorkQueue q, IFixClock clock) : base(config, logFactory, fixMessageFactory, parser, encoder, msgStore, q, clock)
        {
            m_logReceivedMessages = true;
            var me = config.Name();
            FixLogName = $"csfix.{me}.fix";
            AppLogName = $"csfix.{me}.app";
            m_fixLog = logFactory.MakePlainLogger(FixLogName);
            m_logger = logFactory.MakeLogger(AppLogName);
            Recovery = fixLogRecovery;
        }

        protected override void OnDecoded(string msgType, string txt)
        {
            m_fixLog.Info(txt);
        }

        // have not yet logged in so here we can recover seq numbers if possible.
        protected override async Task OnRun()
        {
            await Recovery.Recover();
            m_sessionState.LastPeerMsgSeqNum = Recovery.PeerSeqNum;
            m_encoder.MsgSeqNum = Recovery.MySeqNum + 1 ?? 1;
            m_logger.Info($"OnRun m_encoder.MsgSeqNum = {m_encoder.MsgSeqNum}, m_sessionState.LastPeerMsgSeqNum = {m_sessionState.LastPeerMsgSeqNum}");
        }

        protected override async Task OnEncoded(string msgType, int seqNum, string txt)
        {
            var record = new FixMsgStoreRecord(msgType, m_clock.Current, seqNum, txt);
            await Recovery.AddRecord(record);
            m_fixLog.Info(txt);
        }

        protected override void OnStopped(Exception? error)
        {
            m_logger.Info("OnStopped");
        }
    }
}
