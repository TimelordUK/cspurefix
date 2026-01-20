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
        private readonly SessionSequenceCoordinator m_coordinator;
        private readonly SessionId m_sessionId;
        private readonly ISessionRegistry? m_sessionRegistry;
        private FixMsgAsciiStoreResend? m_resender;
        private const int MaxLogonRetries = 100; // Reasonable limit to prevent infinite loops
        private const int MaxTimeoutRecoveryAttempts = 3;
        public bool Heartbeat { get; set; } = true;

        /// <summary>
        /// Tracks if this session was created with wildcard TargetCompID ("*").
        /// Used to update the TargetCompID from the peer's SenderCompID on Logon.
        /// </summary>
        private readonly bool m_isWildcardMode;

        /// <summary>
        /// Prepares the session for reconnection after a disconnect.
        /// Resets transient state via coordinator in addition to base class state reset.
        /// </summary>
        public override void PrepareForReconnect()
        {
            base.PrepareForReconnect();
            m_coordinator.PrepareForReconnect();
            m_sessionLogger?.Info("PrepareForReconnect: coordinator reset transient state");
        }

        protected AsciiSession(IFixConfig config, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixClock clock)
            : base(config, logFactory, parser, encoder, clock)
        {
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(config.Description);
            ArgumentException.ThrowIfNullOrEmpty(config.Description.SenderCompID);
            ArgumentException.ThrowIfNullOrEmpty(config.Description.TargetCompID);
            ArgumentException.ThrowIfNullOrEmpty(config.Description.BeginString);

            // Wildcard TargetCompID ("*") is only valid for acceptors
            m_isWildcardMode = config.Description.TargetCompID == "*";
            if (m_isWildcardMode && config.IsInitiator())
            {
                throw new ArgumentException("Wildcard TargetCompID ('*') is only valid for acceptors, not initiators");
            }

            m_fixMessageFactory = fixMessageFactory;

            // Create session store from factory (defaults to in-memory if not configured)
            var storeFactory = config.SessionStoreFactory ?? new MemorySessionStoreFactory();
            m_sessionId = new SessionId(
                config.Description.BeginString,
                config.Description.SenderCompID,
                config.Description.TargetCompID);
            m_sessionStore = storeFactory.Create(m_sessionId);

            // Get session registry from config (optional - for tracking active sessions)
            m_sessionRegistry = config.SessionRegistry;

            // Create the sequence coordinator - single source of truth for all sequence state
            m_coordinator = new SessionSequenceCoordinator(m_sessionStore, clock, m_sessionLogger);

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

        private bool ValidStateApplicationMsg()
        {
            switch (m_sessionState.State)
            {
                case SessionState.Idle:
                case SessionState.InitiateConnection:
                case SessionState.InitiationLogonSent:
                case SessionState.WaitingForALogon:
                case SessionState.HandleResendRequest:
                case SessionState.AwaitingProcessingResponseToResendRequest:
                    return false;
                // Allow application messages during test request wait - peer may send messages
                // at any time, and test request is just a heartbeat check
                case SessionState.AwaitingProcessingResponseToTestRequest:
                    return true;
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
                        if (m_coordinator.IncrementTimeoutRecovery(MaxTimeoutRecoveryAttempts))
                        {
                            // Try to recover - reset timeout state to give session a fresh window
                            // This helps survive sleep/wake scenarios where TCP connection may still be alive
                            m_sessionState.LastTestRequestAt = null;
                            m_sessionState.LastReceivedAt = m_clock.Current; // Reset receive time to prevent immediate re-timeout
                            SetState(SessionState.ActiveNormalSession);
                        }
                        else
                        {
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

            // Handle wildcard TargetCompID: update from peer's SenderCompID
            // This allows acceptors to accept any client without knowing their CompID in advance
            // We check m_isWildcardMode (set at construction) rather than the config value because
            // the config is shared across sessions and may have been modified by a previous session.
            if (m_isWildcardMode && !string.IsNullOrEmpty(peerCompId))
            {
                if (m_config.Description is PureFix.Types.Config.SessionDescription desc)
                {
                    logger?.Info("Wildcard TargetCompID: updating from '{CurrentTarget}' to '{PeerCompId}'",
                        desc.TargetCompID, peerCompId);
                    desc.TargetCompID = peerCompId;
                }
            }

            // Handle ResetSeqNumFlag from peer's logon
            // When peer sends ResetSeqNumFlag=Y, both sides should reset to 1.
            if (resetSeqNumFlag == true)
            {
                var peerSeqNum = view.MsgSeqNum() ?? 1;
                var weAlsoReset = m_config.ResetSeqNumFlag();

                logger?.Info("Peer sent ResetSeqNumFlag=Y with SeqNum={PeerSeqNum}, weAlsoReset={WeAlsoReset}", peerSeqNum, weAlsoReset);

                // Save encoder sequence if we already reset (we've already sent our logon with reset sequence)
                // The encoder has already incremented after sending, so we need to preserve that
                var savedEncoderSeqNum = weAlsoReset ? m_encoder.MsgSeqNum : (int?)null;

                // Coordinator handles the reset logic (clears store, resets tracking)
                await m_coordinator.HandlePeerReset(peerSeqNum, weAlsoReset);

                // Sync encoder from saved value (if we already reset) or from coordinator
                m_encoder.MsgSeqNum = savedEncoderSeqNum ?? m_coordinator.NextSenderSeqNum;
                m_sessionState.LastPeerMsgSeqNum = m_coordinator.LastProcessedPeerSeqNum;

                // Recreate resender with empty store
                m_resender = new FixMsgAsciiStoreResend(m_sessionStore, m_fixMessageFactory, m_config, m_clock);

                // Notify derived classes to clear their state (e.g., recovery stores)
                await OnSessionReset();

                logger?.Info("Reset complete: EncoderSeqNum={EncoderSeqNum}, ExpectedTarget={ExpectedTarget}, LastProcessedPeer={LastProcessed}",
                    m_encoder.MsgSeqNum, m_coordinator.ExpectedTargetSeqNum, m_coordinator.LastProcessedPeerSeqNum);
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

                // If WE (acceptor) are sending ResetSeqNumFlag=Y, we need to reset our sequences
                // BEFORE sending our logon. This handles the broker-reset pattern where client sends N, we respond with Y.
                // Only do this if the peer did NOT already send ResetSeqNumFlag=Y (that case is handled above).
                var weReset = m_config.ResetSeqNumFlag();
                if (weReset && resetSeqNumFlag != true)
                {
                    logger?.Info("Acceptor sending ResetSeqNumFlag=Y (peer sent N), resetting sequences");
                    await m_coordinator.ResetAsAcceptor();

                    // Sync encoder and session state from coordinator
                    m_encoder.MsgSeqNum = m_coordinator.NextSenderSeqNum;
                    m_sessionState.LastPeerMsgSeqNum = m_coordinator.LastProcessedPeerSeqNum;
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
            m_coordinator.ResetLogonRetryCount();

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

                // Track this message in the coordinator's resend manager
                // (updates pending request tracking - clears request when all messages received)
                var seqNo = view.MsgSeqNum();
                if (seqNo.HasValue)
                {
                    await m_coordinator.OnMessageReceived(seqNo.Value, possDupFlag: true);
                }

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
                        var expectedSeq = lastSeq.Value + 1;

                        m_sessionLogger?.Debug("CheckSeqNo: {MsgType} seq={ReceivedSeq}, expected={Expected}",
                            msgType, seqNo, $"{expectedSeq} (delta={seqDelta})");

                        if (seqDelta <= 0)
                        {
                            // Check if this is a delayed message that fills a pending gap.
                            // This handles the case where messages arrive out of order due to network delays
                            // (e.g., after sleep/wake). The original message may arrive AFTER higher sequences.
                            var pendingRequests = m_coordinator.PendingResendRequests;
                            var inPendingGapRange = pendingRequests.Any(p => seqNo >= p.Begin && seqNo <= p.End);

                            if (inPendingGapRange)
                            {
                                m_sessionLogger?.Info("Accepting delayed message seq {SeqNo} (in pending gap range)",
                                    seqNo);

                                // Track the message receipt - coordinator will clear request when all received
                                await m_coordinator.OnMessageReceived(seqNo.Value, possDupFlag: false);

                                // Don't update LastPeerMsgSeqNum - it's already ahead of this sequence
                                return true;
                            }

                            // MsgSeqNum too low - send Reject
                            m_sessionLogger?.Warn("MsgSeqNum too low: received {SeqNo}, expected {ExpectedSeq}. Sending Reject.", seqNo, expectedSeq);
                            await SendReject(msgType, seqNo.Value, $"MsgSeqNum too low, expecting {expectedSeq}", (int)SessionRejectReason.ValueIsIncorrect);

                            // For Logon messages: don't terminate - let client retry with higher seq
                            // For other messages: terminate the session
                            if (msgType != MsgType.Logon)
                            {
                                Stop();
                            }
                            else
                            {
                                m_sessionLogger?.Info("Logon rejected for seq too low, waiting for retry with higher sequence");
                            }
                        }
                        else if (seqDelta > 1)
                        {
                            // Gap detected - we've missed messages.
                            // Strategy: Send ResendRequest (optimistic) but also process this message (pragmatic).
                            // Don't block waiting for gap fill - keep the session running.
                            // If gap fill arrives later, great. If not, we've already moved on.
                            var lostCount = seqNo.Value - lastSeq.Value - 1;

                            var pendingCount = m_coordinator.PendingResendRequests.Count;
                            m_sessionLogger?.Warn("GAP DETECTED: received={ReceivedSeq}, expected={Expected}, lost={LostCount} pending={Pending}",
                                seqNo.Value, expectedSeq, $"{lostCount}, pending={pendingCount}");

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

                            // Use coordinator to determine what action to take
                            var action = m_coordinator.OnGapDetected(expectedSeq, seqNo.Value);
                            m_sessionLogger?.Debug("Gap action: {Action}", action);

                            switch (action.Type)
                            {
                                case ResendActionType.SendResendRequest:
                                    if (action.Begin.HasValue && action.End.HasValue)
                                    {
                                        var resend = m_config.MessageFactory?.ResendRequest(action.Begin.Value, 0);
                                        if (resend != null)
                                        {
                                            m_sessionLogger?.Warn("Sending ResendRequest: begin={Begin}, end=0 (infinity)", action.Begin);
                                            await Send(MsgType.ResendRequest, resend);
                                            m_coordinator.RecordResendRequestSent(action.Begin.Value, action.End.Value);
                                        }
                                    }
                                    break;

                                case ResendActionType.Wait:
                                    m_sessionLogger?.Info("Waiting for existing resend request to complete: {Reason}", action.Reason);
                                    break;

                                case ResendActionType.SendGapFill:
                                    // Storm protection triggered - we've sent too many ResendRequests.
                                    // Don't send another request - just accept the current message and move on.
                                    // The gap (expectedSeq to seqNo-1) will be permanently lost.
                                    m_sessionLogger?.Warn("Gap recovery abandoned (storm protection): {Reason}. " +
                                        "Accepting seq={SeqNo}, gap {GapRange} will not be recovered.",
                                        action.Reason, seqNo, $"{action.Begin}-{action.End}");
                                    break;
                            }

                            // Always accept the current message and continue - don't block waiting for gap fill
                            ret = true;
                            var prevSeq = state.LastPeerMsgSeqNum;
                            state.LastPeerMsgSeqNum = seqNo;
                            await m_sessionStore.SetTargetSeqNum(seqNo.Value + 1);

                            // Notify coordinator of gap message receipt (coordinator tracks but doesn't advance expected)
                            await m_coordinator.OnMessageReceived(seqNo.Value, possDupFlag: false);

                            m_sessionLogger?.Debug("Gap message accepted: sessionState {PrevSeq}->{NewSeq}, store={StoreInfo}",
                                prevSeq, seqNo, $"target={seqNo.Value + 1}, coord expected={m_coordinator.ExpectedTargetSeqNum}");
                        }
                        else
                        {
                            ret = true;
                            state.LastPeerMsgSeqNum = seqNo;

                            // Update store's target sequence number (next expected incoming seq = seqNo + 1)
                            await m_sessionStore.SetTargetSeqNum(seqNo.Value + 1);

                            // CRITICAL: Notify coordinator of in-sequence message receipt.
                            // This keeps coordinator's expected in sync with session state.
                            await m_coordinator.OnMessageReceived(seqNo.Value, possDupFlag: false);
                        }

                        // Reset timeout recovery counter when we successfully receive messages
                        if (ret)
                        {
                            m_coordinator.ResetTimeoutRecovery();

                            // Sanity check: session state and coordinator should be in sync
                            // (session expected = lastPeerMsgSeqNum + 1, coordinator expected should be <= this)
                            var sessionExpected = (state.LastPeerMsgSeqNum ?? 0) + 1;
                            var coordExpected = m_coordinator.ExpectedTargetSeqNum;
                            if (coordExpected > sessionExpected)
                            {
                                m_sessionLogger?.Warn("SEQUENCE DESYNC: coordinator expected ({CoordExpected}) > session expected ({SessionExpected}). " +
                                    "This indicates a bug in sequence tracking.",
                                    coordExpected, sessionExpected);
                            }
                        }
                        return ret;
                    }
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
        /// Handles logon rejection due to sequence number mismatch.
        /// Retries the logon with the next sequence number.
        /// This handles the scenario where the client's sequence number is behind the server's expectation.
        /// The encoder already increments MsgSeqNum after each message is sent, so we just retry.
        /// </summary>
        private async Task HandleLogonRejected(int? refSeqNum, string? text)
        {
            // Ask coordinator if we should retry
            if (!m_coordinator.OnLogonRejectedForSequence(MaxLogonRetries))
            {
                m_sessionLogger?.Warn("Max logon retries ({MaxRetries}) exceeded, giving up. Text='{Text}'", MaxLogonRetries, text);
                SetState(SessionState.PeerLogonRejected);
                Stop();
                return;
            }

            // The encoder's MsgSeqNum is already incremented after each message is sent,
            // so we just need to retry the logon. The next logon will use the next sequence number.
            var nextSeq = m_encoder.MsgSeqNum;
            m_sessionLogger?.Info("LOGON_SEQ_RETRY: attempt={Attempt}, nextSeq={NextSeq}, reason='{Text}'",
                $"{m_coordinator.LogonRetryCount}/{MaxLogonRetries}", nextSeq, text);

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

            // Register with session registry - this will stop any existing session with same SessionId
            // This prevents stale transport writes when a client reconnects before the old session detects disconnect
            if (m_sessionRegistry != null)
            {
                var stoppedOld = m_sessionRegistry.Register(m_sessionId, this);
                if (stoppedOld)
                {
                    m_sessionLogger?.Info("Session registry stopped previous session for {SessionId}", m_sessionId);
                }
            }

            // Initialize coordinator from store - it becomes the source of truth
            m_coordinator.InitializeFromStore();

            // Sync encoder and session state from coordinator
            m_encoder.MsgSeqNum = m_coordinator.NextSenderSeqNum;
            m_sessionState.LastPeerMsgSeqNum = m_coordinator.LastProcessedPeerSeqNum;

            m_sessionLogger?.Info("Session store initialized: NextSender={NextSender}, ExpectedTarget={ExpectedTarget}",
                m_coordinator.NextSenderSeqNum, m_coordinator.ExpectedTargetSeqNum);

            // Create resender now that store is initialized
            m_resender = new FixMsgAsciiStoreResend(m_sessionStore, m_fixMessageFactory, m_config, m_clock);
        }

        /// <summary>
        /// Called when the session stops. Unregisters from session registry.
        /// </summary>
        protected override void OnSessionStopping()
        {
            base.OnSessionStopping();

            if (m_sessionRegistry != null)
            {
                m_sessionLogger?.Info("Session stopping - unregistering from registry: {SessionId}", m_sessionId);
                m_sessionRegistry.Unregister(m_sessionId, this);
            }
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

        private bool OkForLogon()
        {
            var state = m_sessionState.State;
            if (m_acceptor)
            {
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
                        var gapFillSeq = view.MsgSeqNum() ?? m_coordinator.ExpectedTargetSeqNum;
                        logger?.Info("peer sends '{MsgType}' sequence reset. newSeqNo = {NewSeqNo}", msgType, newSeqNo);

                        if (newSeqNo.HasValue)
                        {
                            var sessionSeqBefore = m_sessionState.LastPeerMsgSeqNum;
                            var coordinatorSeqBefore = m_coordinator.LastProcessedPeerSeqNum;

                            // Let coordinator handle the gap fill - updates expected sequence and clears pending requests
                            await m_coordinator.OnGapFillReceived(gapFillSeq, newSeqNo.Value);

                            // Sync session state from coordinator - but NEVER rewind!
                            // Old GapFills (from previous resend requests) may have lower sequence numbers
                            // than what we've already processed. Only advance, never go backwards.
                            var coordinatorSeqAfter = m_coordinator.LastProcessedPeerSeqNum;
                            var newSessionSeq = Math.Max(sessionSeqBefore ?? 0, coordinatorSeqAfter);

                            if (newSessionSeq < (sessionSeqBefore ?? 0))
                            {
                                logger?.Warn("GapFill would rewind session state: session={SessionSeq}, coordinator={CoordSeq}, keeping session value",
                                    sessionSeqBefore, coordinatorSeqAfter);
                            }
                            else if (newSessionSeq > (sessionSeqBefore ?? 0))
                            {
                                logger?.Info("GapFill advancing session state: {Before} -> {After}, coordinator: {CoordChange}",
                                    sessionSeqBefore, newSessionSeq, $"{coordinatorSeqBefore}->{coordinatorSeqAfter}");
                            }

                            m_sessionState.LastPeerMsgSeqNum = newSessionSeq;
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

        /// <summary>
        /// Called before sending the logon message (initiator only).
        /// Resets session store and encoder when ResetSeqNumFlag=Y to ensure
        /// the logon is sent with sequence number 1, not the recovered sequence.
        /// </summary>
        protected override async Task OnPreLogon()
        {
            if (m_config.ResetSeqNumFlag())
            {
                m_sessionLogger?.Info("Initiator has ResetSeqNumFlag=Y, resetting sequences before logon");

                // Coordinator handles the full reset
                await m_coordinator.ResetSession("Initiator ResetSeqNumFlag=Y");

                // Sync encoder and session state from coordinator
                m_encoder.MsgSeqNum = m_coordinator.NextSenderSeqNum;
                m_sessionState.LastPeerMsgSeqNum = m_coordinator.LastProcessedPeerSeqNum;

                // Recreate resender with empty store
                m_resender = new FixMsgAsciiStoreResend(m_sessionStore, m_fixMessageFactory, m_config, m_clock);

                // Notify derived classes to clear their state (e.g., recovery stores)
                await OnSessionReset();

                m_sessionLogger?.Info("Pre-logon reset complete: EncoderSeqNum={EncoderSeqNum}", m_encoder.MsgSeqNum);
            }
        }


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


