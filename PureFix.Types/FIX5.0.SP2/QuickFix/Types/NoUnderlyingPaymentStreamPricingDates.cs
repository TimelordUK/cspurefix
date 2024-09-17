using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingPaymentStreamPricingDates : IFixGroup
	{
		[TagDetails(Tag = 41942, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? UnderlyingPaymentStreamPricingDate {get; set;}
		
		[TagDetails(Tag = 41943, Type = TagType.Int, Offset = 1, Required = false)]
		public int? UnderlyingPaymentStreamPricingDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingPaymentStreamPricingDate is not null) writer.WriteLocalDateOnly(41942, UnderlyingPaymentStreamPricingDate.Value);
			if (UnderlyingPaymentStreamPricingDateType is not null) writer.WriteWholeNumber(41943, UnderlyingPaymentStreamPricingDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingPaymentStreamPricingDate = view.GetDateOnly(41942);
			UnderlyingPaymentStreamPricingDateType = view.GetInt32(41943);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingPaymentStreamPricingDate":
					value = UnderlyingPaymentStreamPricingDate;
					break;
				case "UnderlyingPaymentStreamPricingDateType":
					value = UnderlyingPaymentStreamPricingDateType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
