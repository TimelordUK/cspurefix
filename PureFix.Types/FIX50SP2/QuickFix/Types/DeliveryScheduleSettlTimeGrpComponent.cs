using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class DeliveryScheduleSettlTimeGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41054, Offset = 0, Required = false)]
		public IOINoDeliveryScheduleSettlTimes[]? NoDeliveryScheduleSettlTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDeliveryScheduleSettlTimes is not null && NoDeliveryScheduleSettlTimes.Length != 0)
			{
				writer.WriteWholeNumber(41054, NoDeliveryScheduleSettlTimes.Length);
				for (int i = 0; i < NoDeliveryScheduleSettlTimes.Length; i++)
				{
					((IFixEncoder)NoDeliveryScheduleSettlTimes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDeliveryScheduleSettlTimes") is IMessageView viewNoDeliveryScheduleSettlTimes)
			{
				var count = viewNoDeliveryScheduleSettlTimes.GroupCount();
				NoDeliveryScheduleSettlTimes = new IOINoDeliveryScheduleSettlTimes[count];
				for (int i = 0; i < count; i++)
				{
					NoDeliveryScheduleSettlTimes[i] = new();
					((IFixParser)NoDeliveryScheduleSettlTimes[i]).Parse(viewNoDeliveryScheduleSettlTimes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDeliveryScheduleSettlTimes":
					value = NoDeliveryScheduleSettlTimes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoDeliveryScheduleSettlTimes = null;
		}
	}
}
