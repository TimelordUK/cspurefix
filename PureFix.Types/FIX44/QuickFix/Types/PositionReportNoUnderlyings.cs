using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PositionReportNoUnderlyings : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public UnderlyingInstrumentComponent? UnderlyingInstrument {get; set;}
		
		[TagDetails(Tag = 732, Type = TagType.Float, Offset = 1, Required = true)]
		public double? UnderlyingSettlPrice {get; set;}
		
		[TagDetails(Tag = 733, Type = TagType.Int, Offset = 2, Required = true)]
		public int? UnderlyingSettlPriceType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				UnderlyingSettlPrice is not null
				&& UnderlyingSettlPriceType is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingInstrument is not null) ((IFixEncoder)UnderlyingInstrument).Encode(writer);
			if (UnderlyingSettlPrice is not null) writer.WriteNumber(732, UnderlyingSettlPrice.Value);
			if (UnderlyingSettlPriceType is not null) writer.WriteWholeNumber(733, UnderlyingSettlPriceType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("UnderlyingInstrument") is IMessageView viewUnderlyingInstrument)
			{
				UnderlyingInstrument = new();
				((IFixParser)UnderlyingInstrument).Parse(viewUnderlyingInstrument);
			}
			UnderlyingSettlPrice = view.GetDouble(732);
			UnderlyingSettlPriceType = view.GetInt32(733);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingInstrument":
					value = UnderlyingInstrument;
					break;
				case "UnderlyingSettlPrice":
					value = UnderlyingSettlPrice;
					break;
				case "UnderlyingSettlPriceType":
					value = UnderlyingSettlPriceType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)UnderlyingInstrument)?.Reset();
			UnderlyingSettlPrice = null;
			UnderlyingSettlPriceType = null;
		}
	}
}
