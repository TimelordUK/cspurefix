using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoCompIDs
	{
		public string? RefCompID { get; set; } // 930 STRING
		public string? RefSubID { get; set; } // 931 STRING
		public string? LocationID { get; set; } // 283 STRING
		public string? DeskID { get; set; } // 284 STRING
	}
}
