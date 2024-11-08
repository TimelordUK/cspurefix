using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoComplexEventDates : IFixGroup
	{
		[TagDetails(Tag = 1492, Type = TagType.UtcDateOnly, Offset = 0, Required = false)]
		public DateOnly? ComplexEventStartDate {get; set;}
		
		[TagDetails(Tag = 1493, Type = TagType.UtcDateOnly, Offset = 1, Required = false)]
		public DateOnly? ComplexEventEndDate {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public ComplexEventTimesComponent? ComplexEventTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ComplexEventStartDate is not null) writer.WriteUtcDateOnly(1492, ComplexEventStartDate.Value);
			if (ComplexEventEndDate is not null) writer.WriteUtcDateOnly(1493, ComplexEventEndDate.Value);
			if (ComplexEventTimes is not null) ((IFixEncoder)ComplexEventTimes).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ComplexEventStartDate = view.GetDateOnly(1492);
			ComplexEventEndDate = view.GetDateOnly(1493);
			if (view.GetView("ComplexEventTimes") is IMessageView viewComplexEventTimes)
			{
				ComplexEventTimes = new();
				((IFixParser)ComplexEventTimes).Parse(viewComplexEventTimes);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ComplexEventStartDate":
					value = ComplexEventStartDate;
					break;
				case "ComplexEventEndDate":
					value = ComplexEventEndDate;
					break;
				case "ComplexEventTimes":
					value = ComplexEventTimes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ComplexEventStartDate = null;
			ComplexEventEndDate = null;
			((IFixReset?)ComplexEventTimes)?.Reset();
		}
	}
}
