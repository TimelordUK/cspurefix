using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentStreamPricingDayGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41227, Offset = 0, Required = false)]
		public NoPaymentStreamPricingDays[]? NoPaymentStreamPricingDays {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentStreamPricingDays is not null && NoPaymentStreamPricingDays.Length != 0)
			{
				writer.WriteWholeNumber(41227, NoPaymentStreamPricingDays.Length);
				for (int i = 0; i < NoPaymentStreamPricingDays.Length; i++)
				{
					((IFixEncoder)NoPaymentStreamPricingDays[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentStreamPricingDays") is IMessageView viewNoPaymentStreamPricingDays)
			{
				var count = viewNoPaymentStreamPricingDays.GroupCount();
				NoPaymentStreamPricingDays = new NoPaymentStreamPricingDays[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentStreamPricingDays[i] = new();
					((IFixParser)NoPaymentStreamPricingDays[i]).Parse(viewNoPaymentStreamPricingDays.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentStreamPricingDays":
					value = NoPaymentStreamPricingDays;
					break;
				default: return false;
			}
			return true;
		}
	}
}
