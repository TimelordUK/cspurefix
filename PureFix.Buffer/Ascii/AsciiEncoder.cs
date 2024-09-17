using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Contained;
using PureFix.Dictionary.Definition;
using PureFix.Types;
using static PureFix.Buffer.Ascii.AsciiParser.Pool;

namespace PureFix.Buffer.Ascii
{
    public class AsciiEncoder
    {
        public IFixDefinitions Definitions { get; set; }
        private AsciiParser.Pool Pool { get; set; }
        public byte LogDelimiter { get; set; } = AsciiChars.Pipe;
        public byte Delimiter { get; set; } = AsciiChars.Soh;
        public SessionDescription SessionDescription { get; }
        public int MsgSeqNum { get; set; }
        public ISessionMessageFactory SessionMessageFactory { get; }
        public IFixClock Clock {get;}

        public AsciiEncoder(IFixDefinitions definitions, SessionDescription sessionDescription, ISessionMessageFactory messageFactory, IFixClock clock)
        {
            Definitions = definitions;
            Pool = new AsciiParser.Pool();
            SessionDescription = sessionDescription;
            SessionMessageFactory = messageFactory;
            Clock = clock;
        }

        // take an application created object e.g. Logon, and encode to fix wire format such that it
        // can be transmitted to the socket. We expect the storage to be returned to the pool once
        // message has been sent.

        public Storage? Encode(string msgType, IFixMessage message)
        {
            if (message.MsgType == null) return null;
            if (!Definitions.Message.TryGetValue(message.MsgType, out var msgDefinition))
            {
                return null; 
            }

            // may have to fold pemitted user specified header override fields onto the destination message.
            if (message.StandardHeader != null)
            {
            }

            var hdr = SessionMessageFactory.Header(msgType, MsgSeqNum, Clock.Current);
            if (hdr == null)
            {
                return null;
            }
            var storage = Pool.Rent();
            var writer = new DefaultFixWriter(storage.Buffer, storage.Locations);
            message.Encode(writer);
            return storage;
        }
    }
}
