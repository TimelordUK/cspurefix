using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class PegInstructions
	{
		[TagDetails(Tag = 211, Type = TagType.Float, Offset = 0, Required = false)]
		public double? PegOffsetValue { get; set; }
		
		[TagDetails(Tag = 835, Type = TagType.Int, Offset = 1, Required = false)]
		public int? PegMoveType { get; set; }
		
		[TagDetails(Tag = 836, Type = TagType.Int, Offset = 2, Required = false)]
		public int? PegOffsetType { get; set; }
		
		[TagDetails(Tag = 837, Type = TagType.Int, Offset = 3, Required = false)]
		public int? PegLimitType { get; set; }
		
		[TagDetails(Tag = 838, Type = TagType.Int, Offset = 4, Required = false)]
		public int? PegRoundDirection { get; set; }
		
		[TagDetails(Tag = 840, Type = TagType.Int, Offset = 5, Required = false)]
		public int? PegScope { get; set; }
		
	}
}
