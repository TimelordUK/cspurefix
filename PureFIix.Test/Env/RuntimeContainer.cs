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
        public IFixMessageFactory FixMessageFactory { get; protected set; }
        public IFixMsgStore MessageStore { get; private set; }
        public IMessageParser Parser { get; private set; }
        public IMessageEncoder Encoder { get; private set; }
        public CancellationTokenSource TokenSource { get; private set; }
        public BaseApp App { get; set; }
        public IReadOnlyList<string> FixLog => ((TestLogger)App.Logs.fixLog).Entries();
        public IReadOnlyList<string> AppLog => ((TestLogger)App.Logs.appLog).Entries();
        public AsyncWorkQueue Queue { get; private set; }

        public void Dump()
        {
            var al = AppLog;
            var fl = FixLog;
            Console.WriteLine(string.Join(Environment.NewLine, al));
            Console.WriteLine(string.Join(Environment.NewLine, fl));
        }

        public void CheckSeq(string compId)
        {
            var inflate = InflateLog().Where(v=>v.GetString((int)MsgTag.SenderCompID) == compId).ToList();
            if (inflate.Count > 0)
            {
                var m0 = inflate[0];
                var prevSeqNo = m0.GetInt32((int)MsgTag.MsgSeqNum);
                for (var i = 1; i < inflate.Count; ++i)
                {
                    var seqNo = inflate[i].GetInt32((int)MsgTag.MsgSeqNum);
                    Assert.That(seqNo, Is.EqualTo(prevSeqNo + 1));
                    prevSeqNo = seqNo;
                }
            }
        }

        public IReadOnlyList<MsgView> InflateLog()
        {
            var parser = new AsciiParser(Config.Definitions) { Delimiter = AsciiChars.Pipe, WriteDelimiter = AsciiChars.Pipe };
            var fl = FixLog;
            var views = new List<MsgView>();
            var s = string.Join(Environment.NewLine, fl);
            var b = Encoding.UTF8.GetBytes(s);
            parser.ParseFrom(b, (i, view) => views.Add((AsciiView)view));
            return views;
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

        public int ResendRequestCount()
        {
            return MessageCount(MsgType.ResendRequest);
        }

        public int LogonCount()
        {
            return MessageCount(MsgType.Logon);
        }

        public int LogoutCount()
        {
            return MessageCount(MsgType.Logout);
        }

        public int TradeCaptureReportRequestAckCount()
        {
            return MessageCount(MsgType.TradeCaptureReportRequestAck);
        }

        public int MessageCount(string msgType)
        {
            var fixLog = FixLog;
            var expected = $"|{(int)MsgTag.MsgType}={msgType}|";
            var res = fixLog.Where(l => l.Contains(expected)).ToList();
            return res.Count;
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
