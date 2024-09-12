using PureFix.Types.FIX4._4.quickfix.set;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix;

namespace PureFix.Types
{
    public abstract class FixMsg
    {
        public abstract StandardHeader? StandardHeader { get; set; }
        public abstract StandardTrailer? StandardTrailer { get; set; }
        public string? MsgType => StandardHeader?.MsgType;
        public int? BodyLength => StandardHeader?.BodyLength;
    }
}
