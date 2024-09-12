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
		[TagDetails(41, TagType.String)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(535, TagType.String)]
		public string? AffectedOrderID { get; set; }
		
		[TagDetails(536, TagType.String)]
		public string? AffectedSecondaryOrderID { get; set; }
		
	}
}
