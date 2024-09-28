using Arrow.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.FIX50SP2.QuickFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env.TradeCapture
{
    internal class TradeCaptureRuntimeContainer : RuntimeContainer
    {
        public TradeCaptureRuntimeContainer(IHost host) : base(host)
        {
            // FixMessageFactory = new FixMessageFactory();
        }
    }
}
