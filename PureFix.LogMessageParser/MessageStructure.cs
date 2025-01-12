using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.LogMessageParser
{
    public record MessageStructure(string Name, MessageTag[] Tags, int[] Delims)
    {
    }
}
