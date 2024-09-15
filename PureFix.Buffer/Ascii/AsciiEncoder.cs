using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Dictionary.Definition;
using PureFix.Types;

namespace PureFix.Buffer.Ascii
{
    public class AsciiEncoder
    {
        public FixDefinitions Definitions { get; set; }
        public AsciiEncoder(FixDefinitions definitions)
        {
            Definitions = definitions;
        }

        // take an application created object e.g. Logon, and encode to fix wire format such that it
        // can be transmitted to the socket.

        public byte[]? Encode(IFixMessage message)
        {
            if (message.MsgType == null) return null;
            if (!Definitions.Message.TryGetValue(message.MsgType, out var msgDefinition))
            {
                return null; 
            }
            return null;
        }
    }
}
