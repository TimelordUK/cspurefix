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
using Arrow.Threading.Tasks;

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
        public IReadOnlyList<string> FixLog => ((TestLogger)App.Logs.fixLog).Entries();
        public IReadOnlyList<string> AppLog => ((TestLogger)App.Logs.appLog).Entries();
        public AsyncWorkQueue Queue { get; private set; }
        public bool OnReady() {
            var appLog = AppLog;
            return AppLog.FirstOrDefault(l => l.Contains("OnReady")) != null;
         }

        public int HeartbeatCount()
        {
            return MessageCount(MsgType.Heartbeat);            
        }

        public int TestRequestCount()
        {
            return MessageCount(MsgType.TestRequest);
        }

        public int MessageCount(string msgType)
        {
            var fixLog = FixLog;
            var expected = $"{(int)MsgTag.MsgType}={msgType}";
            var res = fixLog.Where(l => l.Contains(expected)).ToList();
            return res.Count;
        }

        public RuntimeContainer(IFixConfig config, AsyncWorkQueue q, IFixClock clock)
        {
            Queue = q;
            Config = config;
            Transport = new TestMessageTransport();
            FixMessageFactory = new FixMessageFactory();
            MessageStore = new FixMsgMemoryStore(config.Description.SenderCompID);
            Parser = new AsciiParser(config.Definitions) { Delimiter = AsciiChars.Soh, WriteDelimiter = AsciiChars.Pipe };
            Encoder = new AsciiEncoder(config.Definitions, config.Description, config.MessageFactory, clock);
            App = new TestAsciiSkeleton(config, Transport, FixMessageFactory, Parser, Encoder, q, clock);
            TokenSource = new CancellationTokenSource();
        }

        public void ConnectTo(RuntimeContainer container)
        {
            ((TestMessageTransport)Transport).ConnectTo((TestMessageTransport)container.Transport);
        }

        public async Task Run()
        {
            await App.Run(Transport, TokenSource.Token);
        }
    }
}
