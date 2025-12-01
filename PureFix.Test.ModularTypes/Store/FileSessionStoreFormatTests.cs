using PureFix.Buffer.Ascii;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport;
using PureFix.Transport.Store;
using System.Text;

namespace PureFix.Test.ModularTypes.Store;

/// <summary>
/// Tests that verify the FileSessionStore produces QuickFix-compatible file formats.
/// Uses real FIX messages from replay test data to prove:
/// 1. Body file has no CRLF - messages stored back-to-back in ASCII format
/// 2. Header file can index and seek to replay from anywhere
///
/// Note: Tests dispose the store before reading files directly to avoid
/// Windows file locking issues.
/// </summary>
[TestFixture]
public class FileSessionStoreFormatTests
{
    private TestEntity _testEntity = null!;
    private List<AsciiView> _views = null!;
    private string _testDir = null!;
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
        _testDir = Path.Combine(Path.GetTempPath(), "purefix-format-test-" + Guid.NewGuid().ToString("N")[..8]);
        Directory.CreateDirectory(_testDir);
        _sessionId = new SessionId("FIX.4.4", "SENDER", "TARGET");
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(_testDir))
            Directory.Delete(_testDir, recursive: true);
    }

    #region Body File Format Tests

    [Test]
    public async Task BodyFile_ContainsNoLineBreaks()
    {
        var records = GetRecordsFromViews();

        // Write messages and close store
        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            foreach (var record in records)
            {
                await store.Put(record);
            }
        }

        // Now read file (store disposed, file handles released)
        var bodyPath = _sessionId.GetFilePath(_testDir, "body");
        var bodyBytes = await File.ReadAllBytesAsync(bodyPath);

        var crCount = bodyBytes.Count(b => b == 0x0D);
        var lfCount = bodyBytes.Count(b => b == 0x0A);

        Assert.Multiple(() =>
        {
            Assert.That(crCount, Is.EqualTo(0), "Body file should not contain CR (0x0D) characters");
            Assert.That(lfCount, Is.EqualTo(0), "Body file should not contain LF (0x0A) characters");
        });
    }

    [Test]
    public async Task BodyFile_MessagesStoredBackToBack()
    {
        var msg1 = "8=FIX.4.4\u000135=A\u000149=SENDER\u000156=TARGET\u000134=1\u000110=000\u0001";
        var msg2 = "8=FIX.4.4\u000135=0\u000149=SENDER\u000156=TARGET\u000134=2\u000110=000\u0001";

        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            await store.Put(new FixMsgStoreRecord("A", DateTime.UtcNow, 1, msg1));
            await store.Put(new FixMsgStoreRecord("0", DateTime.UtcNow, 2, msg2));
        }

        var bodyPath = _sessionId.GetFilePath(_testDir, "body");
        var bodyContent = await File.ReadAllTextAsync(bodyPath);

        Assert.That(bodyContent, Is.EqualTo(msg1 + msg2));
        Assert.That(bodyContent.Length, Is.EqualTo(msg1.Length + msg2.Length));
    }

    [Test]
    public async Task BodyFile_ReplayMessages_StoredContiguously()
    {
        var records = GetRecordsFromViews();
        var expectedTotalLength = records.Sum(r => r.Encoded?.Length ?? 0);

        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            foreach (var record in records)
            {
                await store.Put(record);
            }
        }

        var bodyPath = _sessionId.GetFilePath(_testDir, "body");
        var bodyBytes = await File.ReadAllBytesAsync(bodyPath);

        Assert.That(bodyBytes.Length, Is.EqualTo(expectedTotalLength),
            "Body file length should equal sum of all message lengths");
    }

    [Test]
    public async Task BodyFile_EachMessage_StartsWith8Equals()
    {
        var records = GetRecordsFromViews();

        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            foreach (var record in records)
            {
                await store.Put(record);
            }
        }

        var headerPath = _sessionId.GetFilePath(_testDir, "header");
        var headerLines = await File.ReadAllLinesAsync(headerPath);

        var bodyPath = _sessionId.GetFilePath(_testDir, "body");
        var bodyBytes = await File.ReadAllBytesAsync(bodyPath);

        foreach (var line in headerLines)
        {
            var parts = line.Split(',');
            var offset = long.Parse(parts[1]);

            Assert.That(bodyBytes[offset], Is.EqualTo((byte)'8'),
                $"Message at offset {offset} should start with '8'");
            Assert.That(bodyBytes[offset + 1], Is.EqualTo((byte)'='),
                $"Message at offset {offset} should have '=' as second character");
        }
    }

    [Test]
    public async Task BodyFile_EachMessage_EndsWithSOH()
    {
        var records = GetRecordsFromViews();

        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            foreach (var record in records)
            {
                await store.Put(record);
            }
        }

        var headerPath = _sessionId.GetFilePath(_testDir, "header");
        var headerLines = await File.ReadAllLinesAsync(headerPath);

        var bodyPath = _sessionId.GetFilePath(_testDir, "body");
        var bodyBytes = await File.ReadAllBytesAsync(bodyPath);

        foreach (var line in headerLines)
        {
            var parts = line.Split(',');
            var offset = long.Parse(parts[1]);
            var length = int.Parse(parts[2]);

            var lastByteIndex = offset + length - 1;
            Assert.That(bodyBytes[lastByteIndex], Is.EqualTo(0x01),
                $"Message at offset {offset} with length {length} should end with SOH (0x01)");
        }
    }

    #endregion

    #region Header File Index Tests

    [Test]
    public async Task HeaderFile_OffsetsAreCorrect()
    {
        var records = GetRecordsFromViews();
        var expectedOffsets = new List<long>();
        long currentOffset = 0;

        foreach (var record in records)
        {
            expectedOffsets.Add(currentOffset);
            currentOffset += record.Encoded?.Length ?? 0;
        }

        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            foreach (var record in records)
            {
                await store.Put(record);
            }
        }

        var headerPath = _sessionId.GetFilePath(_testDir, "header");
        var headerLines = await File.ReadAllLinesAsync(headerPath);

        Assert.That(headerLines.Length, Is.EqualTo(records.Count));

        for (int i = 0; i < headerLines.Length; i++)
        {
            var parts = headerLines[i].Split(',');
            var actualOffset = long.Parse(parts[1]);

            Assert.That(actualOffset, Is.EqualTo(expectedOffsets[i]),
                $"Header line {i}: Expected offset {expectedOffsets[i]}, got {actualOffset}");
        }
    }

    [Test]
    public async Task HeaderFile_LengthsAreCorrect()
    {
        var records = GetRecordsFromViews();

        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            foreach (var record in records)
            {
                await store.Put(record);
            }
        }

        var headerPath = _sessionId.GetFilePath(_testDir, "header");
        var headerLines = await File.ReadAllLinesAsync(headerPath);

        Assert.That(headerLines.Length, Is.EqualTo(records.Count));

        for (int i = 0; i < headerLines.Length; i++)
        {
            var parts = headerLines[i].Split(',');
            var actualLength = int.Parse(parts[2]);
            var expectedLength = Encoding.UTF8.GetByteCount(records[i].Encoded!);

            Assert.That(actualLength, Is.EqualTo(expectedLength),
                $"Header line {i}: Expected length {expectedLength}, got {actualLength}");
        }
    }

    [Test]
    public async Task HeaderFile_SeqNumsAreCorrect()
    {
        var records = GetRecordsFromViews();

        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            foreach (var record in records)
            {
                await store.Put(record);
            }
        }

        var headerPath = _sessionId.GetFilePath(_testDir, "header");
        var headerLines = await File.ReadAllLinesAsync(headerPath);

        Assert.That(headerLines.Length, Is.EqualTo(records.Count));

        for (int i = 0; i < headerLines.Length; i++)
        {
            var parts = headerLines[i].Split(',');
            var actualSeqNum = int.Parse(parts[0]);
            var expectedSeqNum = records[i].SeqNum;

            Assert.That(actualSeqNum, Is.EqualTo(expectedSeqNum),
                $"Header line {i}: Expected seqnum {expectedSeqNum}, got {actualSeqNum}");
        }
    }

    #endregion

    #region Seek and Replay Tests

    [Test]
    public async Task CanSeekAndReadAnyMessage()
    {
        var records = GetRecordsFromViews();

        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            foreach (var record in records)
            {
                await store.Put(record);
            }
        }

        var headerPath = _sessionId.GetFilePath(_testDir, "header");
        var headerLines = await File.ReadAllLinesAsync(headerPath);

        var bodyPath = _sessionId.GetFilePath(_testDir, "body");
        await using var bodyStream = new FileStream(bodyPath, FileMode.Open, FileAccess.Read);

        for (int i = 0; i < headerLines.Length; i++)
        {
            var parts = headerLines[i].Split(',');
            var seqNum = int.Parse(parts[0]);
            var offset = long.Parse(parts[1]);
            var length = int.Parse(parts[2]);

            bodyStream.Seek(offset, SeekOrigin.Begin);

            var buffer = new byte[length];
            var bytesRead = await bodyStream.ReadAsync(buffer);

            Assert.That(bytesRead, Is.EqualTo(length),
                $"Should read exactly {length} bytes for message {seqNum}");

            var messageContent = Encoding.UTF8.GetString(buffer);

            Assert.That(messageContent, Is.EqualTo(records[i].Encoded),
                $"Message {seqNum} content mismatch");
        }
    }

    [Test]
    public async Task CanRandomAccessAnyMessage()
    {
        var records = GetRecordsFromViews();

        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            foreach (var record in records)
            {
                await store.Put(record);
            }
        }

        var headerPath = _sessionId.GetFilePath(_testDir, "header");
        var headerLines = await File.ReadAllLinesAsync(headerPath);

        var index = new Dictionary<int, (long Offset, int Length)>();
        foreach (var line in headerLines)
        {
            var parts = line.Split(',');
            var seqNum = int.Parse(parts[0]);
            var offset = long.Parse(parts[1]);
            var length = int.Parse(parts[2]);
            index[seqNum] = (offset, length);
        }

        var bodyPath = _sessionId.GetFilePath(_testDir, "body");
        var bodyBytes = await File.ReadAllBytesAsync(bodyPath);

        var seqNums = records.Select(r => r.SeqNum).ToList();
        var random = new Random(42);
        var shuffled = seqNums.OrderBy(_ => random.Next()).ToList();

        foreach (var seqNum in shuffled)
        {
            if (!index.TryGetValue(seqNum, out var entry))
            {
                Assert.Fail($"SeqNum {seqNum} not found in index");
                continue;
            }

            var messageBytes = new byte[entry.Length];
            Array.Copy(bodyBytes, entry.Offset, messageBytes, 0, entry.Length);
            var messageContent = Encoding.UTF8.GetString(messageBytes);

            var originalRecord = records.First(r => r.SeqNum == seqNum);
            Assert.That(messageContent, Is.EqualTo(originalRecord.Encoded),
                $"Random access to message {seqNum} content mismatch");
        }
    }

    [Test]
    public async Task StoreGet_ReturnsCorrectMessage()
    {
        var records = GetRecordsFromViews();

        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();

        foreach (var record in records)
        {
            await store.Put(record);
        }

        foreach (var originalRecord in records)
        {
            var retrieved = await store.Get(originalRecord.SeqNum);

            Assert.That(retrieved, Is.Not.Null,
                $"Should retrieve message {originalRecord.SeqNum}");
            Assert.That(retrieved!.SeqNum, Is.EqualTo(originalRecord.SeqNum));
            Assert.That(retrieved.Encoded, Is.EqualTo(originalRecord.Encoded),
                $"Message {originalRecord.SeqNum} content should match");
        }
    }

    [Test]
    public async Task StoreGetRange_ReturnsCorrectMessages()
    {
        var records = GetRecordsFromViews();

        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();

        foreach (var record in records)
        {
            await store.Put(record);
        }

        var minSeq = records.Min(r => r.SeqNum);
        var maxSeq = records.Max(r => r.SeqNum);
        var range = await store.GetRange(minSeq, maxSeq);

        Assert.That(range.Count, Is.EqualTo(records.Count));

        for (int i = 0; i < range.Count; i++)
        {
            Assert.That(range[i].Encoded, Is.EqualTo(records[i].Encoded),
                $"Range message {i} content should match");
        }
    }

    #endregion

    #region Recovery After Reopen Tests

    [Test]
    public async Task AfterReopen_CanReadAllMessages()
    {
        var records = GetRecordsFromViews();

        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            foreach (var record in records)
            {
                await store.Put(record);
            }
        }

        await using var store2 = new FileSessionStore(_sessionId, _testDir);
        await store2.Initialize();

        foreach (var originalRecord in records)
        {
            var retrieved = await store2.Get(originalRecord.SeqNum);

            Assert.That(retrieved, Is.Not.Null,
                $"Should retrieve message {originalRecord.SeqNum} after reopen");
            Assert.That(retrieved!.Encoded, Is.EqualTo(originalRecord.Encoded),
                $"Message {originalRecord.SeqNum} content should match after reopen");
        }
    }

    [Test]
    public async Task AfterReopen_HeaderIndexIsCorrect()
    {
        var records = GetRecordsFromViews();

        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            foreach (var record in records)
            {
                await store.Put(record);
            }
        }

        var headerPath = _sessionId.GetFilePath(_testDir, "header");
        var headerLines = await File.ReadAllLinesAsync(headerPath);

        Assert.That(headerLines.Length, Is.EqualTo(records.Count),
            "Header should have one line per message");

        long expectedOffset = 0;
        for (int i = 0; i < records.Count; i++)
        {
            var parts = headerLines[i].Split(',');
            var seqNum = int.Parse(parts[0]);
            var offset = long.Parse(parts[1]);
            var length = int.Parse(parts[2]);

            var expectedLength = Encoding.UTF8.GetByteCount(records[i].Encoded!);

            Assert.Multiple(() =>
            {
                Assert.That(seqNum, Is.EqualTo(records[i].SeqNum), $"Line {i}: SeqNum mismatch");
                Assert.That(offset, Is.EqualTo(expectedOffset), $"Line {i}: Offset mismatch");
                Assert.That(length, Is.EqualTo(expectedLength), $"Line {i}: Length mismatch");
            });

            expectedOffset += expectedLength;
        }
    }

    #endregion

    #region Helpers

    private List<FixMsgStoreRecord> GetRecordsFromViews()
    {
        var sessionMessages = new HashSet<string>
        {
            "A", // Logon
            "5", // Logout
            "2", // ResendRequest
            "0", // Heartbeat
            "1", // TestRequest
            "4"  // SequenceReset
        };

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
                    var sohString = bufferString.Replace('|', '\u0001');
                    sohString = sohString.Trim('\r', '\n');

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
