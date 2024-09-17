using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.MessageStore.BinaryFileStore
{
    /// <summary>
    /// Stores messages as binary data in a file.
    /// This is the prefered way of storing FIX messages that may contain raw data
    /// </summary>
    public sealed class BinaryFileMessageStore : BinaryFileMessageStoreBase, IMessageStore
    {
        private readonly Dictionary<int, ReadOnlyMemory<byte>> m_Cache = new();

        private readonly IBinaryFileAccess m_FileAccess;
        private readonly BinaryFileMessageStoreConfig m_Config;

        private Stream? m_Stream;

        /// <summary>
        /// Initializes the instance
        /// </summary>
        /// <param name="fileAccess"></param>
        /// <param name="config"></param>
        public BinaryFileMessageStore(IBinaryFileAccess fileAccess, BinaryFileMessageStoreConfig config)
        {
            ArgumentNullException.ThrowIfNull(fileAccess);
            ArgumentNullException.ThrowIfNull(config);
            
            m_FileAccess = fileAccess;
            m_Config = config;
        }

        /// <inheritdoc/>
        public async ValueTask Initialize()
        {
            if(m_Stream is not null) throw new InvalidOperationException("store already initialized");

            m_Stream = m_FileAccess.MakeWriterForAppend(m_Config.Filename);
            await LoadFromStore(m_Stream, m_Cache);
        }

        /// <inheritdoc/>
        public void Dispose()
        {
            m_Stream?.Dispose();
            m_Stream = null;
        }

        /// <inheritdoc/>
        public ValueTask DisposeAsync()
        {
            Dispose();
            return default;
        }

        /// <inheritdoc/>
        public override async ValueTask Store(int sequenceNumber, Memory<byte> message)
        {
            if(m_Stream is Stream stream)
            {
                await WriteToStream(stream, message);
                m_Cache[sequenceNumber] = message;
            }
            else
            {
                throw new InvalidOperationException("store is not initialized");
            }
        }

        /// <inheritdoc/>
        public ValueTask<bool> TryGetMessage(int sequenceNumber, out ReadOnlyMemory<byte> message)
        {
            var found = m_Cache.TryGetValue(sequenceNumber, out message);
            return new(found);
        }
    }
}
