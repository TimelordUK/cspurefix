using PureFix.Dictionary.Definition;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.Config;
using PureFix.Types.FIX44;

namespace PureFix.Test.ModularTypes.Session;

/// <summary>
/// Tests for ResendGapFillOnly safety mode.
///
/// When ResendGapFillOnly=true, the session should NEVER replay stored messages.
/// Instead, it should always send a SequenceReset-GapFill to skip the requested range.
/// This is critical for clients/initiators to prevent accidentally resending old orders.
/// </summary>
[TestFixture]
public class ResendGapFillOnlyTests
{
    private TestEntity _testEntity = null!;
    private TestClock _clock = null!;

    [OneTimeSetUp]
    public void OneTimeSetup()
    {
        _testEntity = new TestEntity();
    }

    [SetUp]
    public void Setup()
    {
        _clock = new TestClock { Current = DateTime.UtcNow };
    }

    /// <summary>
    /// When ResendGapFillOnly=true, GetResendRequest should return a single GapFill
    /// even when the store has messages to replay.
    /// </summary>
    [Test]
    public async Task GapFillOnly_True_Returns_Single_GapFill_When_Store_Has_Messages()
    {
        // Arrange - store has messages for seq 10-20
        var store = await CreateStoreWithMessages(10, 20);
        var config = CreateConfig(resendGapFillOnly: true);
        var resender = CreateResender(store, config);

        // Act - request resend of seq 10-20
        var records = await resender.GetResendRequest(10, 20);

        // Assert - should be single GapFill, not 11 replayed messages
        Assert.That(records.Count, Is.EqualTo(1), "Should return exactly one GapFill message");
        Assert.That(records[0].MsgType, Is.EqualTo(MsgType.SequenceReset), "Should be SequenceReset");
        Assert.That(records[0].InflatedMessage, Is.Not.Null, "Should have inflated message");

        // The GapFill should advance to seq 21 (endSeq + 1)
        var seqReset = records[0].InflatedMessage;
        Assert.That(seqReset?.StandardHeader?.MsgSeqNum, Is.EqualTo(10), "GapFill should start at beginSeq");
        Assert.That(seqReset?.StandardHeader?.PossDupFlag, Is.True, "GapFill should have PossDupFlag=Y");
    }

    /// <summary>
    /// When ResendGapFillOnly=true with an empty store, should still return GapFill.
    /// </summary>
    [Test]
    public async Task GapFillOnly_True_Returns_GapFill_When_Store_Empty()
    {
        // Arrange - empty store
        var store = await CreateEmptyStore();
        var config = CreateConfig(resendGapFillOnly: true);
        var resender = CreateResender(store, config);

        // Act
        var records = await resender.GetResendRequest(10, 20);

        // Assert
        Assert.That(records.Count, Is.EqualTo(1), "Should return exactly one GapFill");
        Assert.That(records[0].MsgType, Is.EqualTo(MsgType.SequenceReset));
    }

    /// <summary>
    /// When ResendGapFillOnly=false and store has messages, should replay them.
    /// </summary>
    [Test]
    public async Task GapFillOnly_False_Replays_Stored_Messages()
    {
        // Arrange - store has messages for seq 10-15 (6 messages)
        var store = await CreateStoreWithMessages(10, 15);
        var config = CreateConfig(resendGapFillOnly: false);
        var resender = CreateResender(store, config);

        // Act
        var records = await resender.GetResendRequest(10, 15);

        // Assert - should replay 6 messages (with PossDupFlag)
        Assert.That(records.Count, Is.EqualTo(6), "Should replay all 6 stored messages");
        foreach (var record in records)
        {
            Assert.That(record.InflatedMessage?.StandardHeader?.PossDupFlag, Is.True,
                $"Message seq {record.SeqNum} should have PossDupFlag=Y");
        }
    }

    /// <summary>
    /// When ResendGapFillOnly=false but store is empty, should return GapFill.
    /// </summary>
    [Test]
    public async Task GapFillOnly_False_Returns_GapFill_When_Store_Empty()
    {
        // Arrange - empty store
        var store = await CreateEmptyStore();
        var config = CreateConfig(resendGapFillOnly: false);
        var resender = CreateResender(store, config);

        // Act
        var records = await resender.GetResendRequest(10, 20);

        // Assert - no messages to replay, so GapFill
        Assert.That(records.Count, Is.EqualTo(1), "Should return GapFill for empty range");
        Assert.That(records[0].MsgType, Is.EqualTo(MsgType.SequenceReset));
    }

    /// <summary>
    /// When ResendGapFillOnly=false with gaps in store, should fill gaps with GapFill.
    /// </summary>
    [Test]
    public async Task GapFillOnly_False_Fills_Gaps_In_Store()
    {
        // Arrange - store has messages for seq 10, 15, 20 (gaps at 11-14, 16-19)
        var store = await CreateStoreWithSpecificMessages(10, 15, 20);
        var config = CreateConfig(resendGapFillOnly: false);
        var resender = CreateResender(store, config);

        // Act - request 10-20
        var records = await resender.GetResendRequest(10, 20);

        // Assert - should have: msg10, gapfill(11-15), msg15, gapfill(16-20), msg20
        // Actually looking at InflateRange, it fills gaps between stored messages
        Assert.That(records.Count, Is.GreaterThan(3), "Should have messages and gap fills");

        // Find the SequenceReset records (gap fills)
        var gapFills = records.Where(r => r.MsgType == MsgType.SequenceReset).ToList();
        Assert.That(gapFills.Count, Is.GreaterThan(0), "Should have gap fills for missing sequences");
    }

    /// <summary>
    /// Default behavior (null ResendGapFillOnly) should replay messages.
    /// </summary>
    [Test]
    public async Task GapFillOnly_Default_Null_Replays_Messages()
    {
        // Arrange - store has messages, config has null ResendGapFillOnly
        var store = await CreateStoreWithMessages(10, 12);
        var config = CreateConfig(resendGapFillOnly: null);
        var resender = CreateResender(store, config);

        // Act
        var records = await resender.GetResendRequest(10, 12);

        // Assert - should replay (default is false = replay)
        Assert.That(records.Count, Is.EqualTo(3), "Should replay all 3 messages");
    }

    #region Helper Methods

    private async Task<IFixSessionStore> CreateEmptyStore()
    {
        var sessionId = new SessionId("FIX.4.4", "SENDER", "TARGET");
        var store = new MemorySessionStore(sessionId);
        await store.Initialize();
        return store;
    }

    private async Task<IFixSessionStore> CreateStoreWithMessages(int fromSeq, int toSeq)
    {
        var sessionId = new SessionId("FIX.4.4", "SENDER", "TARGET");
        var store = new MemorySessionStore(sessionId);
        await store.Initialize();

        for (int seq = fromSeq; seq <= toSeq; seq++)
        {
            var record = CreateMockRecord(MsgType.NewOrderSingle, seq);
            await store.Put(record);
        }

        return store;
    }

    private async Task<IFixSessionStore> CreateStoreWithSpecificMessages(params int[] seqNums)
    {
        var sessionId = new SessionId("FIX.4.4", "SENDER", "TARGET");
        var store = new MemorySessionStore(sessionId);
        await store.Initialize();

        foreach (var seq in seqNums)
        {
            var record = CreateMockRecord(MsgType.NewOrderSingle, seq);
            await store.Put(record);
        }

        return store;
    }

    private FixMsgStoreRecord CreateMockRecord(string msgType, int seqNum)
    {
        // Create a properly formatted FIX message
        // BodyLength (9) is calculated from after 9= to before 10=
        // Using Heartbeat ("0") for simplicity as it has minimal fields
        var body = $"35={msgType}|49=SENDER|56=TARGET|34={seqNum}|52=20240101-12:00:00.000|";
        var bodyLength = body.Length;
        var encoded = $"8=FIX.4.4|9={bodyLength}|{body}10=000|";
        return new FixMsgStoreRecord(msgType, _clock.Current, seqNum, encoded);
    }

    private IFixConfig CreateConfig(bool? resendGapFillOnly)
    {
        return new MockFixConfig(_testEntity.Definitions, resendGapFillOnly);
    }

    private FixMsgAsciiStoreResend CreateResender(IFixSessionStore store, IFixConfig config)
    {
        var factory = new FixMessageFactory();
        return new FixMsgAsciiStoreResend(store, factory, config, _clock);
    }

    #endregion
}

/// <summary>
/// Minimal mock config for testing FixMsgAsciiStoreResend.
/// </summary>
internal class MockFixConfig : IFixConfig
{
    private readonly MockSessionDescription _description;

    public MockFixConfig(IFixDefinitions definitions, bool? resendGapFillOnly)
    {
        Definitions = definitions;
        _description = new MockSessionDescription { ResendGapFillOnly = resendGapFillOnly };
        MessageFactory = new Fix44ModularSessionMessageFactory(_description);
    }

    public byte LogDelimiter { get; set; } = (byte)'|'; // Pipe for readability
    public byte Delimiter { get; set; } = (byte)'|';     // Pipe for readability
    public byte StoreDelimiter { get; set; } = (byte)'|';

    public IFixDefinitions? Definitions { get; }
    public ISessionDescription? Description => _description;
    public ISessionMessageFactory? MessageFactory { get; }
    public IFixSessionStoreFactory? SessionStoreFactory => null;
}

/// <summary>
/// Minimal mock session description for testing.
/// </summary>
internal class MockSessionDescription : ISessionDescription
{
    public MsgApplication? Application { get; set; }
    public string? Name { get; set; }
    public string? SenderCompID { get; set; } = "SENDER";
    public string? TargetCompID { get; set; } = "TARGET";
    public bool? ResetSeqNumFlag { get; set; }
    public int? MsgSeqNum { get; set; }
    public int? PeerSeqNum { get; set; }
    public string? SenderSubID { get; set; }
    public string? TargetSubID { get; set; }
    public string? BeginString { get; set; } = "FIX.4.4";
    public string? Username { get; set; }
    public string? Password { get; set; }
    public int? LastSentSeqNum { get; set; }
    public int? LastReceivedSeqNum { get; set; }
    public int? BodyLengthChars { get; set; }
    public int? HeartBtInt { get; set; } = 30;
    public LoggingConfig? Logging { get; set; }
    public StoreConfig? Store { get; set; }
    public bool? ResendGapFillOnly { get; set; }
}
