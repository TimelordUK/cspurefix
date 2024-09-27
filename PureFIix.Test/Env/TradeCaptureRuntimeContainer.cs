using Arrow.Threading.Tasks;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.FIX50SP2.QuickFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    internal class TradeCaptureRuntimeContainer : RuntimeContainer
    {
        public TradeCaptureRuntimeContainer(IFixConfig config, ISessionFactory factory, AsyncWorkQueue q, IFixClock clock) : base(config, factory, q, clock)
        {
            FixMessageFactory = new FixMessageFactory();
            App = (BaseApp)factory.MakeSession(config, Transport, FixMessageFactory, Parser, Encoder, q, clock);
        }
    }
}
