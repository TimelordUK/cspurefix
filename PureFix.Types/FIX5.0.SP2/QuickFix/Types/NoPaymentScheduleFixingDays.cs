using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoPaymentScheduleFixingDays : IFixGroup
	{
		[TagDetails(Tag = 41162, Type = TagType.Int, Offset = 0, Required = false)]
		public int? PaymentScheduleFixingDayOfWeek {get; set;}
		
		[TagDetails(Tag = 41163, Type = TagType.Int, Offset = 1, Required = false)]
		public int? PaymentScheduleFixingDayNumber {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PaymentScheduleFixingDayOfWeek is not null) writer.WriteWholeNumber(41162, PaymentScheduleFixingDayOfWeek.Value);
			if (PaymentScheduleFixingDayNumber is not null) writer.WriteWholeNumber(41163, PaymentScheduleFixingDayNumber.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PaymentScheduleFixingDayOfWeek = view.GetInt32(41162);
			PaymentScheduleFixingDayNumber = view.GetInt32(41163);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PaymentScheduleFixingDayOfWeek":
					value = PaymentScheduleFixingDayOfWeek;
					break;
				case "PaymentScheduleFixingDayNumber":
					value = PaymentScheduleFixingDayNumber;
					break;
				default: return false;
			}
			return true;
		}
	}
}
