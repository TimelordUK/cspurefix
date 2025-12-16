using PureFix.Examples.Skeleton;
using PureFix.Examples.TradeCapture;
﻿using Arrow.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PureFix.Types.FIX50SP2;
using PureFix.Test.ModularTypes.Env.Experiment;



namespace PureFix.Test.ModularTypes.Env.TradeCapture
{
    internal class TradeCaptureDIContainer
    {
        public IHost AppHost { get; private set; }

        public TradeCaptureDIContainer(AsyncWorkQueue q, IFixClock clock, IFixConfig config)
        {
            var builder = Host.CreateApplicationBuilder();
            builder.BuildCommon<TradeCaptureTestLogRecovery>(q, clock, config);
            builder.Services.AddSingleton<IFixMessageFactory, FixMessageFactory>();
            builder.Services.AddSingleton<ISessionFactory, TradeCaptureSessionFactory>();

            AppHost = builder.Build();
        }
    }
}


