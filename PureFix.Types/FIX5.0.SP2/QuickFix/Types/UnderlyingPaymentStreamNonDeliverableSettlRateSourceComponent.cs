using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamNonDeliverableSettlRateSourceComponent : IFixComponent
	{
		[TagDetails(Tag = 40661, Type = TagType.Int, Offset = 0, Required = false)]
		public int? UnderlyingPaymentStreamNonDeliverableSettlRateSource {get; set;}
		
		[TagDetails(Tag = 40824, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingPaymentStreamNonDeliverableSettlReferencePage {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingPaymentStreamNonDeliverableSettlRateSource is not null) writer.WriteWholeNumber(40661, UnderlyingPaymentStreamNonDeliverableSettlRateSource.Value);
			if (UnderlyingPaymentStreamNonDeliverableSettlReferencePage is not null) writer.WriteString(40824, UnderlyingPaymentStreamNonDeliverableSettlReferencePage);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingPaymentStreamNonDeliverableSettlRateSource = view.GetInt32(40661);
			UnderlyingPaymentStreamNonDeliverableSettlReferencePage = view.GetString(40824);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingPaymentStreamNonDeliverableSettlRateSource":
					value = UnderlyingPaymentStreamNonDeliverableSettlRateSource;
					break;
				case "UnderlyingPaymentStreamNonDeliverableSettlReferencePage":
					value = UnderlyingPaymentStreamNonDeliverableSettlReferencePage;
					break;
				default: return false;
			}
			return true;
		}
	}
}
