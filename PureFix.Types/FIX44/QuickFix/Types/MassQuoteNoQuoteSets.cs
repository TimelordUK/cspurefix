using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class MassQuoteNoQuoteSets : IFixGroup
	{
		[TagDetails(Tag = 302, Type = TagType.String, Offset = 0, Required = true)]
		public string? QuoteSetID {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public UnderlyingInstrumentComponent? UnderlyingInstrument {get; set;}
		
		[TagDetails(Tag = 367, Type = TagType.UtcTimestamp, Offset = 2, Required = false)]
		public DateTime? QuoteSetValidUntilTime {get; set;}
		
		[TagDetails(Tag = 304, Type = TagType.Int, Offset = 3, Required = true)]
		public int? TotNoQuoteEntries {get; set;}
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 4, Required = false)]
		public bool? LastFragment {get; set;}
		
		[Component(Offset = 5, Required = true)]
		public QuotEntryGrpComponent? QuotEntryGrp {get; set;}
		
		
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
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			QuoteSetID = view.GetString(302);
			if (view.GetView("UnderlyingInstrument") is IMessageView viewUnderlyingInstrument)
			{
				UnderlyingInstrument = new();
				((IFixParser)UnderlyingInstrument).Parse(viewUnderlyingInstrument);
			}
			QuoteSetValidUntilTime = view.GetDateTime(367);
			TotNoQuoteEntries = view.GetInt32(304);
			LastFragment = view.GetBool(893);
			if (view.GetView("QuotEntryGrp") is IMessageView viewQuotEntryGrp)
			{
				QuotEntryGrp = new();
				((IFixParser)QuotEntryGrp).Parse(viewQuotEntryGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "QuoteSetID":
					value = QuoteSetID;
					break;
				case "UnderlyingInstrument":
					value = UnderlyingInstrument;
					break;
				case "QuoteSetValidUntilTime":
					value = QuoteSetValidUntilTime;
					break;
				case "TotNoQuoteEntries":
					value = TotNoQuoteEntries;
					break;
				case "LastFragment":
					value = LastFragment;
					break;
				case "QuotEntryGrp":
					value = QuotEntryGrp;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			QuoteSetID = null;
			((IFixReset?)UnderlyingInstrument)?.Reset();
			QuoteSetValidUntilTime = null;
			TotNoQuoteEntries = null;
			LastFragment = null;
			((IFixReset?)QuotEntryGrp)?.Reset();
		}
	}
}
