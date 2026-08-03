using PureFix.Dictionary.Definition;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.Config;
using PureFix.Types.Validation;

namespace PureFix.Transport.Session
{
    /// <summary>
    /// Wraps a template <see cref="IFixConfig"/> so a session sees its own
    /// <see cref="ISessionDescription"/> and <see cref="ISessionMessageFactory"/> while
    /// still sharing the genuinely immutable pieces (definitions, store factory, registry).
    /// </summary>
    /// <remarks>
    /// Works with any <see cref="IFixConfig"/> implementation, not just <see cref="FixConfig"/>,
    /// so applications with a custom config type still get per-connection isolation.
    /// </remarks>
    public sealed class ScopedFixConfig : IFixConfig
    {
        private readonly IFixConfig _template;

        public ScopedFixConfig(
            IFixConfig template,
            ISessionDescription? description,
            ISessionMessageFactory? messageFactory)
        {
            ArgumentNullException.ThrowIfNull(template);
            _template = template;
            Description = description;
            MessageFactory = messageFactory;
        }

        public ISessionDescription? Description { get; }
        public ISessionMessageFactory? MessageFactory { get; }

        // Delimiters live on the scoped Description's Application, so reads and writes
        // stay inside this scope rather than reaching the shared template.
        public byte LogDelimiter
        {
            get => Description?.Application?.LogDelimiter ?? _template.LogDelimiter;
            set
            {
                if (Description?.Application != null) Description.Application.LogDelimiter = value;
            }
        }

        public byte Delimiter
        {
            get => Description?.Application?.Delimiter ?? _template.Delimiter;
            set
            {
                if (Description?.Application != null) Description.Application.Delimiter = value;
            }
        }

        public byte StoreDelimiter
        {
            get => Description?.Application?.StoreDelimiter ?? _template.StoreDelimiter;
            set
            {
                if (Description?.Application != null) Description.Application.StoreDelimiter = value;
            }
        }

        // Immutable or intentionally shared across sessions.
        public IFixDefinitions? Definitions => _template.Definitions;
        public IFixSessionStoreFactory? SessionStoreFactory => _template.SessionStoreFactory;
        public ISessionRegistry? SessionRegistry => _template.SessionRegistry;
        public ValidationConfig? Validation => _template.Validation;
    }
}
