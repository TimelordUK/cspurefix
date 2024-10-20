using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Env
{
    public class TestLoggerFactory : ILogFactory
    {
        private readonly IFixClock _clock;

        private readonly LogTrace m_al = [];
        private readonly LogTrace m_fl = [];

        public TestLoggerFactory(IFixClock clock = null)
        {
            _clock = clock ?? new RealtimeClock();
        }

        public ILogger MakeLogger(string name)
        {
            return new TestLogger(name, m_al, TestLogger.LogFormatTypes.App, _clock);
        }

        public ILogger MakePlainLogger(string name)
        {
            return new TestLogger(name, m_fl, TestLogger.LogFormatTypes.Plain, _clock);
        }
    }
}
