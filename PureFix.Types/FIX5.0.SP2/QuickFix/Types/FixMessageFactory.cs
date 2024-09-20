
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
    public static class FixMessageFactoryExt
    {
        public static IFixMessage? ToFixMessage(this IMessageView view)
        {
            var msgType = view.GetString((int)MsgTag.MsgType);
            switch (msgType)
            {
                case MsgTypeValues.Heartbeat:
                    {
                        var o = new Heartbeat();
                        ((IFixParser)o).Parse(view);
                        return o;
                    }

                case MsgTypeValues.Logon:
                    {
                        var o = new Heartbeat();
                        ((IFixParser)o).Parse(view);
                        return o;
                    }
            }

            return null;
        }
    }
}
