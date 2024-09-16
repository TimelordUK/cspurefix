using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PaymentScheduleFixingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40977, Offset = 0, Required = false)]
		public NoPaymentScheduleFixingDateBusinessCenters[]? NoPaymentScheduleFixingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPaymentScheduleFixingDateBusinessCenters is not null && NoPaymentScheduleFixingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40977, NoPaymentScheduleFixingDateBusinessCenters.Length);
				for (int i = 0; i < NoPaymentScheduleFixingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoPaymentScheduleFixingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPaymentScheduleFixingDateBusinessCenters") is IMessageView viewNoPaymentScheduleFixingDateBusinessCenters)
			{
				var count = viewNoPaymentScheduleFixingDateBusinessCenters.GroupCount();
				NoPaymentScheduleFixingDateBusinessCenters = new NoPaymentScheduleFixingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoPaymentScheduleFixingDateBusinessCenters[i] = new();
					((IFixParser)NoPaymentScheduleFixingDateBusinessCenters[i]).Parse(viewNoPaymentScheduleFixingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPaymentScheduleFixingDateBusinessCenters":
					value = NoPaymentScheduleFixingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
