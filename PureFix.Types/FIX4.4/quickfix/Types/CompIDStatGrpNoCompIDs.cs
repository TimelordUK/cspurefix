using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CompIDStatGrpNoCompIDs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 930, Type = TagType.String, Offset = 0, Required = false)]
		public string? RefCompID { get; set; }
		
		[TagDetails(Tag = 931, Type = TagType.String, Offset = 1, Required = false)]
		public string? RefSubID { get; set; }
		
		[TagDetails(Tag = 283, Type = TagType.String, Offset = 2, Required = false)]
		public string? LocationID { get; set; }
		
		[TagDetails(Tag = 284, Type = TagType.String, Offset = 3, Required = false)]
		public string? DeskID { get; set; }
		
		[TagDetails(Tag = 928, Type = TagType.Int, Offset = 4, Required = false)]
		public int? StatusValue { get; set; }
		
		[TagDetails(Tag = 929, Type = TagType.String, Offset = 5, Required = false)]
		public string? StatusText { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RefCompID is not null) writer.WriteString(930, RefCompID);
			if (RefSubID is not null) writer.WriteString(931, RefSubID);
			if (LocationID is not null) writer.WriteString(283, LocationID);
			if (DeskID is not null) writer.WriteString(284, DeskID);
			if (StatusValue is not null) writer.WriteWholeNumber(928, StatusValue.Value);
			if (StatusText is not null) writer.WriteString(929, StatusText);
		}
	}
}
