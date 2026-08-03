using Arrow.Threading.Tasks;
using PureFix.Test.ModularTypes.Env.Experiment;
using PureFix.Test.ModularTypes.Env.Skeleton;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Test.ModularTypes.Transport;

/// <summary>
/// One acceptor host serving two counterparties at the same time - the scenario the C#
/// demo never exercised, because it shipped a single initiator config against a wildcard
/// acceptor.
/// </summary>
/// <remarks>
/// Both sessions come from one host, exactly as TcpAcceptorListener builds them: one
/// ISessionScope per accepted connection. Before the scope work these two sessions shared
/// a parser, an encoder and a session description, so their frames and sequence numbers
/// interleaved and the second Logon rebound the first client's TargetCompID.
/// </remarks>
internal class MultiClientAcceptorTests
{
    private const string ClientA = "hedge-fund-a";
    private const string ClientB = "market-maker-b";

    private TestEntity _testEntity = null!;
    private AsyncWorkQueue _queue = null!;

    [SetUp]
    public void Setup()
    {
        _testEntity = new TestEntity();
        _testEntity.Prepare();
        _queue = new AsyncWorkQueue();
    }

    [TearDown]
    public void TearDown() => _queue.Dispose();

    [Test]
    public async Task Two_clients_on_one_wildcard_acceptor_stay_isolated()
    {
        var registry = new SessionRegistry();

        var acceptorConfig = _testEntity.GetTestAcceptorConfig();
        var acceptorDescription = (SessionDescription)acceptorConfig.Description!;
        acceptorDescription.TargetCompID = "*";
        ((FixConfig)acceptorConfig).SessionRegistry = registry;

        // A single acceptor host, as a real process would have.
        var acceptorHost = new SkeletonDIContainer(_queue, _testEntity.Clock, acceptorConfig);

        var clientAHost = new SkeletonDIContainer(_queue, _testEntity.Clock, MakeClientConfig(ClientA));
        var clientBHost = new SkeletonDIContainer(_queue, _testEntity.Clock, MakeClientConfig(ClientB));

        // Two connections, two scopes, one host.
        var servingA = new RuntimeContainer(acceptorHost.AppHost);
        var servingB = new RuntimeContainer(acceptorHost.AppHost);
        var clientA = new RuntimeContainer(clientAHost.AppHost);
        var clientB = new RuntimeContainer(clientBHost.AppHost);

        Pair(clientA, servingA);
        Pair(clientB, servingB);

        var containers = new[] { servingA, servingB, clientA, clientB };
        var running = containers.Select(c => c.Run()).ToArray();

        // The two acceptor sessions share a host, and TestLoggerFactory gives every logger
        // from one host the same backing trace - so their logs cannot tell them apart.
        // Wait on per-session state instead: each client ready, each acceptor session bound.
        await WaitFor(
            () => clientA.OnReady()
                  && clientB.OnReady()
                  && Bound(servingA)
                  && Bound(servingB),
            TimeSpan.FromSeconds(10));

        Assert.Multiple(() =>
        {
            Assert.That(clientA.OnReady(), Is.True, "client A never reached ready");
            Assert.That(clientB.OnReady(), Is.True, "client B never reached ready");
            Assert.That(Bound(servingA), Is.True, "acceptor session A never bound its peer");
            Assert.That(Bound(servingB), Is.True, "acceptor session B never bound its peer");
        });

        Assert.Multiple(() =>
        {
            // Each acceptor session bound to its own counterparty...
            Assert.That(servingA.Config.Description!.TargetCompID, Is.EqualTo(ClientA));
            Assert.That(servingB.Config.Description!.TargetCompID, Is.EqualTo(ClientB));

            // ...without writing through to the host's shared description.
            Assert.That(acceptorDescription.TargetCompID, Is.EqualTo("*"),
                "the acceptor's template description must remain unbound");

            // ...and without sharing the machinery that carries a message.
            Assert.That(servingA.Parser, Is.Not.SameAs(servingB.Parser));
            Assert.That(servingA.Encoder, Is.Not.SameAs(servingB.Encoder));
            Assert.That(servingA.Config.Description, Is.Not.SameAs(servingB.Config.Description));
        });

        // What actually broke before: outbound headers. Each session must address its own
        // counterparty, not the most recent one to log on.
        Assert.Multiple(() =>
        {
            Assert.That(OutboundTarget(servingA), Is.EqualTo(ClientA));
            Assert.That(OutboundTarget(servingB), Is.EqualTo(ClientB));
        });

        // Each client saw exactly its own Logon pair - its own request plus the acceptor's
        // response - so neither was evicted by the other's arrival.
        Assert.Multiple(() =>
        {
            Assert.That(clientA.LogonCount(), Is.EqualTo(2));
            Assert.That(clientB.LogonCount(), Is.EqualTo(2));
            Assert.That(clientA.LogoutCount(), Is.EqualTo(0), "client A was disconnected");
            Assert.That(clientB.LogoutCount(), Is.EqualTo(0), "client B was disconnected");
        });

        await clientA.App.Done();
        await clientB.App.Done();

        foreach (var c in containers) await c.TokenSource.CancelAsync();
        await Task.WhenAll(running.Select(t => t.ContinueWith(_ => { })));

        Assert.Multiple(() =>
        {
            foreach (var t in running) Assert.That(t.IsFaulted, Is.False);
        });
    }

    private static bool Bound(RuntimeContainer acceptorSession)
    {
        var target = acceptorSession.Config.Description?.TargetCompID;
        return target != null && target != "*";
    }

    /// <summary>
    /// The TargetCompID this session's message factory stamps on an outbound header.
    /// </summary>
    private string? OutboundTarget(RuntimeContainer acceptorSession)
    {
        return acceptorSession.Config.MessageFactory!
            .Header(PureFix.Types.Core.MsgType.Heartbeat, 1, _testEntity.Clock.Current)!
            .TargetCompID;
    }

    private IFixConfig MakeClientConfig(string senderCompId)
    {
        var config = _testEntity.GetTestInitiatorConfig();
        var description = (SessionDescription)config.Description!;
        description.SenderCompID = senderCompId;
        return config;
    }

    private static void Pair(RuntimeContainer client, RuntimeContainer acceptor)
    {
        client.ConnectTo(acceptor);
        acceptor.ConnectTo(client);
    }

    private static async Task WaitFor(Func<bool> condition, TimeSpan timeout)
    {
        var deadline = DateTime.UtcNow + timeout;
        while (DateTime.UtcNow < deadline)
        {
            if (condition()) return;
            await Task.Delay(20);
        }
    }
}
