using PureFix.Dictionary.Definition;
using PureFix.Transport.Store;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Transport.Session
{
    public interface IFixConfig
    {
        public byte? LogDelimiter
        {
            get => Description?.Application?.LogDelimiter;
            set
            {
                if (Description?.Application != null)
                {
                    Description.Application.LogDelimiter = value;
                }
            }
        }

        public byte? StoreDelimiter
        {
            get => Description?.Application?.StoreDelimiter;
            set
            {
                if (Description?.Application != null)
                {
                    Description.Application.StoreDelimiter = value;
                }
            }
        }

        public byte? Delimiter
        {
            get => Description?.Application?.Delimiter;
            set
            {
                if (Description?.Application != null)
                {
                    Description.Application.Delimiter = value;
                }
            }
        }

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
