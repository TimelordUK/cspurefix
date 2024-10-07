using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Session
{
    public interface IMessageTransport : IDisposable
    {
        Task SendAsync(ReadOnlyMemory<byte> messageBytes, CancellationToken token);
        Task<int> ReceiveAsync(Memory<byte> buffer, CancellationToken token);
        bool Connected { get; }
    }
}
