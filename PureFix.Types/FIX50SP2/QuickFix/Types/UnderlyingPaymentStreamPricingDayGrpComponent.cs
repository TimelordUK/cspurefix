using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamPricingDayGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41944, Offset = 0, Required = false)]
		public IOINoUnderlyingPaymentStreamPricingDays[]? NoUnderlyingPaymentStreamPricingDays {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStreamPricingDays is not null && NoUnderlyingPaymentStreamPricingDays.Length != 0)
			{
				writer.WriteWholeNumber(41944, NoUnderlyingPaymentStreamPricingDays.Length);
				for (int i = 0; i < NoUnderlyingPaymentStreamPricingDays.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStreamPricingDays[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStreamPricingDays") is IMessageView viewNoUnderlyingPaymentStreamPricingDays)
			{
				var count = viewNoUnderlyingPaymentStreamPricingDays.GroupCount();
				NoUnderlyingPaymentStreamPricingDays = new IOINoUnderlyingPaymentStreamPricingDays[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStreamPricingDays[i] = new();
					((IFixParser)NoUnderlyingPaymentStreamPricingDays[i]).Parse(viewNoUnderlyingPaymentStreamPricingDays.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStreamPricingDays":
					value = NoUnderlyingPaymentStreamPricingDays;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentStreamPricingDays = null;
		}
	}
}
