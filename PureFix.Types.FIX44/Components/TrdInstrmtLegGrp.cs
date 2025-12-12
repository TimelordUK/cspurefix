using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44.Components
{
	public sealed partial class TrdInstrmtLegGrp : IFixComponent
	{
		public sealed partial class NoLegs : IFixGroup
		{
			[Component(Offset = 0, Required = false)]
			public InstrumentLeg? InstrumentLeg {get; set;}
			
			[TagDetails(Tag = 687, Type = TagType.Float, Offset = 1, Required = false)]
			public double? LegQty {get; set;}
			
			[TagDetails(Tag = 690, Type = TagType.Int, Offset = 2, Required = false)]
			public int? LegSwapType {get; set;}
			
			[Component(Offset = 3, Required = false)]
			public LegStipulations? LegStipulations {get; set;}
			
			[TagDetails(Tag = 564, Type = TagType.String, Offset = 4, Required = false)]
			public string? LegPositionEffect {get; set;}
			
			[TagDetails(Tag = 565, Type = TagType.Int, Offset = 5, Required = false)]
			public int? LegCoveredOrUncovered {get; set;}
			
			[Component(Offset = 6, Required = false)]
			public NestedParties? NestedParties {get; set;}
			
			[TagDetails(Tag = 654, Type = TagType.String, Offset = 7, Required = false)]
			public string? LegRefID {get; set;}
			
			[TagDetails(Tag = 566, Type = TagType.Float, Offset = 8, Required = false)]
			public double? LegPrice {get; set;}
			
			[TagDetails(Tag = 587, Type = TagType.String, Offset = 9, Required = false)]
			public string? LegSettlType {get; set;}
			
			[TagDetails(Tag = 588, Type = TagType.LocalDate, Offset = 10, Required = false)]
			public DateOnly? LegSettlDate {get; set;}
			
			[TagDetails(Tag = 637, Type = TagType.Float, Offset = 11, Required = false)]
			public double? LegLastPx {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (InstrumentLeg is not null) ((IFixEncoder)InstrumentLeg).Encode(writer);
				if (LegQty is not null) writer.WriteNumber(687, LegQty.Value);
				if (LegSwapType is not null) writer.WriteWholeNumber(690, LegSwapType.Value);
				if (LegStipulations is not null) ((IFixEncoder)LegStipulations).Encode(writer);
				if (LegPositionEffect is not null) writer.WriteString(564, LegPositionEffect);
				if (LegCoveredOrUncovered is not null) writer.WriteWholeNumber(565, LegCoveredOrUncovered.Value);
				if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
				if (LegRefID is not null) writer.WriteString(654, LegRefID);
				if (LegPrice is not null) writer.WriteNumber(566, LegPrice.Value);
				if (LegSettlType is not null) writer.WriteString(587, LegSettlType);
				if (LegSettlDate is not null) writer.WriteLocalDateOnly(588, LegSettlDate.Value);
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
				LegQty = view.GetDouble(687);
				LegSwapType = view.GetInt32(690);
				if (view.GetView("LegStipulations") is IMessageView viewLegStipulations)
				{
					LegStipulations = new();
					((IFixParser)LegStipulations).Parse(viewLegStipulations);
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
				LegSettlType = view.GetString(587);
				LegSettlDate = view.GetDateOnly(588);
				LegLastPx = view.GetDouble(637);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "InstrumentLeg":
					{
						value = InstrumentLeg;
						break;
					}
					case "LegQty":
					{
						value = LegQty;
						break;
					}
					case "LegSwapType":
					{
						value = LegSwapType;
						break;
					}
					case "LegStipulations":
					{
						value = LegStipulations;
						break;
					}
					case "LegPositionEffect":
					{
						value = LegPositionEffect;
						break;
					}
					case "LegCoveredOrUncovered":
					{
						value = LegCoveredOrUncovered;
						break;
					}
					case "NestedParties":
					{
						value = NestedParties;
						break;
					}
					case "LegRefID":
					{
						value = LegRefID;
						break;
					}
					case "LegPrice":
					{
						value = LegPrice;
						break;
					}
					case "LegSettlType":
					{
						value = LegSettlType;
						break;
					}
					case "LegSettlDate":
					{
						value = LegSettlDate;
						break;
					}
					case "LegLastPx":
					{
						value = LegLastPx;
						break;
					}
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				((IFixReset?)InstrumentLeg)?.Reset();
				LegQty = null;
				LegSwapType = null;
				((IFixReset?)LegStipulations)?.Reset();
				LegPositionEffect = null;
				LegCoveredOrUncovered = null;
				((IFixReset?)NestedParties)?.Reset();
				LegRefID = null;
				LegPrice = null;
				LegSettlType = null;
				LegSettlDate = null;
				LegLastPx = null;
			}
		}
		[Group(NoOfTag = 555, Offset = 0, Required = false)]
		public NoLegs[]? Legs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Legs is not null && Legs.Length != 0)
			{
				writer.WriteWholeNumber(555, Legs.Length);
				for (int i = 0; i < Legs.Length; i++)
				{
					((IFixEncoder)Legs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegs") is IMessageView viewNoLegs)
			{
				var count = viewNoLegs.GroupCount();
				Legs = new NoLegs[count];
				for (int i = 0; i < count; i++)
				{
					Legs[i] = new();
					((IFixParser)Legs[i]).Parse(viewNoLegs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegs":
				{
					value = Legs;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			Legs = null;
		}
	}
}
