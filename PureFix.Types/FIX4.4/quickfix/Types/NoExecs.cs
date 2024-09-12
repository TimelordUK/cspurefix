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
		[TagDetails(32, TagType.Float)]
		public double? LastQty { get; set; }
		
		[TagDetails(17, TagType.String)]
		public string? ExecID { get; set; }
		
		[TagDetails(527, TagType.String)]
		public string? SecondaryExecID { get; set; }
		
		[TagDetails(31, TagType.Float)]
		public double? LastPx { get; set; }
		
		[TagDetails(669, TagType.Float)]
		public double? LastParPx { get; set; }
		
		[TagDetails(29, TagType.String)]
		public string? LastCapacity { get; set; }
		
	}
}
