using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingDeliveryScheduleSettlDayGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41770, Offset = 0, Required = false)]
		public NoUnderlyingDeliveryScheduleSettlDays[]? NoUnderlyingDeliveryScheduleSettlDays {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingDeliveryScheduleSettlDays is not null && NoUnderlyingDeliveryScheduleSettlDays.Length != 0)
			{
				writer.WriteWholeNumber(41770, NoUnderlyingDeliveryScheduleSettlDays.Length);
				for (int i = 0; i < NoUnderlyingDeliveryScheduleSettlDays.Length; i++)
				{
					((IFixEncoder)NoUnderlyingDeliveryScheduleSettlDays[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingDeliveryScheduleSettlDays") is IMessageView viewNoUnderlyingDeliveryScheduleSettlDays)
			{
				var count = viewNoUnderlyingDeliveryScheduleSettlDays.GroupCount();
				NoUnderlyingDeliveryScheduleSettlDays = new NoUnderlyingDeliveryScheduleSettlDays[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingDeliveryScheduleSettlDays[i] = new();
					((IFixParser)NoUnderlyingDeliveryScheduleSettlDays[i]).Parse(viewNoUnderlyingDeliveryScheduleSettlDays.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingDeliveryScheduleSettlDays":
					value = NoUnderlyingDeliveryScheduleSettlDays;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingDeliveryScheduleSettlDays = null;
		}
	}
}
