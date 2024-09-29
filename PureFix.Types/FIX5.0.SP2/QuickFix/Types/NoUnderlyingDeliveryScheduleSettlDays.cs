using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingDeliveryScheduleSettlDays : IFixGroup
	{
		[TagDetails(Tag = 41771, Type = TagType.Int, Offset = 0, Required = false)]
		public int? UnderlyingDeliveryScheduleSettlDay {get; set;}
		
		[TagDetails(Tag = 41772, Type = TagType.Int, Offset = 1, Required = false)]
		public int? UnderlyingDeliveryScheduleSettlTotalHours {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public UnderlyingDeliveryScheduleSettlTimeGrpComponent? UnderlyingDeliveryScheduleSettlTimeGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingDeliveryScheduleSettlDay is not null) writer.WriteWholeNumber(41771, UnderlyingDeliveryScheduleSettlDay.Value);
			if (UnderlyingDeliveryScheduleSettlTotalHours is not null) writer.WriteWholeNumber(41772, UnderlyingDeliveryScheduleSettlTotalHours.Value);
			if (UnderlyingDeliveryScheduleSettlTimeGrp is not null) ((IFixEncoder)UnderlyingDeliveryScheduleSettlTimeGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingDeliveryScheduleSettlDay = view.GetInt32(41771);
			UnderlyingDeliveryScheduleSettlTotalHours = view.GetInt32(41772);
			if (view.GetView("UnderlyingDeliveryScheduleSettlTimeGrp") is IMessageView viewUnderlyingDeliveryScheduleSettlTimeGrp)
			{
				UnderlyingDeliveryScheduleSettlTimeGrp = new();
				((IFixParser)UnderlyingDeliveryScheduleSettlTimeGrp).Parse(viewUnderlyingDeliveryScheduleSettlTimeGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingDeliveryScheduleSettlDay":
					value = UnderlyingDeliveryScheduleSettlDay;
					break;
				case "UnderlyingDeliveryScheduleSettlTotalHours":
					value = UnderlyingDeliveryScheduleSettlTotalHours;
					break;
				case "UnderlyingDeliveryScheduleSettlTimeGrp":
					value = UnderlyingDeliveryScheduleSettlTimeGrp;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingDeliveryScheduleSettlDay = null;
			UnderlyingDeliveryScheduleSettlTotalHours = null;
			((IFixReset?)UnderlyingDeliveryScheduleSettlTimeGrp)?.Reset();
		}
	}
}
