using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class MassQuoteAcknowledgementQuotSetAckGrpNoQuoteSets : IFixGroup
	{
		[TagDetails(Tag = 302, Type = TagType.String, Offset = 0, Required = false)]
		public string? QuoteSetID {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public UnderlyingInstrumentComponent? UnderlyingInstrument {get; set;}
		
		[TagDetails(Tag = 304, Type = TagType.Int, Offset = 2, Required = false)]
		public int? TotNoQuoteEntries {get; set;}
		
		[TagDetails(Tag = 893, Type = TagType.Boolean, Offset = 3, Required = false)]
		public bool? LastFragment {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public QuotEntryAckGrpComponent? QuotEntryAckGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (QuoteSetID is not null) writer.WriteString(302, QuoteSetID);
			if (UnderlyingInstrument is not null) ((IFixEncoder)UnderlyingInstrument).Encode(writer);
			if (TotNoQuoteEntries is not null) writer.WriteWholeNumber(304, TotNoQuoteEntries.Value);
			if (LastFragment is not null) writer.WriteBoolean(893, LastFragment.Value);
			if (QuotEntryAckGrp is not null) ((IFixEncoder)QuotEntryAckGrp).Encode(writer);
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
			TotNoQuoteEntries = view.GetInt32(304);
			LastFragment = view.GetBool(893);
			if (view.GetView("QuotEntryAckGrp") is IMessageView viewQuotEntryAckGrp)
			{
				QuotEntryAckGrp = new();
				((IFixParser)QuotEntryAckGrp).Parse(viewQuotEntryAckGrp);
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
				case "TotNoQuoteEntries":
					value = TotNoQuoteEntries;
					break;
				case "LastFragment":
					value = LastFragment;
					break;
				case "QuotEntryAckGrp":
					value = QuotEntryAckGrp;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			QuoteSetID = null;
			((IFixReset?)UnderlyingInstrument)?.Reset();
			TotNoQuoteEntries = null;
			LastFragment = null;
			((IFixReset?)QuotEntryAckGrp)?.Reset();
		}
	}
}
