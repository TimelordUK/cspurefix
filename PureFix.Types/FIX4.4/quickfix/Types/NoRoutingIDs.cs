using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoRoutingIDs
	{
		[TagDetails(216)]
		public int? RoutingType { get; set; } // INT
		
		[TagDetails(217)]
		public string? RoutingID { get; set; } // STRING
		
	}
}
