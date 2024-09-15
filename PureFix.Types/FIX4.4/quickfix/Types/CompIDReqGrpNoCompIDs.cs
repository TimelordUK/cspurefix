using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CompIDReqGrpNoCompIDs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 930, Type = TagType.String, Offset = 0, Required = false)]
		public string? RefCompID { get; set; }
		
		[TagDetails(Tag = 931, Type = TagType.String, Offset = 1, Required = false)]
		public string? RefSubID { get; set; }
		
		[TagDetails(Tag = 283, Type = TagType.String, Offset = 2, Required = false)]
		public string? LocationID { get; set; }
		
		[TagDetails(Tag = 284, Type = TagType.String, Offset = 3, Required = false)]
		public string? DeskID { get; set; }
		
	}
}
