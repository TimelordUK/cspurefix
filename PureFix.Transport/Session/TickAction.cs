using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Transport.Session
{
    public enum TickAction
    {
        Nothing = 1,
        Heartbeat = 2,
        TestRequest = 3,
        TerminateOnError = 4,
        WaitLogoutConfirmExpired = 5,
        Stop = 6
    }
}
