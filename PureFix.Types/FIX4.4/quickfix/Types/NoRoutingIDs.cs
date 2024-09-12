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
		[TagDetails(Tag = 216, Type = TagType.Int, Offset = 0)]
		public int? RoutingType { get; set; }
		
		[TagDetails(Tag = 217, Type = TagType.String, Offset = 1)]
		public string? RoutingID { get; set; }
		
	}
}
