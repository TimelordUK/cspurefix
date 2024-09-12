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
		[TagDetails(211, TagType.Float)]
		public double? PegOffsetValue { get; set; }
		
		[TagDetails(835, TagType.Int)]
		public int? PegMoveType { get; set; }
		
		[TagDetails(836, TagType.Int)]
		public int? PegOffsetType { get; set; }
		
		[TagDetails(837, TagType.Int)]
		public int? PegLimitType { get; set; }
		
		[TagDetails(838, TagType.Int)]
		public int? PegRoundDirection { get; set; }
		
		[TagDetails(840, TagType.Int)]
		public int? PegScope { get; set; }
		
	}
}
