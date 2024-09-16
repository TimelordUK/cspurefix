using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoPaymentStreamPricingDates : IFixGroup
	{
		[TagDetails(Tag = 41225, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? PaymentStreamPricingDate {get; set;}
		
		[TagDetails(Tag = 41226, Type = TagType.Int, Offset = 1, Required = false)]
		public int? PaymentStreamPricingDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PaymentStreamPricingDate is not null) writer.WriteLocalDateOnly(41225, PaymentStreamPricingDate.Value);
			if (PaymentStreamPricingDateType is not null) writer.WriteWholeNumber(41226, PaymentStreamPricingDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PaymentStreamPricingDate = view.GetDateOnly(41225);
			PaymentStreamPricingDateType = view.GetInt32(41226);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PaymentStreamPricingDate":
					value = PaymentStreamPricingDate;
					break;
				case "PaymentStreamPricingDateType":
					value = PaymentStreamPricingDateType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
