using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class ExecutionReportNoLegs : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentLegComponent? InstrumentLeg {get; set;}
		
		[TagDetails(Tag = 564, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegPositionEffect {get; set;}
		
		[TagDetails(Tag = 565, Type = TagType.Int, Offset = 2, Required = false)]
		public int? LegCoveredOrUncovered {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public NestedPartiesComponent? NestedParties {get; set;}
		
		[TagDetails(Tag = 654, Type = TagType.String, Offset = 4, Required = false)]
		public string? LegRefID {get; set;}
		
		[TagDetails(Tag = 566, Type = TagType.Float, Offset = 5, Required = false)]
		public double? LegPrice {get; set;}
		
		[TagDetails(Tag = 587, Type = TagType.String, Offset = 6, Required = false)]
		public string? LegSettlmntTyp {get; set;}
		
		[TagDetails(Tag = 588, Type = TagType.LocalDate, Offset = 7, Required = false)]
		public DateOnly? LegFutSettDate {get; set;}
		
		[TagDetails(Tag = 637, Type = TagType.Float, Offset = 8, Required = false)]
		public double? LegLastPx {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (InstrumentLeg is not null) ((IFixEncoder)InstrumentLeg).Encode(writer);
			if (LegPositionEffect is not null) writer.WriteString(564, LegPositionEffect);
			if (LegCoveredOrUncovered is not null) writer.WriteWholeNumber(565, LegCoveredOrUncovered.Value);
			if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
			if (LegRefID is not null) writer.WriteString(654, LegRefID);
			if (LegPrice is not null) writer.WriteNumber(566, LegPrice.Value);
			if (LegSettlmntTyp is not null) writer.WriteString(587, LegSettlmntTyp);
			if (LegFutSettDate is not null) writer.WriteLocalDateOnly(588, LegFutSettDate.Value);
			if (LegLastPx is not null) writer.WriteNumber(637, LegLastPx.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("InstrumentLeg") is IMessageView viewInstrumentLeg)
			{
				InstrumentLeg = new();
				((IFixParser)InstrumentLeg).Parse(viewInstrumentLeg);
			}
			LegPositionEffect = view.GetString(564);
			LegCoveredOrUncovered = view.GetInt32(565);
			if (view.GetView("NestedParties") is IMessageView viewNestedParties)
			{
				NestedParties = new();
				((IFixParser)NestedParties).Parse(viewNestedParties);
			}
			LegRefID = view.GetString(654);
			LegPrice = view.GetDouble(566);
			LegSettlmntTyp = view.GetString(587);
			LegFutSettDate = view.GetDateOnly(588);
			LegLastPx = view.GetDouble(637);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "InstrumentLeg":
					value = InstrumentLeg;
					break;
				case "LegPositionEffect":
					value = LegPositionEffect;
					break;
				case "LegCoveredOrUncovered":
					value = LegCoveredOrUncovered;
					break;
				case "NestedParties":
					value = NestedParties;
					break;
				case "LegRefID":
					value = LegRefID;
					break;
				case "LegPrice":
					value = LegPrice;
					break;
				case "LegSettlmntTyp":
					value = LegSettlmntTyp;
					break;
				case "LegFutSettDate":
					value = LegFutSettDate;
					break;
				case "LegLastPx":
					value = LegLastPx;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)InstrumentLeg)?.Reset();
			LegPositionEffect = null;
			LegCoveredOrUncovered = null;
			((IFixReset?)NestedParties)?.Reset();
			LegRefID = null;
			LegPrice = null;
			LegSettlmntTyp = null;
			LegFutSettDate = null;
			LegLastPx = null;
		}
	}
}
