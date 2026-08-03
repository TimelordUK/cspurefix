using System.Text;
using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Transport;
using PureFix.Transport.Session;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Test.ModularTypes.Transport;

/// <summary>
/// An acceptor serves many connections from one host. These tests pin down that each
/// connection gets its own parser, encoder and session description.
/// </summary>
/// <remarks>
/// Regression cover for the acceptor corruption reported against the TypeScript port
/// (jspurefix#153): every session was handed the same container singletons, so two
/// counterparties receiving at once wrote into a single partially-parsed frame buffer
/// and consumed each other's outbound sequence numbers.
/// </remarks>
internal class SessionScopeIsolationTests
{
    private TestEntity _testEntity = null!;
    private IFixConfig _template = null!;

    [SetUp]
    public void Setup()
    {
        _testEntity = new TestEntity();
        _template = _testEntity.GetTestAcceptorConfig();
    }

    private DefaultSessionScopeFactory MakeFactory(IFixConfig? template = null)
    {
        var cfg = template ?? _template;
        return new DefaultSessionScopeFactory(
            cfg,
            _testEntity.Clock,
            description => new Fix44ModularSessionMessageFactory(description));
    }

    [Test]
    public void Each_scope_gets_distinct_parser_encoder_and_description()
    {
        var factory = MakeFactory();

        using var a = factory.CreateScope();
        using var b = factory.CreateScope();

        Assert.Multiple(() =>
        {
            Assert.That(a.Parser, Is.Not.SameAs(b.Parser));
            Assert.That(a.Encoder, Is.Not.SameAs(b.Encoder));
            Assert.That(a.Config, Is.Not.SameAs(b.Config));
            Assert.That(a.Config.Description, Is.Not.SameAs(b.Config.Description));
            Assert.That(a.Config.MessageFactory, Is.Not.SameAs(b.Config.MessageFactory));
            Assert.That(a.Id, Is.Not.EqualTo(b.Id));

            // Definitions are immutable and deliberately shared - cloning them per
            // connection would be pure waste.
            Assert.That(a.Config.Definitions, Is.SameAs(b.Config.Definitions));
        });
    }

    [Test]
    public void Rebinding_target_comp_id_in_one_scope_does_not_touch_another()
    {
        var factory = MakeFactory();

        using var a = factory.CreateScope();
        using var b = factory.CreateScope();

        ((SessionDescription)a.Config.Description!).TargetCompID = "HEDGE-FUND-A";

        Assert.Multiple(() =>
        {
            Assert.That(b.Config.Description!.TargetCompID, Is.Not.EqualTo("HEDGE-FUND-A"));
            Assert.That(_template.Description!.TargetCompID, Is.Not.EqualTo("HEDGE-FUND-A"),
                "the shared template must never be mutated by a session");
        });
    }

    [Test]
    public void Outbound_sequence_numbers_do_not_bleed_between_scopes()
    {
        var factory = MakeFactory();

        using var a = factory.CreateScope();
        using var b = factory.CreateScope();

        var logon = a.Config.MessageFactory!.Logon();
        Assert.That(logon, Is.Not.Null);

        for (var i = 0; i < 5; i++)
        {
            var storage = a.Encoder.Encode(PureFix.Types.Core.MsgType.Logon, logon!);
            Assert.That(storage, Is.Not.Null);
            a.Encoder.Return(storage!);
        }

        Assert.Multiple(() =>
        {
            Assert.That(a.Encoder.MsgSeqNum, Is.EqualTo(6));
            Assert.That(b.Encoder.MsgSeqNum, Is.EqualTo(1), "second connection must start from its own sequence");
        });
    }

    [Test]
    public void Interleaved_fragments_from_two_connections_parse_intact()
    {
        var factory = MakeFactory();

        using var a = factory.CreateScope();
        using var b = factory.CreateScope();

        var delim = (char)a.Config.Delimiter;
        var first = MakeHeartbeat("HEDGE-FUND-A", 11, "req-aaaa", delim);
        var second = MakeHeartbeat("MARKET-MAKER-B", 22, "req-bbbb", delim);

        var fromA = new List<AsciiView>();
        var fromB = new List<AsciiView>();

        // Alternate small fragments the way two busy sockets would arrive on the
        // acceptor's thread pool.
        FeedInterleaved(a.Parser, first, fromA, b.Parser, second, fromB, chunk: 7);

        Assert.Multiple(() =>
        {
            Assert.That(fromA, Has.Count.EqualTo(1));
            Assert.That(fromB, Has.Count.EqualTo(1));
            Assert.That(fromA[0].GetString((int)MsgTag.SenderCompID), Is.EqualTo("HEDGE-FUND-A"));
            Assert.That(fromA[0].GetInt32((int)MsgTag.MsgSeqNum), Is.EqualTo(11));
            Assert.That(fromA[0].GetString((int)MsgTag.TestReqID), Is.EqualTo("req-aaaa"));
            Assert.That(fromB[0].GetString((int)MsgTag.SenderCompID), Is.EqualTo("MARKET-MAKER-B"));
            Assert.That(fromB[0].GetInt32((int)MsgTag.MsgSeqNum), Is.EqualTo(22));
            Assert.That(fromB[0].GetString((int)MsgTag.TestReqID), Is.EqualTo("req-bbbb"));
        });
    }

    [Test]
    public void Sharing_one_parser_between_connections_corrupts_both_messages()
    {
        // The shape of the original defect, kept as executable documentation of why
        // ISessionScope exists. A single parser fed two interleaved streams cannot
        // reproduce either message.
        var shared = new AsciiParser(_template.Definitions!) { Delimiter = _template.Delimiter };

        var delim = (char)_template.Delimiter;
        var first = MakeHeartbeat("HEDGE-FUND-A", 11, "req-aaaa", delim);
        var second = MakeHeartbeat("MARKET-MAKER-B", 22, "req-bbbb", delim);

        var received = new List<AsciiView>();

        try
        {
            FeedInterleaved(shared, first, received, shared, second, received, chunk: 7);
        }
        catch (Exception)
        {
            // A desynced stream throwing is itself a legitimate corrupt outcome.
            Assert.Pass("shared parser rejected the interleaved stream");
        }

        var intact = received.Count(v =>
            (v.GetString((int)MsgTag.SenderCompID) == "HEDGE-FUND-A" && v.GetString((int)MsgTag.TestReqID) == "req-aaaa")
            || (v.GetString((int)MsgTag.SenderCompID) == "MARKET-MAKER-B" && v.GetString((int)MsgTag.TestReqID) == "req-bbbb"));

        Assert.That(intact, Is.LessThan(2),
            "a shared parser must not be able to deliver both messages intact - if it can, this test no longer guards anything");
    }

    [Test]
    public void Wildcard_acceptor_without_per_scope_message_factory_is_rejected()
    {
        ((SessionDescription)_template.Description!).TargetCompID = "*";

        // No messageFactoryProvider: headers would be stamped from the shared description,
        // so every client would be addressed as "*".
        var factory = new DefaultSessionScopeFactory(_template, _testEntity.Clock);

        var ex = Assert.Throws<InvalidOperationException>(() => factory.CreateScope());
        Assert.That(ex!.Message, Does.Contain("wildcard"));
    }

    [Test]
    public void Wildcard_acceptor_with_per_scope_message_factory_is_allowed()
    {
        ((SessionDescription)_template.Description!).TargetCompID = "*";

        var factory = MakeFactory();
        using var scope = factory.CreateScope();

        Assert.That(scope.Config.Description!.TargetCompID, Is.EqualTo("*"));
    }

    private static void FeedInterleaved(
        IMessageParser parserA, byte[] a, List<AsciiView> sinkA,
        IMessageParser parserB, byte[] b, List<AsciiView> sinkB,
        int chunk)
    {
        var offsetA = 0;
        var offsetB = 0;

        while (offsetA < a.Length || offsetB < b.Length)
        {
            offsetA = Feed(parserA, a, offsetA, chunk, sinkA);
            offsetB = Feed(parserB, b, offsetB, chunk, sinkB);
        }
    }

    private static int Feed(IMessageParser parser, byte[] source, int offset, int chunk, List<AsciiView> sink)
    {
        if (offset >= source.Length) return offset;

        var take = Math.Min(chunk, source.Length - offset);
        var slice = new byte[take];
        Array.Copy(source, offset, slice, 0, take);
        parser.ParseFrom(slice, take, (_, view) => sink.Add(((AsciiView)view).Clone()));
        return offset + take;
    }

    /// <summary>
    /// Builds a Heartbeat with a correct BodyLength and CheckSum.
    /// </summary>
    private static byte[] MakeHeartbeat(string senderCompId, int seqNum, string testReqId, char delim)
    {
        var body =
            $"35=0{delim}49={senderCompId}{delim}56=ACCEPT-COMP{delim}34={seqNum}{delim}" +
            $"52=20260803-10:00:00.000{delim}112={testReqId}{delim}";
        var head = $"8=FIX.4.4{delim}9={body.Length}{delim}";
        var withoutChecksum = head + body;

        var sum = Encoding.ASCII.GetBytes(withoutChecksum).Aggregate(0, (acc, c) => acc + c) % 256;
        return Encoding.ASCII.GetBytes($"{withoutChecksum}10={sum:D3}{delim}");
    }
}
