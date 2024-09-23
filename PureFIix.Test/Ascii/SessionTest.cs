using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PureFIix.Test.Env;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFIix.Test.Ascii
{
    internal class SessionTest
    {
        private TestEntity _testEntity;

        [OneTimeSetUp]
        public void OnceSetup()
        {
            _testEntity = new TestEntity();
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        [Test]
        public async Task Test_Transport_Test()
        {
            var lhs = new TestMessageTransport();
            var rhs = new TestMessageTransport();
            lhs.ConnectTo(rhs);
            rhs.ConnectTo(lhs);
            const string s = "hello world";
            var bytes = Encoding.UTF8.GetBytes(s);
            var cts = new CancellationTokenSource();
            await lhs.SendAsync(bytes, cts.Token);
            var buffer = new byte[1024];
            var received = await rhs.ReceiveAsync(buffer, cts.Token);
            var str = Encoding.Default.GetString(buffer,0,received);
            Assert.That(str, Is.EqualTo(s));

            await rhs.SendAsync(bytes, cts.Token);
            received = await lhs.ReceiveAsync(buffer, cts.Token);
            Assert.That(received, Is.EqualTo(s.Length));
            Assert.That(Encoding.Default.GetString(buffer, 0, received), Is.EqualTo(s));
        }

        [Test]
        public void Test_Logging_Test()
        {
            var clock = new TestClock();
            var factory = new TestLoggerFactory(clock);
            var myLog = factory.MakeLogger("initiator");
            myLog.Info("initiating logon");
            var logger = (TestLogger)myLog;
            var entries = logger.Entries;
            Assert.That(entries, Has.Count.EqualTo(1));
        }

        [Test]
        public void Get_Config_Test()
        {
            var clock = new TestClock();
            var factory = new TestLoggerFactory(clock);
            var config = FixConfig.MakeConfigFromPaths(factory, Fix44PathHelper.DataDictRootPath, Path.Join(Fix44PathHelper.SessionRootPath, "test-qf44-initiator.json"));
            Assert.Multiple(() =>
            {
                Assert.That(config, Is.Not.Null);
                Assert.That(config.Description, Is.Not.Null);
                Assert.That(config.Description.Application, Is.Not.Null);
                Assert.That(config.Definitions, Is.Not.Null);
                Assert.That(config.Definitions.Message.ContainsKey("0"));
                Assert.That(config.Definitions.Simple.ContainsKey("BeginString"));
                Assert.That(config.MessageFactory, Is.Not.Null);
            });
        }

        public class RuntimeContainer
        {
            public IMessageTransport Transport { get; private set; }
            public IFixConfig Config { get; private set; }
            public IFixMessageFactory FixMessageFactory { get; private set; } 
            public IFixMsgStore MessageStore { get; private set; }
            public IMessageParser Parser { get; private set; }
            public IMessageEncoder Encoder { get; private set; }
            public CancellationTokenSource TokenSource { get; private set; }
            public TestAsciiSkeleton App { get; private set; }
            public IReadOnlyList<string> FixLog => ((TestLogger)App.Logs.fixLog).Entries;
            public IReadOnlyList<string> AppLog => ((TestLogger)App.Logs.appLog).Entries;

            public RuntimeContainer(IFixConfig initiatorConfig, IFixClock clock)
            {
                Config = initiatorConfig;
                Transport = new TestMessageTransport();
                FixMessageFactory = new FixMessageFactory();
                MessageStore = new FixMsgMemoryStore(initiatorConfig.Description.SenderCompID);
                Parser = new AsciiParser(initiatorConfig.Definitions) { Delimiter = AsciiChars.Soh, WriteDelimiter = AsciiChars.Pipe };
                Encoder = new AsciiEncoder(initiatorConfig.Definitions, initiatorConfig.Description, initiatorConfig.MessageFactory, clock);
                App = new TestAsciiSkeleton(initiatorConfig, Transport, FixMessageFactory, Parser, Encoder, clock);
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

        [Test]
        public Task Initiator_Acceptor_Login_Test()
        {
            var clock = new TestClock();
 
            var initiatorConfig = _testEntity.GetTestInitiatorConfig();
            var acceptorConfig = _testEntity.GetTestAcceptorConfig();
            var initiator = new RuntimeContainer(initiatorConfig, clock);
            var acceptor = new RuntimeContainer(acceptorConfig, clock);

            initiator.ConnectTo(acceptor);
            acceptor.ConnectTo(initiator);

            var t1 = initiator.Run();
            var t2 = acceptor.Run();
            Task.Factory.StartNew(async () =>
            {
                while (true)
                {
                    await Task.Delay(100);
                    if (initiator.FixLog.Count == 1 && acceptor.FixLog.Count == 1)
                    {
                        initiator.TokenSource.Cancel();
                    }
                }
            });
            var res = Task.WaitAny(t1, t2);
            var (iapp, ifix) = initiator.App.Logs;
            var (aapp, afix) = acceptor.App.Logs;
            return Task.FromResult(res);
        }
    }
}
