using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamPricingBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41192, Offset = 0, Required = false)]
		public NoPaymentStreamPricingBusinessCenters[]? NoPaymentStreamPricingBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStreamPricingBusinessCenters is not null && NoPaymentStreamPricingBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41192, NoPaymentStreamPricingBusinessCenters.Length);
				for (int i = 0; i < NoPaymentStreamPricingBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPaymentStreamPricingBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStreamPricingBusinessCenters") is IMessageView viewNoPaymentStreamPricingBusinessCenters)
			{
				var count = viewNoPaymentStreamPricingBusinessCenters.GroupCount();
				NoPaymentStreamPricingBusinessCenters = new NoPaymentStreamPricingBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStreamPricingBusinessCenters[i] = new();
					((IFixParser)NoPaymentStreamPricingBusinessCenters[i]).Parse(viewNoPaymentStreamPricingBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStreamPricingBusinessCenters":
					value = NoPaymentStreamPricingBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
