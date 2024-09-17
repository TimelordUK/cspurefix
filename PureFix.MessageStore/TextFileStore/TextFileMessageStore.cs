using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Microsoft.VisualBasic.FileIO;
using System.Globalization;
using System.Collections.Concurrent;

namespace PureFix.MessageStore.TextFileStore
{
    /// <summary>
    /// Stores messages in a file.
    /// This class is not thread safe, and therefore does not support multiple message producers.
    /// </summary>
    public sealed class TextFileMessageStore : TextFileMessageStoreBase, IMessageStore
    {
        public TextFileMessageStore(ITextFileAccess fileAccess, TextFileMessageStoreConfig config) : base(fileAccess, config, new Dictionary<int, ReadOnlyMemory<byte>>())
        {
        }

        /// <inheritdoc/>
        public override async ValueTask Store(int sequenceNumber, Memory<byte> message)
        {
            if(this.Writer is TextWriter writer)
            {

                var (buffer, length) = EncodeBuffer(message);

                try
                {
                    var encodedBuffer = new ReadOnlyMemory<char>(buffer, 0, length);
                    await writer.WriteLineAsync(encodedBuffer).ConfigureAwait(false);
                    this.Cache[sequenceNumber] = message;
                }
                finally
                {
                    ReturnBuffer(buffer);
                }
            }
            else
            {
                throw new InvalidOperationException("store is not initialized");
            }
        }

                
    }
}
