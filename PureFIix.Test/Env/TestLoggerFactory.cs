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
        
        private LogTrace al = new LogTrace();
        private LogTrace fl = new LogTrace();

        public TestLoggerFactory(IFixClock clock = null)
        {
            _clock = clock ?? new RealtimeClock();
        }

        public ILogger MakeLogger(string name)
        {
            return new TestLogger(name, al, TestLogger.LogFormatTypes.App, _clock);
        }

        public ILogger MakePlainLogger(string name)
        {
            return new TestLogger(name, fl, TestLogger.LogFormatTypes.Plain, _clock);
        }
    }
}
