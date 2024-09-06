using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.tag;

namespace PureFix.Buffer.Ascii
{
    public class MsgView(TagPos[] tags)
    {
        public TagPos[] Tags { get; set; } = tags;
    }
}
