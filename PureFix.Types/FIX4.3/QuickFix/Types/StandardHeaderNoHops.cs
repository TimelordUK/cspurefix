using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class StandardHeaderNoHops : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 628, Type = TagType.String, Offset = 0, Required = false)]
		public string? HopCompID { get; set; }
		
		[TagDetails(Tag = 629, Type = TagType.UtcTimestamp, Offset = 1, Required = false)]
		public DateTime? HopSendingTime { get; set; }
		
		[TagDetails(Tag = 630, Type = TagType.Int, Offset = 2, Required = false)]
		public int? HopRefID { get; set; }
		
		
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
	}
}
