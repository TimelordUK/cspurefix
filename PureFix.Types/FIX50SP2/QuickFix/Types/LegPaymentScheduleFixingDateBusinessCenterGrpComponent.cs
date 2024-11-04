using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPaymentScheduleFixingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40927, Offset = 0, Required = false)]
		public NoLegPaymentScheduleFixingDateBusinessCenters[]? NoLegPaymentScheduleFixingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPaymentScheduleFixingDateBusinessCenters is not null && NoLegPaymentScheduleFixingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40927, NoLegPaymentScheduleFixingDateBusinessCenters.Length);
				for (int i = 0; i < NoLegPaymentScheduleFixingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegPaymentScheduleFixingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPaymentScheduleFixingDateBusinessCenters") is IMessageView viewNoLegPaymentScheduleFixingDateBusinessCenters)
			{
				var count = viewNoLegPaymentScheduleFixingDateBusinessCenters.GroupCount();
				NoLegPaymentScheduleFixingDateBusinessCenters = new NoLegPaymentScheduleFixingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPaymentScheduleFixingDateBusinessCenters[i] = new();
					((IFixParser)NoLegPaymentScheduleFixingDateBusinessCenters[i]).Parse(viewNoLegPaymentScheduleFixingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPaymentScheduleFixingDateBusinessCenters":
					value = NoLegPaymentScheduleFixingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPaymentScheduleFixingDateBusinessCenters = null;
		}
	}
}
