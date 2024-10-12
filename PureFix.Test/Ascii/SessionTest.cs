using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arrow.Threading.Tasks;
using PureFix.Test.Env;
using PureFix.Test.Env.Experiment;
using PureFix.Test.Env.Skeleton;
using PureFix.Test.Env.TradeCapture;
using PureFix.Transport;
using PureFix.Types.Config;

namespace PureFix.Test.Ascii
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
        public void Test_DI_Skeleton()
        {
            var config = _testEntity.GetTestInitiatorConfig();
            var q = new AsyncWorkQueue();
            var di = new SkeletonDIContainer(q, _testEntity.Clock, config);
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
            var config = FixConfig.MakeConfigFromPaths(Fix44PathHelper.DataDictRootPath, Path.Join(Fix44PathHelper.SessionRootPath, "test-qf44-initiator.json"));
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

        private static void CheckLog(BaseSessionExperiment experiment)
        {
            var (iapp, ifix) = experiment.Initiator.App.Logs;
            Assert.Multiple(() =>
            {
                Assert.That(iapp, Is.Not.Null);
                Assert.That(((TestLogger)iapp).Entries(), Has.Count.GreaterThan(0));
                Assert.That(ifix, Is.Not.Null);
                Assert.That(((TestLogger)ifix).Entries(), Has.Count.GreaterThan(0));
            });
            var (aapp, afix) = experiment.Acceptor.App.Logs;
            Assert.Multiple(() =>
            {
                Assert.That(aapp, Is.Not.Null);
                Assert.That(((TestLogger)aapp).Entries(), Has.Count.GreaterThan(0));
                Assert.That(afix, Is.Not.Null);
                Assert.That(((TestLogger)afix).Entries(), Has.Count.GreaterThan(0));
            });
        }

        [Test]
        public async Task Initiator_Acceptor_Login_Test()
        {
            var experiment = new SkeletonSessionExperiment(_testEntity);
            await experiment.Run(experiment.OnReady, experiment.Initiator.App.Done);
            CheckLog(experiment);
        }

        [Test]
        public async Task Recovery_File_Test()
        {
            var experiment = new SkeletonSessionExperiment(_testEntity);
            var initDescription = (SessionDescription)experiment.InitiatorConfig.Description;
            var acceptDescription = (SessionDescription)experiment.AcceptorConfig.Description;
            Assert.Multiple(() =>
            {
                Assert.That(initDescription, Is.Not.Null);
                Assert.That(acceptDescription, Is.Not.Null);
            });
            initDescription.ResetSeqNumFlag = false;
            acceptDescription.ResetSeqNumFlag = false;
            var recovery = experiment.Initiator.Recovery;
            Assert.That(recovery, Is.Not.Null);
            await recovery.Recover();
            Assert.That(recovery.MySeqNum, Is.EqualTo(40));
            Assert.That(recovery.PeerSeqNum, Is.EqualTo(40));
        } 

        [Test]
        public async Task Initiator_Acceptor_Recover_From_Log()
        {
            var experiment = new SkeletonSessionExperiment(_testEntity);
            var initDescription = (SessionDescription)experiment.InitiatorConfig.Description;
            var acceptDescription = (SessionDescription)experiment.AcceptorConfig.Description;
            Assert.Multiple(() =>
            {
                Assert.That(initDescription, Is.Not.Null);
                Assert.That(acceptDescription, Is.Not.Null);
            });
            initDescription.ResetSeqNumFlag = false;
            acceptDescription.ResetSeqNumFlag = false;
            await experiment.Run(experiment.OnReady, experiment.Initiator.App.Done);

            CheckLog(experiment);
            Assert.Multiple(() =>
            {
                Assert.That(experiment.Initiator.Encoder.MsgSeqNum, Is.GreaterThan(41));
                Assert.That(experiment.Acceptor.Encoder.MsgSeqNum, Is.GreaterThan(41));
            });           
        }

        [Test]
        public async Task Initiator_Acceptor_Heatbeat_Test()
        {
            var experiment = new SkeletonSessionExperiment(_testEntity);
            var q = experiment.Queue;
            await experiment.Run(() =>
            {
                q.EnqueueAsync(() => experiment.Clock.Current = experiment.Clock.Current.AddSeconds(5));
                return experiment.Initiator.HeartbeatCount() >= 1 && experiment.Acceptor.HeartbeatCount() >= 1;
            }, experiment.Initiator.App.Done);

            CheckLog(experiment);
        }

        [Test]
        public async Task Initiator_Acceptor_Login_Initiator_Logout_Test()
        {
            var experiment = new SkeletonSessionExperiment(_testEntity);
            await experiment.Run(experiment.OnReady, experiment.Initiator.App.Done);

            CheckLog(experiment);
        }

        [Test]
        public async Task Initiator_Acceptor_Login_Acceptor_Logout_Test()
        {
            var experiment = new SkeletonSessionExperiment(_testEntity);
            await experiment.Run(experiment.OnReady, experiment.Acceptor.App.Done);

            CheckLog(experiment);
        }

        /*
         * Show that when sat idle, the connections will send heartbeats.
         */
        [Test]
        public async Task Initiator_Acceptor_Idle_Test()
        {
            var experiment = new SkeletonSessionExperiment(_testEntity);
            var q = experiment.Queue;
            await experiment.Run(() =>
            {
                q.EnqueueAsync(() => experiment.Clock.Current = experiment.Clock.Current.AddSeconds(5));
                return experiment.Initiator.HeartbeatCount() >= 10 && experiment.Acceptor.HeartbeatCount() >= 10;
            }, experiment.Initiator.App.Done);

            CheckLog(experiment);
        }

        [Test]
        public async Task Initiator_Acceptor_TestRequest_Test()
        {
            var experiment = new SkeletonSessionExperiment(_testEntity);
            var q = experiment.Queue;
            await experiment.Run(() =>
            {
                experiment.Acceptor.App.Heartbeat = false;
                q.EnqueueAsync(() => experiment.Clock.Current = experiment.Clock.Current.AddSeconds(5));
                return experiment.Initiator.HeartbeatCount() > 0 && experiment.Acceptor.TestRequestCount() > 0;
            }, experiment.Initiator.App.Done);

            CheckLog(experiment);
        }

        [Test]
        public async Task TradeCapture_Test()
        {
            var experiment = new TradeCaptureSessionExperiment(_testEntity);
            var q = experiment.Queue;
            await experiment.Run(() =>
            {
                q.EnqueueAsync(() => experiment.Clock.Current = experiment.Clock.Current.AddSeconds(5));
                return experiment.Initiator.TradeCaptureReportRequestAckCount() == 2;
            }, experiment.Initiator.App.Done);

            CheckLog(experiment);
            var tcci = experiment.Initiator.TradeCaptureReportCount();
            var tcca = experiment.Acceptor.TradeCaptureReportCount();
            Assert.Multiple(() =>
            {
                Assert.That(tcci, Is.EqualTo(10));
                Assert.That(tcca, Is.EqualTo(10));
            });     
        }
    }
}
