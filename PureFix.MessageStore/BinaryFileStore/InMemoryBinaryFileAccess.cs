using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.MessageStore.BinaryFileStore
{
    public class InMemoryBinaryFileAccess : IBinaryFileAccess
    {
        private List<Memory<byte>> m_Messages = new();

        public InMemoryBinaryFileAccess(params string[] messages)
        {
            foreach(var message in messages)
            {
                var buffer = BinaryFileMessageStoreBase.MakeEncodedBuffer(message);
                m_Messages.Add(buffer);
            }
        }

        public Stream MakeWriterForAppend(string filename)
        {
            var stream = new MemoryStream();

            foreach(var message in m_Messages)
            {
                stream.Write(message.Span);
            }

            stream.Position = 0;

            return stream;
        }
    }
}
