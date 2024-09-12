using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoExecs
	{
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 0, Required = false)]
		public double? LastQty { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 1, Required = false)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryExecID { get; set; }
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 3, Required = false)]
		public double? LastPx { get; set; }
		
		[TagDetails(Tag = 669, Type = TagType.Float, Offset = 4, Required = false)]
		public double? LastParPx { get; set; }
		
		[TagDetails(Tag = 29, Type = TagType.String, Offset = 5, Required = false)]
		public string? LastCapacity { get; set; }
		
	}
}
