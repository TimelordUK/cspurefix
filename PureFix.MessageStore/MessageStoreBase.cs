using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.MessageStore
{
    /// <summary>
    /// Useful methods for implementing an IMessageStore class
    /// </summary>
    public abstract class MessageStoreBase
    {
        protected const byte SOH = 0x01;
        private const byte AsciiZero = (byte)'0';
        private const byte AsciiNine = (byte)'9';
        
        private static readonly ReadOnlyMemory<byte> MsgSeqNum = new byte[]{51, 52, 61}; // 34=

        /// <summary>
        /// Looks for the sequence number tag (34) in a message and extracts the number assigned to it
        /// </summary>
        /// <param name="buffer"></param>
        /// <returns></returns>
        /// <exception cref="InvalidOperationException"></exception>
        protected int ExtractSequenceNumber(ReadOnlyMemory<byte> buffer)
        {
            var span = buffer.Span;
            
            var index = span.IndexOf(MsgSeqNum.Span);
            if(index == -1) throw new InvalidOperationException("no MsgSeqNum in message");

            var number = 0;
            index += MsgSeqNum.Length;

            while(span[index] >= AsciiZero && span[index] <= AsciiNine)
            {
                number = (number * 10) + (span[index] - AsciiZero);
                index++;
            }

            return number;
        }
    }
}
