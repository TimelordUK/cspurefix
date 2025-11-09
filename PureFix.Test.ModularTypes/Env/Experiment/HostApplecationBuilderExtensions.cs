using PureFix.Examples.Skeleton;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Examples.TradeCapture;
using PureFix.Test.ModularTypes.Helpers;
﻿using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Arrow.Threading.Tasks;
using PureFix.Buffer.Ascii;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Transport;
using PureFix.Transport.Recovery;

namespace PureFix.Test.ModularTypes.Env.Experiment
{
    internal static class HostApplecationBuilderExtensions
    {
        public static void BuildCommon<T>(this HostApplicationBuilder builder, AsyncWorkQueue q, IFixClock clock, IFixConfig config) where T:class, IFixLogRecovery
        {
            if (config.Description == null) throw new InvalidDataException("no config description.");
            if (config.Definitions == null) throw new InvalidDataException("no config definitions.");
            if (config.Description.Application == null) throw new InvalidDataException("no config application.");
            if (config.Description.SenderCompID == null) throw new InvalidDataException("no config sender compId.");

            var factory = new TestLoggerFactory();
            builder.Services.AddSingleton<IMessageTransport, TestMessageTransport>();
            builder.Services.AddSingleton<ILogFactory>(factory);
            builder.Services.AddSingleton(clock);
            builder.Services.AddSingleton<ISessionMessageFactory, Fix44ModularSessionMessageFactory>();
            builder.Services.AddSingleton(config);
            builder.Services.AddSingleton<IMessageParser, AsciiParser>();
            builder.Services.AddSingleton<IMessageEncoder, AsciiEncoder>();
            builder.Services.AddSingleton(config.Description);
            builder.Services.AddSingleton(config.Definitions);
            builder.Services.AddSingleton(config.Description.Application);
            builder.Services.AddSingleton(q);
            var store = new FixMsgMemoryStore(config.Description.SenderCompID);
            builder.Services.AddSingleton<IFixMsgStore>(store);
            builder.Services.AddSingleton<IFixLogParser, FixLogParser>();
            builder.Services.AddSingleton<IFixLogRecovery, T>();
        }
    }
}
