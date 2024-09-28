using Arrow.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.Config;
using PureFix.Types.FIX44.QuickFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env.Skeleton
{
    internal class SkeletonDIContainer
    {
        public IHost AppHost { get; private set; }
        public BaseApp App { get; private set; }


        public SkeletonDIContainer(AsyncWorkQueue q, IFixClock clock, IFixConfig config)
        {
            var builder = Host.CreateApplicationBuilder();
            builder.Services.AddSingleton<IMessageTransport, TestMessageTransport>();
            builder.Services.AddSingleton<ILogFactory, TestLoggerFactory>();
            builder.Services.AddSingleton(clock);
            builder.Services.AddSingleton<ISessionMessageFactory, Fix44SessionMessageFactory>();
            builder.Services.AddSingleton(config);
            builder.Services.AddSingleton<IMessageParser, AsciiParser>();
            builder.Services.AddSingleton<IMessageEncoder, AsciiEncoder>();
            builder.Services.AddSingleton<IFixMessageFactory, FixMessageFactory>();
            builder.Services.AddSingleton(config.Description);
            builder.Services.AddSingleton(config.Definitions);
            builder.Services.AddSingleton(config.Description.Application);
            builder.Services.AddSingleton<ISessionFactory, SkeletonSessionFactory>();
            builder.Services.AddSingleton(q);
            builder.Services.AddSingleton<IFixMsgStore>(new FixMsgMemoryStore(config.Description.SenderCompID));

            AppHost = builder.Build();
            var sf = AppHost.Services.GetService<ISessionFactory>();
            App = (BaseApp)sf.MakeSession();
        }
    }
}
