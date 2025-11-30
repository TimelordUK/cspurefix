using PureFix.Dictionary.Definition;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Transport.Session
{
    public interface IFixConfig
    {
        byte? LogDelimiter { get; set; }
        byte? Delimiter { get; set; }
        IFixDefinitions? Definitions { get; }
        ISessionDescription? Description { get; }
        ISessionMessageFactory? MessageFactory { get; }

        /// <summary>
        /// Factory for creating session stores. If null, uses MemorySessionStoreFactory.
        /// Applications can provide FileSessionStoreFactory for persistence or custom implementations.
        /// </summary>
        IFixSessionStoreFactory? SessionStoreFactory { get; }
    }
}
