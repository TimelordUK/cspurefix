using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoRoutingIDs
	{
		public int? RoutingType { get; set; } // 216 INT
		public string? RoutingID { get; set; } // 217 STRING
	}
}
