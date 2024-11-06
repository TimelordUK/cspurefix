using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegPaymentStreamCompoundingDates : IFixGroup
	{
		[TagDetails(Tag = 42406, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? LegPaymentStreamCompoundingDate {get; set;}
		
		[TagDetails(Tag = 42407, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegPaymentStreamCompoundingDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentStreamCompoundingDate is not null) writer.WriteLocalDateOnly(42406, LegPaymentStreamCompoundingDate.Value);
			if (LegPaymentStreamCompoundingDateType is not null) writer.WriteWholeNumber(42407, LegPaymentStreamCompoundingDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentStreamCompoundingDate = view.GetDateOnly(42406);
			LegPaymentStreamCompoundingDateType = view.GetInt32(42407);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentStreamCompoundingDate":
					value = LegPaymentStreamCompoundingDate;
					break;
				case "LegPaymentStreamCompoundingDateType":
					value = LegPaymentStreamCompoundingDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegPaymentStreamCompoundingDate = null;
			LegPaymentStreamCompoundingDateType = null;
		}
	}
}
