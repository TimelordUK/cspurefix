using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingPaymentStreamPricingDays : IFixGroup
	{
		[TagDetails(Tag = 41945, Type = TagType.Int, Offset = 0, Required = false)]
		public int? UnderlyingPaymentStreamPricingDayOfWeek {get; set;}
		
		[TagDetails(Tag = 41946, Type = TagType.Int, Offset = 1, Required = false)]
		public int? UnderlyingPaymentStreamPricingDayNumber {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingPaymentStreamPricingDayOfWeek is not null) writer.WriteWholeNumber(41945, UnderlyingPaymentStreamPricingDayOfWeek.Value);
			if (UnderlyingPaymentStreamPricingDayNumber is not null) writer.WriteWholeNumber(41946, UnderlyingPaymentStreamPricingDayNumber.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingPaymentStreamPricingDayOfWeek = view.GetInt32(41945);
			UnderlyingPaymentStreamPricingDayNumber = view.GetInt32(41946);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingPaymentStreamPricingDayOfWeek":
					value = UnderlyingPaymentStreamPricingDayOfWeek;
					break;
				case "UnderlyingPaymentStreamPricingDayNumber":
					value = UnderlyingPaymentStreamPricingDayNumber;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingPaymentStreamPricingDayOfWeek = null;
			UnderlyingPaymentStreamPricingDayNumber = null;
		}
	}
}
