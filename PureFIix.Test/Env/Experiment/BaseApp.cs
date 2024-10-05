using Arrow.Threading.Tasks;
using PureFix.Buffer.Ascii;
using PureFix.Buffer;
using PureFix.Transport.Ascii;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env.Experiment
{
    internal abstract class BaseApp : AsciiSession
    {
        protected readonly ILogger m_logger;
        protected readonly ILogger m_fixLog;
        public (ILogger appLog, ILogger fixLog) Logs => (m_logger, m_fixLog);

        protected BaseApp(IFixConfig config, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixMsgStore msgStore, AsyncWorkQueue q, IFixClock clock) : base(config, logFactory, fixMessageFactory, parser, encoder, msgStore, q, clock)
        {
            m_logReceivedMessages = true;
            var me = config.Name();
            m_fixLog = logFactory.MakePlainLogger($"csfix.{me}.txt");
            m_logger = logFactory.MakeLogger($"csfix.{me}.app");
        }

        protected override void OnDecoded(string msgType, string txt)
        {
            m_fixLog.Info(txt);
        }

        protected override async Task OnEncoded(string msgType, int seqNum, string txt)
        {
            var record = new FixMsgStoreRecord(msgType, m_clock.Current, seqNum, txt);
            var state = await m_msgStore.Put(record);
            m_logger.Info($"[{msgType},{seqNum}] store state {state}");
            m_fixLog.Info(txt);
        }

        protected override void OnStopped(Exception error)
        {
            m_logger.Info("OnStopped");
        }
    }
}
