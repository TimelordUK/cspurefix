using Arrow.Threading.Tasks;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFIix.Test.Env.TradeCapture;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.ConsoleApp
{
    internal class TradeCaptureDI(AsyncWorkQueue q, ILogFactory factory, IFixClock clock, IFixConfig config) 
        : AppHost<TradeCaptureSessionFactory, FixMessageFactory>(q, factory, clock, config)
    {
    }
}
