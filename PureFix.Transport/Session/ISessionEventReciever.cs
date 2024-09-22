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
        Task OnTimer();
        // data received from peer which must be sent to the parser to eventually form a fully constructed view.
        void OnRx(byte[] buffer); 
    }
}
