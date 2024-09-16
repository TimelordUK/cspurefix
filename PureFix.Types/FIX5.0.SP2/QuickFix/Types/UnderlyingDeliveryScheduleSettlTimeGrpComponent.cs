using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingDeliveryScheduleSettlTimeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41773, Offset = 0, Required = false)]
		public NoUnderlyingDeliveryScheduleSettlTimes[]? NoUnderlyingDeliveryScheduleSettlTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingDeliveryScheduleSettlTimes is not null && NoUnderlyingDeliveryScheduleSettlTimes.Length != 0)
			{
				writer.WriteWholeNumber(41773, NoUnderlyingDeliveryScheduleSettlTimes.Length);
				for (int i = 0; i < NoUnderlyingDeliveryScheduleSettlTimes.Length; i++)
				{
					((IFixEncoder)NoUnderlyingDeliveryScheduleSettlTimes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingDeliveryScheduleSettlTimes") is IMessageView viewNoUnderlyingDeliveryScheduleSettlTimes)
			{
				var count = viewNoUnderlyingDeliveryScheduleSettlTimes.GroupCount();
				NoUnderlyingDeliveryScheduleSettlTimes = new NoUnderlyingDeliveryScheduleSettlTimes[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingDeliveryScheduleSettlTimes[i] = new();
					((IFixParser)NoUnderlyingDeliveryScheduleSettlTimes[i]).Parse(viewNoUnderlyingDeliveryScheduleSettlTimes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingDeliveryScheduleSettlTimes":
					value = NoUnderlyingDeliveryScheduleSettlTimes;
					break;
				default: return false;
			}
			return true;
		}
	}
}
