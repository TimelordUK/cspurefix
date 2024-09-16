using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoPaymentScheduleFixingDateBusinessCenters : IFixGroup
	{
		[TagDetails(Tag = 40854, Type = TagType.String, Offset = 0, Required = false)]
		public string? PaymentScheduleFixingDateBusinessCenter {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PaymentScheduleFixingDateBusinessCenter is not null) writer.WriteString(40854, PaymentScheduleFixingDateBusinessCenter);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PaymentScheduleFixingDateBusinessCenter = view.GetString(40854);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PaymentScheduleFixingDateBusinessCenter":
					value = PaymentScheduleFixingDateBusinessCenter;
					break;
				default: return false;
			}
			return true;
		}
	}
}
