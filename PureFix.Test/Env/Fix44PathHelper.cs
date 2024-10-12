using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Env
{
    public static class Fix44PathHelper
    {
        public static readonly string ExampleRootBasePath = Path.Join(Directory.GetCurrentDirectory(), "Data", "examples");
        public static readonly string ExampleRootPath = Path.Join(ExampleRootBasePath, "FIX.4.4");
        public static readonly string ExampleRootPath50SP2 = Path.Join(ExampleRootBasePath, "FIX.5.0SP2");
        public static readonly string DataDictRootPath = Path.Join(Directory.GetCurrentDirectory(), "..", "..",
            "..", "..", "Data");
        public static readonly string SessionRootPath = Path.Join(DataDictRootPath, "Session");
        public static readonly string DataDictPath = Path.Join(DataDictRootPath, "FIX44.xml");
        public static readonly string SkeletonRecoveryPath = Path.Join(ExampleRootPath, "skeleton-client.txt");
        public static readonly string TradeCaptureRecoveryPath = Path.Join(ExampleRootPath50SP2, "tc-client.txt");
        public static readonly string LogonReplayPath = Path.Join(ExampleRootPath, "quickfix", "logon", "fix.txt");
        public static readonly string ExecutionReportReplayPath = Path.Join(ExampleRootPath, "quickfix", "execution-report", "fix.txt");
        public static readonly string HeartbeatReplayPath = Path.Join(ExampleRootPath, "quickfix", "heartbeat", "fix.txt");
        public static readonly string ReplayPath = Path.Join(ExampleRootPath, "fix.txt");
        public static readonly string ReplayTestClientPath = Path.Join(ExampleRootPath, "fix.test_client.txt");
        public static readonly string JsonPath = Path.Join(ExampleRootPath, "fix.json");
    }
}
