using PureFIix.Test.Env;
using PureFix.Buffer.Ascii;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types.FIX44.QuickFix;
using PureFix.Types.FIX44.QuickFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Ascii
{
    internal class AsciiStoreReplayTest
    {
        private TestEntity _testEntity;
        private List<AsciiView> _views;
        private IFixConfig _client_config;
        private IFixConfig _server_config;

        [OneTimeSetUp]
        public async Task OnceSetup()
        {
            _testEntity = new TestEntity();
            _views = await _testEntity.Replay(Fix44PathHelper.ReplayTestClientPath);
            _client_config = _testEntity.GetTestInitiatorConfig();
            _server_config = _testEntity.GetTestAcceptorConfig();
        }

        [SetUp]
        public void Setup()
        {
            _testEntity.Prepare();
        }

        [Test]
        public void Check_Replay_View_Count_Test()
        {
            Assert.That(_views, Has.Count.EqualTo(15));
        }

        [Test]
        public async Task Check_Server_Store_State_Test()
        {
            var store = await _testEntity.MakeMsgStore(_views, _server_config?.Description?.SenderCompID);
            var state = await store.GetState();
            Assert.That(state.Length, Is.EqualTo(9));
        }

        [Test]
        public async Task Check_Client_Store_State_Test()
        {
            var store = await _testEntity.MakeMsgStore(_views, _client_config?.Description?.SenderCompID);
            var state = await store.GetState();
            Assert.That(state.Length, Is.EqualTo(1));
        }

        [Test]
        public async Task Server_Replay_Request_Seq_1_To_10_Test()
        {
            var store = await _testEntity.MakeMsgStore(_views, _server_config?.Description?.SenderCompID);
            var msgFactory = new FixMessageFactory();
            var clock = new TestClock { Current = DateTime.Today.AddHours(8) };
            Assert.That(_server_config, Is.Not.Null);
            IFixMsgResender replayer = new FixMsgAsciiStoreResend(store, msgFactory, _server_config, clock);
            var state = await store.GetState();
            Assert.That(state.Length, Is.EqualTo(9));
            var vec = await replayer.GetResendRequest(1, 10);
            Assert.That(vec, Is.Not.Null);
            Assert.That(vec, Has.Count.EqualTo(10));

            CheckSeqReset(vec[0], 1, 2);

            Assert.Multiple(() =>
            {
                var v1 = vec[1];
                Assert.That(v1.MsgType, Is.EqualTo(MsgTypeValues.TradeCaptureReportRequestAck));
                Assert.That(v1.SeqNum, Is.EqualTo(2));
                Assert.That(v1.InflatedMessage?.StandardHeader, Is.Not.Null);
                Assert.That(v1.InflatedMessage.StandardHeader?.PossDupFlag, Is.True);
                Assert.That(v1.InflatedMessage?.StandardHeader?.OrigSendingTime, Is.Not.Null);
            });

            for (var i = 2; i <= 6; ++i)
            {
                var v = vec[i];
                Assert.Multiple(() =>
                {
                    Assert.That(v.MsgType, Is.EqualTo(MsgTypeValues.TradeCaptureReport));
                    Assert.That(v.SeqNum, Is.EqualTo(i + 1));
                    Assert.That(v.InflatedMessage?.StandardHeader, Is.Not.Null);
                    Assert.That(v.InflatedMessage.StandardHeader?.PossDupFlag, Is.True);
                    Assert.That(v.InflatedMessage?.StandardHeader?.OrigSendingTime, Is.Not.Null);
                });
            }

            Assert.Multiple(() =>
            {
                var v1 = vec[7];
                Assert.That(v1.MsgType, Is.EqualTo(MsgTypeValues.TradeCaptureReportRequestAck));
                Assert.That(v1.SeqNum, Is.EqualTo(8));
                Assert.That(v1.InflatedMessage?.StandardHeader, Is.Not.Null);
                Assert.That(v1.InflatedMessage.StandardHeader?.PossDupFlag, Is.True);
                Assert.That(v1.InflatedMessage?.StandardHeader?.OrigSendingTime, Is.Not.Null);
            });

            for (var i = 8; i < 10; ++i)
            {
                var v = vec[i];
                Assert.Multiple(() =>
                {
                    Assert.That(v.MsgType, Is.EqualTo(MsgTypeValues.TradeCaptureReport));
                    Assert.That(v.SeqNum, Is.EqualTo(i + 1));
                    Assert.That(v.InflatedMessage?.StandardHeader, Is.Not.Null);
                    Assert.That(v.InflatedMessage.StandardHeader?.PossDupFlag, Is.True);
                    Assert.That(v.InflatedMessage?.StandardHeader?.OrigSendingTime, Is.Not.Null);
                });
            }
        }

        [Test]
        public async Task Client_Replay_Request_Seq_1_To_10_Test()
        {
            var store = await _testEntity.MakeMsgStore(_views, _client_config?.Description?.SenderCompID);
            var msgFactory = new FixMessageFactory();
            var clock = new TestClock { Current = DateTime.Today.AddHours(8) };
            Assert.That(_client_config, Is.Not.Null);
            IFixMsgResender replayer = new FixMsgAsciiStoreResend(store, msgFactory, _client_config, clock);
            var state = await store.GetState();
            Assert.That(state.Length, Is.EqualTo(1));
            var vec = await replayer.GetResendRequest(1, 10);
            Assert.That(vec, Is.Not.Null);
            Assert.That(vec, Has.Count.EqualTo(3));

            CheckSeqReset(vec[0], 1, 2);

            Assert.Multiple(() =>
            {
                var v1 = vec[1];
                Assert.That(v1.MsgType, Is.EqualTo(MsgTypeValues.TradeCaptureReportRequest));
                Assert.That(v1.SeqNum, Is.EqualTo(2));
                Assert.That(v1.InflatedMessage?.StandardHeader, Is.Not.Null);
                Assert.That(v1.InflatedMessage.StandardHeader?.PossDupFlag, Is.True);
                Assert.That(v1.InflatedMessage.StandardHeader?.OrigSendingTime, Is.Not.Null);
            });

            CheckSeqReset(vec[2], 3, 11);
        }


        private void CheckSeqReset(IFixMsgStoreRecord rec, int from, int to)
        {
            var reset = rec.InflatedMessage as SequenceReset;
            Assert.That(reset, Is.Not.Null);
            Assert.Multiple(() =>
            {
                Assert.That(rec.MsgType, Is.EqualTo(MsgTypeValues.SequenceReset));
                Assert.That(rec.InflatedMessage, Is.Not.Null);
                Assert.That(rec.SeqNum, Is.EqualTo(from));
                Assert.That(reset.NewSeqNo, Is.EqualTo(to));
                Assert.That(reset.GapFillFlag, Is.True);
                Assert.That(reset.StandardHeader, Is.Not.Null);
                Assert.That(reset.StandardHeader.MsgType, Is.EqualTo(MsgTypeValues.SequenceReset));
                Assert.That(reset.StandardHeader.PossDupFlag, Is.True);
                Assert.That(reset.StandardHeader.MsgSeqNum, Is.EqualTo(from));
            });
        }
    }
}
