using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegCashSettlDealers : IFixGroup
	{
		[TagDetails(Tag = 41343, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegCashSettlDealer {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegCashSettlDealer is not null) writer.WriteString(41343, LegCashSettlDealer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegCashSettlDealer = view.GetString(41343);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegCashSettlDealer":
					value = LegCashSettlDealer;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegCashSettlDealer = null;
		}
	}
}
