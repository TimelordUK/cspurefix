using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Runtime.InteropServices.JavaScript;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;

namespace PureFix.Transport.Ascii
{
    public abstract class AsciiSession : FixSession
    {
        protected readonly IFixMsgStore m_msgStore;
        private readonly IFixMsgResender m_resender;
        private readonly IFixMessageFactory m_fixMessageFactory;
        public bool Heartbeat { get; set; }
        protected AsciiSession(IFixConfig config, IMessageTransport transport, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixClock clock)
            : base(config, transport, parser, encoder, clock)
        {
            if (config == null) throw new ArgumentNullException("config must be provided");
            if (config?.Description?.SenderCompID == null) throw new ArgumentNullException("config must have application description with SenderCompID");
            m_fixMessageFactory = fixMessageFactory;
            m_msgStore = new FixMsgMemoryStore(config.Description.SenderCompID);
            m_resender = new FixMsgAsciiStoreResend(m_msgStore, fixMessageFactory, config, clock);
        }

        private async Task SendTestRequest()
        {
            SetState(SessionState.AwaitingProcessingResponseToTestRequest);
            var now = m_clock.Current.ToUniversalTime().ToString(CultureInfo.InvariantCulture);
            var text = $"test-req-{now}";
            var tr = m_factory?.TestRequest(text);
            if (tr != null)
            {
                await Send(MsgType.TestRequest, tr);
            }
        }

        async Task SendHeartbeat(string? text = null)
        {
            var now = m_clock.Current.ToUniversalTime().ToString(CultureInfo.InvariantCulture);
            text ??= $"heartbeat-{now}";
            var tr = m_factory?.Heartbeat(text);
            if (tr != null)
            {
                await Send(MsgType.Heartbeat, tr);
            }
        }

        protected bool ValidStateApplicationMsg() {
            switch (m_sessionState.State)
            {
                case SessionState.Idle:
                case SessionState.InitiateConnection:
                case SessionState.InitiationLogonSent:
                case SessionState.WaitingForALogon:
                case SessionState.HandleResendRequest:
                case SessionState.AwaitingProcessingResponseToTestRequest:
                case SessionState.AwaitingProcessingResponseToResendRequest:
                    return false;
                default:
                    return true;
            }
        }

        protected override async Task Tick()
        {
            var action = m_sessionState.CalcAction(m_clock.Current);
            switch (action)
            {
                case TickAction.Nothing:
                    break;

                case TickAction.TestRequest:
                    {
                        m_sessionLogger?.Debug($"sending test req. state = {m_sessionState.State}");
                        await SendTestRequest();
                        break;
                    }

                case TickAction.Heartbeat:
                    {
                        m_sessionLogger?.Debug($"sending heartbeat. state = {m_sessionState.State}");
                        await SendHeartbeat();
                        break;
                    }

                case TickAction.TerminateOnError:
                    {
                        m_sessionLogger?.Warn(m_sessionState.ToString());
                        break;
                    }
            }
        }

        private async Task PeerLogon(MsgView view)
        {
            var logger = m_sessionLogger;
            var heartBtInt = view.HeartBtInt();
            var peerCompId = view.SenderCompID();
            var userName = view.Username();
            var password = view.Password();
            logger?.Info($"peerLogon Username = {userName}, heartBtInt = {heartBtInt}, peerCompId = {peerCompId}, userName = {userName}");
            var state = m_sessionState;
            state.PeerHeartBeatSecs = view.MsgSeqNum();
            state.PeerCompID = peerCompId;
            var res = OnLogon(view, userName, password);
            // currently not using this.
            logger?.Info($"peerLogon onLogon returns {res}");
            if (m_acceptor)
            {
                SetState(SessionState.InitiationLogonResponse);
                logger?.Info("acceptor responds to logon request");
                await SendLogon();  // if res send response else reject, terminate
            }
            else
            { // as an initiator the acceptor has responded
                logger?.Info("initiator receives logon response");
                SetState(SessionState.InitiationLogonReceived);
            }
            if (Heartbeat)
            {
                //   this.startTimer();
            }
            logger?.Info("system ready, inform app");
            OnReady(view);
        }

        private async Task<bool> CheckSeqNo(string msgType, MsgView view)
        {
            switch (msgType)
            {
                case MsgType.SequenceReset:
                    {
                        return true;
                    }

                default:
                    {
                        var state = m_sessionState;
                        var lastSeq = state.LastPeerMsgSeqNum;
                        var seqNo = view.MsgSeqNum();
                        if (lastSeq == null) return false;
                        if (seqNo == null) return false;
                        bool ret = false;
                        var seqDelta = seqNo - lastSeq;
                        if (seqDelta <= 0)
                        {
                            // serious problem ... drop immediately
                            m_sessionLogger?.Warn($"terminate as seqDelta({seqDelta}) < 0 lastSeq = {lastSeq} seqNo = {seqNo}");
                            Stop();
                        }
                        else if (seqDelta > 1)
                        {
                            // resend request required as have missed messages.
                            // We process a Logon beforehand to confirm the connection even we out of sync
                            if (msgType == MsgType.Logon)
                            {
                                await PeerLogon(view);
                            }
                            // If the out of sync message is a resend request itself, then we handle it first in order
                            // to avoid triggering an endless loop of both sides sending resend requests in response to resend requests.
                            if (msgType == MsgType.ResendRequest)
                            {
                                await OnResendRequest(view);
                            }
                            await SendResendRequest(lastSeq.Value, seqNo.Value);
                        }
                        else
                        {
                            ret = true;
                            state.LastPeerMsgSeqNum = seqNo;
                        }
                        return ret;
                    }
            }
        }

        protected async Task SendResendRequest(int lastSeq, int receivedSeq)
        {
            var resend = m_config.MessageFactory?.ResendRequest(lastSeq + 1, 0);
            if (resend != null)
            {
                m_sessionLogger?.Warn($"received seq {receivedSeq}, but last known seq is {lastSeq}. Sending resend request for all messages > {lastSeq}");
                await Send(MsgType.ResendRequest, resend);
            }
        }

        private async Task SendReject(string msgType, int seqNo, string msg, int reason)
        {
            var factory = m_config.MessageFactory;
            var reject = factory?.Reject(msgType, seqNo, msg, reason);
            if (reject != null)
            {
                m_sessionLogger?.Warn($"rejecting with {JsonHelper.ToJson(reject)}");
                await Send(MsgType.Reject, reject);
            }
        }


/**
* Override to resend stored messages following a sequence reset.
* @protected
*/

        protected async Task OnResendRequest(MsgView view)
        {
            // if no records are in store then send a gap fill for entire sequence
            SetState(SessionState.HandleResendRequest);
            var beginSeqNo = view.BeginSeqNo();
            var requestedEndSeqNo = view.EndSeqNo();
            if (beginSeqNo == null) return;
            if (requestedEndSeqNo == null) return;
            var endSeqNo = requestedEndSeqNo == 0 ? m_sessionState.LastSentSeqNum : requestedEndSeqNo.Value;

            m_sessionLogger?.Info($"onResendRequest getResendRequest beginSeqNo = {beginSeqNo}, endSeqNo = {endSeqNo}");
            var records = await m_resender.GetResendRequest(beginSeqNo.Value, endSeqNo);
            m_sessionLogger?.Info($"sending {records.Count}");
            foreach (var rec in records)
            {
                if (rec.InflatedMessage != null)
                {
                    await Send(rec.MsgType, rec.InflatedMessage);
                }
            }
            SetState(SessionState.ActiveNormalSession);
        }

        bool OkForLogon() {
            var state = m_sessionState.State;
            if (m_acceptor) {
                return state == SessionState.WaitingForALogon;
            }
            return state == SessionState.InitiationLogonSent;
        }

        protected async Task OnSessionMsg(string msgType, MsgView view)
        {
            var logger = m_sessionLogger;

            switch (msgType)
            {
                case MsgType.Logon:
                    {
                        // only valid to receive a logon when in LogonSent or WaitingALogon
                        // else will drop connection immediately.
                        if (OkForLogon())
                        {
                            await PeerLogon(view);
                        }
                        else
                        {
                            Terminate(new InvalidDataException($"state ${m_sessionState.State} is illegal for Logon"));
                        }
                        break;
                    }

                case MsgType.Logout:
                    {
                        await PeerLogout(view);
                        break;
                    }

                case MsgType.TestRequest:
                    {
                        var req = view.GetString((int)MsgTag.TestReqID);
                        if (req != null)
                        {
                            await SendHeartbeat(req);
                        }
                        break;
                    }

                case MsgType.Heartbeat:
                    {
                        m_sessionState.LastTestRequestAt = null;
                        SetState(SessionState.ActiveNormalSession);
                        break;
                    }

                case MsgType.ResendRequest:
                    {
                        logger?.Info($"peer sends '${msgType}' resend request.");
                        await OnResendRequest(view);
                        break;
                    }

                case MsgType.SequenceReset:
                    {
                        var newSeqNo = view.GetInt32((int)MsgTag.NewSeqNo);
                        logger?.Info($"peer sends '{msgType}' sequence reset.newSeqNo = {newSeqNo}");
                        // expect  newSeqNo to be the next message's sequence number.
                        m_sessionState.LastPeerMsgSeqNum = newSeqNo - 1;
                        break;
                    }

                case MsgType.Reject:
                    {
                        logger?.Info($"peer rejects type '{msgType}' with text '{view.GetString((int)MsgTag.Text)}");
                        break;
                    }
            }
        }

        protected void StartTimer()
        {
        }

        protected override async Task OnMsg(string msgType, MsgView view)
        {
            var checkSeqNo = await CheckSeqNo(msgType, view);
            if (!checkSeqNo)
            {
                m_sessionLogger?.Info($"message '{msgType}' failed checkSeqNo.");
                return;
            }

            if (m_checkMsgIntegrity)
            {
                var checkIntegrity = await CheckIntegrity(msgType, view);
                if (!checkIntegrity)
                {
                    m_sessionLogger?.Info($"message '${msgType}' failed checkIntegrity.");
                    switch (msgType)
                    {
                        case MsgType.Logon:
                            {
                                SetState(SessionState.PeerLogonRejected);
                                StartTimer();
                            }
                            break;
                    }
                    return;
                }
            }

            switch (msgType)
            {
                case MsgType.Logon:
                case MsgType.Logout:
                case MsgType.TestRequest:
                case MsgType.Reject:
                case MsgType.SequenceReset:
                case MsgType.Heartbeat:
                case MsgType.ResendRequest:
                    {
                        await OnSessionMsg(msgType, view);
                        break;
                    }

                default:
                    {
                        await CheckForwardMsg(msgType, view);
                        break;
                    }
            }
        }

        private async Task<bool> CheckIntegrity(string msgType, MsgView view)
        {
            var state = m_sessionState;
            var seqNum = view.GetInt32((int)MsgTag.MsgSeqNum);
            if (seqNum == null) return false;

            var received = int.Parse(view.GetString((int)MsgTag.CheckSum) ?? "");
            var computed = view.Checksum();
            if (received != computed)
            {
                var msg = $"msgType {msgType} checksum failed. received = {received} computed = {computed}";
                await SendReject(msgType, seqNum.Value, msg, (int)SessionRejectReason.ValueIsIncorrect);
                return false;
            }

            return true;
        }

        private void Terminate(Exception ex)
        {
        }

        protected async Task CheckForwardMsg(string msgType, MsgView view)
        {
            var okToForward = ValidStateApplicationMsg();
            if (okToForward)
            {
                m_sessionLogger?.Info($"ascii forwarding msgType = '{msgType}' to application");
                SetState(SessionState.ActiveNormalSession);
                await OnApplicationMsg(msgType, view);
            }
            else
            {
                Terminate(new Exception($"msgType ${msgType} received in state {m_sessionState.State}"));
            }
        }
    }
}
 

