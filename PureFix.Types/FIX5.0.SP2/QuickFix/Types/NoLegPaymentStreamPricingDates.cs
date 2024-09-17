using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegPaymentStreamPricingDates : IFixGroup
	{
		[TagDetails(Tag = 41594, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? LegPaymentStreamPricingDate {get; set;}
		
		[TagDetails(Tag = 41595, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegPaymentStreamPricingDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentStreamPricingDate is not null) writer.WriteLocalDateOnly(41594, LegPaymentStreamPricingDate.Value);
			if (LegPaymentStreamPricingDateType is not null) writer.WriteWholeNumber(41595, LegPaymentStreamPricingDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentStreamPricingDate = view.GetDateOnly(41594);
			LegPaymentStreamPricingDateType = view.GetInt32(41595);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentStreamPricingDate":
					value = LegPaymentStreamPricingDate;
					break;
				case "LegPaymentStreamPricingDateType":
					value = LegPaymentStreamPricingDateType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
