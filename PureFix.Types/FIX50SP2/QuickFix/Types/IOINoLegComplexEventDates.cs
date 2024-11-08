using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoLegComplexEventDates : IFixGroup
	{
		[TagDetails(Tag = 2251, Type = TagType.UtcDateOnly, Offset = 0, Required = false)]
		public DateOnly? LegComplexEventStartDate {get; set;}
		
		[TagDetails(Tag = 2252, Type = TagType.UtcDateOnly, Offset = 1, Required = false)]
		public DateOnly? LegComplexEventEndDate {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public LegComplexEventTimesComponent? LegComplexEventTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegComplexEventStartDate is not null) writer.WriteUtcDateOnly(2251, LegComplexEventStartDate.Value);
			if (LegComplexEventEndDate is not null) writer.WriteUtcDateOnly(2252, LegComplexEventEndDate.Value);
			if (LegComplexEventTimes is not null) ((IFixEncoder)LegComplexEventTimes).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegComplexEventStartDate = view.GetDateOnly(2251);
			LegComplexEventEndDate = view.GetDateOnly(2252);
			if (view.GetView("LegComplexEventTimes") is IMessageView viewLegComplexEventTimes)
			{
				LegComplexEventTimes = new();
				((IFixParser)LegComplexEventTimes).Parse(viewLegComplexEventTimes);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegComplexEventStartDate":
					value = LegComplexEventStartDate;
					break;
				case "LegComplexEventEndDate":
					value = LegComplexEventEndDate;
					break;
				case "LegComplexEventTimes":
					value = LegComplexEventTimes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegComplexEventStartDate = null;
			LegComplexEventEndDate = null;
			((IFixReset?)LegComplexEventTimes)?.Reset();
		}
	}
}
