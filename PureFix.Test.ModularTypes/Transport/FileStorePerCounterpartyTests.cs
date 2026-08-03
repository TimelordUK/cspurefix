using PureFix.Transport.Store;

namespace PureFix.Test.ModularTypes.Transport;

/// <summary>
/// A wildcard acceptor with a file store must give each counterparty its own set of
/// files, and must release them when a session ends.
/// </summary>
internal class FileStorePerCounterpartyTests
{
    private const string BeginString = "FIX.4.4";
    private const string Sender = "accept-comp";

    private string _directory = null!;

    [SetUp]
    public void Setup()
    {
        _directory = Path.Combine(Path.GetTempPath(), $"purefix-store-{Guid.NewGuid():N}");
        Directory.CreateDirectory(_directory);
    }

    [TearDown]
    public void TearDown()
    {
        try
        {
            if (Directory.Exists(_directory)) Directory.Delete(_directory, recursive: true);
        }
        catch (IOException)
        {
            // A leaked handle would block deletion; the tests below assert on that directly.
        }
    }

    private async Task<IFixSessionStore> OpenStore(string peerCompId)
    {
        var factory = new FileSessionStoreFactory(_directory);
        var store = factory.Create(new SessionId(BeginString, Sender, peerCompId));
        await store.Initialize();
        return store;
    }

    [Test]
    public async Task Two_counterparties_get_separate_files_and_sequences()
    {
        var a = await OpenStore("hedge-fund-a");
        var b = await OpenStore("market-maker-b");

        await a.SetSenderSeqNum(41);
        await a.SetTargetSeqNum(11);
        await b.SetSenderSeqNum(7);
        await b.SetTargetSeqNum(3);

        await a.Put(new FixMsgStoreRecord("D", DateTime.UtcNow, 40, "8=FIX.4.4\x0135=D\x0149=accept-comp\x01"));

        Assert.Multiple(() =>
        {
            Assert.That(a.SenderSeqNum, Is.EqualTo(41));
            Assert.That(b.SenderSeqNum, Is.EqualTo(7), "client B must not see client A's sequence");
            Assert.That(a.TargetSeqNum, Is.EqualTo(11));
            Assert.That(b.TargetSeqNum, Is.EqualTo(3));
        });

        await a.DisposeAsync();
        await b.DisposeAsync();

        var files = Directory.GetFiles(_directory).Select(Path.GetFileName).ToList();
        Assert.Multiple(() =>
        {
            Assert.That(files, Has.Some.EqualTo("FIX.4.4-accept-comp-hedge-fund-a.seqnums"));
            Assert.That(files, Has.Some.EqualTo("FIX.4.4-accept-comp-market-maker-b.seqnums"));
            Assert.That(files, Has.None.Contains("-*."), "no store may be keyed on the wildcard");
        });
    }

    [Test]
    public async Task A_counterparty_reconnecting_can_reopen_its_store()
    {
        // The acceptor stops the displaced session and immediately builds a new one for
        // the same SessionId. If the old store never released its file handles the
        // reopen fails, and the reconnecting client cannot log on at all.
        var first = await OpenStore("hedge-fund-a");
        await first.SetSenderSeqNum(12);
        await first.DisposeAsync();

        IFixSessionStore? second = null;
        Assert.DoesNotThrowAsync(async () => second = await OpenStore("hedge-fund-a"));

        Assert.That(second!.SenderSeqNum, Is.EqualTo(12), "recovered sequence must survive the reconnect");
        await second.DisposeAsync();
    }

    [Test]
    public async Task An_undisposed_store_does_not_block_the_reconnecting_session()
    {
        // Worst case: the displaced session is stopped but its store has not been
        // disposed yet when the replacement opens the same files.
        var stale = await OpenStore("hedge-fund-a");
        await stale.SetSenderSeqNum(5);

        IFixSessionStore? fresh = null;
        Assert.DoesNotThrowAsync(async () => fresh = await OpenStore("hedge-fund-a"));

        await stale.DisposeAsync();
        if (fresh != null) await fresh.DisposeAsync();
    }

    [TestCase("../../../escape")]
    [TestCase("..\\..\\escape")]
    [TestCase("bad:name")]
    [TestCase("wild*card")]
    [TestCase("has/slash")]
    public async Task A_hostile_peer_comp_id_cannot_escape_the_store_directory(string peerCompId)
    {
        // TargetCompID is taken from the peer's Logon tag 49, so it is untrusted input
        // that ends up in a file path.
        var sessionId = new SessionId(BeginString, Sender, peerCompId);
        var root = Path.TrimEndingDirectorySeparator(Path.GetFullPath(_directory));

        foreach (var extension in new[] { "seqnums", "session", "header", "body" })
        {
            var resolved = Path.GetFullPath(sessionId.GetFilePath(_directory, extension));
            Assert.That(resolved, Does.StartWith(root + Path.DirectorySeparatorChar),
                $"store path escapes the configured directory: {resolved}");
            Assert.That(Path.GetDirectoryName(resolved), Is.EqualTo(root),
                "store files must sit directly in the configured directory");
        }

        // ...and the store must actually open, rather than throwing on an illegal name.
        IFixSessionStore? store = null;
        try
        {
            store = await OpenStore(peerCompId);
            await store.SetSenderSeqNum(1);
        }
        finally
        {
            if (store != null) await store.DisposeAsync();
        }
    }
}
