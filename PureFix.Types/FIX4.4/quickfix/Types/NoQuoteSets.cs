using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoQuoteSets
	{
		[TagDetails(302)]
		public string? QuoteSetID { get; set; } // STRING
		
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		[TagDetails(304)]
		public int? TotNoQuoteEntries { get; set; } // INT
		
		[TagDetails(893)]
		public bool? LastFragment { get; set; } // BOOLEAN
		
		public QuotEntryAckGrp? QuotEntryAckGrp { get; set; }
	}
}
