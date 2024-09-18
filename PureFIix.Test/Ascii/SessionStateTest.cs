using PureFIix.Test.Env;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Transport.Session;
using PureFix.Types.FIX50SP2.QuickFix.Types;

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
    }
}
