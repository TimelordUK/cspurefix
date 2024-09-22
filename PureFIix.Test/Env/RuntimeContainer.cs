using PureFix.Buffer.Ascii;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFIix.Test.Env
{
    internal class RuntimeContainer
    {
        public IMessageTransport Transport { get; private set; }
        public IFixConfig Config { get; private set; }
        public IFixMessageFactory FixMessageFactory { get; private set; }
        public IFixMsgStore MessageStore { get; private set; }
        public IMessageParser Parser { get; private set; }
        public IMessageEncoder Encoder { get; private set; }
        public CancellationTokenSource TokenSource { get; private set; }
        public TestAsciiSkeleton App { get; private set; }

        public RuntimeContainer(IFixConfig initiatorConfig, IFixClock clock)
        {
            Config = initiatorConfig;
            Transport = new TestMessageTransport();
            FixMessageFactory = new FixMessageFactory();
            MessageStore = new FixMsgMemoryStore(initiatorConfig.Description.SenderCompID);
            Parser = new AsciiParser(initiatorConfig.Definitions) { Delimiter = AsciiChars.Soh, WriteDelimiter = AsciiChars.Pipe };
            Encoder = new AsciiEncoder(initiatorConfig.Definitions, initiatorConfig!.Description, initiatorConfig.MessageFactory, clock);
            App = new TestAsciiSkeleton(initiatorConfig, Transport, FixMessageFactory, Parser, Encoder, clock);
            TokenSource = new CancellationTokenSource();
        }

        public void ConnectTo(RuntimeContainer container)
        {
            ((TestMessageTransport)Transport).ConnectTo((TestMessageTransport)container.Transport);
        }

        public Task Run()
        {
            return Task.Factory.StartNew(async () =>
            {
                var token = TokenSource.Token;
                while (!token.IsCancellationRequested)
                {
                    await App.Run(Transport, token);
                }
            }, TaskCreationOptions.LongRunning);
        }
    }
}
