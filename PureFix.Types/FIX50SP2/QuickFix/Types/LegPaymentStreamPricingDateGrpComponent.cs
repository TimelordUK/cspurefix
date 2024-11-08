using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamPricingDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41593, Offset = 0, Required = false)]
		public IOINoLegPaymentStreamPricingDates[]? NoLegPaymentStreamPricingDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStreamPricingDates is not null && NoLegPaymentStreamPricingDates.Length != 0)
			{
				writer.WriteWholeNumber(41593, NoLegPaymentStreamPricingDates.Length);
				for (int i = 0; i < NoLegPaymentStreamPricingDates.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStreamPricingDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStreamPricingDates") is IMessageView viewNoLegPaymentStreamPricingDates)
			{
				var count = viewNoLegPaymentStreamPricingDates.GroupCount();
				NoLegPaymentStreamPricingDates = new IOINoLegPaymentStreamPricingDates[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStreamPricingDates[i] = new();
					((IFixParser)NoLegPaymentStreamPricingDates[i]).Parse(viewNoLegPaymentStreamPricingDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStreamPricingDates":
					value = NoLegPaymentStreamPricingDates;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentStreamPricingDates = null;
		}
	}
}
