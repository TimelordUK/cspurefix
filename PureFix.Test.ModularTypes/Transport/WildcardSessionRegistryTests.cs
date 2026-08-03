using PureFix.Buffer;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Test.ModularTypes.Transport;

/// <summary>
/// A wildcard acceptor used to build its SessionId at construction from the literal "*",
/// so every counterparty collapsed onto one registry key and each new connection evicted
/// the previous client. Binding is now deferred to the peer's Logon; these tests pin the
/// registry behaviour that depends on it.
/// </summary>
internal class WildcardSessionRegistryTests
{
    private const string BeginString = "FIX.4.4";
    private const string Sender = "accept-comp";

    private TestEntity _testEntity = null!;
    private ISessionScopeFactory _scopeFactory = null!;
    private readonly List<ISessionScope> _scopes = [];

    [SetUp]
    public void Setup()
    {
        _testEntity = new TestEntity();
        var config = _testEntity.GetTestAcceptorConfig();
        ((SessionDescription)config.Description!).TargetCompID = "*";

        _scopeFactory = new DefaultSessionScopeFactory(
            config,
            _testEntity.Clock,
            description => new Fix44ModularSessionMessageFactory(description));
    }

    [TearDown]
    public void TearDown()
    {
        foreach (var scope in _scopes) scope.Dispose();
        _scopes.Clear();
    }

    /// <summary>A session on its own connection scope, as the acceptor would build it.</summary>
    private RecordingSession NewSession()
    {
        var scope = _scopeFactory.CreateScope();
        _scopes.Add(scope);
        return new RecordingSession(
            scope.Config, new TestLoggerFactory(), scope.Parser, scope.Encoder, _testEntity.Clock);
    }

    [Test]
    public void Different_counterparties_coexist()
    {
        var registry = new SessionRegistry();

        var a = NewSession();
        var b = NewSession();

        var evictedA = registry.Register(new SessionId(BeginString, Sender, "HEDGE-FUND-A"), a);
        var evictedB = registry.Register(new SessionId(BeginString, Sender, "MARKET-MAKER-B"), b);

        Assert.Multiple(() =>
        {
            Assert.That(evictedA, Is.False);
            Assert.That(evictedB, Is.False, "a second client must not displace the first");
            Assert.That(a.StopReason, Is.Null, "the first client's session must still be running");
            Assert.That(b.StopReason, Is.Null);
        });
    }

    [Test]
    public void Same_counterparty_reconnecting_displaces_its_stale_session()
    {
        var registry = new SessionRegistry();
        var sessionId = new SessionId(BeginString, Sender, "HEDGE-FUND-A");

        var stale = NewSession();
        var fresh = NewSession();

        registry.Register(sessionId, stale);
        var evicted = registry.Register(sessionId, fresh);

        Assert.Multiple(() =>
        {
            Assert.That(evicted, Is.True);
            Assert.That(stale.StopReason, Is.Not.Null, "the stale transport must be stopped");
            Assert.That(fresh.StopReason, Is.Null);
        });
    }

    [Test]
    public void A_displaced_session_unregistering_does_not_remove_its_successor()
    {
        var registry = new SessionRegistry();
        var sessionId = new SessionId(BeginString, Sender, "HEDGE-FUND-A");

        var stale = NewSession();
        registry.Register(sessionId, stale);
        registry.Register(sessionId, NewSession());

        // The displaced session notices its transport died and cleans up afterwards. Its
        // Unregister must not take the successor's entry with it.
        registry.Unregister(sessionId, stale);

        // If the successor had been evicted, registering again would report no eviction.
        var evicted = registry.Register(sessionId, NewSession());
        Assert.That(evicted, Is.True, "the successor must still have been registered");
    }

    /// <summary>
    /// A real FixSession that records the registry's stop request rather than tearing down
    /// a transport it does not have.
    /// </summary>
    private sealed class RecordingSession(
        IFixConfig config,
        ILogFactory logFactory,
        IMessageParser parser,
        IMessageEncoder encoder,
        IFixClock clock)
        : FixSession(config, logFactory, parser, encoder, clock)
    {
        public string? StopReason { get; private set; }

        public override void RequestStop(string reason) => StopReason = reason;

        protected override Task OnMsg(string msgType, IMessageView view) => Task.CompletedTask;
        protected override void OnDecoded(string msgType, string txt) { }
        protected override Task OnEncoded(string msgType, int seqNum, string logTxt, string storeTxt) => Task.CompletedTask;
        protected override Task OnApplicationMsg(string msgType, IMessageView view) => Task.CompletedTask;
        protected override Task OnReady(IMessageView view) => Task.CompletedTask;
        protected override void OnStopped(Exception? error) { }
        protected override bool OnLogon(IMessageView view, string? user, string? password) => true;
        protected override Task Tick() => Task.CompletedTask;
        protected override Task OnRun() => Task.CompletedTask;
    }
}
