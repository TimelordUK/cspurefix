using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegDeliveryScheduleSettlTimeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41425, Offset = 0, Required = false)]
		public NoLegDeliveryScheduleSettlTimes[]? NoLegDeliveryScheduleSettlTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegDeliveryScheduleSettlTimes is not null && NoLegDeliveryScheduleSettlTimes.Length != 0)
			{
				writer.WriteWholeNumber(41425, NoLegDeliveryScheduleSettlTimes.Length);
				for (int i = 0; i < NoLegDeliveryScheduleSettlTimes.Length; i++)
				{
					((IFixEncoder)NoLegDeliveryScheduleSettlTimes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegDeliveryScheduleSettlTimes") is IMessageView viewNoLegDeliveryScheduleSettlTimes)
			{
				var count = viewNoLegDeliveryScheduleSettlTimes.GroupCount();
				NoLegDeliveryScheduleSettlTimes = new NoLegDeliveryScheduleSettlTimes[count];
				for (int i = 0; i < count; i++)
				{
					NoLegDeliveryScheduleSettlTimes[i] = new();
					((IFixParser)NoLegDeliveryScheduleSettlTimes[i]).Parse(viewNoLegDeliveryScheduleSettlTimes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegDeliveryScheduleSettlTimes":
					value = NoLegDeliveryScheduleSettlTimes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegDeliveryScheduleSettlTimes = null;
		}
	}
}
