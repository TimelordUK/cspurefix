using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingComplexEventPeriods : IFixGroup
	{
		[TagDetails(Tag = 41730, Type = TagType.Int, Offset = 0, Required = false)]
		public int? UnderlyingComplexEventPeriodType {get; set;}
		
		[TagDetails(Tag = 41731, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingComplexEventBusinessCenter {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public UnderlyingComplexEventScheduleGrpComponent? UnderlyingComplexEventScheduleGrp {get; set;}
		
		[Component(Offset = 3, Required = false)]
		public UnderlyingComplexEventPeriodDateGrpComponent? UnderlyingComplexEventPeriodDateGrp {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public UnderlyingComplexEventAveragingObservationGrpComponent? UnderlyingComplexEventAveragingObservationGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingComplexEventPeriodType is not null) writer.WriteWholeNumber(41730, UnderlyingComplexEventPeriodType.Value);
			if (UnderlyingComplexEventBusinessCenter is not null) writer.WriteString(41731, UnderlyingComplexEventBusinessCenter);
			if (UnderlyingComplexEventScheduleGrp is not null) ((IFixEncoder)UnderlyingComplexEventScheduleGrp).Encode(writer);
			if (UnderlyingComplexEventPeriodDateGrp is not null) ((IFixEncoder)UnderlyingComplexEventPeriodDateGrp).Encode(writer);
			if (UnderlyingComplexEventAveragingObservationGrp is not null) ((IFixEncoder)UnderlyingComplexEventAveragingObservationGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingComplexEventPeriodType = view.GetInt32(41730);
			UnderlyingComplexEventBusinessCenter = view.GetString(41731);
			if (view.GetView("UnderlyingComplexEventScheduleGrp") is IMessageView viewUnderlyingComplexEventScheduleGrp)
			{
				UnderlyingComplexEventScheduleGrp = new();
				((IFixParser)UnderlyingComplexEventScheduleGrp).Parse(viewUnderlyingComplexEventScheduleGrp);
			}
			if (view.GetView("UnderlyingComplexEventPeriodDateGrp") is IMessageView viewUnderlyingComplexEventPeriodDateGrp)
			{
				UnderlyingComplexEventPeriodDateGrp = new();
				((IFixParser)UnderlyingComplexEventPeriodDateGrp).Parse(viewUnderlyingComplexEventPeriodDateGrp);
			}
			if (view.GetView("UnderlyingComplexEventAveragingObservationGrp") is IMessageView viewUnderlyingComplexEventAveragingObservationGrp)
			{
				UnderlyingComplexEventAveragingObservationGrp = new();
				((IFixParser)UnderlyingComplexEventAveragingObservationGrp).Parse(viewUnderlyingComplexEventAveragingObservationGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingComplexEventPeriodType":
					value = UnderlyingComplexEventPeriodType;
					break;
				case "UnderlyingComplexEventBusinessCenter":
					value = UnderlyingComplexEventBusinessCenter;
					break;
				case "UnderlyingComplexEventScheduleGrp":
					value = UnderlyingComplexEventScheduleGrp;
					break;
				case "UnderlyingComplexEventPeriodDateGrp":
					value = UnderlyingComplexEventPeriodDateGrp;
					break;
				case "UnderlyingComplexEventAveragingObservationGrp":
					value = UnderlyingComplexEventAveragingObservationGrp;
					break;
				default: return false;
			}
			return true;
		}
	}
}
