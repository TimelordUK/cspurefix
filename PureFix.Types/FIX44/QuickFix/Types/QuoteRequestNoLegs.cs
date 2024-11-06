using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class QuoteRequestNoLegs : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public InstrumentLegComponent? InstrumentLeg {get; set;}
		
		[TagDetails(Tag = 687, Type = TagType.Float, Offset = 1, Required = false)]
		public double? LegQty {get; set;}
		
		[TagDetails(Tag = 690, Type = TagType.Int, Offset = 2, Required = false)]
		public int? LegSwapType {get; set;}
		
		[TagDetails(Tag = 587, Type = TagType.String, Offset = 3, Required = false)]
		public string? LegSettlType {get; set;}
		
		[TagDetails(Tag = 588, Type = TagType.LocalDate, Offset = 4, Required = false)]
		public DateOnly? LegSettlDate {get; set;}
		
		[Component(Offset = 5, Required = false)]
		public LegStipulationsComponent? LegStipulations {get; set;}
		
		[Component(Offset = 6, Required = false)]
		public NestedPartiesComponent? NestedParties {get; set;}
		
		[Component(Offset = 7, Required = false)]
		public LegBenchmarkCurveDataComponent? LegBenchmarkCurveData {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (InstrumentLeg is not null) ((IFixEncoder)InstrumentLeg).Encode(writer);
			if (LegQty is not null) writer.WriteNumber(687, LegQty.Value);
			if (LegSwapType is not null) writer.WriteWholeNumber(690, LegSwapType.Value);
			if (LegSettlType is not null) writer.WriteString(587, LegSettlType);
			if (LegSettlDate is not null) writer.WriteLocalDateOnly(588, LegSettlDate.Value);
			if (LegStipulations is not null) ((IFixEncoder)LegStipulations).Encode(writer);
			if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
			if (LegBenchmarkCurveData is not null) ((IFixEncoder)LegBenchmarkCurveData).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("InstrumentLeg") is IMessageView viewInstrumentLeg)
			{
				InstrumentLeg = new();
				((IFixParser)InstrumentLeg).Parse(viewInstrumentLeg);
			}
			LegQty = view.GetDouble(687);
			LegSwapType = view.GetInt32(690);
			LegSettlType = view.GetString(587);
			LegSettlDate = view.GetDateOnly(588);
			if (view.GetView("LegStipulations") is IMessageView viewLegStipulations)
			{
				LegStipulations = new();
				((IFixParser)LegStipulations).Parse(viewLegStipulations);
			}
			if (view.GetView("NestedParties") is IMessageView viewNestedParties)
			{
				NestedParties = new();
				((IFixParser)NestedParties).Parse(viewNestedParties);
			}
			if (view.GetView("LegBenchmarkCurveData") is IMessageView viewLegBenchmarkCurveData)
			{
				LegBenchmarkCurveData = new();
				((IFixParser)LegBenchmarkCurveData).Parse(viewLegBenchmarkCurveData);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "InstrumentLeg":
					value = InstrumentLeg;
					break;
				case "LegQty":
					value = LegQty;
					break;
				case "LegSwapType":
					value = LegSwapType;
					break;
				case "LegSettlType":
					value = LegSettlType;
					break;
				case "LegSettlDate":
					value = LegSettlDate;
					break;
				case "LegStipulations":
					value = LegStipulations;
					break;
				case "NestedParties":
					value = NestedParties;
					break;
				case "LegBenchmarkCurveData":
					value = LegBenchmarkCurveData;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)InstrumentLeg)?.Reset();
			LegQty = null;
			LegSwapType = null;
			LegSettlType = null;
			LegSettlDate = null;
			((IFixReset?)LegStipulations)?.Reset();
			((IFixReset?)NestedParties)?.Reset();
			((IFixReset?)LegBenchmarkCurveData)?.Reset();
		}
	}
}
