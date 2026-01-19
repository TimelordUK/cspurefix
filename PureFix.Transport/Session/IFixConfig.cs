using PureFix.Dictionary.Definition;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Transport.Session
{
    public interface IFixConfig
    {
        public byte LogDelimiter { get; set; }
        public byte Delimiter { get; set; }
        public byte StoreDelimiter { get; set; }

        IFixDefinitions? Definitions { get; }
        ISessionDescription? Description { get; }
        ISessionMessageFactory? MessageFactory { get; }
        /// <summary>
        /// Factory for creating session stores. If null, uses MemorySessionStoreFactory.
        /// Applications can provide FileSessionStoreFactory for persistence or custom implementations.
        /// </summary>
        IFixSessionStoreFactory? SessionStoreFactory { get; }

        /// <summary>
        /// Registry for tracking active sessions. When a new session with the same SessionId
        /// is created (e.g., client reconnect), the old session is stopped to prevent
        /// stale transport writes.
        /// Optional - if null, session replacement is not tracked.
        /// </summary>
        ISessionRegistry? SessionRegistry { get; }
    }
}
