using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.MessageStore.TextFileStore
{
    /// <summary>
    /// A file message store that can be used my multiple message producers.
    /// Writes to the underlying file are serialized to ensure that writes are not intermixed.
    /// </summary>
    public sealed class ConcurrentTextFileMessageStore : TextFileMessageStoreBase, IMessageStore
    {
        private readonly SemaphoreSlim m_Lock = new(1);

        /// <summary>
        /// Initialized the instance
        /// </summary>
        /// <param name="fileAccess"></param>
        /// <param name="config"></param>
        public ConcurrentTextFileMessageStore(ITextFileAccess fileAccess, TextFileMessageStoreConfig config) : base(fileAccess, config, new ConcurrentDictionary<int, ReadOnlyMemory<byte>>())
        {
        }       

        /// <inheritdoc/>
        public override void Dispose()
        {
            m_Lock.Dispose();
            base.Dispose();
        }

        /// <inheritdoc/>
        public override async ValueTask Store(int sequenceNumber, Memory<byte> message)
        {
            if(this.Writer is TextWriter writer)
            {
                var (buffer, length) = EncodeBuffer(message);

                // We'll serialize access to the underlying message store
                await m_Lock.WaitAsync().ConfigureAwait(false);
                try
                {
                    var memory = new ReadOnlyMemory<char>(buffer, 0, length);
                    await writer.WriteLineAsync(memory).ConfigureAwait(false);
                }
                finally
                {
                    m_Lock.Release();
                    ReturnBuffer(buffer);
                }

                this.Cache[sequenceNumber] = message;
            }
            else
            {
                throw new InvalidOperationException("store is not initialized");
            }
        }
    }
}
