using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoCompIDs
	{
		[TagDetails(930, TagType.String)]
		public string? RefCompID { get; set; }
		
		[TagDetails(931, TagType.String)]
		public string? RefSubID { get; set; }
		
		[TagDetails(283, TagType.String)]
		public string? LocationID { get; set; }
		
		[TagDetails(284, TagType.String)]
		public string? DeskID { get; set; }
		
	}
}
