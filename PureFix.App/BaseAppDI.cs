using Arrow.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Transport.Store;
using PureFix.Transport.SocketTransport;

namespace PureFix.ConsoleApp
{
    internal abstract class BaseAppDI<T, U> where T : class, ISessionFactory where U : class, IFixMessageFactory
    {
        public V? Resolve<V>()
        {
            return AppHost.Services.GetService<V>();
        }
        public IHost AppHost { get; }

        protected BaseAppDI(AsyncWorkQueue q, ILogFactory factory, IFixClock clock, IFixConfig config)
        {
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(config.Description);
            ArgumentNullException.ThrowIfNull(config.Definitions);
            ArgumentNullException.ThrowIfNull(config.Description.Application);
            ArgumentNullException.ThrowIfNull(config.Description.SenderCompID);
            var builder = Host.CreateApplicationBuilder();

            builder.Services.AddSingleton(factory);
            builder.Services.AddSingleton(clock);
            builder.Services.AddSingleton<ISessionMessageFactory, Fix44SessionMessageFactory>();
            builder.Services.AddSingleton(config);
            builder.Services.AddSingleton<IMessageParser, AsciiParser>();
            builder.Services.AddSingleton<IMessageEncoder, AsciiEncoder>();
            builder.Services.AddSingleton(config.Description);
            builder.Services.AddSingleton(config.Definitions);
            builder.Services.AddSingleton(config.Description.Application);
            builder.Services.AddSingleton(q);
            builder.Services.AddSingleton<ISessionFactory, T>();
            builder.Services.AddSingleton<IFixMessageFactory, U>();
            builder.Services.AddSingleton<IFixMsgStore>(new FixMsgMemoryStore(config.Description.SenderCompID));
            if (config.IsInitiator())
            {
                builder.Services.AddSingleton<ITcpEntity, TcpInitiatorConnector>();
            }
            else
            {
                builder.Services.AddSingleton<ITcpEntity, TcpAcceptorListener>();
            }
            AppHost = builder.Build();
        }
    }
}
