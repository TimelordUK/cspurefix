using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class QuotSetGrpNoQuoteSets : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 302, Type = TagType.String, Offset = 0, Required = true)]
		public string? QuoteSetID { get; set; }
		
		[Component(Offset = 1, Required = false)]
		public UnderlyingInstrument? UnderlyingInstrument { get; set; }
		
		[TagDetails(Tag = 367, Type = TagType.UtcTimestamp, Offset = 2, Required = false)]
		public DateTime? QuoteSetValidUntilTime { get; set; }
		
		[TagDetails(Tag = 304, Type = TagType.Int, Offset = 3, Required = true)]
		public int? TotNoQuoteEntries { get; set; }
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 4, Required = false)]
		public bool? LastFragment { get; set; }
		
		[Component(Offset = 5, Required = true)]
		public QuotEntryGrp? QuotEntryGrp { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				QuoteSetID is not null
				&& TotNoQuoteEntries is not null
				&& QuotEntryGrp is not null && ((IFixValidator)QuotEntryGrp).IsValid(in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (QuoteSetID is not null) writer.WriteString(302, QuoteSetID);
			if (UnderlyingInstrument is not null) ((IFixEncoder)UnderlyingInstrument).Encode(writer);
			if (QuoteSetValidUntilTime is not null) writer.WriteUtcTimeStamp(367, QuoteSetValidUntilTime.Value);
			if (TotNoQuoteEntries is not null) writer.WriteWholeNumber(304, TotNoQuoteEntries.Value);
			if (LastFragment is not null) writer.WriteBoolean(893, LastFragment.Value);
			if (QuotEntryGrp is not null) ((IFixEncoder)QuotEntryGrp).Encode(writer);
		}
	}
}
