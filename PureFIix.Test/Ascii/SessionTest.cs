using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using PureFIix.Test.Env;
using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Types;


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
            var str = Encoding.Default.GetString(buffer, 0, received);
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
            var entries = logger.Entries();
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

        [Test]
        public async Task Initiator_Acceptor_Login_Test()
        {
            var experiment = new SessionExperiment(_testEntity);
            await experiment.Run(() =>
            {
                var initiatorLog = experiment.Initiator.FixLog;
                var acceptorLog = experiment.Acceptor.FixLog;
                return initiatorLog.Count == 1 && initiatorLog[0].Contains("35=A") && acceptorLog.Count == 1 && acceptorLog[0].Contains("35=A");
            });

                
            var (iapp, ifix) = experiment.Initiator.App.Logs;
            var (aapp, afix) = experiment.Acceptor.App.Logs;
        }
    }
}
