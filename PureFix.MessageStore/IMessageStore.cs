namespace PureFix.MessageStore
{
    /// <summary>
    /// Stores messages somewhere!
    /// </summary>
    public interface IMessageStore : IDisposable, IAsyncDisposable
    {
        /// <summary>
        /// Initializes the store
        /// </summary>
        /// <returns></returns>
        public ValueTask Initialize();
        
        /// <summary>
        /// Stores a message. An implementation will take ownership of the supplied message.
        /// The sequence number will be extracted from the message
        /// </summary>
        /// <param name="message"></param>
        /// <returns></returns>
        public ValueTask Store(Memory<byte> message);

        /// <summary>
        /// Stores a message. An implementation will take ownership of the supplied message.
        /// It is assumed that the sequence number for the message is correct
        /// </summary>
        /// <param name="sequenceNumber"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ValueTask Store(int sequenceNumber, Memory<byte> message);
        
        /// <summary>
        /// Attempts to get a message from the store
        /// </summary>
        /// <param name="sequenceNumber"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public ValueTask<bool> TryGetMessage(int sequenceNumber, out ReadOnlyMemory<byte> message);
    }
}
