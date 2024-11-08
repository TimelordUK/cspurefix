using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LogoutNoHops : IFixGroup
	{
		[TagDetails(Tag = 628, Type = TagType.String, Offset = 0, Required = false)]
		public string? HopCompID {get; set;}
		
		[TagDetails(Tag = 629, Type = TagType.UtcTimestamp, Offset = 1, Required = false)]
		public DateTime? HopSendingTime {get; set;}
		
		[TagDetails(Tag = 630, Type = TagType.Int, Offset = 2, Required = false)]
		public int? HopRefID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (HopCompID is not null) writer.WriteString(628, HopCompID);
			if (HopSendingTime is not null) writer.WriteUtcTimeStamp(629, HopSendingTime.Value);
			if (HopRefID is not null) writer.WriteWholeNumber(630, HopRefID.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			HopCompID = view.GetString(628);
			HopSendingTime = view.GetDateTime(629);
			HopRefID = view.GetInt32(630);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "HopCompID":
					value = HopCompID;
					break;
				case "HopSendingTime":
					value = HopSendingTime;
					break;
				case "HopRefID":
					value = HopRefID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			HopCompID = null;
			HopSendingTime = null;
			HopRefID = null;
		}
	}
}
