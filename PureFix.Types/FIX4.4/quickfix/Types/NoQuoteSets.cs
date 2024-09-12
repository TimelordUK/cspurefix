using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public class NoQuoteSets
	{
		public string? QuoteSetID { get; set; } // 302 STRING
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		public int? TotNoQuoteEntries { get; set; } // 304 INT
		public bool? LastFragment { get; set; } // 893 BOOLEAN
		public QuotEntryAckGrp? QuotEntryAckGrp { get; set; }
	}
}
