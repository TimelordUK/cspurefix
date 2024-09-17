using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegPaymentStreamFixingDates : IFixGroup
	{
		[TagDetails(Tag = 42460, Type = TagType.LocalDate, Offset = 0, Required = false)]
		public DateOnly? LegPaymentStreamFixingDate {get; set;}
		
		[TagDetails(Tag = 42461, Type = TagType.Int, Offset = 1, Required = false)]
		public int? LegPaymentStreamFixingDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegPaymentStreamFixingDate is not null) writer.WriteLocalDateOnly(42460, LegPaymentStreamFixingDate.Value);
			if (LegPaymentStreamFixingDateType is not null) writer.WriteWholeNumber(42461, LegPaymentStreamFixingDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegPaymentStreamFixingDate = view.GetDateOnly(42460);
			LegPaymentStreamFixingDateType = view.GetInt32(42461);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegPaymentStreamFixingDate":
					value = LegPaymentStreamFixingDate;
					break;
				case "LegPaymentStreamFixingDateType":
					value = LegPaymentStreamFixingDateType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
