using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Buffer.Ascii;
using PureFix.Types;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Transport.Session
{
    public interface ISessionEventReciever
    {
        // the session must check state and if necessary send heartbeat or test request.
        void OnTimer();
        // a structure and view have been computed and the session can either respond on a session message
        // or forward to the application.
        void OnRx(ReadOnlySpan<byte> buffer);
        // a message has been scanned and a set of tagpos produced - the structure and view 
        // have not yet beem computed, the applicaton should use this event to log the message.
    }
}
