using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Benchmarks
{
    public static class Fix44PathHelper
    {
        public static readonly string Root = Directory.GetCurrentDirectory();
        public static readonly string Fix44 = Path.Join(Root, "Data", "FIX44.xml");
        public static readonly string HeartbeatFile = Path.Join(Root, "Messages", "Heartbeat.txt");
    }
}
