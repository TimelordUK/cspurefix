using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Channels;
using System.Threading.Tasks;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using PureFix.Types.Config;

namespace PureFix.Transport
{
    public class EngineRuntimeConfig
    {
        public IFixDefinitions Definitions { get; }
        public SessionDescription Session { get; }
        public ISessionMessageFactory MessageFactory { get; }
        public EngineRuntimeConfig(IFixDefinitions definitions, SessionDescription session,
            ISessionMessageFactory messageFactory)
        {
            Definitions = definitions;
            Session = session;
            MessageFactory = messageFactory;
        }
    }
}