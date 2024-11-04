using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingDividendPayments : IFixGroup
	{
		[TagDetails(Tag = 42856, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? UnderlyingDividendPaymentDate {get; set;}
		
		[TagDetails(Tag = 42857, Type = TagType.Float, Offset = 1, Required = false)]
		public double? UnderlyingDividendPaymentAmount {get; set;}
		
		[TagDetails(Tag = 42858, Type = TagType.String, Offset = 2, Required = false)]
		public string? UnderlyingDividendPaymentCurrency {get; set;}
		
		[TagDetails(Tag = 42859, Type = TagType.Float, Offset = 3, Required = false)]
		public double? UnderlyingDividendAccruedInterest {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingDividendPaymentDate is not null) writer.WriteLocalDateOnly(42856, UnderlyingDividendPaymentDate.Value);
			if (UnderlyingDividendPaymentAmount is not null) writer.WriteNumber(42857, UnderlyingDividendPaymentAmount.Value);
			if (UnderlyingDividendPaymentCurrency is not null) writer.WriteString(42858, UnderlyingDividendPaymentCurrency);
			if (UnderlyingDividendAccruedInterest is not null) writer.WriteNumber(42859, UnderlyingDividendAccruedInterest.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingDividendPaymentDate = view.GetDateOnly(42856);
			UnderlyingDividendPaymentAmount = view.GetDouble(42857);
			UnderlyingDividendPaymentCurrency = view.GetString(42858);
			UnderlyingDividendAccruedInterest = view.GetDouble(42859);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingDividendPaymentDate":
					value = UnderlyingDividendPaymentDate;
					break;
				case "UnderlyingDividendPaymentAmount":
					value = UnderlyingDividendPaymentAmount;
					break;
				case "UnderlyingDividendPaymentCurrency":
					value = UnderlyingDividendPaymentCurrency;
					break;
				case "UnderlyingDividendAccruedInterest":
					value = UnderlyingDividendAccruedInterest;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingDividendPaymentDate = null;
			UnderlyingDividendPaymentAmount = null;
			UnderlyingDividendPaymentCurrency = null;
			UnderlyingDividendAccruedInterest = null;
		}
	}
}
