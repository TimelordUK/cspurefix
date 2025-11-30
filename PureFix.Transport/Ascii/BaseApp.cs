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
        private IFixLogRecovery? Recovery { get; }

        protected BaseApp(IFixConfig config, IFixLogRecovery? fixLogRecovery, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, AsyncWorkQueue q, IFixClock clock)
            : base(config, logFactory, fixMessageFactory, parser, encoder, q, clock)
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

        // Initialize session store and optionally recover from log files
        protected override async Task OnRun()
        {
            // Initialize the session store (loads persisted seq nums from files if using FileSessionStore)
            await InitializeSessionStore();

            // If legacy log recovery is configured, use it as fallback/override
            if (Recovery != null)
            {
                await Recovery.Recover();
                // Config values take precedence, then recovery, then store values (already loaded)
                if (m_config?.Description?.PeerSeqNum != null)
                {
                    m_sessionState.LastPeerMsgSeqNum = m_config.Description.PeerSeqNum;
                }
                else if (Recovery.PeerSeqNum != null)
                {
                    m_sessionState.LastPeerMsgSeqNum = Recovery.PeerSeqNum;
                }

                if (m_config?.Description?.MsgSeqNum != null)
                {
                    m_encoder.MsgSeqNum = m_config.Description.MsgSeqNum.Value;
                }
                else if (Recovery.MySeqNum != null)
                {
                    m_encoder.MsgSeqNum = Recovery.MySeqNum.Value + 1;
                }
            }

            m_logger.Info($"OnRun MsgSeqNum = {m_encoder.MsgSeqNum}, LastPeerMsgSeqNum = {m_sessionState.LastPeerMsgSeqNum}");
        }

        protected override async Task OnEncoded(string msgType, int seqNum, string txt)
        {
            // Store to session store
            await StoreEncodedMessage(msgType, seqNum, txt);

            // Also add to legacy recovery if configured
            if (Recovery != null)
            {
                var record = new FixMsgStoreRecord(msgType, m_clock.Current, seqNum, txt);
                await Recovery.AddRecord(record);
            }

            m_fixLog.Info(txt);
        }

        protected override void OnStopped(Exception? error)
        {
            m_logger.Info("OnStopped");
        }
    }
}
