using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    internal class SkeletonSessionFactory : ISessionFactory
    {
        public FixSession MakeSession(IFixConfig config, IMessageTransport transport, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, AsyncWorkQueue q, IFixClock clock)
        {
            return new TestAsciiSkeleton(config, transport, fixMessageFactory, parser, encoder, q, clock);
        }
    }
}
