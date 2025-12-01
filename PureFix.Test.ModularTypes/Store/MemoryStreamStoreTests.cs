using PureFix.Buffer.Ascii;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport;
using PureFix.Transport.Store;
using System.Text;

namespace PureFix.Test.ModularTypes.Store;

/// <summary>
/// Tests that use MemorySessionStreamProvider to verify store format
/// without touching the file system. This allows direct inspection of
/// the exact bytes written to body and header streams.
/// </summary>
[TestFixture]
public class MemoryStreamStoreTests
{
    private TestEntity _testEntity = null!;
    private List<AsciiView> _views = null!;
    private SessionId _sessionId = null!;

    [OneTimeSetUp]
    public async Task OneTimeSetUp()
    {
        _testEntity = new TestEntity();
        _views = await _testEntity.Replay(Fix44PathHelper.ReplayTestClientPath);
    }

    [SetUp]
    public void Setup()
    {
        _testEntity.Prepare();
        _sessionId = new SessionId("FIX.4.4", "SENDER", "TARGET");
    }

    #region Basic Memory Store Tests

    [Test]
    public async Task MemoryProvider_StoresMessages_CanRetrieve()
    {
        var provider = new MemorySessionStreamProvider();
        await using var store = new FileSessionStore(_sessionId, provider);
        await store.Initialize();

        var record = new FixMsgStoreRecord("D", DateTime.UtcNow, 1,
            "8=FIX.4.4\u000135=D\u000149=SENDER\u000156=TARGET\u000134=1\u000152=20231201-10:00:00.000\u000110=000\u0001");

        await store.Put(record);
        var retrieved = await store.Get(1);

        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.SeqNum, Is.EqualTo(1));
        Assert.That(retrieved.Encoded, Is.EqualTo(record.Encoded));
    }

    [Test]
    public async Task MemoryProvider_BodyBytes_ContainsExactMessage()
    {
        var provider = new MemorySessionStreamProvider();
        await using var store = new FileSessionStore(_sessionId, provider);
        await store.Initialize();

        var msg = "8=FIX.4.4\u000135=D\u000110=000\u0001";
        var record = new FixMsgStoreRecord("D", DateTime.UtcNow, 1, msg);

        await store.Put(record);
        await store.Flush();

        // Inspect the raw bytes
        var bodyBytes = provider.GetBodyBytes();
        var bodyString = provider.GetBodyString();

        Assert.That(bodyString, Is.EqualTo(msg));
        Assert.That(bodyBytes.Length, Is.EqualTo(Encoding.UTF8.GetByteCount(msg)));
    }

    [Test]
    public async Task MemoryProvider_HeaderLines_ContainsCorrectIndex()
    {
        var provider = new MemorySessionStreamProvider();
        await using var store = new FileSessionStore(_sessionId, provider);
        await store.Initialize();

        var msg1 = "8=FIX.4.4\u000135=A\u000134=1\u000110=000\u0001";
        var msg2 = "8=FIX.4.4\u000135=0\u000134=2\u000110=000\u0001";

        await store.Put(new FixMsgStoreRecord("A", DateTime.UtcNow, 1, msg1));
        await store.Put(new FixMsgStoreRecord("0", DateTime.UtcNow, 2, msg2));
        await store.Flush();

        var headerLines = provider.GetHeaderLines();

        Assert.That(headerLines.Length, Is.EqualTo(2));
        Assert.That(headerLines[0], Is.EqualTo($"1,0,{msg1.Length}"));
        Assert.That(headerLines[1], Is.EqualTo($"2,{msg1.Length},{msg2.Length}"));
    }

    #endregion

    #region Body Format Inspection Tests

    [Test]
    public async Task MemoryProvider_BodyBytes_NoLineBreaks()
    {
        var provider = new MemorySessionStreamProvider();
        await using var store = new FileSessionStore(_sessionId, provider);
        await store.Initialize();

        var records = GetRecordsFromViews();
        foreach (var record in records)
        {
            await store.Put(record);
        }
        await store.Flush();

        var bodyBytes = provider.GetBodyBytes();

        var crCount = bodyBytes.Count(b => b == 0x0D);
        var lfCount = bodyBytes.Count(b => b == 0x0A);

        Assert.Multiple(() =>
        {
            Assert.That(crCount, Is.EqualTo(0), "Body should not contain CR");
            Assert.That(lfCount, Is.EqualTo(0), "Body should not contain LF");
        });
    }

    [Test]
    public async Task MemoryProvider_BodyBytes_MessagesContiguous()
    {
        var provider = new MemorySessionStreamProvider();
        await using var store = new FileSessionStore(_sessionId, provider);
        await store.Initialize();

        var records = GetRecordsFromViews();
        var expectedTotalLength = records.Sum(r => Encoding.UTF8.GetByteCount(r.Encoded!));

        foreach (var record in records)
        {
            await store.Put(record);
        }
        await store.Flush();

        var bodyBytes = provider.GetBodyBytes();

        Assert.That(bodyBytes.Length, Is.EqualTo(expectedTotalLength));
    }

    [Test]
    public async Task MemoryProvider_BodyBytes_EachMessageStartsWith8Equals()
    {
        var provider = new MemorySessionStreamProvider();
        await using var store = new FileSessionStore(_sessionId, provider);
        await store.Initialize();

        var records = GetRecordsFromViews();
        foreach (var record in records)
        {
            await store.Put(record);
        }
        await store.Flush();

        var bodyBytes = provider.GetBodyBytes();
        var headerLines = provider.GetHeaderLines();

        foreach (var line in headerLines)
        {
            var parts = line.Split(',');
            var offset = int.Parse(parts[1]);

            Assert.That(bodyBytes[offset], Is.EqualTo((byte)'8'),
                $"Message at offset {offset} should start with '8'");
            Assert.That(bodyBytes[offset + 1], Is.EqualTo((byte)'='),
                $"Message at offset {offset} should have '=' as second char");
        }
    }

    [Test]
    public async Task MemoryProvider_BodyBytes_EachMessageEndsWithSOH()
    {
        var provider = new MemorySessionStreamProvider();
        await using var store = new FileSessionStore(_sessionId, provider);
        await store.Initialize();

        var records = GetRecordsFromViews();
        foreach (var record in records)
        {
            await store.Put(record);
        }
        await store.Flush();

        var bodyBytes = provider.GetBodyBytes();
        var headerLines = provider.GetHeaderLines();

        foreach (var line in headerLines)
        {
            var parts = line.Split(',');
            var offset = int.Parse(parts[1]);
            var length = int.Parse(parts[2]);

            var lastByteIndex = offset + length - 1;
            Assert.That(bodyBytes[lastByteIndex], Is.EqualTo(0x01),
                $"Message at offset {offset} should end with SOH");
        }
    }

    #endregion

    #region Sequence Numbers and Session Time Tests

    [Test]
    public async Task MemoryProvider_SeqNumsContent_QuickFixFormat()
    {
        var provider = new MemorySessionStreamProvider();
        await using var store = new FileSessionStore(_sessionId, provider);
        await store.Initialize();

        await store.SetSenderSeqNum(982);
        await store.SetTargetSeqNum(978);

        var content = provider.SeqNumsContent;

        Assert.That(content, Is.EqualTo("                 982 :                  978"));
    }

    [Test]
    public async Task MemoryProvider_SessionTimeContent_QuickFixFormat()
    {
        var provider = new MemorySessionStreamProvider();
        await using var store = new FileSessionStore(_sessionId, provider);
        await store.Initialize();

        var content = provider.SessionTimeContent;

        // Format: YYYYMMDD-HH:MM:SS.ffffff
        Assert.That(content, Does.Match(@"^\d{8}-\d{2}:\d{2}:\d{2}\.\d{6}$"));
    }

    #endregion

    #region Random Access Tests

    [Test]
    public async Task MemoryProvider_RandomAccess_RetrievesCorrectMessages()
    {
        var provider = new MemorySessionStreamProvider();
        await using var store = new FileSessionStore(_sessionId, provider);
        await store.Initialize();

        var records = GetRecordsFromViews();
        foreach (var record in records)
        {
            await store.Put(record);
        }

        // Access in random order
        var random = new Random(42);
        var seqNums = records.Select(r => r.SeqNum).OrderBy(_ => random.Next()).ToList();

        foreach (var seqNum in seqNums)
        {
            var retrieved = await store.Get(seqNum);
            var original = records.First(r => r.SeqNum == seqNum);

            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.Encoded, Is.EqualTo(original.Encoded));
        }
    }

    #endregion

    #region Direct Byte Inspection Tests

    [Test]
    public async Task MemoryProvider_CanInspectExactByteLayout()
    {
        var provider = new MemorySessionStreamProvider();
        await using var store = new FileSessionStore(_sessionId, provider);
        await store.Initialize();

        // Use a simple message with known content
        var msg = "8=FIX.4.4\u000135=A\u000110=000\u0001";
        await store.Put(new FixMsgStoreRecord("A", DateTime.UtcNow, 1, msg));
        await store.Flush();

        var bytes = provider.GetBodyBytes();

        // Verify exact byte sequence
        Assert.Multiple(() =>
        {
            Assert.That(bytes[0], Is.EqualTo((byte)'8'));
            Assert.That(bytes[1], Is.EqualTo((byte)'='));
            Assert.That(bytes[2], Is.EqualTo((byte)'F'));
            Assert.That(bytes[3], Is.EqualTo((byte)'I'));
            Assert.That(bytes[4], Is.EqualTo((byte)'X'));

            // Last byte should be SOH
            Assert.That(bytes[^1], Is.EqualTo(0x01));
        });
    }

    [Test]
    public async Task MemoryProvider_CanVerifySOHDelimiters()
    {
        var provider = new MemorySessionStreamProvider();
        await using var store = new FileSessionStore(_sessionId, provider);
        await store.Initialize();

        var msg = "8=FIX.4.4\u000135=A\u000149=SENDER\u000156=TARGET\u000134=1\u000110=000\u0001";
        await store.Put(new FixMsgStoreRecord("A", DateTime.UtcNow, 1, msg));
        await store.Flush();

        var bytes = provider.GetBodyBytes();

        // Count SOH characters (0x01)
        var sohCount = bytes.Count(b => b == 0x01);

        // Should have one SOH after each tag=value pair (6 in this message)
        Assert.That(sohCount, Is.EqualTo(6));
    }

    #endregion

    #region Helpers

    private List<FixMsgStoreRecord> GetRecordsFromViews()
    {
        var sessionMessages = new HashSet<string> { "A", "5", "2", "0", "1", "4" };
        var senderCompId = "accept-comp";
        var records = new List<FixMsgStoreRecord>();

        foreach (var view in _views)
        {
            if (view.SenderCompID() == senderCompId)
            {
                var msgType = view.MsgType();
                if (msgType != null && !sessionMessages.Contains(msgType))
                {
                    var bufferString = view.BufferString();
                    var sohString = bufferString.Replace('|', '\u0001').Trim('\r', '\n');

                    records.Add(new FixMsgStoreRecord(
                        msgType,
                        view.SendingTime() ?? DateTime.MinValue,
                        view.MsgSeqNum() ?? -1,
                        sohString));
                }
            }
        }

        return records;
    }

    #endregion
}
