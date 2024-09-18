using PureFIix.Test.Env;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Transport.Session;

namespace PureFIix.Test.Ascii
{
    public class SessionStateTest
    {
        private FixSessionState m_state;
        private DateTime m_now;
        [SetUp]
        public void Setup()
        {
            m_state = new FixSessionState(new FixSessionStateArgs { HeartBeat = 30, State = SessionState.InitiationLogonResponse});
            m_now = new DateTime(2018, 1, 1, 20, 0, 0);
            m_state.Now = m_now;
            m_state.LastSentAt = m_now;
            m_state.LastReceivedAt = m_now;
            m_state.PeerHeartBeatSecs = 30;
            m_state.LastPeerMsgSeqNum = 1;
        }

        [Test]
        public void Do_Nothing_Test()
        {
            var action = m_state.CalcAction(m_now);
            Assert.That(action, Is.EqualTo(TickAction.Nothing));
        }

        [Test]
        public void Heartbeat_Test()
        {
            Assert.That(m_state.Now, Is.Not.Null);
            var next = m_state.Now.Value.Add(TimeSpan.FromSeconds(31));
            m_state.LastReceivedAt = next;
            var action = m_state.CalcAction(next);
            Assert.Multiple(() =>
            {
                Assert.That(m_state.TimeToDie, Is.False);
                Assert.That(m_state.TimeToHeartbeat, Is.True);
                Assert.That(m_state.TimeToTerminate, Is.False);
                Assert.That(m_state.TimeToTestRequest, Is.False);
                Assert.That(action, Is.EqualTo(TickAction.Heartbeat));
            });
        }

        [Test]
        public void TestRequest_Test()
        {
            Assert.That(m_state.Now, Is.Not.Null);
            var next = m_state.Now.Value.Add(TimeSpan.FromSeconds(51));
            m_state.LastSentAt = next;
            var action = m_state.CalcAction(next);
            Assert.Multiple(() =>
            {
                Assert.That(m_state.TimeToDie, Is.False);
                Assert.That(m_state.TimeToHeartbeat, Is.False);
                Assert.That(m_state.TimeToTerminate, Is.False);
                Assert.That(m_state.TimeToTestRequest, Is.True);
                Assert.That(action, Is.EqualTo(TickAction.TestRequest));
            });
        }

        [Test]
        public void TestRequest_No_Response_Test()
        {
            Assert.That(m_state.Now, Is.Not.Null);
            var next = m_state.Now.Value.Add(TimeSpan.FromSeconds(55 * 2));
            m_state.LastSentAt = next;
            m_state.LastTestRequestAt = m_now;
            var action = m_state.CalcAction(next);
            Assert.Multiple(() =>
            {
                Assert.That(m_state.TimeToDie, Is.False);
                Assert.That(m_state.TimeToHeartbeat, Is.False);
                Assert.That(m_state.TimeToTerminate, Is.True);
                Assert.That(m_state.TimeToTestRequest, Is.True);
                Assert.That(action, Is.EqualTo(TickAction.TerminateOnError));
            });
        }
    }
}
