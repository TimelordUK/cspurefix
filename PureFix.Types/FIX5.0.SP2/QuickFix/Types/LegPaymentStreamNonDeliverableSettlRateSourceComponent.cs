using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamNonDeliverableSettlRateSourceComponent : IFixComponent
	{
		[TagDetails(Tag = 40087, Type = TagType.Int, Offset = 0, Required = false)]
		public int? LegPaymentStreamNonDeliverableSettlRateSource {get; set;}
		
		[TagDetails(Tag = 40228, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegPaymentStreamNonDeliverableSettlReferencePage {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentStreamNonDeliverableSettlRateSource is not null) writer.WriteWholeNumber(40087, LegPaymentStreamNonDeliverableSettlRateSource.Value);
			if (LegPaymentStreamNonDeliverableSettlReferencePage is not null) writer.WriteString(40228, LegPaymentStreamNonDeliverableSettlReferencePage);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentStreamNonDeliverableSettlRateSource = view.GetInt32(40087);
			LegPaymentStreamNonDeliverableSettlReferencePage = view.GetString(40228);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentStreamNonDeliverableSettlRateSource":
					value = LegPaymentStreamNonDeliverableSettlRateSource;
					break;
				case "LegPaymentStreamNonDeliverableSettlReferencePage":
					value = LegPaymentStreamNonDeliverableSettlReferencePage;
					break;
				default: return false;
			}
			return true;
		}
	}
}
