using Arrow.Threading.Tasks;
using PureFix.Buffer;
using PureFix.Transport.Recovery;
using PureFix.Transport.Session;
using PureFix.Transport.Store;
using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Env.Experiment
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
        protected readonly ILogFactory m_logFactory;
        protected readonly IFixLogRecovery m_fixLogRecovery;

        protected BaseSessionFactory(IFixConfig config, IFixLogRecovery fixLogRecovery, ILogFactory logFactory, IFixMessageFactory fixMessageFactory, IMessageParser parser, IMessageEncoder encoder, IFixMsgStore store, AsyncWorkQueue q, IFixClock clock)
        {
            m_config = config;
            m_fixMessageFactory = fixMessageFactory;
            m_parser = parser;
            m_encoder = encoder;
            m_q = q;
            m_clock = clock;
            m_msgStore = store;
            m_logFactory = logFactory;
            m_fixLogRecovery = fixLogRecovery;
        }
        public abstract FixSession MakeSession();
    }
}
