namespace PureFix.Transport.Session;

/// <summary>
/// Action types that can be taken when a sequence gap is detected.
/// </summary>
public enum ResendActionType
{
    /// <summary>No action needed.</summary>
    Nothing,

    /// <summary>Send a ResendRequest for the specified range.</summary>
    SendResendRequest,

    /// <summary>Send a GapFill (SequenceReset with GapFillFlag=Y) for the range.</summary>
    SendGapFill,

    /// <summary>Wait - a request is already pending that covers this range.</summary>
    Wait
}

/// <summary>
/// Represents the action to take when a sequence gap is detected.
/// Immutable record for clarity and testability.
/// </summary>
public record ResendAction
{
    public ResendActionType Type { get; init; }
    public int? Begin { get; init; }
    public int? End { get; init; }
    public string? Reason { get; init; }

    private ResendAction() { }

    public static ResendAction SendResendRequest(int begin, int end) => new()
    {
        Type = ResendActionType.SendResendRequest,
        Begin = begin,
        End = end,
        Reason = null
    };

    public static ResendAction GapFill(int begin, int end, string reason) => new()
    {
        Type = ResendActionType.SendGapFill,
        Begin = begin,
        End = end,
        Reason = reason
    };

    public static ResendAction Wait(string reason) => new()
    {
        Type = ResendActionType.Wait,
        Begin = null,
        End = null,
        Reason = reason
    };

    public static ResendAction Nothing() => new()
    {
        Type = ResendActionType.Nothing,
        Begin = null,
        End = null,
        Reason = null
    };

    public override string ToString() => Type switch
    {
        ResendActionType.SendResendRequest => $"SendResendRequest({Begin}-{End})",
        ResendActionType.SendGapFill => $"GapFill({Begin}-{End}): {Reason}",
        ResendActionType.Wait => $"Wait: {Reason}",
        ResendActionType.Nothing => "Nothing",
        _ => $"Unknown({Type})"
    };
}

/// <summary>
/// Tracks a pending resend request that we've sent and are waiting for responses.
/// </summary>
public class PendingResendRange
{
    public int Begin { get; }
    public int End { get; }
    public DateTime SentAt { get; }

    /// <summary>
    /// Sequences within this range that have been received.
    /// </summary>
    private readonly HashSet<int> _receivedSeqs = new();

    public PendingResendRange(int begin, int end, DateTime sentAt)
    {
        Begin = begin;
        End = end;
        SentAt = sentAt;
    }

    /// <summary>
    /// Mark a sequence number as received.
    /// </summary>
    public void MarkReceived(int seqNum)
    {
        if (seqNum >= Begin && seqNum <= End)
        {
            _receivedSeqs.Add(seqNum);
        }
    }

    /// <summary>
    /// Mark a range as received (e.g., from a GapFill).
    /// </summary>
    public void MarkRangeReceived(int fromSeq, int toSeq)
    {
        for (int seq = Math.Max(fromSeq, Begin); seq <= Math.Min(toSeq, End); seq++)
        {
            _receivedSeqs.Add(seq);
        }
    }

    /// <summary>
    /// Returns true if all sequences in this range have been received.
    /// </summary>
    public bool IsFullySatisfied => _receivedSeqs.Count >= (End - Begin + 1);

    /// <summary>
    /// Returns true if the given range is fully covered by this pending request.
    /// </summary>
    public bool FullyCovers(int begin, int end) => begin >= Begin && end <= End;

    /// <summary>
    /// Returns true if this pending request overlaps with the given range.
    /// </summary>
    public bool Overlaps(int begin, int end) => begin <= End && end >= Begin;

    /// <summary>
    /// Count of sequences still pending (not yet received).
    /// </summary>
    public int PendingCount => (End - Begin + 1) - _receivedSeqs.Count;

    public override string ToString() => $"Pending({Begin}-{End}, received={_receivedSeqs.Count}/{End - Begin + 1})";
}

/// <summary>
/// Record of a resend request that was sent (for history/debugging).
/// </summary>
public record ResendRequestRecord(
    int Begin,
    int End,
    DateTime SentAt,
    string? Reason
);

/// <summary>
/// Manages ResendRequest strategy with intelligent overlap handling and storm protection.
///
/// Philosophy:
/// 1. Stay alive - don't let resend handling crash or overwhelm the session
/// 2. Keep receiving new messages - don't block normal message flow
/// 3. Don't make things worse - avoid storms, handle overlaps intelligently
///
/// This class is designed to be testable independently of the session.
/// All decisions are based on explicit inputs with clear outputs.
/// </summary>
public class ResendRequestManager
{
    private readonly List<PendingResendRange> _pendingRequests = new();
    private readonly List<ResendRequestRecord> _requestHistory = new();
    private readonly object _lock = new();

    // Configuration
    private readonly int _maxPendingRequests;
    private readonly int _maxRequestsPerWindow;
    private readonly TimeSpan _rateLimitWindow;
    private readonly TimeSpan _requestTimeout;

    // Rate limiting state
    private readonly Queue<DateTime> _recentRequestTimes = new();

    /// <summary>
    /// Creates a new ResendRequestManager with the specified configuration.
    /// </summary>
    /// <param name="maxPendingRequests">Maximum concurrent pending requests (default: 1)</param>
    /// <param name="maxRequestsPerWindow">Max requests in rate limit window before storm protection (default: 5)</param>
    /// <param name="rateLimitWindowSeconds">Rate limit window in seconds (default: 10)</param>
    /// <param name="requestTimeoutSeconds">Seconds before a pending request is considered timed out (default: 30)</param>
    public ResendRequestManager(
        int maxPendingRequests = 1,
        int maxRequestsPerWindow = 5,
        int rateLimitWindowSeconds = 10,
        int requestTimeoutSeconds = 30)
    {
        _maxPendingRequests = maxPendingRequests;
        _maxRequestsPerWindow = maxRequestsPerWindow;
        _rateLimitWindow = TimeSpan.FromSeconds(rateLimitWindowSeconds);
        _requestTimeout = TimeSpan.FromSeconds(requestTimeoutSeconds);
    }

    /// <summary>
    /// Given a detected gap, compute what action to take.
    /// This is the core decision-making method.
    /// </summary>
    /// <param name="expectedSeq">The sequence number we expected</param>
    /// <param name="receivedSeq">The sequence number we actually received</param>
    /// <param name="now">Current time</param>
    /// <returns>The action to take</returns>
    public ResendAction ComputeAction(int expectedSeq, int receivedSeq, DateTime now)
    {
        if (receivedSeq <= expectedSeq)
        {
            return ResendAction.Nothing();
        }

        var gapBegin = expectedSeq;
        var gapEnd = receivedSeq - 1;

        lock (_lock)
        {
            // 1. Clean up timed-out requests first
            CleanupTimedOutRequests(now);

            // 2. Check rate limiting - if we're storming, gap fill everything
            if (IsStorming(now))
            {
                return ResendAction.GapFill(gapBegin, gapEnd, "storm protection - too many requests");
            }

            // 3. Check if this gap is fully covered by an existing pending request
            foreach (var pending in _pendingRequests)
            {
                if (pending.FullyCovers(gapBegin, gapEnd))
                {
                    return ResendAction.Wait($"fully covered by pending request {pending}");
                }
            }

            // 4. Compute the uncovered portion of this gap
            var uncovered = ComputeUncoveredRange(gapBegin, gapEnd);
            if (uncovered == null)
            {
                return ResendAction.Wait("gap partially covered, waiting for pending requests");
            }

            // 5. Check if we can send another request
            if (_pendingRequests.Count >= _maxPendingRequests)
            {
                return ResendAction.Wait($"max pending requests ({_maxPendingRequests}) reached");
            }

            // 6. We can send a request for the uncovered range
            return ResendAction.SendResendRequest(uncovered.Value.Begin, uncovered.Value.End);
        }
    }

    /// <summary>
    /// Records that a ResendRequest was actually sent.
    /// Call this after successfully sending the request.
    /// </summary>
    public void RecordRequestSent(int begin, int end, DateTime now, string? reason = null)
    {
        lock (_lock)
        {
            _pendingRequests.Add(new PendingResendRange(begin, end, now));
            _requestHistory.Add(new ResendRequestRecord(begin, end, now, reason));
            _recentRequestTimes.Enqueue(now);
        }
    }

    /// <summary>
    /// Called when a message is received (possibly a PossDup from resend).
    /// </summary>
    public void OnMessageReceived(int seqNum, bool possDupFlag, DateTime now)
    {
        lock (_lock)
        {
            foreach (var pending in _pendingRequests)
            {
                pending.MarkReceived(seqNum);
            }

            // Remove fully satisfied requests
            _pendingRequests.RemoveAll(p => p.IsFullySatisfied);
        }
    }

    /// <summary>
    /// Called when a SequenceReset-GapFill is received.
    /// </summary>
    public void OnGapFillReceived(int gapFillSeq, int newSeqNo, DateTime now)
    {
        lock (_lock)
        {
            // GapFill covers from gapFillSeq to newSeqNo - 1
            foreach (var pending in _pendingRequests)
            {
                pending.MarkRangeReceived(gapFillSeq, newSeqNo - 1);
            }

            // Remove fully satisfied requests
            _pendingRequests.RemoveAll(p => p.IsFullySatisfied);
        }
    }

    /// <summary>
    /// Called periodically to clean up state (e.g., timed-out requests).
    /// </summary>
    public void Tick(DateTime now)
    {
        lock (_lock)
        {
            CleanupTimedOutRequests(now);
            CleanupRateLimitWindow(now);
        }
    }

    /// <summary>
    /// Resets all state. Call when session is reset or reconnecting.
    /// </summary>
    public void Reset()
    {
        lock (_lock)
        {
            _pendingRequests.Clear();
            _recentRequestTimes.Clear();
            // Note: we keep _requestHistory for debugging
        }
    }

    #region State Inspection (for testing and debugging)

    public IReadOnlyList<PendingResendRange> PendingRequests
    {
        get { lock (_lock) return _pendingRequests.ToList(); }
    }

    public IReadOnlyList<ResendRequestRecord> RequestHistory
    {
        get { lock (_lock) return _requestHistory.ToList(); }
    }

    public int PendingCount
    {
        get { lock (_lock) return _pendingRequests.Count; }
    }

    public bool HasPendingRequests
    {
        get { lock (_lock) return _pendingRequests.Count > 0; }
    }

    #endregion

    #region Private Methods

    private bool IsStorming(DateTime now)
    {
        CleanupRateLimitWindow(now);
        return _recentRequestTimes.Count >= _maxRequestsPerWindow;
    }

    private void CleanupRateLimitWindow(DateTime now)
    {
        var cutoff = now - _rateLimitWindow;
        while (_recentRequestTimes.Count > 0 && _recentRequestTimes.Peek() < cutoff)
        {
            _recentRequestTimes.Dequeue();
        }
    }

    private void CleanupTimedOutRequests(DateTime now)
    {
        var cutoff = now - _requestTimeout;
        _pendingRequests.RemoveAll(p => p.SentAt < cutoff);
    }

    /// <summary>
    /// Computes the portion of the requested range that is NOT covered by any pending request.
    /// Returns null if the entire range is covered (possibly across multiple pending requests).
    /// </summary>
    private (int Begin, int End)? ComputeUncoveredRange(int gapBegin, int gapEnd)
    {
        if (_pendingRequests.Count == 0)
        {
            return (gapBegin, gapEnd);
        }

        // Simple implementation: find the first uncovered sequence
        // More sophisticated: could return multiple disjoint ranges

        // Build a set of all covered sequences
        var covered = new HashSet<int>();
        foreach (var pending in _pendingRequests)
        {
            for (int seq = pending.Begin; seq <= pending.End; seq++)
            {
                covered.Add(seq);
            }
        }

        // Find first uncovered sequence in the gap
        int? uncoveredBegin = null;
        int? uncoveredEnd = null;

        for (int seq = gapBegin; seq <= gapEnd; seq++)
        {
            if (!covered.Contains(seq))
            {
                uncoveredBegin ??= seq;
                uncoveredEnd = seq;
            }
            else if (uncoveredBegin.HasValue)
            {
                // Found a covered seq after uncovered - stop at the first contiguous uncovered range
                break;
            }
        }

        if (uncoveredBegin.HasValue && uncoveredEnd.HasValue)
        {
            return (uncoveredBegin.Value, uncoveredEnd.Value);
        }

        return null;
    }

    #endregion
}
