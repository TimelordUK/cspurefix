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
using Arrow.Threading.Tasks;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using PureFix.Transport.Recovery;
using PureFix.Transport.Ascii;
using PureFix.Types.Config;

namespace PureFix.Test.Env.Experiment
{
    internal class RuntimeContainer
    {
        public IMessageTransport Transport { get; }
        public IFixConfig Config { get; }
        public IFixMessageFactory FixMessageFactory { get; protected set; }
        public IFixMsgStore MessageStore { get; private set; }
        public IMessageParser Parser { get; private set; }
        public IMessageEncoder Encoder { get; private set; }
        public IFixLogRecovery Recovery { get; private set; }
        public CancellationTokenSource TokenSource { get; }
        public BaseApp App { get; set; }
        public IReadOnlyList<string> FixLog => ((TestLogger)App.Logs.fixLog).Entries();
        public IReadOnlyList<string> AppLog => ((TestLogger)App.Logs.appLog).Entries();
        public AsyncWorkQueue Queue { get; private set; }
        public IHost Host { get; }
        public void NoResetSeqNumFlag()
        {
            if (Config.Description == null) return;
            ((SessionDescription)Config.Description).ResetSeqNumFlag = false;
        }
      
        public void Dump()
        {
            var al = AppLog;
            var fl = FixLog;
            Console.WriteLine(string.Join(Environment.NewLine, al));
            Console.WriteLine(string.Join(Environment.NewLine, fl));
        }

        public void CheckSeq(string compId)
        {
            var inflate = InflateLog().Where(v => v.GetString((int)MsgTag.SenderCompID) == compId).ToList();
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
            if (Config.Definitions == null) throw new InvalidDataException("no config definitions");
            var parser = new AsciiParser(Config.Definitions) { Delimiter = AsciiChars.Pipe, WriteDelimiter = AsciiChars.Pipe };
            var fl = FixLog;
            var views = new List<MsgView>();
            var s = string.Join(Environment.NewLine, fl);
            var b = Encoding.UTF8.GetBytes(s);
            parser.ParseFrom(b, b.Length, (_, view) => views.Add((AsciiView)view));
            return views;
        }

        public RuntimeContainer(IHost host)
        {
            Queue = host.Services.GetService<AsyncWorkQueue>();
            Config = host.Services.GetService<IFixConfig>();
            Transport = new TestMessageTransport();
            FixMessageFactory = host.Services.GetService<IFixMessageFactory>();
            MessageStore = host.Services.GetService<IFixMsgStore>();
            Parser = host.Services.GetService<IMessageParser>();
            Encoder = host.Services.GetService<IMessageEncoder>();
            Recovery = host.Services.GetService<IFixLogRecovery>();
            var sf = host.Services.GetService<ISessionFactory>();
            App = (BaseApp)sf.MakeSession();
            TokenSource = new CancellationTokenSource();
            Host = host;
        }

        public int MessageCount(string msgType, char delim = '|')
        {
            var fixLog = FixLog;
            var expected = $"{delim}{(int)MsgTag.MsgType}={msgType}{delim}";
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
