using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.SocketTransport
{
    public interface ITcpEntity
    {
        public Task Start(CancellationToken cancellationToken);
    }
}
