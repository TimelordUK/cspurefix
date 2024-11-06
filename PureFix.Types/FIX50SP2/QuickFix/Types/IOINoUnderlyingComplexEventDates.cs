using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingComplexEventDates : IFixGroup
	{
		[TagDetails(Tag = 2054, Type = TagType.UtcDateOnly, Offset = 0, Required = false)]
		public DateOnly? UnderlyingComplexEventStartDate {get; set;}
		
		[TagDetails(Tag = 2055, Type = TagType.UtcDateOnly, Offset = 1, Required = false)]
		public DateOnly? UnderlyingComplexEventEndDate {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public UnderlyingComplexEventTimesComponent? UnderlyingComplexEventTimes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingComplexEventStartDate is not null) writer.WriteUtcDateOnly(2054, UnderlyingComplexEventStartDate.Value);
			if (UnderlyingComplexEventEndDate is not null) writer.WriteUtcDateOnly(2055, UnderlyingComplexEventEndDate.Value);
			if (UnderlyingComplexEventTimes is not null) ((IFixEncoder)UnderlyingComplexEventTimes).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingComplexEventStartDate = view.GetDateOnly(2054);
			UnderlyingComplexEventEndDate = view.GetDateOnly(2055);
			if (view.GetView("UnderlyingComplexEventTimes") is IMessageView viewUnderlyingComplexEventTimes)
			{
				UnderlyingComplexEventTimes = new();
				((IFixParser)UnderlyingComplexEventTimes).Parse(viewUnderlyingComplexEventTimes);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingComplexEventStartDate":
					value = UnderlyingComplexEventStartDate;
					break;
				case "UnderlyingComplexEventEndDate":
					value = UnderlyingComplexEventEndDate;
					break;
				case "UnderlyingComplexEventTimes":
					value = UnderlyingComplexEventTimes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingComplexEventStartDate = null;
			UnderlyingComplexEventEndDate = null;
			((IFixReset?)UnderlyingComplexEventTimes)?.Reset();
		}
	}
}
