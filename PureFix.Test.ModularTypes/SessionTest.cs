using System.Text;
using Arrow.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PureFix.Buffer.Ascii;
using PureFix.Examples.Skeleton;
using PureFix.Examples.TradeCapture;
using PureFix.Test.ModularTypes.Env.Experiment;
using PureFix.Test.ModularTypes.Env.Skeleton;
using PureFix.Test.ModularTypes.Env.TradeCapture;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Test.ModularTypes
{
    internal partial class SessionTest
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
                Assert.That(config.Description!.Application, Is.Not.Null);
                Assert.That(config.Definitions, Is.Not.Null);
                Assert.That(config.Definitions!.Message.ContainsKey("0"));
                Assert.That(config.Definitions.Simple.ContainsKey("BeginString"));
                // MessageFactory is intentionally null - it should be set by the application using appropriate generated types
                Assert.That(config.MessageFactory, Is.Null);
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

        /// <summary>
        /// Tests the sequence mismatch recovery scenario where:
        /// 1. Client starts with sequence number lower than server expects
        /// 2. Server sends Reject for each Logon until sequence catches up
        /// 3. Client increments sequence and retries until successful
        /// </summary>
        [Test]
        public async Task Initiator_Sequence_Mismatch_Retry_Test()
        {
            // Configuration: Client starts at seq 1 (default), server expects seq 5
            // This means 4 retries are needed before success
            const int serverExpectedSeq = 5;

            // Set up initiator config - disable ResetSeqNumFlag so we test the retry logic
            var config = _testEntity.GetTestInitiatorConfig();
            if (config.Description is SessionDescription desc)
            {
                desc.ResetSeqNumFlag = false;
            }

            // Create transports and connect them
            var clientTransport = new TestMessageTransport();
            var serverTransport = new TestMessageTransport();
            clientTransport.ConnectTo(serverTransport);
            serverTransport.ConnectTo(clientTransport);

            // Set up initiator session using DI container
            var queue = new AsyncWorkQueue();
            var diContainer = new SkeletonDIContainer(queue, _testEntity.Clock, config);
            var container = new RuntimeContainer(diContainer.AppHost);

            // Track logon attempts received by mock server
            var logonSeqNumsReceived = new List<int>();
            var rejectsSent = new List<string>();
            var sessionReady = false;
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            // Run the initiator session in background
            var initiatorTask = Task.Run(async () =>
            {
                try
                {
                    await container.App.Run(clientTransport, cts.Token);
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    Console.WriteLine($"[INITIATOR] Exception: {ex.Message}");
                }
            });

            // Mock server loop: read messages and respond
            var serverTask = Task.Run(async () =>
            {
                var buffer = new byte[4096];
                var parser = new AsciiParser(config.Definitions!) { Delimiter = AsciiChars.Soh };
                var serverSeqNum = 1;

                try
                {
                    while (!cts.Token.IsCancellationRequested)
                    {
                        // Read message from client
                        var bytesRead = await serverTransport.ReceiveAsync(buffer, cts.Token);
                        if (bytesRead == 0) break;

                        var rawMsg = Encoding.UTF8.GetString(buffer, 0, bytesRead);
                        Console.WriteLine($"[MOCK SERVER] Raw received ({bytesRead} bytes): {rawMsg.Replace('\x01', '|')}");

                        // Parse the message
                        var views = new List<MsgView>();
                        parser.ParseFrom(buffer, bytesRead, (_, view) => views.Add(view));
                        Console.WriteLine($"[MOCK SERVER] Parsed {views.Count} message(s)");

                        foreach (var view in views)
                        {
                            var msgType = view.MsgType();
                            var clientSeqNum = view.MsgSeqNum() ?? 0;

                            Console.WriteLine($"[MOCK SERVER] Message type={msgType}, seq={clientSeqNum}");

                            if (msgType == MsgType.Logon)
                            {
                                logonSeqNumsReceived.Add(clientSeqNum);
                                Console.WriteLine($"[MOCK SERVER] Received Logon with seq={clientSeqNum}");

                                if (clientSeqNum < serverExpectedSeq)
                                {
                                    // Send Reject - client seq is too low
                                    var rejectMsg = BuildRejectMessage(config.Description!, serverSeqNum++,
                                        MsgType.Logon, clientSeqNum, "MsgSeqNum too low");
                                    rejectsSent.Add(rejectMsg.Replace('\x01', '|'));
                                    Console.WriteLine($"[MOCK SERVER] Sending Reject: {rejectMsg.Replace('\x01', '|')}");
                                    await serverTransport.SendAsync(Encoding.UTF8.GetBytes(rejectMsg), cts.Token);
                                    Console.WriteLine($"[MOCK SERVER] Reject sent for seq={clientSeqNum}");
                                }
                                else
                                {
                                    // Send Logon response - sequence is acceptable
                                    var logonResponse = BuildLogonMessage(config.Description!, serverSeqNum++);
                                    Console.WriteLine($"[MOCK SERVER] Sending Logon response: {logonResponse.Replace('\x01', '|')}");
                                    await serverTransport.SendAsync(Encoding.UTF8.GetBytes(logonResponse), cts.Token);
                                    Console.WriteLine($"[MOCK SERVER] Logon response sent, session established");
                                    sessionReady = true;

                                    // Wait a bit then send logout to end test cleanly
                                    await Task.Delay(200, cts.Token);
                                    var logoutMsg = BuildLogoutMessage(config.Description!, serverSeqNum++, "Test complete");
                                    await serverTransport.SendAsync(Encoding.UTF8.GetBytes(logoutMsg), cts.Token);
                                    Console.WriteLine($"[MOCK SERVER] Logout sent");
                                    return;
                                }
                            }
                        }
                    }
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    Console.WriteLine($"[MOCK SERVER] Exception: {ex.Message}");
                }
            });

            // Wait for either completion or timeout
            await Task.WhenAny(Task.WhenAll(initiatorTask, serverTask), Task.Delay(8000));
            cts.Cancel();

            // Verify results
            Console.WriteLine($"Logon seq nums received: [{string.Join(", ", logonSeqNumsReceived)}]");
            Console.WriteLine($"Rejects sent: {rejectsSent.Count}");

            Assert.Multiple(() =>
            {
                // Should have received multiple logon attempts (at least serverExpectedSeq logons: 1,2,3,4,5)
                Assert.That(logonSeqNumsReceived.Count, Is.GreaterThanOrEqualTo(serverExpectedSeq),
                    "Should have received enough logon retries");

                // First logon should be at seq 1 (default starting sequence)
                Assert.That(logonSeqNumsReceived.First(), Is.EqualTo(1),
                    "First logon should be at seq 1");

                // Last logon should be >= serverExpectedSeq (the successful one)
                Assert.That(logonSeqNumsReceived.Last(), Is.GreaterThanOrEqualTo(serverExpectedSeq),
                    "Last logon should have caught up to server's expected seq");

                // Verify sequence numbers increased monotonically
                for (int i = 1; i < logonSeqNumsReceived.Count; i++)
                {
                    Assert.That(logonSeqNumsReceived[i], Is.EqualTo(logonSeqNumsReceived[i - 1] + 1),
                        $"Sequence should increment by 1 each retry (at index {i})");
                }

                // Session should have become ready
                Assert.That(sessionReady, Is.True, "Session should have established successfully");
            });

            queue.Dispose();
        }

        /// <summary>
        /// Builds a FIX Reject message with the given parameters.
        /// </summary>
        private static string BuildRejectMessage(ISessionDescription desc, int seqNum, string refMsgType, int refSeqNum, string text)
        {
            const char SOH = '\x01';
            var time = DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff");
            // Build body first to calculate length
            var body = $"35=3{SOH}" +                          // MsgType = Reject
                       $"49={desc.TargetCompID}{SOH}" +        // SenderCompID (server's perspective)
                       $"56={desc.SenderCompID}{SOH}" +        // TargetCompID (server's perspective)
                       $"34={seqNum}{SOH}" +                   // MsgSeqNum
                       $"52={time}{SOH}" +                     // SendingTime
                       $"45={refSeqNum}{SOH}" +                // RefSeqNum
                       $"372={refMsgType}{SOH}" +              // RefMsgType
                       $"58={text}{SOH}";                      // Text

            var bodyLen = body.Length;
            var msg = $"8={desc.BeginString}{SOH}9={bodyLen:D7}{SOH}{body}";
            var checksum = CalculateChecksum(msg);
            return $"{msg}10={checksum:D3}{SOH}";
        }

        /// <summary>
        /// Builds a FIX Logon message response.
        /// </summary>
        private static string BuildLogonMessage(ISessionDescription desc, int seqNum)
        {
            const char SOH = '\x01';
            var time = DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff");
            // Build body first to calculate length
            var body = $"35=A{SOH}" +                          // MsgType = Logon
                       $"49={desc.TargetCompID}{SOH}" +        // SenderCompID (server's perspective)
                       $"56={desc.SenderCompID}{SOH}" +        // TargetCompID (server's perspective)
                       $"34={seqNum}{SOH}" +                   // MsgSeqNum
                       $"52={time}{SOH}" +                     // SendingTime
                       $"98=0{SOH}" +                          // EncryptMethod = None
                       $"108=30{SOH}";                         // HeartBtInt

            var bodyLen = body.Length;
            var msg = $"8={desc.BeginString}{SOH}9={bodyLen:D7}{SOH}{body}";
            var checksum = CalculateChecksum(msg);
            return $"{msg}10={checksum:D3}{SOH}";
        }

        /// <summary>
        /// Builds a FIX Logout message.
        /// </summary>
        private static string BuildLogoutMessage(ISessionDescription desc, int seqNum, string text)
        {
            const char SOH = '\x01';
            var time = DateTime.UtcNow.ToString("yyyyMMdd-HH:mm:ss.fff");
            // Build body first to calculate length
            var body = $"35=5{SOH}" +                          // MsgType = Logout
                       $"49={desc.TargetCompID}{SOH}" +        // SenderCompID (server's perspective)
                       $"56={desc.SenderCompID}{SOH}" +        // TargetCompID (server's perspective)
                       $"34={seqNum}{SOH}" +                   // MsgSeqNum
                       $"52={time}{SOH}" +                     // SendingTime
                       $"58={text}{SOH}";                      // Text

            var bodyLen = body.Length;
            var msg = $"8={desc.BeginString}{SOH}9={bodyLen:D7}{SOH}{body}";
            var checksum = CalculateChecksum(msg);
            return $"{msg}10={checksum:D3}{SOH}";
        }

        /// <summary>
        /// Calculates FIX checksum (sum of all bytes mod 256).
        /// </summary>
        private static int CalculateChecksum(string msg)
        {
            return Encoding.ASCII.GetBytes(msg).Sum(b => (int)b) % 256;
        }
    }
}
