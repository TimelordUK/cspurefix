using Microsoft.Extensions.Hosting;
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

namespace PureFix.Test.Env.Experiment
{
    internal static class HostApplecationBuilderExtensions
    {
        public static void BuildCommon(this HostApplicationBuilder builder, AsyncWorkQueue q, IFixClock clock, IFixConfig config)
        {
            builder.Services.AddSingleton<IMessageTransport, TestMessageTransport>();
            builder.Services.AddSingleton<ILogFactory, TestLoggerFactory>();
            builder.Services.AddSingleton(clock);
            builder.Services.AddSingleton<ISessionMessageFactory, Fix44SessionMessageFactory>();
            builder.Services.AddSingleton(config);
            builder.Services.AddSingleton<IMessageParser, AsciiParser>();
            builder.Services.AddSingleton<IMessageEncoder, AsciiEncoder>();
            builder.Services.AddSingleton(config.Description);
            builder.Services.AddSingleton(config.Definitions);
            builder.Services.AddSingleton(config.Description.Application);
            builder.Services.AddSingleton(q);
            builder.Services.AddSingleton<IFixMsgStore>(new FixMsgMemoryStore(config.Description.SenderCompID));
        }
    }
}
