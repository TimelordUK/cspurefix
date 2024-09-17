using System;
using System.Buffers;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace PureFix.MessageStore.TextFileStore
{
    /// <summary>
    /// Base class for message stores that write to file.
    /// The implementation uses ArrayPool to avoid allocating memory.
    /// </summary>
    public abstract class TextFileMessageStoreBase : MessageStoreBase
    {
        protected TextFileMessageStoreBase(ITextFileAccess fileAccess, TextFileMessageStoreConfig config, IDictionary<int, ReadOnlyMemory<byte>> cache)
        {
            ArgumentNullException.ThrowIfNull(fileAccess);
            ArgumentNullException.ThrowIfNull(config);
            ArgumentNullException.ThrowIfNull(cache);

            this.FileAccess = fileAccess;
            this.Config = config;
            this.Cache = cache;
        }

        /// <inheritdoc/>
        public virtual void Dispose()
        {
            this.Writer?.Dispose();
            this.Writer = null;
        }

        /// <inheritdoc/>
        public virtual ValueTask DisposeAsync()
        {
            Dispose();
            return default;
        }

        /// <summary>
        /// The object providing access to the underlying file
        /// </summary>
        private ITextFileAccess FileAccess{get;}

        /// <summary>
        /// The writer we will append to
        /// </summary>
        protected TextWriter? Writer{get; set;}

        /// <summary>
        /// The config for the store
        /// </summary>
        protected TextFileMessageStoreConfig Config{get;}

        /// <summary>
        /// The cache that maps sequence numbers to their actual messages
        /// </summary>
        protected IDictionary<int, ReadOnlyMemory<byte>> Cache{get;}

        /// <summary>
        /// Loads any messages from the underlying file into the store
        /// </summary>
        /// <returns></returns>
        public async ValueTask Initialize()
        {
            if(this.Writer is not null) throw new InvalidOperationException("store already initialized");

            await LoadFromStore(this.Cache).ConfigureAwait(false);
            this.Writer = this.FileAccess.MakeWriterForAppend(this.Config.Filename);
        }

        /// <summary>
        /// Loads the buffer from a file
        /// </summary>
        /// <returns></returns>
        protected async Task LoadFromStore(IDictionary<int, ReadOnlyMemory<byte>> cache)
        {
            if(this.FileAccess.TryMakeReader(this.Config.Filename, out var reader))
            {
                using(reader)
                {
                    while(true)
                    {
                        var line = await reader.ReadLineAsync();
                        if(line is null) break;

                        if(ShouldSkipLine(line)) continue;

                        var buffer = DecodeLine(line);
                        var sequenceNumber = ExtractSequenceNumber(buffer);

                        cache.Add(sequenceNumber, buffer);
                    }
                }
            }
        }

        /// <inheritdoc/>
        public ValueTask Store(Memory<byte> message)
        {
            var sequenceNumber = ExtractSequenceNumber(message);
            return Store(sequenceNumber, message);
        }

        /// <inheritdoc/>
        public abstract ValueTask Store(int sequenceNumber, Memory<byte> message);

        /// <inheritdoc/>
        public ValueTask<bool> TryGetMessage(int sequenceNumber, out ReadOnlyMemory<byte> message)
        {
            var found = this.Cache.TryGetValue(sequenceNumber, out message);
            return new(found);
        }

        /// <summary>
        /// Encode the buffer as a string for storing in the file
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns>A buffer that must be freed via a call to ReturnBuffer</returns>
        protected (char[] Buffer, int Length) EncodeBuffer(ReadOnlyMemory<byte> buffer)
        {
            var length = buffer.Length;
            var bufferSpan = buffer.Span;

            var text = ArrayPool<char>.Shared.Rent(length);

            var shouldReplaceSoh = this.Config.ShouldReplaceSoh;
            var sohReplacement = this.Config.SohReplacement;

            for(int i = 0; i < length; i++)
            {
                var b = bufferSpan[i];
                if(shouldReplaceSoh && b == SOH) b = sohReplacement;

                text[i] = (char)b;
            }

            return (text, length);
        }

        /// <summary>
        /// Returns the buffer allocated by EncodeBuffer to wherever it came from
        /// </summary>
        /// <param name="buffer"></param>
        protected void ReturnBuffer(char[] buffer)
        {
            ArrayPool<char>.Shared.Return(buffer);
        }

        /// <summary>
        /// Converts the line back to a buffer
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        protected ReadOnlyMemory<byte> DecodeLine(string line)
        {
            Memory<byte> buffer = new byte[line.Length];
            var span = buffer.Span;

            var shouldReplaceSoh = this.Config.ShouldReplaceSoh;
            var sohReplacement = this.Config.SohReplacement;

            for(int i = 0, length = line.Length; i < length; i++)
            {
                var b = (byte)line[i];
                if(shouldReplaceSoh && b == sohReplacement)
                {
                    b = SOH;
                }

                span[i] = b;
            }

            return buffer;
        }

        
        /// <summary>
        /// Checks to see if the line should be skipped.
        /// This allows users/support to easily comment out lines
        /// or add documentation to the file if they're that way inclined!
        /// </summary>
        /// <param name="line"></param>
        /// <returns></returns>
        private bool ShouldSkipLine(string line)
        {
            return line.Length == 0     // It's empty
                   || line[0] == '#';   // It's a comment
        }
    }
}
