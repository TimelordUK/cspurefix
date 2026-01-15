using PureFix.Transport.Session;

namespace PureFix.Test.ModularTypes.Session;

/// <summary>
/// Tests for ResendRequestManager - intelligent resend request handling.
///
/// Test categories:
/// 1. Basic gap detection
/// 2. Overlap handling
/// 3. Rate limiting / storm protection
/// 4. Request completion tracking
/// 5. Timeout handling
/// </summary>
[TestFixture]
public class ResendRequestManagerTests
{
    private DateTime _now;

    [SetUp]
    public void Setup()
    {
        _now = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
    }

    #region Basic Gap Detection

    [Test]
    public void NoGap_ReturnsNothing()
    {
        var manager = new ResendRequestManager();

        // Expected 5, received 5 - no gap
        var action = manager.ComputeAction(5, 5, _now);

        Assert.That(action.Type, Is.EqualTo(ResendActionType.Nothing));
    }

    [Test]
    public void ReceivedLessThanExpected_ReturnsNothing()
    {
        var manager = new ResendRequestManager();

        // Expected 10, received 5 - this is a duplicate/old message, not a gap
        var action = manager.ComputeAction(10, 5, _now);

        Assert.That(action.Type, Is.EqualTo(ResendActionType.Nothing));
    }

    [Test]
    public void SimpleGap_ReturnsSendResendRequest()
    {
        var manager = new ResendRequestManager();

        // Expected 5, received 10 - gap of 5-9
        var action = manager.ComputeAction(5, 10, _now);

        Assert.That(action.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        Assert.That(action.Begin, Is.EqualTo(5));
        Assert.That(action.End, Is.EqualTo(9));
    }

    [Test]
    public void SingleMessageGap_ReturnsSendResendRequest()
    {
        var manager = new ResendRequestManager();

        // Expected 5, received 6 - gap of just seq 5
        var action = manager.ComputeAction(5, 6, _now);

        Assert.That(action.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        Assert.That(action.Begin, Is.EqualTo(5));
        Assert.That(action.End, Is.EqualTo(5));
    }

    #endregion

    #region Overlap Handling - Fully Covered

    [Test]
    public void GapFullyCoveredByPending_ReturnsWait()
    {
        var manager = new ResendRequestManager();

        // First gap: 50-100
        var action1 = manager.ComputeAction(50, 101, _now);
        Assert.That(action1.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        manager.RecordRequestSent(50, 100, _now);

        // Second gap: 60-90 (subset of first)
        var action2 = manager.ComputeAction(60, 91, _now);

        Assert.That(action2.Type, Is.EqualTo(ResendActionType.Wait));
        Assert.That(action2.Reason, Does.Contain("fully covered"));
    }

    [Test]
    public void SameGapRequestedTwice_ReturnsWait()
    {
        var manager = new ResendRequestManager();

        // First request
        var action1 = manager.ComputeAction(50, 101, _now);
        Assert.That(action1.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        manager.RecordRequestSent(50, 100, _now);

        // Same gap again
        var action2 = manager.ComputeAction(50, 101, _now);

        Assert.That(action2.Type, Is.EqualTo(ResendActionType.Wait));
    }

    #endregion

    #region Overlap Handling - Partial Overlap

    [Test]
    public void GapExtendsBeforePending_WithMaxOne_ReturnsWait()
    {
        // With max 1 pending (default), can't send additional request
        var manager = new ResendRequestManager(maxPendingRequests: 1);

        // First: request 50-100
        manager.RecordRequestSent(50, 100, _now);

        // New gap: 40-60 (40-49 not covered, but we're at max pending)
        var action = manager.ComputeAction(40, 61, _now);

        // Must wait because we're at max pending requests
        Assert.That(action.Type, Is.EqualTo(ResendActionType.Wait));
        Assert.That(action.Reason, Does.Contain("max pending"));
    }

    [Test]
    public void GapExtendsBeforePending_WithMaxTwo_RequestsUncoveredPortion()
    {
        // With max 2 pending, CAN send additional request for uncovered portion
        var manager = new ResendRequestManager(maxPendingRequests: 2);

        // First: request 50-100
        manager.RecordRequestSent(50, 100, _now);

        // New gap: 40-60 (40-49 not covered)
        var action = manager.ComputeAction(40, 61, _now);

        // Should request 40-49 only (the uncovered portion before)
        Assert.That(action.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        Assert.That(action.Begin, Is.EqualTo(40));
        Assert.That(action.End, Is.EqualTo(49));
    }

    [Test]
    public void GapExtendsBeyondPending_WithMaxTwo_RequestsUncoveredPortion()
    {
        var manager = new ResendRequestManager(maxPendingRequests: 2);

        // First: request 50-100
        manager.RecordRequestSent(50, 100, _now);

        // New gap: 90-120 (101-120 not covered)
        var action = manager.ComputeAction(90, 121, _now);

        // Should request 101-120 only (the uncovered portion after)
        Assert.That(action.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        Assert.That(action.Begin, Is.EqualTo(101));
        Assert.That(action.End, Is.EqualTo(120));
    }

    [Test]
    public void GapStraddlesPending_WithMaxTwo_RequestsFirstUncoveredPortion()
    {
        var manager = new ResendRequestManager(maxPendingRequests: 2);

        // First: request 50-100
        manager.RecordRequestSent(50, 100, _now);

        // New gap: 40-120 (straddles: 40-49 and 101-120 uncovered)
        var action = manager.ComputeAction(40, 121, _now);

        // Should request first uncovered portion (40-49)
        // The second portion (101-120) would be handled in a subsequent call
        Assert.That(action.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        Assert.That(action.Begin, Is.EqualTo(40));
        Assert.That(action.End, Is.EqualTo(49));
    }

    [Test]
    public void PartialOverlap_AfterFirstCompletes_CanRequestRemaining()
    {
        var manager = new ResendRequestManager(maxPendingRequests: 1);

        // First: request 50-100
        manager.RecordRequestSent(50, 100, _now);

        // New gap detected: 40-120 (can't request now - at max)
        var action1 = manager.ComputeAction(40, 121, _now);
        Assert.That(action1.Type, Is.EqualTo(ResendActionType.Wait));

        // First request completes
        manager.OnGapFillReceived(50, 101, _now);
        Assert.That(manager.PendingCount, Is.EqualTo(0));

        // Now can request the uncovered portions
        var action2 = manager.ComputeAction(40, 121, _now);
        Assert.That(action2.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        // Should request 40-120 since nothing is pending now
        Assert.That(action2.Begin, Is.EqualTo(40));
        Assert.That(action2.End, Is.EqualTo(120));
    }

    #endregion

    #region Max Pending Requests

    [Test]
    public void MaxPendingReached_ReturnsWait()
    {
        // Manager with max 1 pending request (default)
        var manager = new ResendRequestManager(maxPendingRequests: 1);

        // First request
        manager.RecordRequestSent(50, 100, _now);

        // New non-overlapping gap
        var action = manager.ComputeAction(200, 251, _now);

        Assert.That(action.Type, Is.EqualTo(ResendActionType.Wait));
        Assert.That(action.Reason, Does.Contain("max pending"));
    }

    [Test]
    public void MultipleAllowed_CanSendMultiple()
    {
        // Manager with max 3 pending requests
        var manager = new ResendRequestManager(maxPendingRequests: 3);

        // Send 3 non-overlapping requests
        manager.RecordRequestSent(10, 20, _now);
        manager.RecordRequestSent(30, 40, _now);
        manager.RecordRequestSent(50, 60, _now);

        // Fourth should wait
        var action = manager.ComputeAction(70, 81, _now);

        Assert.That(action.Type, Is.EqualTo(ResendActionType.Wait));
        Assert.That(manager.PendingCount, Is.EqualTo(3));
    }

    #endregion

    #region Storm Protection

    [Test]
    public void TooManyRequestsInWindow_ReturnsGapFill()
    {
        // Manager with 3 requests per 10 seconds
        var manager = new ResendRequestManager(
            maxRequestsPerWindow: 3,
            rateLimitWindowSeconds: 10
        );

        // Send 3 requests rapidly
        manager.RecordRequestSent(10, 20, _now);
        manager.RecordRequestSent(30, 40, _now);
        manager.RecordRequestSent(50, 60, _now);

        // Mark them all as satisfied so we can send more
        for (int seq = 10; seq <= 60; seq++)
        {
            manager.OnMessageReceived(seq, true, _now);
        }

        // Fourth request should trigger storm protection
        var action = manager.ComputeAction(70, 81, _now);

        Assert.That(action.Type, Is.EqualTo(ResendActionType.SendGapFill));
        Assert.That(action.Reason, Does.Contain("storm"));
    }

    [Test]
    public void StormProtectionClearsAfterWindow()
    {
        var manager = new ResendRequestManager(
            maxRequestsPerWindow: 3,
            rateLimitWindowSeconds: 10
        );

        // Send 3 requests
        manager.RecordRequestSent(10, 20, _now);
        manager.RecordRequestSent(30, 40, _now);
        manager.RecordRequestSent(50, 60, _now);

        // Satisfy them
        for (int seq = 10; seq <= 60; seq++)
        {
            manager.OnMessageReceived(seq, true, _now);
        }

        // Advance time past the window
        var laterTime = _now.AddSeconds(15);
        manager.Tick(laterTime);

        // Should now allow new request
        var action = manager.ComputeAction(70, 81, laterTime);

        Assert.That(action.Type, Is.EqualTo(ResendActionType.SendResendRequest));
    }

    #endregion

    #region Request Completion - Message Receipt

    [Test]
    public void AllMessagesReceived_RequestSatisfied()
    {
        var manager = new ResendRequestManager();

        // Request 5-10
        manager.RecordRequestSent(5, 10, _now);
        Assert.That(manager.PendingCount, Is.EqualTo(1));

        // Receive all messages
        for (int seq = 5; seq <= 10; seq++)
        {
            manager.OnMessageReceived(seq, true, _now);
        }

        Assert.That(manager.PendingCount, Is.EqualTo(0));
        Assert.That(manager.HasPendingRequests, Is.False);
    }

    [Test]
    public void PartialMessagesReceived_RequestStillPending()
    {
        var manager = new ResendRequestManager();

        // Request 5-10
        manager.RecordRequestSent(5, 10, _now);

        // Receive only some messages
        manager.OnMessageReceived(5, true, _now);
        manager.OnMessageReceived(6, true, _now);

        Assert.That(manager.PendingCount, Is.EqualTo(1));
        Assert.That(manager.PendingRequests[0].PendingCount, Is.EqualTo(4)); // 7,8,9,10 still pending
    }

    [Test]
    public void MessageOutsidePendingRange_Ignored()
    {
        var manager = new ResendRequestManager();

        // Request 5-10
        manager.RecordRequestSent(5, 10, _now);

        // Receive message outside range
        manager.OnMessageReceived(15, true, _now);

        // Should still have all 6 pending
        Assert.That(manager.PendingRequests[0].PendingCount, Is.EqualTo(6));
    }

    #endregion

    #region Request Completion - GapFill Receipt

    [Test]
    public void GapFillCoversEntireRequest_RequestSatisfied()
    {
        var manager = new ResendRequestManager();

        // Request 5-10
        manager.RecordRequestSent(5, 10, _now);

        // Receive GapFill that covers it
        manager.OnGapFillReceived(5, 11, _now); // GapFill 5, NewSeqNo 11 means 5-10 covered

        Assert.That(manager.PendingCount, Is.EqualTo(0));
    }

    [Test]
    public void GapFillCoversPartialRequest_RequestPartiallyComplete()
    {
        var manager = new ResendRequestManager();

        // Request 5-10
        manager.RecordRequestSent(5, 10, _now);

        // Receive GapFill that covers 5-7
        manager.OnGapFillReceived(5, 8, _now);

        Assert.That(manager.PendingCount, Is.EqualTo(1));
        Assert.That(manager.PendingRequests[0].PendingCount, Is.EqualTo(3)); // 8,9,10 still pending
    }

    [Test]
    public void CombinedGapFillAndMessages_RequestSatisfied()
    {
        var manager = new ResendRequestManager();

        // Request 5-10
        manager.RecordRequestSent(5, 10, _now);

        // Receive GapFill for 5-7
        manager.OnGapFillReceived(5, 8, _now);

        // Receive messages 8, 9, 10
        manager.OnMessageReceived(8, true, _now);
        manager.OnMessageReceived(9, true, _now);
        manager.OnMessageReceived(10, true, _now);

        Assert.That(manager.PendingCount, Is.EqualTo(0));
    }

    #endregion

    #region Timeout Handling

    [Test]
    public void PendingRequestTimesOut_RemovedAutomatically()
    {
        var manager = new ResendRequestManager(requestTimeoutSeconds: 30);

        // Send request at time 0
        manager.RecordRequestSent(5, 10, _now);
        Assert.That(manager.PendingCount, Is.EqualTo(1));

        // Advance time past timeout
        var laterTime = _now.AddSeconds(35);
        manager.Tick(laterTime);

        Assert.That(manager.PendingCount, Is.EqualTo(0));
    }

    [Test]
    public void TimeoutAllowsRetry()
    {
        var manager = new ResendRequestManager(requestTimeoutSeconds: 30);

        // Send request at time 0
        manager.RecordRequestSent(5, 10, _now);

        // Advance time past timeout
        var laterTime = _now.AddSeconds(35);
        manager.Tick(laterTime);

        // Same gap should now be allowed again
        var action = manager.ComputeAction(5, 11, laterTime);

        Assert.That(action.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        Assert.That(action.Begin, Is.EqualTo(5));
        Assert.That(action.End, Is.EqualTo(10));
    }

    #endregion

    #region Reset

    [Test]
    public void Reset_ClearsPendingRequests()
    {
        var manager = new ResendRequestManager();

        manager.RecordRequestSent(5, 10, _now);
        manager.RecordRequestSent(20, 30, _now);

        manager.Reset();

        Assert.That(manager.PendingCount, Is.EqualTo(0));
    }

    [Test]
    public void Reset_PreservesHistory()
    {
        var manager = new ResendRequestManager();

        manager.RecordRequestSent(5, 10, _now);
        manager.Reset();

        // History should still be available for debugging
        Assert.That(manager.RequestHistory.Count, Is.EqualTo(1));
    }

    #endregion

    #region Edge Cases

    [Test]
    public void ZeroEndSequence_HandledCorrectly()
    {
        var manager = new ResendRequestManager();

        // FIX protocol: EndSeqNo=0 means "to infinity"
        // Our manager deals with concrete ranges, so this shouldn't happen
        // but let's make sure it doesn't crash

        var action = manager.ComputeAction(1, 1, _now);
        Assert.That(action.Type, Is.EqualTo(ResendActionType.Nothing));
    }

    [Test]
    public void LargeGap_StillWorks()
    {
        var manager = new ResendRequestManager();

        // Very large gap
        var action = manager.ComputeAction(1, 10001, _now);

        Assert.That(action.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        Assert.That(action.Begin, Is.EqualTo(1));
        Assert.That(action.End, Is.EqualTo(10000));
    }

    #endregion
}
