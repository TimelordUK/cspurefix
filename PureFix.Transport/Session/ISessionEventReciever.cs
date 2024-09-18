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
        void OnMsg(string msgType, IMessageView view);
        // a message has been scanned and a set of tagpos produced - the structure and view 
        // have not yet beem computed, the applicaton should use this event to log the message.
        void OnDecoded(string msgType, ElasticBuffer buffer);
        // the outbound buffer is ready to be transmitted to the remote entity, the session should log
        // this message.
        void OnEncoded(string msgType, IStandardHeader header, IFixMessage message, AsciiParser.Pool.Storage storage);
        // application initiates a graceful termination of the session - send logout to remote and proceed to end state
        // when logout received.
        void Done();
        // a forced end i.e. the transport is disconnected if active, and the session is terminated.
        void End();
    }
}
