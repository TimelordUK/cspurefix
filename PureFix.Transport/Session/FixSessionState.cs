using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;

namespace PureFix.Transport.Session
{
    public class FixSessionState
    {
        private TickAction? m_nextTickAction;

        public DateTime? LastReceivedAt { get; set; }
        public DateTime? LastSentAt { get; set; }
        public DateTime? LogoutSentAt { get; set; }
        public DateTime? LastTestRequestAt { get; set; }
        public SessionState? State { get; set; }
        public int? PeerHeartBeatSecs { get; set; }
        public int? LastPeerMsgSeqNum { get; set; }
        public DateTime? Now { get; set; }
        public string? CompID { get; set; }
        public string? PeerCompID { get; set; }
        private readonly int? m_heartBeat;
        private readonly int? m_waitLogoutConfirmSeconds;
        private readonly int? m_stopSeconds;
        private int? m_secondsSinceLogoutSent = -1;
        private int? m_secondsSinceSent = -1;
        private int? m_secondsSinceReceive = -1;
        private IStandardHeader? m_lastHeader;

        public int LastSentSeqNum => m_lastHeader?.MsgSeqNum ?? 0;
        public bool TimeToHeartbeat => m_secondsSinceSent >= m_heartBeat;
        public bool TimeToTerminate => LastTestRequestAt != null && m_secondsSinceReceive >= 2.5 * PeerHeartBeatSecs;
        public bool TimeToDie => m_secondsSinceLogoutSent > m_waitLogoutConfirmSeconds ||
                                 m_secondsSinceLogoutSent > m_stopSeconds;
        public bool TimeToTestRequest => m_secondsSinceReceive >= 1.5 * PeerHeartBeatSecs;

        public FixSessionState(FixSessionStateArgs args)
        {
            m_heartBeat = args.HeartBeat;
            State = args.State ?? SessionState.Idle;
            m_waitLogoutConfirmSeconds = args.WaitLogoutConfirmSeconds;
            m_stopSeconds = args.StopSeconds;
            LastPeerMsgSeqNum = args.LastPeerMsgSeqNum;
            m_waitLogoutConfirmSeconds = 5;
            m_stopSeconds = 2;
            LastPeerMsgSeqNum = 0;
        }

        public void Reset(int lastPeerMsgSeqNum)
        {
            LastReceivedAt = null;
            LastSentAt = null;
            LastTestRequestAt = null;
            m_secondsSinceLogoutSent = -1;
            m_secondsSinceSent = -1;
            m_secondsSinceReceive = -1;
            PeerHeartBeatSecs = 0;
            LogoutSentAt = null;
            m_nextTickAction = TickAction.Nothing;
            LastPeerMsgSeqNum = lastPeerMsgSeqNum;
            m_lastHeader = null;
        }

        public override string ToString()
        {
            var buffer = new StringBuilder(512);

            buffer.Append("compId=").Append(CompID)
                .Append(", heartBeat=").Append(m_heartBeat)
                .Append(", state=").Append(State)
                .Append(", nextTickAction=").Append(m_nextTickAction)
                .Append(", now=").Append(DateAsString(Now))
                .Append(", timeToDie=").Append(TimeToDie)
                .Append(", timeToHeartbeat=").Append(TimeToHeartbeat)
                .Append(", timeToTerminate=").Append(TimeToTerminate)
                .Append(", timeToTestRequest=").Append(TimeToTestRequest)
                .Append(", lastReceivedAt=").Append(DateAsString(LastReceivedAt))
                .Append(", lastSentAt=").Append(DateAsString(LastSentAt))
                .Append(", lastTestRequestAt=").Append(DateAsString(LastTestRequestAt))
                .Append(", logoutSentAt=").Append(DateAsString(LogoutSentAt))
                .Append(", peerHeartBeatSecs=").Append(PeerHeartBeatSecs)
                .Append(", peerCompId=").Append(PeerCompID)
                .Append(", lastPeerMsgSeqNum=").Append(LastPeerMsgSeqNum)
                .Append(", lastSentSeqNum=").Append(LastSentSeqNum)
                .Append(", secondsSinceLogoutSent=").Append(m_secondsSinceLogoutSent)
                .Append(", secondsSinceSent=").Append(m_secondsSinceSent)
                .Append(", secondsSinceReceive=").Append(m_secondsSinceReceive);

            return buffer.ToString();
        }
       
        public static string DateAsString(DateTime? date)
        {
            return date == null ? "na" : date.Value.ToString("HH:mm:ss.fff");
        }

        public TickAction? CalcAction(DateTime now)
        {
            Now = now;
            CalcState();

            switch (State)
            {
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
                        if (TimeToDie)
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
                        if (TimeToTerminate)
                        {
                            m_nextTickAction = TickAction.TerminateOnError;
                        }
                        else if (TimeToTestRequest)
                        {
                            if (LastTestRequestAt == null)
                            {
                                // not received anything from peer
                                m_nextTickAction = TickAction.TestRequest;
                              
                            }
                        }
                        else if (TimeToHeartbeat)
                        {
                            // have not sent anything for heartbeat period so let other side know still alive.
                            m_nextTickAction = TickAction.Heartbeat;
                        }
                        break;
                    }
            }
            return m_nextTickAction;
        }

        private void CalcState()
        {
            if (Now == null) return;
            m_nextTickAction = TickAction.Nothing;
            m_secondsSinceLogoutSent = LogoutSentAt != null
                ? (int)(Now.Value - LogoutSentAt.Value).TotalSeconds
                : -1;

            m_secondsSinceSent = LastSentAt != null
                ? (int)(Now.Value - LastSentAt.Value).TotalSeconds
                : 0;

            m_secondsSinceReceive = LastReceivedAt != null
                ? (int)(Now.Value - LastReceivedAt.Value).TotalSeconds
                : 0;
        }
    }
}
