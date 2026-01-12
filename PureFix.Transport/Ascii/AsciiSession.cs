using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.Core;


namespace PureFix.Transport.Ascii
{
    public abstract class AsciiSession : FixSession
    {
        private readonly IFixSessionStore m_sessionStore;
        private readonly IFixMessageFactory m_fixMessageFactory;
        private FixMsgAsciiStoreResend? m_resender;
        private int m_logonRetryCount;
        private const int MaxLogonRetries = 100; // Reasonable limit to prevent infinite loops

        // Circuit breaker for ResendRequest - prevents repeatedly sending the same request
        private int? m_lastResendRequestBeginSeq;
        private int m_resendRequestDuplicateCount;

        // Timeout recovery - allow multiple attempts before terminating (helps survive sleep/wake)
        private int m_timeoutRecoveryAttempts;
        private const int MaxTimeoutRecoveryAttempts = 3;
        public bool Heartbeat { get; set; } = true;

        /// <summary>
        /// Prepares the session for reconnection after a disconnect.
        /// Resets the logon retry counter in addition to base class state reset.
        /// </summary>
        public override void PrepareForReconnect()
        {
            base.PrepareForReconnect();
            m_logonRetryCount = 0;
            m_lastResendRequestBeginSeq = null;
            m_resendRequestDuplicateCount = 0;
            m_timeoutRecoveryAttempts = 0;
            m_sessionLogger?.Info("PrepareForReconnect: reset logon retry counter, resend request circuit breaker, and timeout recovery");
        }

        protected AsciiSession(IFixConfig config, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixClock clock)
            : base(config, logFactory, parser, encoder, clock)
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

            m_sessionLogger?.Info("message count = {DefinitionCount}, component count = {ComponentCount}, simple count = {FieldCount}", 
            Definitions.Message.Count, 
            Definitions.Component.Count,
            Definitions.Simple.Count);
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

            // Diagnostic: bypass Serilog to verify tick is running (uncomment for debugging)
            // Console.WriteLine($"[TICK] {DateTime.Now:HH:mm:ss} action={action} state={m_sessionState.State}");

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
                        m_timeoutRecoveryAttempts++;
                        if (m_timeoutRecoveryAttempts <= MaxTimeoutRecoveryAttempts)
                        {
                            // Try to recover - reset timeout state to give session a fresh window
                            // This helps survive sleep/wake scenarios where TCP connection may still be alive
                            m_sessionLogger?.Warn("Session timeout (attempt {Attempt}/{Max}). Attempting recovery - resetting timeout window.",
                                m_timeoutRecoveryAttempts, MaxTimeoutRecoveryAttempts);
                            m_sessionState.LastTestRequestAt = null;
                            m_sessionState.LastReceivedAt = m_clock.Current; // Reset receive time to prevent immediate re-timeout
                            SetState(SessionState.ActiveNormalSession);
                        }
                        else
                        {
                            m_sessionLogger?.Warn("Session timeout - max recovery attempts ({Max}) exceeded. Terminating.",
                                MaxTimeoutRecoveryAttempts);
                            Terminate(new TimeoutException("No response to test request within timeout period"));
                        }
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
            if (logger?.IsEnabled(LogLevel.Info) == true)
                logger.Info($"peerLogon Username={userName}, heartBtInt={heartBtInt}, peerCompId={peerCompId}, resetSeqNumFlag={resetSeqNumFlag}");

            // Handle ResetSeqNumFlag from peer's logon
            // When peer sends ResetSeqNumFlag=Y, both sides should reset to 1.
            if (resetSeqNumFlag == true)
            {
                var peerSeqNum = view.MsgSeqNum() ?? 1;
                var weAlsoReset = m_config.ResetSeqNumFlag();

                logger?.Info("Peer sent ResetSeqNumFlag=Y with SeqNum={PeerSeqNum}, weAlsoReset={WeAlsoReset}", peerSeqNum, weAlsoReset);

                // Always reset the store to clear old messages (prevents duplicate key errors on reconnect)
                // This must happen even if weAlsoReset=true, because the store may have old messages from before sleep/wake
                logger?.Info("Resetting session store to clear old messages");
                await m_sessionStore.Reset();
                m_encoder.MsgSeqNum = m_sessionStore.SenderSeqNum;

                // Recreate resender with empty store
                m_resender = new FixMsgAsciiStoreResend(m_sessionStore, m_fixMessageFactory, m_config, m_clock);

                // Reset our expected incoming sequence
                m_sessionState.LastPeerMsgSeqNum = peerSeqNum;
                await m_sessionStore.SetTargetSeqNum(peerSeqNum + 1);

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

            // Reset logon retry counter on successful logon
            m_logonRetryCount = 0;

            logger?.Info("system ready, inform app");
            await OnReady(view);
        }

        private async Task<bool> CheckSeqNo(string msgType, IMessageView view)
        {
            // Messages with PossDupFlag=Y are resent messages (gap fill responses).
            // They have "old" sequence numbers and should bypass normal sequence checking.
            var possDupFlag = view.GetBool((int)MsgTag.PossDupFlag);
            if (possDupFlag == true)
            {
                m_sessionLogger?.Debug("Message {MsgType} has PossDupFlag=Y, bypassing sequence check", msgType);
                return true;
            }

            switch (msgType)
            {
                case MsgType.SequenceReset:
                    {
                        return true;
                    }

                case MsgType.Logon:
                    {
                        // For Logon messages, check if peer is requesting a reset
                        // If ResetSeqNumFlag=Y, accept any sequence number and let PeerLogon handle the reset
                        var resetFlag = view.ResetSeqNumFlag();
                        if (resetFlag == true)
                        {
                            m_sessionLogger?.Info("Logon with ResetSeqNumFlag=Y, accepting regardless of sequence");
                            var seqNo = view.MsgSeqNum();
                            if (seqNo != null)
                            {
                                m_sessionState.LastPeerMsgSeqNum = seqNo;
                                await m_sessionStore.SetTargetSeqNum(seqNo.Value + 1);
                            }
                            return true;
                        }
                        // Otherwise fall through to normal sequence check
                        goto default;
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
                            // MsgSeqNum too low - send Reject and terminate
                            var expectedSeq = lastSeq.Value + 1;
                            m_sessionLogger?.Warn("MsgSeqNum too low: received {SeqNo}, expected {ExpectedSeq}. Sending Reject.", seqNo, expectedSeq);
                            await SendReject(msgType, seqNo.Value, $"MsgSeqNum too low, expecting {expectedSeq}", (int)SessionRejectReason.ValueIsIncorrect);
                            Stop();
                        }
                        else if (seqDelta > 1)
                        {
                            // Gap detected - we've missed messages.
                            // Strategy: Send ResendRequest (optimistic) but also process this message (pragmatic).
                            // Don't block waiting for gap fill - keep the session running.
                            // If gap fill arrives later, great. If not, we've already moved on.
                            var lostCount = seqNo.Value - lastSeq.Value - 1;

                            // We process a Logon beforehand to confirm the connection even if out of sync
                            if (msgType == MsgType.Logon)
                            {
                                await PeerLogon(view);
                            }
                            // If the out of sync message is a resend request itself, handle it first to
                            // avoid triggering an endless loop of both sides sending resend requests.
                            if (msgType == MsgType.ResendRequest)
                            {
                                await OnResendRequest(view);
                            }

                            // Try to send ResendRequest (circuit breaker may block if already sent)
                            var resendSent = await SendResendRequest(lastSeq.Value, seqNo.Value);
                            if (!resendSent)
                            {
                                m_sessionLogger?.Warn("Gap recovery abandoned. Lost {LostCount} messages (seq {FromSeq} to {ToSeq}).",
                                    lostCount, lastSeq.Value + 1, seqNo.Value - 1);
                            }

                            // Always accept the current message and continue - don't block waiting for gap fill
                            ret = true;
                            state.LastPeerMsgSeqNum = seqNo;
                            await m_sessionStore.SetTargetSeqNum(seqNo.Value + 1);
                        }
                        else
                        {
                            ret = true;
                            state.LastPeerMsgSeqNum = seqNo;

                            // Update store's target sequence number (next expected incoming seq = seqNo + 1)
                            await m_sessionStore.SetTargetSeqNum(seqNo.Value + 1);
                        }

                        // Reset timeout recovery counter when we successfully receive messages
                        if (ret && m_timeoutRecoveryAttempts > 0)
                        {
                            m_sessionLogger?.Debug("Message received, resetting timeout recovery counter from {Count}", m_timeoutRecoveryAttempts);
                            m_timeoutRecoveryAttempts = 0;
                        }
                        return ret;
                    }
            }
        }

        /// <summary>
        /// Sends a ResendRequest unless circuit breaker blocks it.
        /// </summary>
        /// <returns>True if request was sent, false if blocked by circuit breaker.</returns>
        private async Task<bool> SendResendRequest(int lastSeq, int receivedSeq)
        {
            var beginSeq = lastSeq + 1;

            // Circuit breaker: if we already sent a ResendRequest for this beginSeq, don't send again
            if (m_lastResendRequestBeginSeq == beginSeq)
            {
                m_resendRequestDuplicateCount++;
                m_sessionLogger?.Warn("ResendRequest circuit breaker: already sent request for beginSeq={BeginSeq}, skipping duplicate #{Count}.",
                    beginSeq, m_resendRequestDuplicateCount);
                return false;
            }

            var resend = m_config.MessageFactory?.ResendRequest(beginSeq, 0);
            if (resend != null)
            {
                m_sessionLogger?.Warn("received seq {ReceivedSeq}, but last known seq is {LastSeq}. Sending resend request", receivedSeq, lastSeq);
                await Send(MsgType.ResendRequest, resend);
                m_lastResendRequestBeginSeq = beginSeq;
                m_resendRequestDuplicateCount = 0;
                return true;
            }
            return false;
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
        /// Handles logon rejection due to sequence number mismatch.
        /// Retries the logon with the next sequence number.
        /// This handles the scenario where the client's sequence number is behind the server's expectation.
        /// The encoder already increments MsgSeqNum after each message is sent, so we just retry.
        /// </summary>
        private async Task HandleLogonRejected(int? refSeqNum, string? text)
        {
            m_logonRetryCount++;

            if (m_logonRetryCount > MaxLogonRetries)
            {
                m_sessionLogger?.Warn("Max logon retries ({MaxRetries}) exceeded, giving up. Text='{Text}'", MaxLogonRetries, text);
                SetState(SessionState.PeerLogonRejected);
                Stop();
                return;
            }

            // The encoder's MsgSeqNum is already incremented after each message is sent,
            // so we just need to retry the logon. The next logon will use the next sequence number.
            var nextSeq = m_encoder.MsgSeqNum;
            m_sessionLogger?.Info("Logon rejected (attempt {Count}), retrying with seq={NextSeq}. Text='{Text}'",
                m_logonRetryCount, nextSeq, text);

            // Retry the logon with the next sequence number
            await SendLogon();
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

            // EndSeqNo=0 in FIX means "to infinity" - resend all from BeginSeqNo to the last sent message
            // Use the persisted SenderSeqNum (reliable) rather than in-memory LastSentSeqNum (can be stale)
            var lastSentFromStore = m_sessionStore.SenderSeqNum - 1; // SenderSeqNum is next to send, so -1 is last sent
            var endSeqNo = requestedEndSeqNo == 0 ? lastSentFromStore : requestedEndSeqNo.Value;

            m_sessionLogger?.Debug("onResendRequest: requestedEndSeqNo={RequestedEnd}, lastSentFromStore={LastSent}, m_sessionState.LastSentSeqNum={StateLast}",
                requestedEndSeqNo, lastSentFromStore, m_sessionState.LastSentSeqNum);

            m_sessionLogger?.Info("onResendRequest getResendRequest beginSeqNo = {BeginSeqNo}, endSeqNo = {EndSeqNo}", beginSeqNo, endSeqNo);

            // Handle invalid range: endSeqNo < beginSeqNo indicates sequence number corruption
            if (endSeqNo < beginSeqNo.Value)
            {
                m_sessionLogger?.Warn("Invalid resend range: beginSeqNo={BeginSeqNo} > endSeqNo={EndSeqNo}. Sequence numbers may be out of sync. Sending GapFill to advance past requested range.",
                    beginSeqNo, endSeqNo);
                // Send GapFill to advance peer past the requested range
                endSeqNo = beginSeqNo.Value;
            }

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

                            // Reset ResendRequest circuit breaker if this SequenceReset advances past our pending request
                            if (m_lastResendRequestBeginSeq.HasValue && newSeqNo.Value > m_lastResendRequestBeginSeq.Value)
                            {
                                m_sessionLogger?.Debug("SequenceReset advances past pending ResendRequest beginSeq={BeginSeq}, clearing circuit breaker",
                                    m_lastResendRequestBeginSeq);
                                m_lastResendRequestBeginSeq = null;
                                m_resendRequestDuplicateCount = 0;
                            }
                        }
                        break;
                    }

                case MsgType.Reject:
                    {
                        var refSeqNum = view.GetInt32((int)MsgTag.RefSeqNum);
                        var refMsgType = view.GetString((int)MsgTag.RefMsgType);
                        var text = view.GetString((int)MsgTag.Text);
                        var reason = view.GetInt32((int)MsgTag.SessionRejectReason);

                        logger?.Info("peer rejects RefSeqNum={RefSeqNum}, RefMsgType={RefMsgType}, Reason={Reason}",
                            refSeqNum, refMsgType, reason);
                        if (text != null)
                        {
                            logger?.Info("reject text: '{Text}'", text);
                        }

                        // Check if this is a logon rejection while we're waiting for logon response
                        if (refMsgType == MsgType.Logon && m_sessionState.State == SessionState.InitiationLogonSent)
                        {
                            await HandleLogonRejected(refSeqNum, text);
                        }
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

        private void Terminate(Exception? error)
        {
            Stop(error);
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
 

