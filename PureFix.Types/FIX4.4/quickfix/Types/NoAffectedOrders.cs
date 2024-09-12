using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoAffectedOrders
	{
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 0, Required = false)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(Tag = 535, Type = TagType.String, Offset = 1, Required = false)]
		public string? AffectedOrderID { get; set; }
		
		[TagDetails(Tag = 536, Type = TagType.String, Offset = 2, Required = false)]
		public string? AffectedSecondaryOrderID { get; set; }
		
	}
}
