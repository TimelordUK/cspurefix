using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.Core;


namespace PureFix.Transport.Ascii
{
    public abstract class AsciiSession : FixSession
    {
        protected readonly IFixSessionStore m_sessionStore;
        private readonly IFixMessageFactory m_fixMessageFactory;
        private FixMsgAsciiStoreResend? m_resender;
        public bool Heartbeat { get; set; } = true;

        protected AsciiSession(IFixConfig config, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, AsyncWorkQueue q, IFixClock clock)
            : base(config, logFactory, parser, encoder, q, clock)
        {
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(config.Description);
            ArgumentException.ThrowIfNullOrEmpty(config.Description.SenderCompID);
            ArgumentException.ThrowIfNullOrEmpty(config.Description.TargetCompID);
            ArgumentException.ThrowIfNullOrEmpty(config.Description.BeginString);

            m_fixMessageFactory = fixMessageFactory;

            // Create session store from factory (defaults to in-memory if not configured)
            var storeFactory = config.SessionStoreFactory ?? new MemorySessionStoreFactory();
            var sessionId = new SessionId(
                config.Description.BeginString,
                config.Description.SenderCompID,
                config.Description.TargetCompID);
            m_sessionStore = storeFactory.Create(sessionId);

            m_sessionLogger?.Info("{Definitions}", Definitions);
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
                m_sessionState.LastTestRequestAt = m_clock.Current;
            }
        }

        private async Task SendHeartbeat(string? text = null)
        {
            var now = m_clock.Current.ToUniversalTime().ToString(CultureInfo.InvariantCulture);
            text ??= $"heartbeat-{now}";
            var tr = m_factory?.Heartbeat(text);
            if (tr != null)
            {
                await Send(MsgType.Heartbeat, tr);
            }
        }

        private bool ValidStateApplicationMsg() {
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
                        m_sessionLogger?.Debug("sending test req. state = {State}", m_sessionState.State);
                        await SendTestRequest();
                        break;
                    }

                case TickAction.Heartbeat:
                    {
                        if (Heartbeat)
                        {
                            m_sessionLogger?.Debug("sending heartbeat. state = {State}", m_sessionState.State);
                            await SendHeartbeat();
                        }
                        break;
                    }

                case TickAction.TerminateOnError:
                    {
                        m_sessionLogger?.Warn(m_sessionState.ToString());
                        break;
                    }
            }
        }

        private async Task PeerLogon(IMessageView view)
        {
            var logger = m_sessionLogger;
            var heartBtInt = view.HeartBtInt();
            var peerCompId = view.SenderCompID();
            var userName = view.Username();
            var password = view.Password();
            var resetSeqNumFlag = view.ResetSeqNumFlag();
            logger?.Info($"peerLogon Username = {userName}, heartBtInt = {heartBtInt}, peerCompId = {peerCompId}, resetSeqNumFlag = {resetSeqNumFlag}");

            // Handle ResetSeqNumFlag from peer's logon
            // When peer sends ResetSeqNumFlag=Y, both sides should reset to 1.
            if (resetSeqNumFlag == true)
            {
                var peerSeqNum = view.MsgSeqNum() ?? 1;
                var weAlsoReset = m_config.ResetSeqNumFlag();

                logger?.Info("Peer sent ResetSeqNumFlag=Y with SeqNum={PeerSeqNum}, weAlsoReset={WeAlsoReset}", peerSeqNum, weAlsoReset);

                // Always reset our expected incoming sequence
                m_sessionState.LastPeerMsgSeqNum = peerSeqNum;
                await m_sessionStore.SetTargetSeqNum(peerSeqNum + 1);

                // Reset our outgoing sequence ONLY if we didn't already reset (via our own ResetSeqNumFlag=Y)
                // If we also sent ResetSeqNumFlag=Y, our outgoing was already reset when we sent our Logon
                if (!weAlsoReset)
                {
                    logger?.Info("Resetting our outgoing sequence to match peer's reset request");
                    await m_sessionStore.Reset();
                    m_encoder.MsgSeqNum = m_sessionStore.SenderSeqNum;

                    // Recreate resender with empty store
                    m_resender = new FixMsgAsciiStoreResend(m_sessionStore, m_fixMessageFactory, m_config, m_clock);
                }

                logger?.Info("Reset complete: SenderSeqNum={SenderSeqNum}, TargetSeqNum={TargetSeqNum}, LastPeerMsgSeqNum={LastPeerMsgSeqNum}", m_sessionStore.SenderSeqNum, m_sessionStore.TargetSeqNum, m_sessionState.LastPeerMsgSeqNum);
            }

            var state = m_sessionState;
            state.PeerHeartBeatSecs = view.GetInt32((int)MsgTag.HeartBtInt);
            state.PeerCompID = peerCompId;
            var res = OnLogon(view, userName, password);
            // currently not using this.
            logger?.Info("peerLogon onLogon returns {Result}", res);
            if (m_acceptor)
            {
                SetState(SessionState.InitiationLogonResponse);
                logger?.Info("acceptor responds to logon request");

                // If WE (acceptor) are sending ResetSeqNumFlag=Y, we need to reset our incoming
                // sequence expectation to 0 (expecting 1 next from peer) BEFORE sending our logon.
                // This handles the broker-reset pattern where client sends N, we respond with Y.
                // Only do this if the peer did NOT already send ResetSeqNumFlag=Y (that case is handled above).
                var weReset = m_config.ResetSeqNumFlag();
                if (weReset && resetSeqNumFlag != true)
                {
                    logger?.Info("Acceptor sending ResetSeqNumFlag=Y (peer sent N), resetting sequences");
                    m_sessionState.LastPeerMsgSeqNum = 0;
                    await m_sessionStore.SetTargetSeqNum(1);
                    await m_sessionStore.Reset();
                    m_encoder.MsgSeqNum = 1;
                    m_resender = new FixMsgAsciiStoreResend(m_sessionStore, m_fixMessageFactory, m_config, m_clock);
                }

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
            await OnReady(view);
        }

        private async Task<bool> CheckSeqNo(string msgType, IMessageView view)
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
                            m_sessionLogger?.Warn("terminate as seqDelta({SeqDelta}) < 0 lastSeq = {LastSeq} seqNo = {SeqNo}", seqDelta, lastSeq, seqNo);
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

                            // Update store's target sequence number (next expected incoming seq = seqNo + 1)
                            await m_sessionStore.SetTargetSeqNum(seqNo.Value + 1);
                        }
                        return ret;
                    }
            }
        }

        private async Task SendResendRequest(int lastSeq, int receivedSeq)
        {
            var resend = m_config.MessageFactory?.ResendRequest(lastSeq + 1, 0);
            if (resend != null)
            {
                m_sessionLogger?.Warn("received seq {ReceivedSeq}, but last known seq is {LastSeq}. Sending resend request", receivedSeq, lastSeq);
                await Send(MsgType.ResendRequest, resend);
            }
        }

        private async Task SendReject(string msgType, int seqNo, string msg, int reason)
        {
            var factory = m_config.MessageFactory;
            var reject = factory?.Reject(msgType, seqNo, msg, reason);
            if (reject != null)
            {
                m_sessionLogger?.Warn("rejecting with {RejectJson}", JsonHelper.ToJson(reject));
                await Send(MsgType.Reject, reject);
            }
        }


/// <summary>
/// Initializes the session store and loads persisted sequence numbers.
/// Must be called before the session starts processing messages.
/// </summary>
        protected async Task InitializeSessionStore()
        {
            await m_sessionStore.Initialize();

            // Load persisted sequence numbers into encoder and session state
            m_encoder.MsgSeqNum = m_sessionStore.SenderSeqNum;
            m_sessionState.LastPeerMsgSeqNum = m_sessionStore.TargetSeqNum > 0
                ? m_sessionStore.TargetSeqNum - 1
                : 0;

            m_sessionLogger?.Info("Session store initialized: SenderSeqNum={SenderSeqNum}, TargetSeqNum={TargetSeqNum}", m_sessionStore.SenderSeqNum, m_sessionStore.TargetSeqNum);

            // Create resender now that store is initialized
            m_resender = new FixMsgAsciiStoreResend(m_sessionStore, m_fixMessageFactory, m_config, m_clock);
        }

/// <summary>
/// Stores an encoded message to the session store and updates sender sequence number.
/// </summary>
        protected async Task StoreEncodedMessage(string msgType, int seqNum, string encoded)
        {
            var record = new FixMsgStoreRecord(msgType, m_clock.Current, seqNum, encoded);
            await m_sessionStore.Put(record);

            // Update store's sender sequence number (next outgoing seq = seqNum + 1)
            await m_sessionStore.SetSenderSeqNum(seqNum + 1);
        }

        /// <summary>
        /// Handles resend request from peer - replays stored messages or sends gap fill.
        /// </summary>
        private async Task OnResendRequest(IMessageView view)
        {
            // if no records are in store then send a gap fill for entire sequence
            SetState(SessionState.HandleResendRequest);
            var beginSeqNo = view.BeginSeqNo();
            var requestedEndSeqNo = view.EndSeqNo();
            if (beginSeqNo == null) return;
            if (requestedEndSeqNo == null) return;
            var endSeqNo = requestedEndSeqNo == 0 ? m_sessionState.LastSentSeqNum : requestedEndSeqNo.Value;

            m_sessionLogger?.Info("onResendRequest getResendRequest beginSeqNo = {BeginSeqNo}, endSeqNo = {EndSeqNo}", beginSeqNo, endSeqNo);

            if (m_resender == null)
            {
                m_sessionLogger?.Warn("Resender not initialized, sending gap fill for entire range");
                return;
            }

            var records = await m_resender.GetResendRequest(beginSeqNo.Value, endSeqNo);
            m_sessionLogger?.Info("sending {Count}", records.Count);
            foreach (var rec in records)
            {
                if (rec.InflatedMessage != null)
                {
                    await Send(rec.MsgType, rec.InflatedMessage);
                }
            }
            SetState(SessionState.ActiveNormalSession);
        }

        private bool OkForLogon() {
            var state = m_sessionState.State;
            if (m_acceptor) {
                return state == SessionState.WaitingForALogon;
            }
            return state == SessionState.InitiationLogonSent;
        }

        private async Task OnSessionMsg(string msgType, IMessageView view)
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
                            m_sessionLogger?.Info("responding to test request - send heartbeat");
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
                        logger?.Info("peer sends '{MsgType}' resend request.", msgType);
                        await OnResendRequest(view);
                        break;
                    }

                case MsgType.SequenceReset:
                    {
                        var newSeqNo = view.GetInt32((int)MsgTag.NewSeqNo);
                        logger?.Info("peer sends '{MsgType}' sequence reset. newSeqNo = {NewSeqNo}", msgType, newSeqNo);
                        // expect newSeqNo to be the next message's sequence number.
                        m_sessionState.LastPeerMsgSeqNum = newSeqNo - 1;

                        // Update store's target sequence number to match
                        if (newSeqNo.HasValue)
                        {
                            await m_sessionStore.SetTargetSeqNum(newSeqNo.Value);
                        }
                        break;
                    }

                case MsgType.Reject:
                    {
                        logger?.Info("peer rejects type '{MsgType}' with text '{Text}'", msgType, view.Lazy((int)MsgTag.Text));
                        break;
                    }
            }
        }

        protected abstract override Task OnRun();


        protected override async Task OnMsg(string msgType, IMessageView view)
        {
            var checkSeqNo = await CheckSeqNo(msgType, view);
            if (!checkSeqNo)
            {
                m_sessionLogger?.Info("message '{MsgType}' failed checkSeqNo.", msgType);
                return;
            }

            if (m_checkMsgIntegrity)
            {
                var checkIntegrity = await CheckIntegrity(msgType, view);
                if (!checkIntegrity)
                {
                    m_sessionLogger?.Info("message '{MsgType}' failed checkIntegrity.", msgType);
                    switch (msgType)
                    {
                        case MsgType.Logon:
                            {
                                SetState(SessionState.PeerLogonRejected);                               
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

        private async Task<bool> CheckIntegrity(string msgType, IMessageView view)
        {
            if (!view.TryGetInt32((int)MsgTag.MsgSeqNum, out var seqNum)) return false;
            if (!view.TryGetInt32((int)MsgTag.CheckSum, out var received)) return false;

            var computed = view.Checksum();
            if (received != computed)
            {
                var msg = $"msgType {msgType} checksum failed. received = {received} computed = {computed}";
                await SendReject(msgType, seqNum, msg, (int)SessionRejectReason.ValueIsIncorrect);
                return false;
            }

            return true;
        }

        private void Terminate(Exception _)
        {
            // TODO: implement proper termination handling
        }

        private async Task CheckForwardMsg(string msgType, IMessageView view)
        {
            var okToForward = ValidStateApplicationMsg();
            if (okToForward)
            {
                m_sessionLogger?.Info("ascii forwarding msgType = '{MsgType}' to application", msgType);
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
 

