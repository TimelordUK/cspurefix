using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoDeliveryScheduleSettlDays : IFixGroup
	{
		[TagDetails(Tag = 41052, Type = TagType.Int, Offset = 0, Required = false)]
		public int? DeliveryScheduleSettlDay {get; set;}
		
		[TagDetails(Tag = 41053, Type = TagType.Int, Offset = 1, Required = false)]
		public int? DeliveryScheduleSettlTotalHours {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public DeliveryScheduleSettlTimeGrpComponent? DeliveryScheduleSettlTimeGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (DeliveryScheduleSettlDay is not null) writer.WriteWholeNumber(41052, DeliveryScheduleSettlDay.Value);
			if (DeliveryScheduleSettlTotalHours is not null) writer.WriteWholeNumber(41053, DeliveryScheduleSettlTotalHours.Value);
			if (DeliveryScheduleSettlTimeGrp is not null) ((IFixEncoder)DeliveryScheduleSettlTimeGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			DeliveryScheduleSettlDay = view.GetInt32(41052);
			DeliveryScheduleSettlTotalHours = view.GetInt32(41053);
			if (view.GetView("DeliveryScheduleSettlTimeGrp") is IMessageView viewDeliveryScheduleSettlTimeGrp)
			{
				DeliveryScheduleSettlTimeGrp = new();
				((IFixParser)DeliveryScheduleSettlTimeGrp).Parse(viewDeliveryScheduleSettlTimeGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "DeliveryScheduleSettlDay":
					value = DeliveryScheduleSettlDay;
					break;
				case "DeliveryScheduleSettlTotalHours":
					value = DeliveryScheduleSettlTotalHours;
					break;
				case "DeliveryScheduleSettlTimeGrp":
					value = DeliveryScheduleSettlTimeGrp;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			DeliveryScheduleSettlDay = null;
			DeliveryScheduleSettlTotalHours = null;
			((IFixReset?)DeliveryScheduleSettlTimeGrp)?.Reset();
		}
	}
}
