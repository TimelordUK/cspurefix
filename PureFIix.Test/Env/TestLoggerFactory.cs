using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFIix.Test.Env
{
    public class TestLoggerFactory : ILogFactory
    {
        private readonly IFixClock _clock;
        private List<string> fp = new List<string>();
        private List<string> fl = new List<string>();
        public TestLoggerFactory(IFixClock clock = null)
        {
            _clock = clock ?? new RealtimeClock();
        }

        public ILogger MakeLogger(string name)
        {
            return new TestLogger(name, fl, TestLogger.LogFormatTypes.App, _clock);
        }

        public ILogger MakePlainLogger(string name)
        {
            return new TestLogger(name, fp, TestLogger.LogFormatTypes.Plain, _clock);
        }
    }
}
