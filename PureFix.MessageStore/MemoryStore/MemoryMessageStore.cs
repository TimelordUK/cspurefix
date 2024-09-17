using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.MessageStore.MemoryStore
{
    /// <summary>
    /// Stores messages in memory
    /// </summary>
    public class MemoryMessageStore : MessageStoreBase, IMessageStore
    {
        private Dictionary<int, ReadOnlyMemory<byte>> m_Cache = new();

        /// <inheritdoc/>
        public void Dispose()
        {
        }

        /// <inheritdoc/>
        public ValueTask DisposeAsync()
        {
            return default;
        }

        /// <inheritdoc/>
        public ValueTask Initialize()
        {
            return default;
        }

        /// <inheritdoc/>
        public ValueTask Store(Memory<byte> message)
        {
            var sequenceNumber = ExtractSequenceNumber(message);
            return Store(sequenceNumber, message);
        }

        /// <inheritdoc/>
        public ValueTask Store(int sequenceNumber, Memory<byte> message)
        {
            m_Cache[sequenceNumber] = message;
            return default;
        }

        /// <inheritdoc/>
        public ValueTask<bool> TryGetMessage(int sequenceNumber, out ReadOnlyMemory<byte> message)
        {
            var found = m_Cache.TryGetValue(sequenceNumber, out message);
            return new(found);
        }
    }
}
