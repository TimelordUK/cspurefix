using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoRoutingIDs
	{
		public int? RoutingType { get; set; } // 216 INT
		public string? RoutingID { get; set; } // 217 STRING
	}
}
