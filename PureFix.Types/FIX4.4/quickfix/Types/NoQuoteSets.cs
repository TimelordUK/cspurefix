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
		[TagDetails(Tag = 302, Type = TagType.String, Offset = 0, Required = false)]
		public string? QuoteSetID { get; set; }
		
		[Component(Offset = 1, Required = false)]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(Tag = 304, Type = TagType.Int, Offset = 2, Required = false)]
		public int? TotNoQuoteEntries { get; set; }
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 3, Required = false)]
		public bool? LastFragment { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public QuotEntryAckGrp? QuotEntryAckGrp { get; set; }
		
	}
}
