using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Test.Env.Experiment;
using PureFix.Transport.Recovery;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Env.TradeCapture
{
    public class TradeCaptureSessionFactory : BaseSessionFactory
    {
        public TradeCaptureSessionFactory(IFixConfig config,  IFixLogRecovery fixLogRecover, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixMsgStore store, AsyncWorkQueue q, IFixClock clock) : base(config, fixLogRecover, logFactory, fixMessageFactory, parser, encoder, store, q, clock)
        {
        }

        public override FixSession MakeSession()
        {
            if (m_config.IsInitiator())
            {
                return new TradeCaptureClient(m_config, m_fixLogRecovery, m_logFactory, m_fixMessageFactory, m_parser, m_encoder, m_msgStore, m_q, m_clock);
            }
            else
            {
                return new TradeCaptureServer(m_config, m_fixLogRecovery, m_logFactory, m_fixMessageFactory, m_parser, m_encoder, m_msgStore, m_q, m_clock);
            }
        }
    }
}
