using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProvisionCashSettlQuoteSourceComponent : IFixComponent
	{
		[TagDetails(Tag = 40470, Type = TagType.Int, Offset = 0, Required = false)]
		public int? LegProvisionCashSettlQuoteSource {get; set;}
		
		[TagDetails(Tag = 41407, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegProvisionCashSettlQuoteReferencePage {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegProvisionCashSettlQuoteSource is not null) writer.WriteWholeNumber(40470, LegProvisionCashSettlQuoteSource.Value);
			if (LegProvisionCashSettlQuoteReferencePage is not null) writer.WriteString(41407, LegProvisionCashSettlQuoteReferencePage);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegProvisionCashSettlQuoteSource = view.GetInt32(40470);
			LegProvisionCashSettlQuoteReferencePage = view.GetString(41407);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegProvisionCashSettlQuoteSource":
					value = LegProvisionCashSettlQuoteSource;
					break;
				case "LegProvisionCashSettlQuoteReferencePage":
					value = LegProvisionCashSettlQuoteReferencePage;
					break;
				default: return false;
			}
			return true;
		}
	}
}
