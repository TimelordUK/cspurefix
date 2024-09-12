using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class Heartbeat : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? TestReqID { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
