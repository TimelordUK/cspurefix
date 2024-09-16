using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingDeliveryScheduleSettlTimes : IFixGroup
	{
		[TagDetails(Tag = 41774, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingDeliveryScheduleSettlStart {get; set;}
		
		[TagDetails(Tag = 41775, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingDeliveryScheduleSettlEnd {get; set;}
		
		[TagDetails(Tag = 41776, Type = TagType.Int, Offset = 2, Required = false)]
		public int? UnderlyingDeliveryScheduleSettlTimeType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingDeliveryScheduleSettlStart is not null) writer.WriteString(41774, UnderlyingDeliveryScheduleSettlStart);
			if (UnderlyingDeliveryScheduleSettlEnd is not null) writer.WriteString(41775, UnderlyingDeliveryScheduleSettlEnd);
			if (UnderlyingDeliveryScheduleSettlTimeType is not null) writer.WriteWholeNumber(41776, UnderlyingDeliveryScheduleSettlTimeType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingDeliveryScheduleSettlStart = view.GetString(41774);
			UnderlyingDeliveryScheduleSettlEnd = view.GetString(41775);
			UnderlyingDeliveryScheduleSettlTimeType = view.GetInt32(41776);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingDeliveryScheduleSettlStart":
					value = UnderlyingDeliveryScheduleSettlStart;
					break;
				case "UnderlyingDeliveryScheduleSettlEnd":
					value = UnderlyingDeliveryScheduleSettlEnd;
					break;
				case "UnderlyingDeliveryScheduleSettlTimeType":
					value = UnderlyingDeliveryScheduleSettlTimeType;
					break;
				default: return false;
			}
			return true;
		}
	}
}
