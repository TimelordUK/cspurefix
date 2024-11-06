using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class DeliveryScheduleSettlDayGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41051, Offset = 0, Required = false)]
		public IOINoDeliveryScheduleSettlDays[]? NoDeliveryScheduleSettlDays {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDeliveryScheduleSettlDays is not null && NoDeliveryScheduleSettlDays.Length != 0)
			{
				writer.WriteWholeNumber(41051, NoDeliveryScheduleSettlDays.Length);
				for (int i = 0; i < NoDeliveryScheduleSettlDays.Length; i++)
				{
					((IFixEncoder)NoDeliveryScheduleSettlDays[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDeliveryScheduleSettlDays") is IMessageView viewNoDeliveryScheduleSettlDays)
			{
				var count = viewNoDeliveryScheduleSettlDays.GroupCount();
				NoDeliveryScheduleSettlDays = new IOINoDeliveryScheduleSettlDays[count];
				for (int i = 0; i < count; i++)
				{
					NoDeliveryScheduleSettlDays[i] = new();
					((IFixParser)NoDeliveryScheduleSettlDays[i]).Parse(viewNoDeliveryScheduleSettlDays.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDeliveryScheduleSettlDays":
					value = NoDeliveryScheduleSettlDays;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoDeliveryScheduleSettlDays = null;
		}
	}
}
