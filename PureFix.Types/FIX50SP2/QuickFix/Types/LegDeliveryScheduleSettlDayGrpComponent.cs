using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegDeliveryScheduleSettlDayGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41422, Offset = 0, Required = false)]
		public IOINoLegDeliveryScheduleSettlDays[]? NoLegDeliveryScheduleSettlDays {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegDeliveryScheduleSettlDays is not null && NoLegDeliveryScheduleSettlDays.Length != 0)
			{
				writer.WriteWholeNumber(41422, NoLegDeliveryScheduleSettlDays.Length);
				for (int i = 0; i < NoLegDeliveryScheduleSettlDays.Length; i++)
				{
					((IFixEncoder)NoLegDeliveryScheduleSettlDays[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegDeliveryScheduleSettlDays") is IMessageView viewNoLegDeliveryScheduleSettlDays)
			{
				var count = viewNoLegDeliveryScheduleSettlDays.GroupCount();
				NoLegDeliveryScheduleSettlDays = new IOINoLegDeliveryScheduleSettlDays[count];
				for (int i = 0; i < count; i++)
				{
					NoLegDeliveryScheduleSettlDays[i] = new();
					((IFixParser)NoLegDeliveryScheduleSettlDays[i]).Parse(viewNoLegDeliveryScheduleSettlDays.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegDeliveryScheduleSettlDays":
					value = NoLegDeliveryScheduleSettlDays;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegDeliveryScheduleSettlDays = null;
		}
	}
}
