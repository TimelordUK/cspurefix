using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport.Session;
using PureFix.Transport.Store;

namespace PureFix.Test.ModularTypes.Session;

/// <summary>
/// Tests for SessionSequenceCoordinator - the single source of truth for session sequence state.
///
/// Test categories:
/// 1. Initialization (from store, from config)
/// 2. Sender sequence management
/// 3. Target sequence management (message receipt)
/// 4. Gap detection and resend requests
/// 5. Reset scenarios (reconnect, full reset, peer reset)
/// 6. Logon retry logic
/// 7. Timeout recovery
/// </summary>
[TestFixture]
public class SessionSequenceCoordinatorTests
{
    private TestClock _clock = null!;
    private MockSessionStore _store = null!;

    [SetUp]
    public async Task Setup()
    {
        _clock = new TestClock { Current = new DateTime(2024, 1, 1, 12, 0, 0, DateTimeKind.Utc) };
        _store = new MockSessionStore();
        await _store.Initialize();
    }

    #region Initialization

    [Test]
    public async Task InitializeFromStore_LoadsStoredSequences()
    {
        // Arrange - store has existing sequences
        await _store.SetSenderSeqNum(50);
        await _store.SetTargetSeqNum(75);

        var coordinator = new SessionSequenceCoordinator(_store, _clock);

        // Act
        coordinator.InitializeFromStore();

        // Assert
        Assert.That(coordinator.NextSenderSeqNum, Is.EqualTo(50));
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(75));
    }

    [Test]
    public async Task InitializeFromConfig_OverridesStoreValues()
    {
        // Arrange - store has sequences, config overrides
        await _store.SetSenderSeqNum(50);
        await _store.SetTargetSeqNum(75);

        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        coordinator.InitializeFromStore();

        // Act - config overrides
        coordinator.InitializeFromConfig(senderSeqNum: 100, targetSeqNum: 200);

        // Assert
        Assert.That(coordinator.NextSenderSeqNum, Is.EqualTo(100));
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(200));
    }

    [Test]
    public void InitializeFromConfig_PartialOverride_OnlySender()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        coordinator.InitializeFromStore();

        coordinator.InitializeFromConfig(senderSeqNum: 100, targetSeqNum: null);

        Assert.That(coordinator.NextSenderSeqNum, Is.EqualTo(100));
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(1)); // Default
    }

    #endregion

    #region Sender Sequence Management

    [Test]
    public void GetNextSenderSeqNum_IncrementsSequence()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);

        var seq1 = coordinator.GetNextSenderSeqNum();
        var seq2 = coordinator.GetNextSenderSeqNum();
        var seq3 = coordinator.GetNextSenderSeqNum();

        Assert.That(seq1, Is.EqualTo(1));
        Assert.That(seq2, Is.EqualTo(2));
        Assert.That(seq3, Is.EqualTo(3));
        Assert.That(coordinator.NextSenderSeqNum, Is.EqualTo(4));
    }

    [Test]
    public void GetNextSenderSeqNum_PossDup_DoesNotIncrement()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        coordinator.GetNextSenderSeqNum(); // Consume 1

        var current = coordinator.NextSenderSeqNum; // Now 2
        var possDupSeq = coordinator.GetNextSenderSeqNum(isPossDup: true);

        Assert.That(possDupSeq, Is.EqualTo(current));
        Assert.That(coordinator.NextSenderSeqNum, Is.EqualTo(current)); // Unchanged
    }

    [Test]
    public async Task OnMessageEncoded_UpdatesStore()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);

        await coordinator.OnMessageEncoded(5, isPossDup: false);

        Assert.That(_store.SenderSeqNum, Is.EqualTo(6)); // seqNum + 1
    }

    [Test]
    public async Task OnMessageEncoded_PossDup_DoesNotUpdateStore()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        await _store.SetSenderSeqNum(10);

        await coordinator.OnMessageEncoded(5, isPossDup: true);

        Assert.That(_store.SenderSeqNum, Is.EqualTo(10)); // Unchanged
    }

    #endregion

    #region Target Sequence Management

    [Test]
    public async Task OnMessageReceived_ExpectedSequence_Advances()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(1));

        var result = await coordinator.OnMessageReceived(1, possDupFlag: false);

        Assert.That(result, Is.True);
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(2));
        Assert.That(coordinator.LastProcessedPeerSeqNum, Is.EqualTo(1));
    }

    [Test]
    public async Task OnMessageReceived_OldSequence_ReturnsFalse()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        await coordinator.OnMessageReceived(1, false);
        await coordinator.OnMessageReceived(2, false);
        // Now expecting 3

        var result = await coordinator.OnMessageReceived(1, possDupFlag: false);

        Assert.That(result, Is.False); // Old message ignored
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(3)); // Unchanged
    }

    [Test]
    public async Task OnMessageReceived_PossDup_AlwaysReturnsTrue()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        await coordinator.OnMessageReceived(1, false);
        await coordinator.OnMessageReceived(2, false);
        // Now expecting 3

        // PossDup of already-seen message
        var result = await coordinator.OnMessageReceived(1, possDupFlag: true);

        Assert.That(result, Is.True); // PossDup is always processed
    }

    [Test]
    public async Task OnMessageReceived_GapDetected_UpdatesLastProcessed()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        // Expecting 1, but receive 5 (gap of 1-4)

        await coordinator.OnMessageReceived(5, possDupFlag: false);

        Assert.That(coordinator.LastProcessedPeerSeqNum, Is.EqualTo(5));
        // ExpectedTargetSeqNum stays at 1 until gap is filled
    }

    [Test]
    public async Task OnGapFillReceived_AdvancesExpected()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        // Expecting 1

        // GapFill from 1 to 10 (skips 1-9)
        await coordinator.OnGapFillReceived(1, 10);

        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(10));
        Assert.That(coordinator.LastProcessedPeerSeqNum, Is.EqualTo(9));
    }

    #endregion

    #region Gap Detection and Resend

    [Test]
    public void OnGapDetected_SendsResendRequest()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);

        // Expected 5, received 10 (gap of 5-9)
        var action = coordinator.OnGapDetected(5, 10);

        Assert.That(action.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        Assert.That(action.Begin, Is.EqualTo(5));
        Assert.That(action.End, Is.EqualTo(9));
    }

    [Test]
    public void OnGapDetected_WithPending_ReturnsWait()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);

        // First gap
        var action1 = coordinator.OnGapDetected(5, 10);
        Assert.That(action1.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        coordinator.RecordResendRequestSent(5, 9);

        // Second gap (same range)
        var action2 = coordinator.OnGapDetected(5, 10);

        Assert.That(action2.Type, Is.EqualTo(ResendActionType.Wait));
    }

    [Test]
    public void RecordResendRequestSent_TracksPending()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);

        coordinator.RecordResendRequestSent(10, 20);

        Assert.That(coordinator.PendingResendRequests.Count, Is.EqualTo(1));
        Assert.That(coordinator.PendingResendRequests[0].Begin, Is.EqualTo(10));
        Assert.That(coordinator.PendingResendRequests[0].End, Is.EqualTo(20));
    }

    #endregion

    #region Reset Scenarios

    [Test]
    public void PrepareForReconnect_PreservesSequences()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        coordinator.GetNextSenderSeqNum(); // 1
        coordinator.GetNextSenderSeqNum(); // 2
        coordinator.GetNextSenderSeqNum(); // 3
        // NextSenderSeqNum is now 4

        coordinator.RecordResendRequestSent(10, 20); // Add pending

        coordinator.PrepareForReconnect();

        // Sequences preserved
        Assert.That(coordinator.NextSenderSeqNum, Is.EqualTo(4));
        // Transient state cleared
        Assert.That(coordinator.PendingResendRequests.Count, Is.EqualTo(0));
        Assert.That(coordinator.LogonRetryCount, Is.EqualTo(0));
        Assert.That(coordinator.TimeoutRecoveryAttempts, Is.EqualTo(0));
    }

    [Test]
    public async Task ResetSession_ClearsEverything()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        coordinator.GetNextSenderSeqNum();
        coordinator.GetNextSenderSeqNum();
        coordinator.GetNextSenderSeqNum();
        await _store.SetTargetSeqNum(50);

        bool resetCalled = false;
        coordinator.OnSessionReset += () => { resetCalled = true; return Task.CompletedTask; };

        await coordinator.ResetSession("test reset");

        Assert.That(coordinator.NextSenderSeqNum, Is.EqualTo(1));
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(1));
        Assert.That(coordinator.LastProcessedPeerSeqNum, Is.EqualTo(0));
        Assert.That(_store.SenderSeqNum, Is.EqualTo(1));
        Assert.That(_store.TargetSeqNum, Is.EqualTo(1));
        Assert.That(resetCalled, Is.True);
    }

    [Test]
    public async Task HandlePeerReset_WeAlsoReset_PreservesOurSenderSeq()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        coordinator.GetNextSenderSeqNum(); // 1
        coordinator.GetNextSenderSeqNum(); // 2
        // NextSenderSeqNum is now 3

        // Peer sends logon with seq 1 and ResetSeqNumFlag=Y
        // We also have ResetSeqNumFlag=Y, so we already sent our logon with our seq
        await coordinator.HandlePeerReset(peerSeqNum: 1, weAlsoReset: true);

        // Our sender seq preserved (we already sent our logon)
        Assert.That(coordinator.NextSenderSeqNum, Is.EqualTo(3));
        // Target seq set based on peer's logon
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(2)); // peerSeqNum + 1
    }

    [Test]
    public async Task HandlePeerReset_WeDidNotReset_TakesFromStore()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        coordinator.GetNextSenderSeqNum();
        coordinator.GetNextSenderSeqNum();
        // NextSenderSeqNum is now 3

        // Peer sends reset but we didn't
        await coordinator.HandlePeerReset(peerSeqNum: 1, weAlsoReset: false);

        // Our sender seq comes from store (which was reset to 1)
        Assert.That(coordinator.NextSenderSeqNum, Is.EqualTo(1));
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(2));
    }

    [Test]
    public async Task ResetAsAcceptor_ResetsEverythingTo1()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        coordinator.GetNextSenderSeqNum();
        coordinator.GetNextSenderSeqNum();
        await coordinator.OnMessageReceived(5, false);

        await coordinator.ResetAsAcceptor();

        Assert.That(coordinator.NextSenderSeqNum, Is.EqualTo(1));
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(1));
        Assert.That(_store.SenderSeqNum, Is.EqualTo(1));
        Assert.That(_store.TargetSeqNum, Is.EqualTo(1));
    }

    #endregion

    #region Logon Retry Logic

    [Test]
    public void OnLogonRejectedForSequence_IncrementsAndRetries()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        var initialSeq = coordinator.NextSenderSeqNum;

        var shouldRetry = coordinator.OnLogonRejectedForSequence(maxRetries: 10);

        Assert.That(shouldRetry, Is.True);
        Assert.That(coordinator.NextSenderSeqNum, Is.EqualTo(initialSeq + 1));
        Assert.That(coordinator.LogonRetryCount, Is.EqualTo(1));
    }

    [Test]
    public void OnLogonRejectedForSequence_MaxRetriesExceeded_ReturnsFalse()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);

        // Exhaust retries
        for (int i = 0; i < 10; i++)
        {
            coordinator.OnLogonRejectedForSequence(maxRetries: 10);
        }

        // 11th should fail
        var shouldRetry = coordinator.OnLogonRejectedForSequence(maxRetries: 10);

        Assert.That(shouldRetry, Is.False);
        Assert.That(coordinator.LogonRetryCount, Is.EqualTo(11));
    }

    [Test]
    public void ResetLogonRetryCount_ClearsCounter()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        coordinator.OnLogonRejectedForSequence();
        coordinator.OnLogonRejectedForSequence();
        Assert.That(coordinator.LogonRetryCount, Is.EqualTo(2));

        coordinator.ResetLogonRetryCount();

        Assert.That(coordinator.LogonRetryCount, Is.EqualTo(0));
    }

    #endregion

    #region Timeout Recovery

    [Test]
    public void IncrementTimeoutRecovery_ReturnsTrue_UntilMaxExceeded()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);

        Assert.That(coordinator.IncrementTimeoutRecovery(maxAttempts: 3), Is.True);
        Assert.That(coordinator.IncrementTimeoutRecovery(maxAttempts: 3), Is.True);
        Assert.That(coordinator.IncrementTimeoutRecovery(maxAttempts: 3), Is.True);
        Assert.That(coordinator.IncrementTimeoutRecovery(maxAttempts: 3), Is.False);
    }

    [Test]
    public void ResetTimeoutRecovery_ClearsCounter()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);
        coordinator.IncrementTimeoutRecovery();
        coordinator.IncrementTimeoutRecovery();

        coordinator.ResetTimeoutRecovery();

        Assert.That(coordinator.TimeoutRecoveryAttempts, Is.EqualTo(0));
    }

    #endregion

    #region GapFill Rewind Prevention (Bug Fix Tests)

    /// <summary>
    /// Scenario from production: We've advanced to seq 22, then an old GapFill arrives
    /// (from an earlier ResendRequest) with newSeqNo=17. This should NOT rewind our state.
    /// </summary>
    [Test]
    public async Task OnGapFillReceived_OldGapFill_DoesNotRewindExpected()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);

        // Receive messages 1-5 normally
        for (int i = 1; i <= 5; i++)
        {
            await coordinator.OnMessageReceived(i, possDupFlag: false);
        }
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(6));
        Assert.That(coordinator.LastProcessedPeerSeqNum, Is.EqualTo(5));

        // Gap detected: receive 10, missing 6-9
        await coordinator.OnMessageReceived(10, possDupFlag: false);
        Assert.That(coordinator.LastProcessedPeerSeqNum, Is.EqualTo(10));
        // Expected stays at 6 (gap not filled)

        // Continue receiving 11-15
        for (int i = 11; i <= 15; i++)
        {
            await coordinator.OnMessageReceived(i, possDupFlag: false);
        }
        Assert.That(coordinator.LastProcessedPeerSeqNum, Is.EqualTo(15));

        // Now an OLD GapFill arrives (from a previous request) with lower sequence
        // This simulates the bug scenario where GapFill for 6->10 arrives late
        await coordinator.OnGapFillReceived(6, 10);

        // Expected should advance to 10, but lastProcessed should NOT rewind from 15 to 9
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(10));
        Assert.That(coordinator.LastProcessedPeerSeqNum, Is.EqualTo(15),
            "LastProcessedPeerSeqNum should NOT be rewound by old GapFill");
    }

    /// <summary>
    /// Scenario: GapFill with newSeqNo lower than current expected should be ignored entirely.
    /// </summary>
    [Test]
    public async Task OnGapFillReceived_LowerThanExpected_IsIgnored()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);

        // Advance to expecting seq 20
        for (int i = 1; i <= 19; i++)
        {
            await coordinator.OnMessageReceived(i, possDupFlag: false);
        }
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(20));
        Assert.That(coordinator.LastProcessedPeerSeqNum, Is.EqualTo(19));

        // Old GapFill arrives with newSeqNo=15 (lower than expected 20)
        await coordinator.OnGapFillReceived(10, 15);

        // Nothing should change
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(20),
            "Expected should not change when GapFill newSeqNo < current expected");
        Assert.That(coordinator.LastProcessedPeerSeqNum, Is.EqualTo(19),
            "LastProcessed should not change when GapFill is ignored");
    }

    /// <summary>
    /// The loop scenario: After advancing past a gap, we should NOT re-request it.
    /// This tests the ResendRequestManager integration.
    /// </summary>
    [Test]
    public async Task OnGapDetected_AfterAdvancingPastGap_DoesNotReRequest()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);

        // Receive 1-5 normally
        for (int i = 1; i <= 5; i++)
        {
            await coordinator.OnMessageReceived(i, possDupFlag: false);
        }

        // Gap detected at seq 10 (missing 6-9)
        var action1 = coordinator.OnGapDetected(6, 10);
        Assert.That(action1.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        Assert.That(action1.Begin, Is.EqualTo(6));
        Assert.That(action1.End, Is.EqualTo(9));
        coordinator.RecordResendRequestSent(6, 9);

        // Continue receiving 10-15 (advancing past the original gap)
        for (int i = 10; i <= 15; i++)
        {
            await coordinator.OnMessageReceived(i, possDupFlag: false);
        }

        // GapFill arrives for the original gap
        await coordinator.OnGapFillReceived(6, 10);

        // Now if another gap is detected at say 20 (missing 16-19),
        // we should request 16-19, NOT re-request 6-9
        var action2 = coordinator.OnGapDetected(16, 20);
        Assert.That(action2.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        Assert.That(action2.Begin, Is.EqualTo(16), "Should request from 16, not from old gap");
        Assert.That(action2.End, Is.EqualTo(19));
    }

    /// <summary>
    /// Simulates the exact production loop scenario:
    /// 1. Normal messages advance session to seq 22
    /// 2. Old GapFill (14->17) arrives from previous ResendRequest
    /// 3. Before the fix, this would rewind lastProcessedPeerSeqNum to 16
    /// 4. Next message (seq 25) would detect gap 17-24
    /// 5. This creates a loop of re-requesting already-received messages
    /// </summary>
    [Test]
    public async Task ProductionLoopScenario_OldGapFillDoesNotCauseLoop()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock);

        // Normal messages 1-13
        for (int i = 1; i <= 13; i++)
        {
            await coordinator.OnMessageReceived(i, possDupFlag: false);
        }
        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(14));

        // Gap at 17 (missing 14-16), ResendRequest sent
        var action1 = coordinator.OnGapDetected(14, 17);
        Assert.That(action1.Type, Is.EqualTo(ResendActionType.SendResendRequest));
        coordinator.RecordResendRequestSent(14, 16);

        // Receive 17-22 while waiting for gap fill
        for (int i = 17; i <= 22; i++)
        {
            await coordinator.OnMessageReceived(i, possDupFlag: false);
        }
        Assert.That(coordinator.LastProcessedPeerSeqNum, Is.EqualTo(22));
        // Expected still 14 (gap not filled)

        // OLD GapFill arrives: 14->17
        // Before fix: this would set lastProcessed to 16, causing rewind
        // After fix: lastProcessed stays at 22
        await coordinator.OnGapFillReceived(14, 17);

        Assert.That(coordinator.ExpectedTargetSeqNum, Is.EqualTo(17));
        Assert.That(coordinator.LastProcessedPeerSeqNum, Is.EqualTo(22),
            "CRITICAL: lastProcessedPeerSeqNum must NOT rewind from 22 to 16");

        // Now message 25 arrives
        await coordinator.OnMessageReceived(25, possDupFlag: false);

        // The gap should be 17-24, NOT 17-24 repeatedly
        // (If lastProcessed had rewound to 16, we'd see gap 17-24 in a loop)
        var action2 = coordinator.OnGapDetected(17, 25);

        // This should request 17-24, not re-request anything we've already seen
        Assert.That(action2.Begin, Is.GreaterThanOrEqualTo(17));
        Assert.That(action2.End, Is.LessThanOrEqualTo(24));
    }

    #endregion

    #region Tick (Cleanup)

    [Test]
    public void Tick_CleansUpStaleResendRequests()
    {
        var coordinator = new SessionSequenceCoordinator(_store, _clock,
            resendManagerConfig: new ResendManagerConfig { RequestTimeoutSeconds = 30 });

        coordinator.RecordResendRequestSent(10, 20);
        Assert.That(coordinator.PendingResendRequests.Count, Is.EqualTo(1));

        // Advance clock past timeout
        _clock.Current = _clock.Current.AddSeconds(35);
        coordinator.Tick();

        Assert.That(coordinator.PendingResendRequests.Count, Is.EqualTo(0));
    }

    #endregion
}

/// <summary>
/// Simple mock session store for testing.
/// </summary>
internal class MockSessionStore : IFixSessionStore
{
    public int SenderSeqNum { get; private set; } = 1;
    public int TargetSeqNum { get; private set; } = 1;
    public SessionId SessionId { get; } = new("FIX.4.4", "SENDER", "TARGET");
    public DateTime CreationTime { get; private set; } = DateTime.UtcNow;

    private readonly List<IFixMsgStoreRecord> _messages = new();

    public Task Initialize() => Task.CompletedTask;

    public Task SetSenderSeqNum(int seqNum)
    {
        SenderSeqNum = seqNum;
        return Task.CompletedTask;
    }

    public Task SetTargetSeqNum(int seqNum)
    {
        TargetSeqNum = seqNum;
        return Task.CompletedTask;
    }

    public Task<int> NextSenderSeqNum()
    {
        return Task.FromResult(SenderSeqNum++);
    }

    public Task<int> NextTargetSeqNum()
    {
        return Task.FromResult(TargetSeqNum++);
    }

    public Task Put(IFixMsgStoreRecord record)
    {
        _messages.Add(record);
        return Task.CompletedTask;
    }

    public Task<IFixMsgStoreRecord?> Get(int seqNum)
    {
        var result = _messages.FirstOrDefault(m => m.SeqNum == seqNum);
        return Task.FromResult(result);
    }

    public Task<IReadOnlyList<IFixMsgStoreRecord>> GetRange(int fromSeqNum, int toSeqNum)
    {
        var result = _messages
            .Where(m => m.SeqNum >= fromSeqNum && m.SeqNum <= toSeqNum)
            .ToList();
        return Task.FromResult<IReadOnlyList<IFixMsgStoreRecord>>(result);
    }

    public Task Reset()
    {
        SenderSeqNum = 1;
        TargetSeqNum = 1;
        _messages.Clear();
        CreationTime = DateTime.UtcNow;
        return Task.CompletedTask;
    }

    public Task Flush() => Task.CompletedTask;

    public ValueTask DisposeAsync() => ValueTask.CompletedTask;
}
