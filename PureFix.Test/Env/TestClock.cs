using PureFix.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Test.Env
{
    internal class TestClock : IFixClock
    {
        public DateTime Current { get; set; }
    }
}
