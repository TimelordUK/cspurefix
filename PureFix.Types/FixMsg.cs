using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace PureFix.Types
{
    public abstract class FixMsg
    {
        [TagDetails(Tag = 35, Type = TagType.String, Offset = 2, Required = true)]
        public abstract string? MsgType { get; }

        [TagDetails(Tag = 9, Type = TagType.Length, Offset = 1, Required = true)]
        public abstract int? BodyLength { get; }
    }
}
