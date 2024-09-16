using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class TrdRegTimestampsNoTrdRegTimestamps : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 769, Type = TagType.UtcTimestamp, Offset = 0, Required = false)]
		public DateTime? TrdRegTimestamp { get; set; }
		
		[TagDetails(Tag = 770, Type = TagType.Int, Offset = 1, Required = false)]
		public int? TrdRegTimestampType { get; set; }
		
		[TagDetails(Tag = 771, Type = TagType.String, Offset = 2, Required = false)]
		public string? TrdRegTimestampOrigin { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TrdRegTimestamp is not null) writer.WriteUtcTimeStamp(769, TrdRegTimestamp.Value);
			if (TrdRegTimestampType is not null) writer.WriteWholeNumber(770, TrdRegTimestampType.Value);
			if (TrdRegTimestampOrigin is not null) writer.WriteString(771, TrdRegTimestampOrigin);
		}
	}
}
