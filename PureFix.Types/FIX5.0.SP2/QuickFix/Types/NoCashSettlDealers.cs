using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoCashSettlDealers : IFixGroup
	{
		[TagDetails(Tag = 40032, Type = TagType.String, Offset = 0, Required = false)]
		public string? CashSettlDealer {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (CashSettlDealer is not null) writer.WriteString(40032, CashSettlDealer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			CashSettlDealer = view.GetString(40032);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "CashSettlDealer":
					value = CashSettlDealer;
					break;
				default: return false;
			}
			return true;
		}
	}
}
