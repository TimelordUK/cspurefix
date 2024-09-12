using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.ParserFormat
{
    public static class HopExt
    {
        public static void Parse(this Hop instance, MsgView? mv)
        {
            if (mv is not AsciiView view) return;

        }
    }
}
