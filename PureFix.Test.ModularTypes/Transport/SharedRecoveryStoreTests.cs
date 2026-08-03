using PureFix.Buffer;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport;
using PureFix.Transport.Ascii;
using PureFix.Transport.Recovery;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Test.ModularTypes.Transport;

/// <summary>
/// IFixLogRecovery is a container singleton in every sample host, so an acceptor's
/// sessions all share one recovery store. BaseApp.OnEncoded used to add to it on every
/// send regardless of whether recovery was in use, so the second counterparty to send an
/// application message at a sequence number the first had already used threw
/// ArgumentException out of the session read loop and dropped that client.
/// </summary>
internal class SharedRecoveryStoreTests
{
    private TestEntity _testEntity = null!;
    private ISessionScopeFactory _scopeFactory = null!;
    private IFixConfig _config = null!;
    private readonly List<ISessionScope> _scopes = [];

    [SetUp]
    public void Setup()
    {
        _testEntity = new TestEntity();
        _config = _testEntity.GetTestAcceptorConfig();
        ((SessionDescription)_config.Description!).TargetCompID = "*";

        _scopeFactory = new DefaultSessionScopeFactory(
            _config,
            _testEntity.Clock,
            description => new Fix44ModularSessionMessageFactory(description));
    }

    [TearDown]
    public void TearDown()
    {
        foreach (var scope in _scopes) scope.Dispose();
        _scopes.Clear();
    }

    private ProbeApp NewSession(IFixLogRecovery recovery)
    {
        var scope = _scopeFactory.CreateScope();
        _scopes.Add(scope);
        return new ProbeApp(
            scope.Config, recovery, new TestLoggerFactory(),
            new PureFix.Types.FIX44.FixMessageFactory(), scope.Parser, scope.Encoder, _testEntity.Clock);
    }

    private FixLogRecovery MakeSharedRecovery() =>
        new(new FixLogParser(_config), new TestLoggerFactory(), _config, new FixMsgMemoryStore("accept-comp"));

    [Test]
    public async Task Two_sessions_sharing_a_recovery_store_can_both_send_at_the_same_sequence()
    {
        // Both counterparties are at sequence 2 - normal, they are independent sessions.
        var recovery = MakeSharedRecovery();
        var a = NewSession(recovery);
        var b = NewSession(recovery);

        await a.Encoded("D", 2, "8=FIX.4.4\x0135=D\x0149=accept-comp\x0156=hedge-fund-a\x0134=2\x01");

        Assert.DoesNotThrowAsync(
            async () => await b.Encoded("D", 2, "8=FIX.4.4\x0135=D\x0149=accept-comp\x0156=market-maker-b\x0134=2\x01"),
            "a second counterparty sending at the same sequence must not fault the session");
    }

    [Test]
    public async Task Recovery_is_left_alone_when_a_session_store_factory_is_configured()
    {
        // MakeConfigFromPaths always supplies a store factory, so recovery is not the
        // store being read back on restart and must not be written to either.
        Assert.That(_config.SessionStoreFactory, Is.Not.Null);

        var recovery = MakeSharedRecovery();
        var session = NewSession(recovery);

        await session.Encoded("D", 7, "8=FIX.4.4\x0135=D\x0134=7\x01");

        var stored = await recovery.FixMsgStore.Get(7);
        Assert.That(stored, Is.Null, "recovery store was written to despite being unused");
    }

    /// <summary>Minimal BaseApp that exposes the encode callback.</summary>
    private sealed class ProbeApp(
        IFixConfig config,
        IFixLogRecovery? recovery,
        ILogFactory logFactory,
        IFixMessageFactory fixMessageFactory,
        IMessageParser parser,
        IMessageEncoder encoder,
        IFixClock clock)
        : BaseApp(config, recovery, logFactory, fixMessageFactory, parser, encoder, clock)
    {
        public Task Encoded(string msgType, int seqNum, string encoded)
            => OnEncoded(msgType, seqNum, encoded, encoded);

        protected override Task OnApplicationMsg(string msgType, IMessageView view) => Task.CompletedTask;
        protected override Task OnReady(IMessageView view) => Task.CompletedTask;
        protected override bool OnLogon(IMessageView view, string? user, string? password) => true;
    }
}
