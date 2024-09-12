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
		[TagDetails(216, TagType.Int)]
		public int? RoutingType { get; set; }
		
		[TagDetails(217, TagType.String)]
		public string? RoutingID { get; set; }
		
	}
}
