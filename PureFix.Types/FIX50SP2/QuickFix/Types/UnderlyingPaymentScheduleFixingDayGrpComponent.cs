using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentScheduleFixingDayGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41878, Offset = 0, Required = false)]
		public IOINoUnderlyingPaymentScheduleFixingDays[]? NoUnderlyingPaymentScheduleFixingDays {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentScheduleFixingDays is not null && NoUnderlyingPaymentScheduleFixingDays.Length != 0)
			{
				writer.WriteWholeNumber(41878, NoUnderlyingPaymentScheduleFixingDays.Length);
				for (int i = 0; i < NoUnderlyingPaymentScheduleFixingDays.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentScheduleFixingDays[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentScheduleFixingDays") is IMessageView viewNoUnderlyingPaymentScheduleFixingDays)
			{
				var count = viewNoUnderlyingPaymentScheduleFixingDays.GroupCount();
				NoUnderlyingPaymentScheduleFixingDays = new IOINoUnderlyingPaymentScheduleFixingDays[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentScheduleFixingDays[i] = new();
					((IFixParser)NoUnderlyingPaymentScheduleFixingDays[i]).Parse(viewNoUnderlyingPaymentScheduleFixingDays.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentScheduleFixingDays":
					value = NoUnderlyingPaymentScheduleFixingDays;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentScheduleFixingDays = null;
		}
	}
}
