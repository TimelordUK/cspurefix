using PureFix.Transport.Store;
using PureFix.Types;

namespace PureFix.Transport.Session;

/// <summary>
/// Coordinates all sequence-related state for a FIX session.
///
/// This is the SINGLE SOURCE OF TRUTH for:
/// - Outgoing sequence numbers (what we send)
/// - Expected incoming sequence numbers (what we expect from peer)
/// - Resend request tracking
/// - Reset coordination
///
/// Philosophy:
/// 1. All sequence state lives here - no scattered state across components
/// 2. All reset operations are coordinated through here
/// 3. Components query this for sequence numbers, don't maintain their own
/// 4. Fully testable without running actual sessions
/// </summary>
public class SessionSequenceCoordinator
{
    private readonly IFixSessionStore _store;
    private readonly ResendRequestManager _resendManager;
    private readonly IFixClock _clock;
    private readonly ILogger? _logger;
    private readonly object _lock = new();

    // THE source of truth for sequence numbers
    private int _nextSenderSeqNum = 1;
    private int _expectedTargetSeqNum = 1;

    // Track the last peer sequence we actually processed
    private int _lastProcessedPeerSeqNum = 0;

    // Transient session state (reset on reconnect)
    private int _logonRetryCount = 0;
    private int _timeoutRecoveryAttempts = 0;

    /// <summary>
    /// Event raised when a full reset occurs (store cleared, sequences reset).
    /// Subscribers should clear any session-specific state (e.g., recovery logs).
    /// </summary>
    public event Func<Task>? OnSessionReset;

    /// <summary>
    /// Creates a new SessionSequenceCoordinator.
    /// </summary>
    /// <param name="store">Session store for persistence</param>
    /// <param name="clock">Clock for timestamps</param>
    /// <param name="logger">Optional logger</param>
    /// <param name="resendManagerConfig">Optional configuration for resend manager</param>
    public SessionSequenceCoordinator(
        IFixSessionStore store,
        IFixClock clock,
        ILogger? logger = null,
        ResendManagerConfig? resendManagerConfig = null)
    {
        _store = store;
        _clock = clock;
        _logger = logger;

        var config = resendManagerConfig ?? new ResendManagerConfig();
        _resendManager = new ResendRequestManager(
            config.MaxPendingRequests,
            config.MaxRequestsPerWindow,
            config.RateLimitWindowSeconds,
            config.RequestTimeoutSeconds
        );
    }

    #region Initialization

    /// <summary>
    /// Initializes sequence numbers from the session store.
    /// Call this after store.Initialize() completes.
    /// </summary>
    public void InitializeFromStore()
    {
        lock (_lock)
        {
            _nextSenderSeqNum = _store.SenderSeqNum;
            _expectedTargetSeqNum = _store.TargetSeqNum;
            _lastProcessedPeerSeqNum = _expectedTargetSeqNum - 1;

            _logger?.Info("Initialized from store: NextSender={Sender}, ExpectedTarget={Target}",
                _nextSenderSeqNum, _expectedTargetSeqNum);
        }
    }

    /// <summary>
    /// Initializes sequence numbers from config overrides.
    /// Takes precedence over store values.
    /// </summary>
    public void InitializeFromConfig(int? senderSeqNum, int? targetSeqNum)
    {
        lock (_lock)
        {
            if (senderSeqNum.HasValue)
            {
                _nextSenderSeqNum = senderSeqNum.Value;
                _logger?.Info("Sender sequence overridden from config: {Seq}", _nextSenderSeqNum);
            }

            if (targetSeqNum.HasValue)
            {
                _expectedTargetSeqNum = targetSeqNum.Value;
                _lastProcessedPeerSeqNum = _expectedTargetSeqNum - 1;
                _logger?.Info("Target sequence overridden from config: {Seq}", _expectedTargetSeqNum);
            }
        }
    }

    #endregion

    #region Sequence Access (Read)

    /// <summary>
    /// The next sequence number that will be used for outgoing messages.
    /// </summary>
    public int NextSenderSeqNum
    {
        get { lock (_lock) return _nextSenderSeqNum; }
    }

    /// <summary>
    /// The sequence number we expect to receive next from the peer.
    /// </summary>
    public int ExpectedTargetSeqNum
    {
        get { lock (_lock) return _expectedTargetSeqNum; }
    }

    /// <summary>
    /// The last peer sequence number we fully processed.
    /// </summary>
    public int LastProcessedPeerSeqNum
    {
        get { lock (_lock) return _lastProcessedPeerSeqNum; }
    }

    #endregion

    #region Sequence Mutations (Controlled)

    /// <summary>
    /// Consumes and returns the next sender sequence number.
    /// Call this when encoding a message (not for PossDup resends).
    /// </summary>
    /// <param name="isPossDup">If true, returns current without incrementing</param>
    /// <returns>The sequence number to use for the message</returns>
    public int GetNextSenderSeqNum(bool isPossDup = false)
    {
        lock (_lock)
        {
            var seq = _nextSenderSeqNum;
            if (!isPossDup)
            {
                _nextSenderSeqNum++;
            }
            return seq;
        }
    }

    /// <summary>
    /// Records that a message was successfully encoded and will be sent.
    /// Updates the store's sender sequence number.
    /// </summary>
    public async Task OnMessageEncoded(int seqNum, bool isPossDup)
    {
        if (isPossDup) return; // PossDup doesn't advance sequence

        await _store.SetSenderSeqNum(seqNum + 1);
    }

    /// <summary>
    /// Called when a message is received from the peer.
    /// Updates expected sequence and resend tracking.
    /// </summary>
    /// <param name="seqNum">The received sequence number</param>
    /// <param name="possDupFlag">Whether this is a possible duplicate</param>
    /// <returns>True if message should be processed, false if duplicate/old</returns>
    public async Task<bool> OnMessageReceived(int seqNum, bool possDupFlag)
    {
        var now = _clock.Current;

        lock (_lock)
        {
            // Update resend manager
            _resendManager.OnMessageReceived(seqNum, possDupFlag, now);

            if (possDupFlag)
            {
                // PossDup messages don't update our expected sequence
                // They're replays of messages we may have already seen
                _logger?.Debug("PossDup message received: seq={Seq}", seqNum);
                return true; // Still process it
            }

            if (seqNum < _expectedTargetSeqNum)
            {
                // Old message - already processed
                _logger?.Debug("Old message ignored: seq={Seq}, expected={Expected}",
                    seqNum, _expectedTargetSeqNum);
                return false;
            }

            if (seqNum == _expectedTargetSeqNum)
            {
                // Expected message - advance
                _lastProcessedPeerSeqNum = seqNum;
                _expectedTargetSeqNum = seqNum + 1;
            }
            else
            {
                // Gap detected - will be handled by caller via OnGapDetected
                // For now just track that we received this one
                _lastProcessedPeerSeqNum = Math.Max(_lastProcessedPeerSeqNum, seqNum);
            }
        }

        // Persist to store
        await _store.SetTargetSeqNum(_expectedTargetSeqNum);
        return true;
    }

    /// <summary>
    /// Called when a SequenceReset-GapFill is received.
    /// </summary>
    public async Task OnGapFillReceived(int gapFillSeq, int newSeqNo)
    {
        var now = _clock.Current;

        lock (_lock)
        {
            _resendManager.OnGapFillReceived(gapFillSeq, newSeqNo, now);

            // GapFill says "skip from gapFillSeq to newSeqNo"
            // So our new expected is newSeqNo
            if (newSeqNo > _expectedTargetSeqNum)
            {
                _expectedTargetSeqNum = newSeqNo;
                _lastProcessedPeerSeqNum = newSeqNo - 1;

                _logger?.Info("GapFill advanced expected sequence: {GapFillSeq} -> {NewSeq}",
                    gapFillSeq, newSeqNo);
            }
        }

        await _store.SetTargetSeqNum(_expectedTargetSeqNum);
    }

    #endregion

    #region Gap Detection and Resend Requests

    /// <summary>
    /// Called when a sequence gap is detected.
    /// Returns the action to take (send ResendRequest, GapFill, wait, etc.)
    /// </summary>
    public ResendAction OnGapDetected(int expectedSeq, int receivedSeq)
    {
        var now = _clock.Current;

        lock (_lock)
        {
            return _resendManager.ComputeAction(expectedSeq, receivedSeq, now);
        }
    }

    /// <summary>
    /// Records that a ResendRequest was sent.
    /// </summary>
    public void RecordResendRequestSent(int begin, int end)
    {
        var now = _clock.Current;

        lock (_lock)
        {
            _resendManager.RecordRequestSent(begin, end, now);
            _logger?.Info("ResendRequest sent: {Begin}-{End}", begin, end);
        }
    }

    /// <summary>
    /// For testing/debugging: exposes resend manager state.
    /// </summary>
    public IReadOnlyList<PendingResendRange> PendingResendRequests
    {
        get { lock (_lock) return _resendManager.PendingRequests; }
    }

    #endregion

    #region Logon Retry Logic

    /// <summary>
    /// Called when logon is rejected due to sequence mismatch.
    /// Returns true if should retry with incremented sequence.
    /// </summary>
    public bool OnLogonRejectedForSequence(int maxRetries = 10)
    {
        lock (_lock)
        {
            _logonRetryCount++;
            if (_logonRetryCount <= maxRetries)
            {
                _nextSenderSeqNum++;
                _logger?.Info("Logon rejected, retrying with seq={Seq} (attempt {Count}/{Max})",
                    _nextSenderSeqNum, _logonRetryCount, maxRetries);
                return true;
            }

            _logger?.Warn("Logon retry limit reached ({Count}), giving up", _logonRetryCount);
            return false;
        }
    }

    /// <summary>
    /// Resets the logon retry counter (call on successful logon).
    /// </summary>
    public void ResetLogonRetryCount()
    {
        lock (_lock)
        {
            _logonRetryCount = 0;
        }
    }

    public int LogonRetryCount
    {
        get { lock (_lock) return _logonRetryCount; }
    }

    #endregion

    #region Timeout Recovery

    /// <summary>
    /// Increments timeout recovery attempt counter.
    /// Returns true if should continue trying, false if max exceeded.
    /// </summary>
    public bool IncrementTimeoutRecovery(int maxAttempts = 3)
    {
        lock (_lock)
        {
            _timeoutRecoveryAttempts++;
            var shouldContinue = _timeoutRecoveryAttempts <= maxAttempts;

            if (shouldContinue)
            {
                _logger?.Info("Timeout recovery attempt {Count}/{Max}",
                    _timeoutRecoveryAttempts, maxAttempts);
            }
            else
            {
                _logger?.Warn("Timeout recovery max attempts ({Max}) exceeded",
                    maxAttempts);
            }

            return shouldContinue;
        }
    }

    /// <summary>
    /// Resets timeout recovery counter (call when message received).
    /// </summary>
    public void ResetTimeoutRecovery()
    {
        lock (_lock)
        {
            if (_timeoutRecoveryAttempts > 0)
            {
                _logger?.Debug("Timeout recovery counter reset");
                _timeoutRecoveryAttempts = 0;
            }
        }
    }

    public int TimeoutRecoveryAttempts
    {
        get { lock (_lock) return _timeoutRecoveryAttempts; }
    }

    #endregion

    #region Reset Operations

    /// <summary>
    /// Prepares for reconnection on the same session.
    /// Resets transient state but preserves sequence numbers.
    /// </summary>
    public void PrepareForReconnect()
    {
        lock (_lock)
        {
            _logonRetryCount = 0;
            _timeoutRecoveryAttempts = 0;
            _resendManager.Reset();

            _logger?.Info("PrepareForReconnect: transient state cleared, sequences preserved (Sender={Sender}, Target={Target})",
                _nextSenderSeqNum, _expectedTargetSeqNum);
        }
    }

    /// <summary>
    /// Full session reset - clears store, resets sequences to 1.
    /// Call when ResetSeqNumFlag=Y is being used.
    /// </summary>
    public async Task ResetSession(string reason)
    {
        _logger?.Info("ResetSession: {Reason}", reason);

        lock (_lock)
        {
            _nextSenderSeqNum = 1;
            _expectedTargetSeqNum = 1;
            _lastProcessedPeerSeqNum = 0;
            _logonRetryCount = 0;
            _timeoutRecoveryAttempts = 0;
            _resendManager.Reset();
        }

        await _store.Reset();

        // Notify subscribers
        if (OnSessionReset != null)
        {
            await OnSessionReset.Invoke();
        }

        _logger?.Info("ResetSession complete: all sequences reset to 1");
    }

    /// <summary>
    /// Handles peer's ResetSeqNumFlag=Y in their Logon.
    /// </summary>
    /// <param name="peerSeqNum">The sequence number peer sent in their logon</param>
    /// <param name="weAlsoReset">Whether our config also has ResetSeqNumFlag=Y</param>
    public async Task HandlePeerReset(int peerSeqNum, bool weAlsoReset)
    {
        _logger?.Info("HandlePeerReset: peerSeq={PeerSeq}, weAlsoReset={WeAlso}",
            peerSeqNum, weAlsoReset);

        int? savedSenderSeq = null;

        lock (_lock)
        {
            // If we already reset (weAlsoReset=true), preserve our sender sequence
            if (weAlsoReset)
            {
                savedSenderSeq = _nextSenderSeqNum;
            }
        }

        // Reset the store (clears old messages)
        await _store.Reset();

        lock (_lock)
        {
            // Restore sender sequence if we already reset, otherwise take from store
            _nextSenderSeqNum = savedSenderSeq ?? _store.SenderSeqNum;

            // Set expected incoming based on peer's logon
            _expectedTargetSeqNum = peerSeqNum + 1;
            _lastProcessedPeerSeqNum = peerSeqNum;

            _resendManager.Reset();
        }

        await _store.SetTargetSeqNum(_expectedTargetSeqNum);

        // Notify subscribers
        if (OnSessionReset != null)
        {
            await OnSessionReset.Invoke();
        }

        _logger?.Info("HandlePeerReset complete: Sender={Sender}, ExpectedTarget={Target}",
            _nextSenderSeqNum, _expectedTargetSeqNum);
    }

    /// <summary>
    /// Handles acceptor responding with ResetSeqNumFlag=Y when peer didn't request it.
    /// </summary>
    public async Task ResetAsAcceptor()
    {
        _logger?.Info("ResetAsAcceptor: we're resetting even though peer didn't request");

        lock (_lock)
        {
            _nextSenderSeqNum = 1;
            _expectedTargetSeqNum = 1;
            _lastProcessedPeerSeqNum = 0;
            _resendManager.Reset();
        }

        await _store.Reset();
        await _store.SetTargetSeqNum(1);

        _logger?.Info("ResetAsAcceptor complete");
    }

    /// <summary>
    /// Periodic tick - cleans up stale resend requests.
    /// </summary>
    public void Tick()
    {
        var now = _clock.Current;
        lock (_lock)
        {
            _resendManager.Tick(now);
        }
    }

    #endregion
}

/// <summary>
/// Configuration for the ResendRequestManager within the coordinator.
/// </summary>
public class ResendManagerConfig
{
    public int MaxPendingRequests { get; init; } = 1;
    public int MaxRequestsPerWindow { get; init; } = 5;
    public int RateLimitWindowSeconds { get; init; } = 10;
    public int RequestTimeoutSeconds { get; init; } = 30;
}
