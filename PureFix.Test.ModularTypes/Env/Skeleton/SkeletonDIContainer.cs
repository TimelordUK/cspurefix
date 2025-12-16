using PureFix.Examples.Skeleton;
using PureFix.Examples.TradeCapture;
﻿using Arrow.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.FIX44;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Test.ModularTypes.Env.Experiment;


namespace PureFix.Test.ModularTypes.Env.Skeleton
{
    internal class SkeletonDIContainer
    {
        public IHost AppHost { get; private set; }

        public SkeletonDIContainer(AsyncWorkQueue q, IFixClock clock, IFixConfig config)
        {
            var builder = Host.CreateApplicationBuilder();
            builder.BuildCommon<SkeletonTestLogRecovery>(q, clock, config);
            builder.Services.AddSingleton<IFixMessageFactory, FixMessageFactory>();
            builder.Services.AddSingleton<ISessionFactory, SkeletonSessionFactory>();

            AppHost = builder.Build();
        }
    }
}
