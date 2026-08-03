using PureFix.Buffer;
using PureFix.Buffer.Ascii;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Transport.Session
{
    /// <summary>
    /// Builds a scope containing a fresh ascii parser, encoder, session description and
    /// session message factory for each connection.
    /// </summary>
    /// <remarks>
    /// The session message factory has to be per-scope as well as the encoder: it reads
    /// SenderCompID/TargetCompID off the description every time it builds a header, so a
    /// factory bound to the shared description would keep stamping the shared (or a rival
    /// peer's) CompID onto this session's outbound messages.
    /// </remarks>
    public sealed class DefaultSessionScopeFactory : ISessionScopeFactory
    {
        private readonly IFixConfig _template;
        private readonly IFixClock _clock;
        private readonly Func<ISessionDescription, ISessionMessageFactory>? _messageFactoryProvider;
        private readonly Func<IFixConfig, IMessageParser>? _parserProvider;
        private readonly Func<IFixConfig, ISessionMessageFactory, IMessageEncoder>? _encoderProvider;
        private int _nextScopeId;

        /// <param name="template">Config to clone per connection.</param>
        /// <param name="clock">Shared, stateless.</param>
        /// <param name="messageFactoryProvider">
        /// Builds a session message factory bound to the supplied per-scope description.
        /// When null the template's factory is reused, which is only safe for an initiator.
        /// </param>
        /// <param name="parserProvider">Overrides parser construction (custom segment parser, string store).</param>
        /// <param name="encoderProvider">Overrides encoder construction.</param>
        public DefaultSessionScopeFactory(
            IFixConfig template,
            IFixClock clock,
            Func<ISessionDescription, ISessionMessageFactory>? messageFactoryProvider = null,
            Func<IFixConfig, IMessageParser>? parserProvider = null,
            Func<IFixConfig, ISessionMessageFactory, IMessageEncoder>? encoderProvider = null)
        {
            ArgumentNullException.ThrowIfNull(template);
            ArgumentNullException.ThrowIfNull(clock);

            _template = template;
            _clock = clock;
            _messageFactoryProvider = messageFactoryProvider;
            _parserProvider = parserProvider;
            _encoderProvider = encoderProvider;
        }

        public ISessionScope CreateScope()
        {
            var id = $"scope-{Interlocked.Increment(ref _nextScopeId)}";

            var description = CloneDescription(_template.Description);

            ISessionMessageFactory? messageFactory;
            if (_messageFactoryProvider != null && description != null)
            {
                messageFactory = _messageFactoryProvider(description);
            }
            else
            {
                // Falling back to the template's factory means headers are stamped from the
                // shared description. Harmless for an initiator with one fixed peer, but a
                // wildcard acceptor would emit the literal "*" (or a rival peer's CompID) as
                // TargetCompID on every outbound message.
                if (_template.Description?.TargetCompID == "*")
                {
                    throw new InvalidOperationException(
                        "a wildcard TargetCompID acceptor needs a per-scope ISessionMessageFactory; " +
                        "pass messageFactoryProvider to DefaultSessionScopeFactory so each connection " +
                        "stamps its own counterparty's CompID.");
                }

                messageFactory = _template.MessageFactory;
            }

            var config = new ScopedFixConfig(_template, description, messageFactory);

            var parser = _parserProvider?.Invoke(config) ?? MakeParser(config);
            var encoder = _encoderProvider != null && messageFactory != null
                ? _encoderProvider(config, messageFactory)
                : MakeEncoder(config, messageFactory);

            return new SessionScope(id, config, parser, encoder);
        }

        private static ISessionDescription? CloneDescription(ISessionDescription? description)
        {
            // Only the concrete SessionDescription knows how to copy itself. A custom
            // implementation is returned as-is, so callers using one must make it
            // immutable or supply their own scope factory.
            return description is SessionDescription concrete ? concrete.Clone() : description;
        }

        private static IMessageParser MakeParser(IFixConfig config)
        {
            var definitions = config.Definitions
                ?? throw new InvalidOperationException("cannot build a session scope without definitions");

            return new AsciiParser(definitions)
            {
                Delimiter = config.Delimiter
            };
        }

        private IMessageEncoder MakeEncoder(IFixConfig config, ISessionMessageFactory? messageFactory)
        {
            var definitions = config.Definitions
                ?? throw new InvalidOperationException("cannot build a session scope without definitions");
            var description = config.Description
                ?? throw new InvalidOperationException("cannot build a session scope without a session description");
            var factory = messageFactory
                ?? throw new InvalidOperationException("cannot build a session scope without a session message factory");

            return new AsciiEncoder(definitions, description, factory, _clock);
        }
    }
}
