using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegComplexEventTimes : IFixGroup
	{
		[TagDetails(Tag = 2204, Type = TagType.UtcTimeOnly, Offset = 0, Required = false)]
		public TimeOnly? LegComplexEventStartTime {get; set;}
		
		[TagDetails(Tag = 2247, Type = TagType.UtcTimeOnly, Offset = 1, Required = false)]
		public TimeOnly? LegComplexEventEndTime {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegComplexEventStartTime is not null) writer.WriteTimeOnly(2204, LegComplexEventStartTime.Value);
			if (LegComplexEventEndTime is not null) writer.WriteTimeOnly(2247, LegComplexEventEndTime.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegComplexEventStartTime = view.GetTimeOnly(2204);
			LegComplexEventEndTime = view.GetTimeOnly(2247);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegComplexEventStartTime":
					value = LegComplexEventStartTime;
					break;
				case "LegComplexEventEndTime":
					value = LegComplexEventEndTime;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegComplexEventStartTime = null;
			LegComplexEventEndTime = null;
		}
	}
}
