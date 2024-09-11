using PureFix.Types.FIX4._4.quickfix.set;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PureFix.Types.FIX4._4.quickfix
{
    public class Heartbeat
    {
        public StandardHeader? StandardHeader { get; set; }
        public string? TestReqID { get; set; }
        public StandardTrailer? StandardTrailer { get; set; }
    }
}
