using PureFix.Buffer;
using PureFix.Types;

namespace PureFix.Transport.Session
{
    /// <summary>
    /// The set of dependencies owned privately by a single FIX session.
    /// </summary>
    /// <remarks>
    /// An acceptor creates one scope per accepted connection; an initiator creates one
    /// scope and reuses it across reconnects (its session object is also reused).
    /// <para>
    /// Nothing reachable from a scope may be shared between two concurrently running
    /// sessions. The parser accumulates a partially received frame in a single buffer
    /// (<c>AsciiParseState</c>), and the encoder holds the outbound MsgSeqNum and a
    /// single writer, so sharing either one lets two counterparties interleave bytes
    /// into the same message. The description is scoped because a wildcard acceptor
    /// rebinds TargetCompID to whichever peer logged on.
    /// </para>
    /// </remarks>
    public interface ISessionScope : IDisposable
    {
        /// <summary>
        /// Identifier for this scope, used in log lines to correlate a connection
        /// with the session, parser and encoder serving it.
        /// </summary>
        string Id { get; }

        /// <summary>
        /// Config whose <see cref="IFixConfig.Description"/> belongs to this scope alone.
        /// Mutating it (for example rebinding a wildcard TargetCompID) cannot affect
        /// another session.
        /// </summary>
        IFixConfig Config { get; }

        /// <summary>Parser private to this connection.</summary>
        IMessageParser Parser { get; }

        /// <summary>Encoder private to this connection.</summary>
        IMessageEncoder Encoder { get; }
    }
}
