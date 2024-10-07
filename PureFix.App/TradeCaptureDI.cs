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
using Microsoft.Extensions.DependencyInjection;

namespace PureFix.ConsoleApp
{
    internal class TradeCaptureDI : BaseAppDI { 
        public TradeCaptureDI(AsyncWorkQueue q, ILogFactory factory, IFixClock clock, IFixConfig config) :base(q, factory, clock, config) {
            _builder.Services.AddSingleton<ISessionFactory, TradeCaptureSessionFactory>();
            _builder.Services.AddSingleton<IFixMessageFactory, FixMessageFactory>();
            AppHost = _builder.Build();
        }
    }
}
