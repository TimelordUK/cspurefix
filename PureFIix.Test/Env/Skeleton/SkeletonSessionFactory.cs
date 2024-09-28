using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env.Skeleton
{
    public abstract class BaseSessionFactory : ISessionFactory
    {
        protected readonly IFixConfig m_config;
        protected readonly IFixMessageFactory m_fixMessageFactory;
        protected readonly IMessageParser m_parser;
        protected readonly IMessageEncoder m_encoder;
        protected readonly AsyncWorkQueue m_q;
        protected readonly IFixClock m_clock;
        protected readonly IFixMsgStore m_msgStore;
        protected BaseSessionFactory(IFixConfig config, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixMsgStore store, AsyncWorkQueue q, IFixClock clock)
        {
            m_config = config;
            m_fixMessageFactory = fixMessageFactory;
            m_parser = parser;
            m_encoder = encoder;
            m_q = q;
            m_clock = clock;
            m_msgStore = store;
        }
        public abstract FixSession MakeSession();
    }

    internal class SkeletonSessionFactory : BaseSessionFactory
    {
        public SkeletonSessionFactory(IFixConfig config, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixMsgStore store, AsyncWorkQueue q, IFixClock clock) : base(config, fixMessageFactory, parser, encoder, store, q, clock)
        {
        }

        public override FixSession MakeSession()
        {
            return new TestAsciiSkeleton(m_config, m_fixMessageFactory, m_parser, m_encoder, m_msgStore, m_q, m_clock);
        }
    }
}
