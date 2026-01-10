using PureFix.Examples.Skeleton;
using PureFix.Test.ModularTypes.Helpers;
using PureFix.Examples.TradeCapture;
using PureFix.Test.ModularTypes.Helpers;
﻿using Arrow.Threading.Tasks;
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


namespace PureFix.Test.ModularTypes.Env.Experiment
{
    public abstract class BaseSessionFactory(
        IFixConfig config,
        IFixLogRecovery fixLogRecovery,
        ILogFactory logFactory,
        IFixMessageFactory fixMessageFactory,
        IMessageParser parser,
        IMessageEncoder encoder,
        IFixMsgStore store,
        AsyncWorkQueue q,
        IFixClock clock)
        : ISessionFactory
    {
        protected readonly IFixConfig m_config = config;
        protected readonly IFixMessageFactory m_fixMessageFactory = fixMessageFactory;
        protected readonly IMessageParser m_parser = parser;
        protected readonly IMessageEncoder m_encoder = encoder;
        protected readonly AsyncWorkQueue m_q = q;
        protected readonly IFixClock m_clock = clock;
        protected readonly IFixMsgStore m_msgStore = store;
        protected readonly ILogFactory m_logFactory = logFactory;
        protected readonly IFixLogRecovery m_fixLogRecovery = fixLogRecovery;

        public abstract FixSession MakeSession();
    }
}
