using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoEvents
	{
		[TagDetails(865)]
		public int? EventType { get; set; } // INT
		
		[TagDetails(866)]
		public DateTime? EventDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(867)]
		public double? EventPx { get; set; } // PRICE
		
		[TagDetails(868)]
		public string? EventText { get; set; } // STRING
		
	}
}
