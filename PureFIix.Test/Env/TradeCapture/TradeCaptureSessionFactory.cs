using Arrow.Threading.Tasks;
using PureFIix.Test.Env.Skeleton;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env.TradeCapture
{
    internal class TradeCaptureSessionFactory : BaseSessionFactory
    {
        public TradeCaptureSessionFactory(IFixConfig config, IMessageTransport transport, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, AsyncWorkQueue q, IFixClock clock) : base(config, transport, fixMessageFactory, parser, encoder, q, clock)
        {
        }


        public override FixSession MakeSession()
        {
            if (m_config.Description.Application.Type == "initiator")
            {
                return new TradeCaptureClient(m_config, m_transport, m_fixMessageFactory, m_parser, m_encoder, m_q, m_clock);
            }
            else
            {
                return new TradeCaptureServer(m_config, m_transport, m_fixMessageFactory, m_parser, m_encoder, m_q, m_clock);
            }
        }
    }
}
