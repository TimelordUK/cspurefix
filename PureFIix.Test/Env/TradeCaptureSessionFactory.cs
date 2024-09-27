using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    internal class TradeCaptureSessionFactory : ISessionFactory
    {
        public FixSession MakeSession(IFixConfig config, IMessageTransport transport, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, AsyncWorkQueue q, IFixClock clock)
        {
            if (config.Description.Application.Type == "initiator")
            {
                return new TradeCaptureClient(config, transport, fixMessageFactory, parser, encoder, q, clock);
            }
            else
            {
                return new TradeCaptureServer(config, transport, fixMessageFactory, parser, encoder, q, clock);
            }
        }
    }
}
