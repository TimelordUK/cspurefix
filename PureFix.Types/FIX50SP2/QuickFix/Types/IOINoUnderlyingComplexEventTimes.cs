using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoUnderlyingComplexEventTimes : IFixGroup
	{
		[TagDetails(Tag = 2057, Type = TagType.UtcTimeOnly, Offset = 0, Required = false)]
		public TimeOnly? UnderlyingComplexEventStartTime {get; set;}
		
		[TagDetails(Tag = 2058, Type = TagType.UtcTimeOnly, Offset = 1, Required = false)]
		public TimeOnly? UnderlyingComplexEventEndTime {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingComplexEventStartTime is not null) writer.WriteTimeOnly(2057, UnderlyingComplexEventStartTime.Value);
			if (UnderlyingComplexEventEndTime is not null) writer.WriteTimeOnly(2058, UnderlyingComplexEventEndTime.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingComplexEventStartTime = view.GetTimeOnly(2057);
			UnderlyingComplexEventEndTime = view.GetTimeOnly(2058);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingComplexEventStartTime":
					value = UnderlyingComplexEventStartTime;
					break;
				case "UnderlyingComplexEventEndTime":
					value = UnderlyingComplexEventEndTime;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			UnderlyingComplexEventStartTime = null;
			UnderlyingComplexEventEndTime = null;
		}
	}
}
