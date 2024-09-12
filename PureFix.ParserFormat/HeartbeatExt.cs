using PureFix.Buffer.Ascii;
using PureFix.Types.FIX44.QuickFix;
using PureFix.Types.FIX44.QuickFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.ParserFormat
{
    public static class HeartbeatExt
    {
        public static void Parse(this Heartbeat instance, MsgView? view)
        {
            instance.StandardHeader ??= new StandardHeader();
            instance.StandardHeader.Parse(view?.GetView("StandardHeader"));
            instance.TestReqID = view?.GetString(112);
            instance.StandardTrailer ??= new StandardTrailer();
            instance.StandardTrailer.Parse(view?.GetView("StandardTrailer"));
        }
    }
}
