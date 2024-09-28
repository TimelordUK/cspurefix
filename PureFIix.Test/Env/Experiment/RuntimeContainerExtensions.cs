using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env.Experiment
{
    internal static class RuntimeContainerExtensions
    {
        public static bool OnReady(this RuntimeContainer runtimeContainer)
        {
            var appLog = runtimeContainer.AppLog;
            return appLog.FirstOrDefault(l => l.Contains("OnReady")) != null;
        }

        public static int HeartbeatCount(this RuntimeContainer runtimeContainer)
        {
            return runtimeContainer.MessageCount(MsgType.Heartbeat);
        }

        public static int TestRequestCount(this RuntimeContainer runtimeContainer)
        {
            return runtimeContainer.MessageCount(MsgType.TestRequest);
        }

        public static int ResendRequestCount(this RuntimeContainer runtimeContainer)
        {
            return runtimeContainer.MessageCount(MsgType.ResendRequest);
        }

        public static int LogonCount(this RuntimeContainer runtimeContainer)
        {
            return runtimeContainer.MessageCount(MsgType.Logon);
        }

        public static int LogoutCount(this RuntimeContainer runtimeContainer)
        {
            return runtimeContainer.MessageCount(MsgType.Logout);
        }

        public static int TradeCaptureReportRequestAckCount(this RuntimeContainer runtimeContainer)
        {
            return runtimeContainer.MessageCount(MsgType.TradeCaptureReportRequestAck);
        }

        public static int TradeCaptureReportCount(this RuntimeContainer runtimeContainer)
        {
            return runtimeContainer.MessageCount(MsgType.TradeCaptureReport);
        }
    }
}
