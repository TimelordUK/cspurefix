using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    internal class AsciiParseState
    {
        public void BeginMessage()
        {
            throw new NotImplementedException();
        }

        public void BeginTag(int writePtr)
        {
            throw new NotImplementedException();
        }

        public void EndTag(int writePtr)
        {
            throw new NotImplementedException();
        }

        public object ParseState { get; set; }

        public void Store()
        {
            throw new NotImplementedException();
        }

        public bool IncRaw()
        {
            throw new NotImplementedException();
        }
    }
}
