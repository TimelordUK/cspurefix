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
		[TagDetails(302, TagType.String)]
		public string? QuoteSetID { get; set; }
		
		[Component]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(304, TagType.Int)]
		public int? TotNoQuoteEntries { get; set; }
		
		[TagDetails(893, TagType.Boolean)]
		public bool? LastFragment { get; set; }
		
		[Component]
		public QuotEntryAckGrp? QuotEntryAckGrp { get; set; }
		
	}
}
