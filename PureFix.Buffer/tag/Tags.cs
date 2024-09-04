using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;

namespace PureFix.Buffer.tag
{
    internal class Tags
    {
        public const int BeginString = (int)MsgTag.BeginString;
        public const int BodyLengthTag = (int)MsgTag.BodyLength;
        public const int CheckSumTag = (int)MsgTag.CheckSum;
        public const int MessageTag = (int)MsgTag.MsgType;
    }
}
