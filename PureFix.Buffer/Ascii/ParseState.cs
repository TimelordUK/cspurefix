using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Buffer.Ascii
{
    public enum ParseState
    {
        BeginField = 1,
        ParsingTag = 2,
        ParsingValue = 3,
        ParsingRawDataLength = 4,
        ParsingRawData = 5,
        MsgComplete = 6
    }
}
