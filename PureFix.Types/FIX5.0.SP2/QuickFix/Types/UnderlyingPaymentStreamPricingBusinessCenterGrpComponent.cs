using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentStreamPricingBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41909, Offset = 0, Required = false)]
		public NoUnderlyingPaymentStreamPricingBusinessCenters[]? NoUnderlyingPaymentStreamPricingBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentStreamPricingBusinessCenters is not null && NoUnderlyingPaymentStreamPricingBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41909, NoUnderlyingPaymentStreamPricingBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingPaymentStreamPricingBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentStreamPricingBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentStreamPricingBusinessCenters") is IMessageView viewNoUnderlyingPaymentStreamPricingBusinessCenters)
			{
				var count = viewNoUnderlyingPaymentStreamPricingBusinessCenters.GroupCount();
				NoUnderlyingPaymentStreamPricingBusinessCenters = new NoUnderlyingPaymentStreamPricingBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentStreamPricingBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingPaymentStreamPricingBusinessCenters[i]).Parse(viewNoUnderlyingPaymentStreamPricingBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentStreamPricingBusinessCenters":
					value = NoUnderlyingPaymentStreamPricingBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
