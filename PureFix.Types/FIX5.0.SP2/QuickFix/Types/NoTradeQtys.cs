using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoTradeQtys : IFixGroup
	{
		[TagDetails(Tag = 1842, Type = TagType.Int, Offset = 0, Required = false)]
		public int? TradeQtyType {get; set;}
		
		[TagDetails(Tag = 1843, Type = TagType.Float, Offset = 1, Required = false)]
		public double? TradeQty {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TradeQtyType is not null) writer.WriteWholeNumber(1842, TradeQtyType.Value);
			if (TradeQty is not null) writer.WriteNumber(1843, TradeQty.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			TradeQtyType = view.GetInt32(1842);
			TradeQty = view.GetDouble(1843);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "TradeQtyType":
					value = TradeQtyType;
					break;
				case "TradeQty":
					value = TradeQty;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			TradeQtyType = null;
			TradeQty = null;
		}
	}
}
