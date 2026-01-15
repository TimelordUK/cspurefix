using PureFix.Transport.Session;

namespace PureFix.Test.ModularTypes.Session;

/// <summary>
/// Tests for race conditions between GapFill and actual message delivery.
///
/// These scenarios occur in real networks where:
/// - ResendRequest is sent for missing messages
/// - Peer responds with GapFill (doesn't have the messages)
/// - But the original messages were just delayed and arrive shortly after
///
/// Key principle: PREFER ACTUAL MESSAGES OVER GAP FILLS
/// If we receive real data, we should use it even if we already "skipped" that sequence.
/// </summary>
[TestFixture]
public class GapFillRaceConditionTests
{
    private DateTime _now;

    [SetUp]
    public void Setup()
    {
        _now = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc);
    }

    #region Scenario A: GapFill first, actual message after

    [Test]
    public void GapFillFirst_ThenActualMessage_ShouldAcceptActualMessage()
    {
        // This is the key race condition:
        // 1. Gap detected (expected 10, received 15)
        // 2. ResendRequest sent for 10-14
        // 3. GapFill received saying "skip 10-14"
        // 4. Actual message seq 12 arrives (delayed on network)
        //
        // We SHOULD accept seq 12 - it's real data!
        // But current implementation rejects it because GapFill cleared the pending request.

        var manager = new ResendRequestManager();

        // Step 1: Gap detected, request sent
        var action = manager.ComputeAction(10, 15, _now);
        Assert.That(action.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        manager.RecordRequestSent(10, 14, _now);

        // Step 2: GapFill arrives - peer says "I don't have 10-14"
        manager.OnGapFillReceived(10, 15, _now);
        Assert.That(manager.PendingCount, Is.EqualTo(0), "GapFill should clear pending request");

        // Step 3: Actual message seq 12 arrives (delayed original)
        // This is the race condition - we need to track that 12 was gap-filled, not processed

        // TODO: Current implementation has no way to know seq 12 was gap-filled
        // We need a way to ask: "Was this sequence gap-filled (skipped) or actually received?"

        // For now, document the expected behavior:
        // manager.WasGapFilled(12) should return true
        // manager.WasActuallyReceived(12) should return false
        // Therefore, we SHOULD accept the actual message for seq 12

        Assert.Inconclusive("Need to implement gap-fill tracking to handle this scenario");
    }

    [Test]
    public void GapFillPartial_ThenActualMessagesForRest_ShouldAccept()
    {
        // Scenario: GapFill only covers part of the range
        // 1. Gap detected: 10-14
        // 2. GapFill for 10-12 (peer doesn't have these)
        // 3. Actual messages 13, 14 arrive
        //
        // Should accept 13, 14 as real data

        var manager = new ResendRequestManager();

        // Gap detected
        manager.RecordRequestSent(10, 14, _now);

        // Partial GapFill - only 10-12
        manager.OnGapFillReceived(10, 13, _now); // NewSeqNo=13 means skip 10-12

        // Pending request should still exist for 13-14
        Assert.That(manager.PendingCount, Is.EqualTo(1));
        Assert.That(manager.PendingRequests[0].PendingCount, Is.EqualTo(2)); // 13, 14 still pending

        // Actual messages 13, 14 arrive
        manager.OnMessageReceived(13, false, _now);
        manager.OnMessageReceived(14, false, _now);

        // Request should now be fully satisfied
        Assert.That(manager.PendingCount, Is.EqualTo(0));
    }

    #endregion

    #region Scenario B: Actual message first, GapFill after

    [Test]
    public void ActualMessageFirst_ThenGapFill_ShouldKeepActualMessage()
    {
        // 1. Gap detected: 10-14
        // 2. Actual message seq 12 arrives (partial fill)
        // 3. GapFill 10-14 arrives
        //
        // Seq 12 was already processed - GapFill shouldn't affect it

        var manager = new ResendRequestManager();

        // Gap detected
        manager.RecordRequestSent(10, 14, _now);

        // Actual message 12 arrives first
        manager.OnMessageReceived(12, false, _now);
        Assert.That(manager.PendingRequests[0].PendingCount, Is.EqualTo(4)); // 10,11,13,14 pending

        // GapFill for entire range arrives
        manager.OnGapFillReceived(10, 15, _now);

        // Request should be cleared
        Assert.That(manager.PendingCount, Is.EqualTo(0));

        // The important part: seq 12 was ACTUALLY received (has data)
        // while 10,11,13,14 were gap-filled (no data)
        // This distinction matters for duplicate detection later
    }

    #endregion

    #region Scenario C: True duplicates

    [Test]
    public void ActualMessage_ThenSameMessageAgain_ShouldDetectDuplicate()
    {
        // 1. Receive actual message seq 12
        // 2. Same message seq 12 arrives again (maybe PossDup, maybe delayed duplicate)
        //
        // Should detect this as a duplicate

        var manager = new ResendRequestManager();

        // First receipt of seq 12 (as part of gap fill response)
        manager.RecordRequestSent(10, 14, _now);
        manager.OnMessageReceived(12, possDupFlag: true, _now);

        // Same seq 12 arrives again
        // TODO: Need way to detect "already received seq 12"

        Assert.Inconclusive("Need to track actually-received sequences for duplicate detection");
    }

    [Test]
    public void GapFilled_ThenPossDup_ShouldRejectPossDup()
    {
        // 1. GapFill skips seq 10-14
        // 2. PossDup for seq 12 arrives
        //
        // Should we accept PossDup? It's marked as possible duplicate...
        // Current FIX semantics: PossDup messages bypass sequence checking
        // But if we gap-filled, we never had the original, so it's not a duplicate!
        //
        // This is tricky - PossDup means "you might have already seen this"
        // If we gap-filled, we definitely DIDN'T see it, so we SHOULD accept it!

        var manager = new ResendRequestManager();

        // Gap detected, request sent
        manager.RecordRequestSent(10, 14, _now);

        // GapFill - we skip 10-14
        manager.OnGapFillReceived(10, 15, _now);

        // PossDup for seq 12 arrives
        // Since we gap-filled (didn't actually receive), we SHOULD accept this!
        // It's not a duplicate - it's the first time we're seeing the actual data

        Assert.Inconclusive("Need gap-fill tracking to distinguish from actual duplicates");
    }

    #endregion

    #region Scenario D: Interleaved arrivals

    [Test]
    public void InterleavedArrivals_MixOfGapFillAndActual()
    {
        // Real-world chaos:
        // 1. Gap 10-20 detected
        // 2. GapFill 10-12 (skip these)
        // 3. Actual msg 15 arrives
        // 4. Actual msg 13 arrives
        // 5. GapFill 14-14 (just skip 14)
        // 6. Actual msg 17 arrives
        // 7. GapFill 16-16, 18-20
        //
        // End state: We have actual data for 13, 15, 17
        //            We gap-filled (skipped) 10-12, 14, 16, 18-20

        var manager = new ResendRequestManager();

        manager.RecordRequestSent(10, 20, _now);
        Assert.That(manager.PendingRequests[0].PendingCount, Is.EqualTo(11)); // 10-20

        // GapFill 10-12
        manager.OnGapFillReceived(10, 13, _now);
        Assert.That(manager.PendingRequests[0].PendingCount, Is.EqualTo(8)); // 13-20

        // Actual 15
        manager.OnMessageReceived(15, false, _now);
        Assert.That(manager.PendingRequests[0].PendingCount, Is.EqualTo(7)); // 13,14,16-20

        // Actual 13
        manager.OnMessageReceived(13, false, _now);
        Assert.That(manager.PendingRequests[0].PendingCount, Is.EqualTo(6)); // 14,16-20

        // GapFill 14
        manager.OnGapFillReceived(14, 15, _now);
        Assert.That(manager.PendingRequests[0].PendingCount, Is.EqualTo(5)); // 16-20

        // Actual 17
        manager.OnMessageReceived(17, false, _now);
        Assert.That(manager.PendingRequests[0].PendingCount, Is.EqualTo(4)); // 16,18-20

        // GapFill rest
        manager.OnGapFillReceived(16, 17, _now); // skip 16
        manager.OnGapFillReceived(18, 21, _now); // skip 18-20

        Assert.That(manager.PendingCount, Is.EqualTo(0), "All sequences accounted for");
    }

    #endregion

    #region Proposed: Gap-fill tracking

    [Test]
    public void ProposedApi_TrackGapFilledVsActuallyReceived()
    {
        // Proposed enhancement to ResendRequestManager:
        // - Track which sequences were gap-filled (skipped, no data)
        // - Track which sequences were actually received (have data)
        // - Provide API to query this distinction

        // Proposed API:
        // manager.WasGapFilled(seq) -> bool
        // manager.WasActuallyReceived(seq) -> bool
        // manager.GetGapFilledRanges() -> List<(int, int)>
        // manager.GetActuallyReceivedInRange(begin, end) -> List<int>

        Assert.Inconclusive("Proposed API - not yet implemented");
    }

    #endregion
}
