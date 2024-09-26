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

namespace PureFIix.Test.Env
{
    internal abstract class BaseApp : AsciiSession
    {
        protected readonly ILogger m_logger;
        protected readonly ILogger m_fixLog;        
        public (ILogger appLog, ILogger fixLog) Logs => (m_logger, m_fixLog);

        public BaseApp(IFixConfig config, IMessageTransport transport, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, AsyncWorkQueue q, IFixClock clock) : base(config, transport, fixMessageFactory, parser, encoder, q, clock)
        {
            m_logReceivedMessages = true;
            var me = config?.Description?.Application?.Name ?? "initiator";
            m_fixLog = config.LogFactory.MakePlainLogger($"csfix.{me}.txt");
            m_logger = config.LogFactory.MakeLogger($"csfix.{me}.app");
        }

        protected override void OnDecoded(string msgType, string txt)
        {
            m_fixLog.Info(txt);
        }

        protected override void OnEncoded(string msgType, string txt)
        {
            m_fixLog.Info(txt);
        }

        protected override void OnStopped(Exception error)
        {
            m_logger.Info("OnStopped");
        }
    }
}
