using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class DiscretionInstructions
	{
		[TagDetails(388, TagType.String)]
		public string? DiscretionInst { get; set; }
		
		[TagDetails(389, TagType.Float)]
		public double? DiscretionOffsetValue { get; set; }
		
		[TagDetails(841, TagType.Int)]
		public int? DiscretionMoveType { get; set; }
		
		[TagDetails(842, TagType.Int)]
		public int? DiscretionOffsetType { get; set; }
		
		[TagDetails(843, TagType.Int)]
		public int? DiscretionLimitType { get; set; }
		
		[TagDetails(844, TagType.Int)]
		public int? DiscretionRoundDirection { get; set; }
		
		[TagDetails(846, TagType.Int)]
		public int? DiscretionScope { get; set; }
		
	}
}
