using PureFix.Transport.Store;

namespace PureFix.Test.ModularTypes.Store;

[TestFixture]
public class FileSessionStoreTests
{
    private string _testDir = null!;
    private SessionId _sessionId = null!;

    [SetUp]
    public void Setup()
    {
        _testDir = Path.Combine(Path.GetTempPath(), "purefix-test-" + Guid.NewGuid().ToString("N")[..8]);
        Directory.CreateDirectory(_testDir);
        _sessionId = new SessionId("FIX.4.4", "SENDER", "TARGET");
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(_testDir))
            Directory.Delete(_testDir, recursive: true);
    }

    #region Sequence Number Tests

    [Test]
    public async Task Initialize_NewStore_StartsAtSeqNum1()
    {
        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();

        Assert.That(store.SenderSeqNum, Is.EqualTo(1));
        Assert.That(store.TargetSeqNum, Is.EqualTo(1));
    }

    [Test]
    public async Task SetSenderSeqNum_PersistsToFile()
    {
        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            await store.SetSenderSeqNum(100);
        }

        // Reopen and verify
        await using var store2 = new FileSessionStore(_sessionId, _testDir);
        await store2.Initialize();
        Assert.That(store2.SenderSeqNum, Is.EqualTo(100));
    }

    [Test]
    public async Task SetTargetSeqNum_PersistsToFile()
    {
        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            await store.SetTargetSeqNum(200);
        }

        // Reopen and verify
        await using var store2 = new FileSessionStore(_sessionId, _testDir);
        await store2.Initialize();
        Assert.That(store2.TargetSeqNum, Is.EqualTo(200));
    }

    [Test]
    public async Task NextSenderSeqNum_IncrementsAndPersists()
    {
        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();

        var next1 = await store.NextSenderSeqNum();
        var next2 = await store.NextSenderSeqNum();
        var next3 = await store.NextSenderSeqNum();

        Assert.That(next1, Is.EqualTo(2));
        Assert.That(next2, Is.EqualTo(3));
        Assert.That(next3, Is.EqualTo(4));
        Assert.That(store.SenderSeqNum, Is.EqualTo(4));
    }

    [Test]
    public async Task SeqNumsFile_QuickFixFormat()
    {
        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();
        await store.SetSenderSeqNum(982);
        await store.SetTargetSeqNum(978);

        var filePath = _sessionId.GetFilePath(_testDir, "seqnums");
        var content = await File.ReadAllTextAsync(filePath);

        // QuickFix format: 20-char right-justified with " : " separator
        Assert.That(content, Is.EqualTo("                 982 :                  978"));
    }

    #endregion

    #region Session Time Tests

    [Test]
    public async Task Initialize_NewStore_SetsCreationTime()
    {
        var before = DateTime.UtcNow;
        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();
        var after = DateTime.UtcNow;

        Assert.That(store.CreationTime, Is.InRange(before, after));
    }

    [Test]
    public async Task Reset_UpdatesCreationTime()
    {
        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();
        var originalTime = store.CreationTime;

        await Task.Delay(10); // Ensure time passes
        await store.Reset();

        Assert.That(store.CreationTime, Is.GreaterThan(originalTime));
    }

    [Test]
    public async Task SessionFile_QuickFixFormat()
    {
        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();

        var filePath = _sessionId.GetFilePath(_testDir, "session");
        var content = await File.ReadAllTextAsync(filePath);

        // QuickFix format: YYYYMMDD-HH:MM:SS.ffffff
        Assert.That(content, Does.Match(@"^\d{8}-\d{2}:\d{2}:\d{2}\.\d{6}$"));
    }

    #endregion

    #region Message Storage Tests

    [Test]
    public async Task Put_StoresMessage_CanRetrieve()
    {
        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();

        // Use \u0001 for SOH - \x01 is ambiguous in C# when followed by hex chars
        var record = new FixMsgStoreRecord("D", DateTime.UtcNow, 1,
            "8=FIX.4.4\u000135=D\u000149=SENDER\u000156=TARGET\u000134=1\u000152=20231201-10:00:00.000\u000110=000\u0001");

        await store.Put(record);
        var retrieved = await store.Get(1);

        Assert.That(retrieved, Is.Not.Null);
        Assert.That(retrieved!.SeqNum, Is.EqualTo(1));
        Assert.That(retrieved.MsgType, Is.EqualTo("D"));
        Assert.That(retrieved.Encoded, Is.EqualTo(record.Encoded));
    }

    [Test]
    public async Task Put_MultipleMessages_AllRetrievable()
    {
        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();

        for (int i = 1; i <= 10; i++)
        {
            var record = new FixMsgStoreRecord("D", DateTime.UtcNow, i,
                $"8=FIX.4.4\u000135=D\u000149=SENDER\u000156=TARGET\u000134={i}\u000152=20231201-10:00:00.000\u000110=000\u0001");
            await store.Put(record);
        }

        for (int i = 1; i <= 10; i++)
        {
            var retrieved = await store.Get(i);
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.SeqNum, Is.EqualTo(i));
        }
    }

    [Test]
    public async Task GetRange_ReturnsCorrectSubset()
    {
        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();

        for (int i = 1; i <= 10; i++)
        {
            var record = new FixMsgStoreRecord("D", DateTime.UtcNow, i,
                $"8=FIX.4.4\u000135=D\u000149=SENDER\u000156=TARGET\u000134={i}\u000152=20231201-10:00:00.000\u000110=000\u0001");
            await store.Put(record);
        }

        var range = await store.GetRange(3, 7);

        Assert.That(range.Count, Is.EqualTo(5));
        Assert.That(range[0].SeqNum, Is.EqualTo(3));
        Assert.That(range[4].SeqNum, Is.EqualTo(7));
    }

    [Test]
    public async Task HeaderFile_QuickFixFormat()
    {
        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();

        var msg1 = "8=FIX.4.4\u000135=A\u000134=1\u000110=000\u0001";
        var msg2 = "8=FIX.4.4\u000135=0\u000134=2\u000110=000\u0001";

        await store.Put(new FixMsgStoreRecord("A", DateTime.UtcNow, 1, msg1));
        await store.Put(new FixMsgStoreRecord("0", DateTime.UtcNow, 2, msg2));

        var filePath = _sessionId.GetFilePath(_testDir, "header");
        var lines = await File.ReadAllLinesAsync(filePath);

        // Format: seqnum,offset,length
        Assert.That(lines.Length, Is.EqualTo(2));
        Assert.That(lines[0], Is.EqualTo($"1,0,{msg1.Length}"));
        Assert.That(lines[1], Is.EqualTo($"2,{msg1.Length},{msg2.Length}"));
    }

    [Test]
    public async Task BodyFile_ContainsConcatenatedMessages()
    {
        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();

        var msg1 = "8=FIX.4.4\u000135=A\u000134=1\u000110=000\u0001";
        var msg2 = "8=FIX.4.4\u000135=0\u000134=2\u000110=000\u0001";

        await store.Put(new FixMsgStoreRecord("A", DateTime.UtcNow, 1, msg1));
        await store.Put(new FixMsgStoreRecord("0", DateTime.UtcNow, 2, msg2));

        var filePath = _sessionId.GetFilePath(_testDir, "body");
        var content = await File.ReadAllTextAsync(filePath);

        Assert.That(content, Is.EqualTo(msg1 + msg2));
    }

    #endregion

    #region Recovery Tests

    [Test]
    public async Task Recovery_LoadsMessagesFromExistingFiles()
    {
        // Create store and add messages
        await using (var store = new FileSessionStore(_sessionId, _testDir))
        {
            await store.Initialize();
            await store.SetSenderSeqNum(100);
            await store.SetTargetSeqNum(50);

            for (int i = 1; i <= 5; i++)
            {
                var record = new FixMsgStoreRecord("D", DateTime.UtcNow, i,
                    $"8=FIX.4.4\u000135=D\u000134={i}\u000110=000\u0001");
                await store.Put(record);
            }
        }

        // Reopen and verify everything loaded
        await using var store2 = new FileSessionStore(_sessionId, _testDir);
        await store2.Initialize();

        Assert.That(store2.SenderSeqNum, Is.EqualTo(100));
        Assert.That(store2.TargetSeqNum, Is.EqualTo(50));

        for (int i = 1; i <= 5; i++)
        {
            var retrieved = await store2.Get(i);
            Assert.That(retrieved, Is.Not.Null);
            Assert.That(retrieved!.SeqNum, Is.EqualTo(i));
        }
    }

    #endregion

    #region Reset Tests

    [Test]
    public async Task Reset_ClearsAllState()
    {
        await using var store = new FileSessionStore(_sessionId, _testDir);
        await store.Initialize();

        await store.SetSenderSeqNum(100);
        await store.SetTargetSeqNum(50);
        await store.Put(new FixMsgStoreRecord("D", DateTime.UtcNow, 1, "8=FIX.4.4\u000135=D\u000110=000\u0001"));

        await store.Reset();

        Assert.That(store.SenderSeqNum, Is.EqualTo(1));
        Assert.That(store.TargetSeqNum, Is.EqualTo(1));
        Assert.That(await store.Get(1), Is.Null);
    }

    #endregion

    #region SessionId Tests

    [Test]
    public void SessionId_ToFilePrefix_FormatsCorrectly()
    {
        var sessionId = new SessionId("FIX.4.4", "MAP_CAPA_BETA1", "MAP_BLP_BETA1");
        Assert.That(sessionId.ToFilePrefix(), Is.EqualTo("FIX.4.4-MAP_CAPA_BETA1-MAP_BLP_BETA1"));
    }

    [Test]
    public void SessionId_GetFilePath_FormatsCorrectly()
    {
        var sessionId = new SessionId("FIX.4.4", "SENDER", "TARGET");
        var path = sessionId.GetFilePath("/store", "seqnums");
        Assert.That(path, Is.EqualTo(Path.Combine("/store", "FIX.4.4-SENDER-TARGET.seqnums")));
    }

    #endregion
}
