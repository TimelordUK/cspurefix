using Arrow.Threading.Tasks;
using PureFIix.Test.Env.Experiment;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env.TradeCapture
{
    public class TradeCaptureSessionFactory : BaseSessionFactory
    {
        public TradeCaptureSessionFactory(IFixConfig config, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixMsgStore store, AsyncWorkQueue q, IFixClock clock) : base(config, logFactory, fixMessageFactory, parser, encoder, store, q, clock)
        {
        }

        public override FixSession MakeSession()
        {
            if (m_config.IsInitiator())
            {
                return new TradeCaptureClient(m_config, m_logFactory, m_fixMessageFactory, m_parser, m_encoder, m_msgStore, m_q, m_clock);
            }
            else
            {
                return new TradeCaptureServer(m_config, m_logFactory, m_fixMessageFactory, m_parser, m_encoder, m_msgStore, m_q, m_clock);
            }
        }
    }
}
