using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamPricingDateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41941, Offset = 0, Required = false)]
		public NoUnderlyingPaymentStreamPricingDates[]? NoUnderlyingPaymentStreamPricingDates {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStreamPricingDates is not null && NoUnderlyingPaymentStreamPricingDates.Length != 0)
			{
				writer.WriteWholeNumber(41941, NoUnderlyingPaymentStreamPricingDates.Length);
				for (int i = 0; i < NoUnderlyingPaymentStreamPricingDates.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStreamPricingDates[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStreamPricingDates") is IMessageView viewNoUnderlyingPaymentStreamPricingDates)
			{
				var count = viewNoUnderlyingPaymentStreamPricingDates.GroupCount();
				NoUnderlyingPaymentStreamPricingDates = new NoUnderlyingPaymentStreamPricingDates[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStreamPricingDates[i] = new();
					((IFixParser)NoUnderlyingPaymentStreamPricingDates[i]).Parse(viewNoUnderlyingPaymentStreamPricingDates.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStreamPricingDates":
					value = NoUnderlyingPaymentStreamPricingDates;
					break;
				default: return false;
			}
			return true;
		}
	}
}
