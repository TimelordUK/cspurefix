using PureFix.Buffer;
using PureFix.Types;

namespace PureFix.Transport.Session
{
    /// <summary>
    /// Plain holder for a connection's private dependencies. See <see cref="ISessionScope"/>.
    /// </summary>
    public sealed class SessionScope : ISessionScope
    {
        private readonly Action? _onDispose;
        private bool _disposed;

        public SessionScope(
            string id,
            IFixConfig config,
            IMessageParser parser,
            IMessageEncoder encoder,
            Action? onDispose = null)
        {
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(parser);
            ArgumentNullException.ThrowIfNull(encoder);

            Id = id;
            Config = config;
            Parser = parser;
            Encoder = encoder;
            _onDispose = onDispose;
        }

        public string Id { get; }
        public IFixConfig Config { get; }
        public IMessageParser Parser { get; }
        public IMessageEncoder Encoder { get; }

        public void Dispose()
        {
            if (_disposed) return;
            _disposed = true;

            // Hand back any storage the parser was still holding for a partially
            // received frame, otherwise a dropped connection leaks its rented buffer.
            Parser.Reset();
            _onDispose?.Invoke();
        }
    }
}
