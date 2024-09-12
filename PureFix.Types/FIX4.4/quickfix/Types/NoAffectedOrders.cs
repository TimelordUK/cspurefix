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
		[TagDetails(41)]
		public string? OrigClOrdID { get; set; } // STRING
		
		[TagDetails(535)]
		public string? AffectedOrderID { get; set; } // STRING
		
		[TagDetails(536)]
		public string? AffectedSecondaryOrderID { get; set; } // STRING
		
	}
}
