using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types
{
    public abstract class FixMsg
    {
        [Component]
        public abstract StandardHeader? StandardHeader { get; set; }
        
        [Component]
        public abstract StandardTrailer? StandardTrailer { get; set; }
        
        [TagDetails(35, TagType.String)]
        public string? MsgType => StandardHeader?.MsgType;
        
        [TagDetails(9, TagType.Length)]
        public int? BodyLength => StandardHeader?.BodyLength;
    }
}
