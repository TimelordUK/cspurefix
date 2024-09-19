using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Session
{
    public interface IFixTransport
    {
        public Task SendAsync(ReadOnlySpan<byte> messageBytes);
    }
}
