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

namespace PureFIix.Test.Env.Experiment
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
            var parser = new AsciiParser(Config.Definitions) { Delimiter = AsciiChars.Pipe, WriteDelimiter = AsciiChars.Pipe };
            var fl = FixLog;
            var views = new List<MsgView>();
            var s = string.Join(Environment.NewLine, fl);
            var b = Encoding.UTF8.GetBytes(s);
            parser.ParseFrom(b, (i, view) => views.Add((AsciiView)view));
            return views;
        }

        public RuntimeContainer(IHost host)
        {
            Queue = host.Services.GetService<AsyncWorkQueue>();
            Config = host.Services.GetService<IFixConfig>();
            Transport = host.Services.GetService<IMessageTransport>();
            FixMessageFactory = host.Services.GetService<IFixMessageFactory>();
            MessageStore = host.Services.GetService<IFixMsgStore>();
            Parser = host.Services.GetService<IMessageParser>();
            Encoder = host.Services.GetService<IMessageEncoder>();
            var sf = host.Services.GetService<ISessionFactory>();
            App = (BaseApp)sf.MakeSession();
            TokenSource = new CancellationTokenSource();
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
