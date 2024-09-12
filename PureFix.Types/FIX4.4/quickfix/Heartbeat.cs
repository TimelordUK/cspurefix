using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class Heartbeat : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(112)]
		public string? TestReqID { get; set; } // STRING
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
