using PureFix.Buffer.Ascii;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.LogMessageParser
{
    public class StructureRequest
    {
        public string DictName { get; set; } = "";
        public string Message { get; set; } = "";
        public byte Delim { get; set; } = AsciiChars.Pipe;
    }

}
