using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoTradePriceConditions : IFixGroup
	{
		[TagDetails(Tag = 1839, Type = TagType.Int, Offset = 0, Required = false)]
		public int? TradePriceCondition {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TradePriceCondition is not null) writer.WriteWholeNumber(1839, TradePriceCondition.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			TradePriceCondition = view.GetInt32(1839);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "TradePriceCondition":
					value = TradePriceCondition;
					break;
				default: return false;
			}
			return true;
		}
	}
}
