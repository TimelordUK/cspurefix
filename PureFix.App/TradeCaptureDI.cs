using Arrow.Threading.Tasks;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;
using PureFix.Test.Env.TradeCapture;

namespace PureFix.ConsoleApp
{
    internal class TradeCaptureDI(AsyncWorkQueue q, ILogFactory factory, IFixClock clock, IFixConfig config) 
        : AppHost<TradeCaptureSessionFactory, FixMessageFactory>(q, factory, clock, config)
    {
    }
}
