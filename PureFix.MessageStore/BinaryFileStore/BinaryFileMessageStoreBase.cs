using System;
using System.Buffers;
using System.Buffers.Binary;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.MessageStore.BinaryFileStore
{
    /// <summary>
    /// The message is written as [length][message] where length is the length of the message in little endian format
    /// </summary>
    public abstract class BinaryFileMessageStoreBase : MessageStoreBase
    {
        /// <inheritdoc/>
        public ValueTask Store(Memory<byte> message)
        {
            var sequenceNumber = ExtractSequenceNumber(message);
            return Store(sequenceNumber, message);
        }

        public abstract ValueTask Store(int sequenceNumber, Memory<byte> message);

        /// <summary>
        /// Loads the buffer from a file.
        /// On success the stream pointer will point to the end of the stream and be ready for writing to
        /// </summary>
        /// <returns></returns>
        protected async Task LoadFromStore(Stream stream, IDictionary<int, ReadOnlyMemory<byte>> cache)
        {
            byte[] lengthBuffer = new byte[sizeof(int)];

            while(true)
            {
                var (readLengthSucceeded, _) = await PopulateBuffer(stream, lengthBuffer);
                if(readLengthSucceeded == false)
                {                
                    // If we couldn't read anything then we're done
                    return;
                }

                var length = BinaryPrimitives.ReadInt32LittleEndian(lengthBuffer);
                var message = new byte[length];

                var (loadedMessage, remainingBytes) = await PopulateBuffer(stream, message);
                if(loadedMessage)
                {
                    var sequenceNumber = ExtractSequenceNumber(message);
                    cache[sequenceNumber] = message;
                }
                else
                {
                    throw new IOException($"not enough data in stream (needed {remainingBytes} bytes");
                }
            }
        }

        private async ValueTask<(bool Success, int Remaining)> PopulateBuffer(Stream stream, byte[] buffer)
        {
            var remaining = buffer.Length;
            var length = buffer.Length;

            while(remaining != 0)
            {
                var bufferBytesRead = await stream.ReadAsync(buffer, length - remaining, remaining);
                if(bufferBytesRead == 0)
                {
                    return (false, remaining);
                }

                remaining -= bufferBytesRead;
            }

            return (true, 0);
        }

        /// <summary>
        /// Writes the message to the stream.        
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        protected async ValueTask WriteToStream(Stream stream, Memory<byte> message)
        {
            var totalLenth = sizeof(int) + message.Length;
            var writeBuffer = ArrayPool<byte>.Shared.Rent(totalLenth);

            try
            {
                // Each entry is prefix by the length to simplify reading back
                BinaryPrimitives.WriteInt32LittleEndian(writeBuffer, message.Length);
                message.CopyTo(new Memory<byte>(writeBuffer).Slice(sizeof(int)));

                await stream.WriteAsync(writeBuffer, 0, totalLenth);
            }
            finally
            {
                ArrayPool<byte>.Shared.Return(writeBuffer);
            }
        }

        /// <summary>
        /// Converts a message to a run-length encoded buffer
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        static internal Memory<byte> MakeEncodedBuffer(string message)
        {
            Memory<byte> buffer = new byte[sizeof(int) + message.Length];

            BinaryPrimitives.WriteInt32LittleEndian(buffer.Span, message.Length);

            Span<byte> messageBytes = message.Select(c => (byte)c).ToArray();
            messageBytes.CopyTo(buffer.Span.Slice(4));

            return buffer;
        }
    }
}
