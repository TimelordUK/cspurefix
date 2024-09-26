using Arrow.Threading.Tasks;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    internal class TradeCaptureRuntimeContainer : RuntimeContainer
    {
        public TradeCaptureRuntimeContainer(IFixConfig config, AsyncWorkQueue q, IFixClock clock) : base(config, q, clock)
        {
            if (config.Description.Application.Type == "initiator")
            {
                App = new TradeCaptureClient(config, Transport, FixMessageFactory, Parser, Encoder, q, clock);
            }
            else
            {
                App = new TradeCaptureServer(config, Transport, FixMessageFactory, Parser, Encoder, q, clock);
            }
        }
    }
}
