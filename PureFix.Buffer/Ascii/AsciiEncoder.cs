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
        public FixDefinitions Definitions { get; set; }
        private AsciiParser.Pool Pool { get; set; }
        public byte LogDelimiter { get; set; } = AsciiChars.Pipe;
        public byte Delimiter { get; set; } = AsciiChars.Soh;

        public AsciiEncoder(FixDefinitions definitions)
        {
            Definitions = definitions;
            Pool = new AsciiParser.Pool();
        }

        // take an application created object e.g. Logon, and encode to fix wire format such that it
        // can be transmitted to the socket. We expect the storage to be returned to the pool once
        // message has been sent.

        public Storage? Encode(IFixMessage message)
        {
            if (message.MsgType == null) return null;
            if (!Definitions.Message.TryGetValue(message.MsgType, out var msgDefinition))
            {
                return null; 
            }

            var storage = Pool.Rent();
            message.Encode(storage.Buffer, storage.Locations, LogDelimiter);
            return storage;
        }
    }
}
