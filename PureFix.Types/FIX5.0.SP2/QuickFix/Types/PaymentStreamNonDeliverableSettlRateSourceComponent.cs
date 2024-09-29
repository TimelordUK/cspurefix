using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamNonDeliverableSettlRateSourceComponent : IFixComponent
	{
		[TagDetails(Tag = 40371, Type = TagType.Int, Offset = 0, Required = false)]
		public int? PaymentStreamNonDeliverableSettlRateSource {get; set;}
		
		[TagDetails(Tag = 40372, Type = TagType.String, Offset = 1, Required = false)]
		public string? PaymentStreamNonDeliverableSettlReferencePage {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PaymentStreamNonDeliverableSettlRateSource is not null) writer.WriteWholeNumber(40371, PaymentStreamNonDeliverableSettlRateSource.Value);
			if (PaymentStreamNonDeliverableSettlReferencePage is not null) writer.WriteString(40372, PaymentStreamNonDeliverableSettlReferencePage);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PaymentStreamNonDeliverableSettlRateSource = view.GetInt32(40371);
			PaymentStreamNonDeliverableSettlReferencePage = view.GetString(40372);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PaymentStreamNonDeliverableSettlRateSource":
					value = PaymentStreamNonDeliverableSettlRateSource;
					break;
				case "PaymentStreamNonDeliverableSettlReferencePage":
					value = PaymentStreamNonDeliverableSettlReferencePage;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			PaymentStreamNonDeliverableSettlRateSource = null;
			PaymentStreamNonDeliverableSettlReferencePage = null;
		}
	}
}
