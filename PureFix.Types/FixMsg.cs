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
        public abstract StandardHeader? StandardHeader { get; set; }
        
        public abstract StandardTrailer? StandardTrailer { get; set; }
        
        [TagDetails(Tag = 35, Type = TagType.String, Offset = 2, Required = true)]
        public string? MsgType => StandardHeader?.MsgType;
        
        [TagDetails(Tag = 9, Type = TagType.Length, Offset = 1, Required = true)]
        public int? BodyLength => StandardHeader?.BodyLength;
    }
}
