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
using PureFix.Types.FIX50SP2.QuickFix.Types;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Transport.Store;
using PureFIix.Test.Env.TradeCapture;
using PureFix.Transport.SocketTransport;
using Serilog.Events;

namespace PureFix.ConsoleApp
{
    public class ConsoleLogFactory : ILogFactory
    {
        private class Logger : ILogger
        {
            public void Debug(string messageTemplate)
            {
                Console.WriteLine(messageTemplate);
            }

            public void Error(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            public void Info(string messageTemplate)
            {
                Console.WriteLine(messageTemplate);
            }

            public void Warn(string messageTemplate)
            {
                Console.WriteLine(messageTemplate);
            }
        }
        public ILogger MakeLogger(string name)
        {
            return new Logger();
        }

        public ILogger MakePlainLogger(string name)
        {
            return new Logger();
        }
    }

    internal class TradeCaptureDI
    {
        public IHost AppHost { get; private set; }
        public TradeCaptureDI(AsyncWorkQueue q, IFixClock clock, IFixConfig config)
        {
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(config.Description);
            ArgumentNullException.ThrowIfNull(config.Definitions);
            ArgumentNullException.ThrowIfNull(config.Description.Application);
            ArgumentNullException.ThrowIfNull(config.Description.SenderCompID);
            var builder = Host.CreateApplicationBuilder();
           
            builder.Services.AddSingleton<ILogFactory, ConsoleLogFactory>();
            builder.Services.AddSingleton(clock);
            builder.Services.AddSingleton<ISessionMessageFactory, Fix44SessionMessageFactory>();
            builder.Services.AddSingleton(config);
            builder.Services.AddSingleton<IMessageParser, AsciiParser>();
            builder.Services.AddSingleton<IMessageEncoder, AsciiEncoder>();
            builder.Services.AddSingleton(config.Description);
            builder.Services.AddSingleton(config.Definitions);
            builder.Services.AddSingleton(config.Description.Application);
            builder.Services.AddSingleton(q);
            builder.Services.AddSingleton<IFixMessageFactory, FixMessageFactory>();
            builder.Services.AddSingleton<ISessionFactory, TradeCaptureSessionFactory>();
            builder.Services.AddSingleton<IFixMsgStore>(new FixMsgMemoryStore(config.Description.SenderCompID));
            if (config.Description.Application.Type == "initiator")
            {
                builder.Services.AddSingleton<BaseTcpEntity, TcpInitiatorConnector>();
            } else
            {
                builder.Services.AddSingleton<BaseTcpEntity, TcpAcceptorListener>();
            }
            AppHost = builder.Build();
        }
    }
}
