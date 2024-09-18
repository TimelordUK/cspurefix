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

        public DateTime? LastReceivedAt { get; set; }
        public DateTime? LastSentAt { get; set; }
        public DateTime? LogoutSentAt { get; set; }
        public DateTime? LastTestRequestAt { get; set; }
        public SessionState? State { get; set; }
        public int? PeerHeartBeatSecs { get; set; }
        public int? LastPeerMsgSeqNum { get; set; }
        public DateTime? Now { get; set; }

        private string? m_compID;
        private string? m_peerCompID;
        private readonly int? m_heartBeat;
        private readonly int? m_waitLogoutConfirmSeconds;
        private readonly int? m_stopSeconds;
        private int? m_secondsSinceLogoutSent = -1;
        private int? m_secondsSinceSent = -1;
        private int? m_secondsSinceReceive = -1;
        private IStandardHeader? m_lastHeader;

        public int LastSentSeqNum => m_lastHeader?.MsgSeqNum ?? 0;
        public bool TimeToHeartbeat => m_secondsSinceSent >= m_heartBeat;
        public bool TimeToTerminate => m_secondsSinceReceive >= 2.5 * PeerHeartBeatSecs;
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
            var buffer = new StringBuilder();

            buffer.AppendFormat($"compId = {m_compID}, ");
            buffer.AppendFormat($"heartBeat = {m_heartBeat}, ");
            buffer.AppendFormat($"state = {State}, ");
            buffer.AppendFormat($"nextTickAction = {m_nextTickAction}, ");
            buffer.AppendFormat($"now = ${DateAsString(Now)}, ");
            buffer.AppendFormat($"timeToDie = ${TimeToDie}, ");
            buffer.AppendFormat($"timeToHeartbeat = ${TimeToHeartbeat}, ");
            buffer.AppendFormat($"timeToTerminate = ${TimeToTerminate}, ");
            buffer.AppendFormat($"timeToTestRequest = ${TimeToTestRequest}, ");
            buffer.AppendFormat($"lastReceivedAt = ${DateAsString(LastReceivedAt)}, ");
            buffer.AppendFormat($"LastSentAt = ${DateAsString(LastSentAt)}, ");
            buffer.AppendFormat($"lastTestRequestAt = ${DateAsString(LastTestRequestAt)}, ");
            buffer.AppendFormat($"logoutSentAt = ${DateAsString(LogoutSentAt)}, ");
            buffer.AppendFormat($"peerHeartBeatSecs = ${PeerHeartBeatSecs}, ");
            buffer.AppendFormat($"peerCompId = ${m_peerCompID}, ");
            buffer.AppendFormat($"lastPeerMsgSeqNum = ${LastPeerMsgSeqNum}, ");
            buffer.AppendFormat($"LastSentSeqNum = ${LastSentSeqNum}, ");
            buffer.AppendFormat($"secondsSinceLogoutSent = ${m_secondsSinceLogoutSent}, ");
            buffer.AppendFormat($"secondsSinceSent = ${m_secondsSinceSent}, ");
            buffer.AppendFormat($"secondsSinceReceive = ${m_secondsSinceReceive}");

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

            switch (State) {
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
                    if (TimeToHeartbeat)
                    {
                            // have not sent anything for heartbeat period so let other side know still alive.
                            m_nextTickAction = TickAction.Heartbeat;
                    }
                    else
                    {
                        // console.log(`${application.name}: secondsSinceSent = ${secondsSinceSent} secondsSinceReceive = ${secondsSinceReceive}`)
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
                                LastTestRequestAt = now;
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
            if (Now == null) return;
            var time = Now.Value.TimeOfDay;
            m_nextTickAction = TickAction.Nothing;
            m_secondsSinceLogoutSent = LogoutSentAt != null
                ? (int)time.TotalSeconds - (int)LogoutSentAt.Value.TimeOfDay.TotalSeconds
                : -1;

            m_secondsSinceSent = LastSentAt != null
                ? (int)time.TotalSeconds - (int)LastSentAt.Value.TimeOfDay.TotalSeconds
                : 0;

            m_secondsSinceReceive = LastReceivedAt != null
                ? (int)time.TotalSeconds - (int)LastReceivedAt.Value.TimeOfDay.TotalSeconds
                : 0;
        }
    }
}
