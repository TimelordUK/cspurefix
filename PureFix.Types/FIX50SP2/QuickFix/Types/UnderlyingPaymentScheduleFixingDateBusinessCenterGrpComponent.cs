using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingPaymentScheduleFixingDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40966, Offset = 0, Required = false)]
		public IOINoUnderlyingPaymentScheduleFixingDateBusinessCenters[]? NoUnderlyingPaymentScheduleFixingDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingPaymentScheduleFixingDateBusinessCenters is not null && NoUnderlyingPaymentScheduleFixingDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40966, NoUnderlyingPaymentScheduleFixingDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingPaymentScheduleFixingDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingPaymentScheduleFixingDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingPaymentScheduleFixingDateBusinessCenters") is IMessageView viewNoUnderlyingPaymentScheduleFixingDateBusinessCenters)
			{
				var count = viewNoUnderlyingPaymentScheduleFixingDateBusinessCenters.GroupCount();
				NoUnderlyingPaymentScheduleFixingDateBusinessCenters = new IOINoUnderlyingPaymentScheduleFixingDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingPaymentScheduleFixingDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingPaymentScheduleFixingDateBusinessCenters[i]).Parse(viewNoUnderlyingPaymentScheduleFixingDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingPaymentScheduleFixingDateBusinessCenters":
					value = NoUnderlyingPaymentScheduleFixingDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingPaymentScheduleFixingDateBusinessCenters = null;
		}
	}
}
