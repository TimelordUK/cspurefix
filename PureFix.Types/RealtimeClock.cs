using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace PureFix.Types
{
    public class RealtimeClock : IFixClock
    {
        public DateTime Current
        {
            get => DateTime.Now;
            set { }
        }
    }
}
