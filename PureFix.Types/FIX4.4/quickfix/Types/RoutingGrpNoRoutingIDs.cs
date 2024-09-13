using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class RoutingGrpNoRoutingIDs
	{
		[TagDetails(Tag = 216, Type = TagType.Int, Offset = 0, Required = false)]
		public int? RoutingType { get; set; }
		
		[TagDetails(Tag = 217, Type = TagType.String, Offset = 1, Required = false)]
		public string? RoutingID { get; set; }
		
	}
}
