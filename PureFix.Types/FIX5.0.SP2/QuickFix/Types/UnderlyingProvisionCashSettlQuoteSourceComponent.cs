using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProvisionCashSettlQuoteSourceComponent : IFixComponent
	{
		[TagDetails(Tag = 42102, Type = TagType.Int, Offset = 0, Required = false)]
		public int? UnderlyingProvisionCashSettlQuoteSource {get; set;}
		
		[TagDetails(Tag = 42103, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingProvisionCashSettlQuoteReferencePage {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingProvisionCashSettlQuoteSource is not null) writer.WriteWholeNumber(42102, UnderlyingProvisionCashSettlQuoteSource.Value);
			if (UnderlyingProvisionCashSettlQuoteReferencePage is not null) writer.WriteString(42103, UnderlyingProvisionCashSettlQuoteReferencePage);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingProvisionCashSettlQuoteSource = view.GetInt32(42102);
			UnderlyingProvisionCashSettlQuoteReferencePage = view.GetString(42103);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingProvisionCashSettlQuoteSource":
					value = UnderlyingProvisionCashSettlQuoteSource;
					break;
				case "UnderlyingProvisionCashSettlQuoteReferencePage":
					value = UnderlyingProvisionCashSettlQuoteReferencePage;
					break;
				default: return false;
			}
			return true;
		}
	}
}
