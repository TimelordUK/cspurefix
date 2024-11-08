using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentStreamPricingDayGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41596, Offset = 0, Required = false)]
		public IOINoLegPaymentStreamPricingDays[]? NoLegPaymentStreamPricingDays {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentStreamPricingDays is not null && NoLegPaymentStreamPricingDays.Length != 0)
			{
				writer.WriteWholeNumber(41596, NoLegPaymentStreamPricingDays.Length);
				for (int i = 0; i < NoLegPaymentStreamPricingDays.Length; i++)
				{
					((IFixEncoder)NoLegPaymentStreamPricingDays[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentStreamPricingDays") is IMessageView viewNoLegPaymentStreamPricingDays)
			{
				var count = viewNoLegPaymentStreamPricingDays.GroupCount();
				NoLegPaymentStreamPricingDays = new IOINoLegPaymentStreamPricingDays[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentStreamPricingDays[i] = new();
					((IFixParser)NoLegPaymentStreamPricingDays[i]).Parse(viewNoLegPaymentStreamPricingDays.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentStreamPricingDays":
					value = NoLegPaymentStreamPricingDays;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentStreamPricingDays = null;
		}
	}
}
