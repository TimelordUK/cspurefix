using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;

namespace PureFix.Transport.Session
{
    public class FixSessionState
    {
        private TickAction? m_nextTickAction;
        private DateTime? m_lastReceivedAt;
        public DateTime? LastReceivedAt
        {
            get => m_lastReceivedAt;
            set => m_lastReceivedAt = value;
        }
        private DateTime? m_lastSentAt;
        public DateTime? LastSentAt
        {
            get => m_lastSentAt;
            set => m_lastSentAt = value;
        }
        private DateTime? m_lastTestRequestAt;
        private DateTime? m_logoutSentAt;
        private DateTime? m_now;
        public DateTime? Now
        {
            get => m_now;
            set => m_now = value;
        }
        private string? m_compID;
        private string? m_peerCompID;
        private int? m_peerHeartBeatSecs;
        public int? PeerHeartBeatSecs
        {
            get => m_peerHeartBeatSecs;
            set => m_peerHeartBeatSecs = value;
        }
        private int? m_lastPeerMsgSeqNum;
        public int? LastPeerMsgSeqNum
        {
            get => m_lastPeerMsgSeqNum;
            set => m_lastPeerMsgSeqNum = value;
        }
        private readonly int? m_heartBeat;
        private SessionState m_state;
        private readonly int? m_waitLogoutConfirmSeconds;
        private readonly int? m_stopSeconds;
        private int? m_secondsSinceLogoutSent = -1;
        private int? m_secondsSinceSent = -1;
        private int? m_secondsSinceReceive = -1;
        private IStandardHeader? m_lastHeader;

        public FixSessionState(FixSessionStateArgs args)
        {
            m_heartBeat = args.HeartBeat;
            m_state = SessionState.Idle;
            m_waitLogoutConfirmSeconds = args.WaitLogoutConfirmSeconds;
            m_stopSeconds = args.StopSeconds;
            m_lastPeerMsgSeqNum = args.LastPeerMsgSeqNum;
        }

        public void Reset(int lastPeerMsgSeqNum)
        {
            m_lastReceivedAt = null;
            m_lastSentAt = null;
            m_lastTestRequestAt = null;
            m_secondsSinceLogoutSent = -1;
            m_secondsSinceSent = -1;
            m_secondsSinceReceive = -1;
            m_peerHeartBeatSecs = 0;
            m_logoutSentAt = null;
            m_nextTickAction = TickAction.Nothing;
            m_lastPeerMsgSeqNum = lastPeerMsgSeqNum;
            m_lastHeader = null;
        }

        public override string ToString()
        {
            var buffer = new StringBuilder();

            buffer.AppendFormat($"compId = {m_compID}, ");
            buffer.AppendFormat($"heartBeat = {m_heartBeat}, ");
            buffer.AppendFormat($"state = {m_state}, ");
            buffer.AppendFormat($"nextTickAction = {m_nextTickAction}, ");
            buffer.AppendFormat($"now = ${DateAsString(m_now)}, ");
            buffer.AppendFormat($"timeToDie = ${TimeToDie()}, ");
            buffer.AppendFormat($"timeToHeartbeat = ${TimeToHeartbeat()}, ");
            buffer.AppendFormat($"timeToTerminate = ${TimeToTerminate()}, ");
            buffer.AppendFormat($"timeToTestRequest = ${TimeToTestRequest()}, ");
            buffer.AppendFormat($"lastReceivedAt = ${DateAsString(m_lastReceivedAt)}, ");
            buffer.AppendFormat($"LastSentAt = ${DateAsString(m_lastSentAt)}, ");
            buffer.AppendFormat($"lastTestRequestAt = ${DateAsString(m_lastTestRequestAt)}, ");
            buffer.AppendFormat($"logoutSentAt = ${DateAsString(m_logoutSentAt)}, ");
            buffer.AppendFormat($"peerHeartBeatSecs = ${m_peerHeartBeatSecs}, ");
            buffer.AppendFormat($"peerCompId = ${m_peerCompID}, ");
            buffer.AppendFormat($"lastPeerMsgSeqNum = ${m_lastPeerMsgSeqNum}, ");
            buffer.AppendFormat($"LastSentSeqNum = ${LastSentSeqNum()}, ");
            buffer.AppendFormat($"secondsSinceLogoutSent = ${m_secondsSinceLogoutSent}, ");
            buffer.AppendFormat($"secondsSinceSent = ${m_secondsSinceSent}, ");
            buffer.AppendFormat($"secondsSinceReceive = ${m_secondsSinceReceive}");

            return buffer.ToString();
        }

        public int LastSentSeqNum()
        {
            return m_lastHeader?.MsgSeqNum ?? 0;
        }

        private bool TimeToHeartbeat()
        {
            return m_secondsSinceSent >= m_heartBeat;
        }

        private bool TimeToTerminate()
        {
            return m_secondsSinceReceive >= 2.5 * m_peerHeartBeatSecs;
        }

        private bool TimeToDie()
        {
            return m_secondsSinceLogoutSent > m_waitLogoutConfirmSeconds ||
                   m_secondsSinceLogoutSent > m_stopSeconds;
        }

        private bool TimeToTestRequest()
        {
            return m_secondsSinceReceive >= 1.5 * m_peerHeartBeatSecs;
        }

        public static string DateAsString(DateTime? date)
        {
            return date == null ? "na" : date.Value.ToString("HH:mm:ss.fff");
        }

        public TickAction? CalcAction(DateTime now)
        {
            m_now = now;
            CalcState();

            switch (m_state) {
                case SessionState.PeerLogonRejected:
                {
                    if (m_secondsSinceSent >= m_stopSeconds)
                    {
                        m_nextTickAction = TickAction.Stop;
                    }
                    break;
                }

                case SessionState.WaitingLogoutConfirm:
                case SessionState.ConfirmingLogout:
                {
                    if (TimeToDie())
                    {
                        m_nextTickAction = TickAction.Stop;
                    }
                    break;
                }

                case SessionState.ActiveNormalSession:
                case SessionState.AwaitingProcessingResponseToTestRequest:
                case SessionState.InitiationLogonReceived:
                case SessionState.InitiationLogonResponse:
                {
                    if (TimeToHeartbeat())
                    {
                            // have not sent anything for heartbeat period so let other side know still alive.
                            m_nextTickAction = TickAction.Heartbeat;
                    }
                    else
                    {
                        // console.log(`${application.name}: secondsSinceSent = ${secondsSinceSent} secondsSinceReceive = ${secondsSinceReceive}`)
                        if (TimeToTerminate())
                        {
                            m_nextTickAction = TickAction.TerminateOnError;
                        }
                        else if (TimeToTestRequest())
                        {
                            if (m_lastTestRequestAt == null)
                            {
                                // not received anything from peer
                                m_nextTickAction = TickAction.TestRequest;
                                m_lastTestRequestAt = now;
                            }
                        }
                    }
                    break;
                }
            }
            return m_nextTickAction;
        }

        private void CalcState()
        {
            if (m_now == null) return;
            var time = m_now.Value.TimeOfDay;
            m_nextTickAction = TickAction.Nothing;
            m_secondsSinceLogoutSent = m_logoutSentAt != null
                ? (int)time.TotalSeconds - (int)m_logoutSentAt.Value.TimeOfDay.TotalSeconds
                : -1;

            m_secondsSinceSent = m_lastSentAt != null
                ? (int)time.TotalSeconds - (int)m_lastSentAt.Value.TimeOfDay.TotalMilliseconds
                : 0;

            m_secondsSinceReceive = m_lastReceivedAt != null
                ? (int)time.TotalSeconds - (int)m_lastReceivedAt.Value.TimeOfDay.TotalMilliseconds
                : 0;
        }
    }
}
