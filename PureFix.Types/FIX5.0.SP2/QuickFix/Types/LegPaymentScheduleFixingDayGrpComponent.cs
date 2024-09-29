using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentScheduleFixingDayGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41530, Offset = 0, Required = false)]
		public NoLegPaymentScheduleFixingDays[]? NoLegPaymentScheduleFixingDays {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentScheduleFixingDays is not null && NoLegPaymentScheduleFixingDays.Length != 0)
			{
				writer.WriteWholeNumber(41530, NoLegPaymentScheduleFixingDays.Length);
				for (int i = 0; i < NoLegPaymentScheduleFixingDays.Length; i++)
				{
					((IFixEncoder)NoLegPaymentScheduleFixingDays[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentScheduleFixingDays") is IMessageView viewNoLegPaymentScheduleFixingDays)
			{
				var count = viewNoLegPaymentScheduleFixingDays.GroupCount();
				NoLegPaymentScheduleFixingDays = new NoLegPaymentScheduleFixingDays[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentScheduleFixingDays[i] = new();
					((IFixParser)NoLegPaymentScheduleFixingDays[i]).Parse(viewNoLegPaymentScheduleFixingDays.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentScheduleFixingDays":
					value = NoLegPaymentScheduleFixingDays;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentScheduleFixingDays = null;
		}
	}
}
