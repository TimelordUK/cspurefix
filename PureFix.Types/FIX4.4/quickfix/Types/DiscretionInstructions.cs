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
		[TagDetails(388)]
		public string? DiscretionInst { get; set; } // CHAR
		
		[TagDetails(389)]
		public double? DiscretionOffsetValue { get; set; } // FLOAT
		
		[TagDetails(841)]
		public int? DiscretionMoveType { get; set; } // INT
		
		[TagDetails(842)]
		public int? DiscretionOffsetType { get; set; } // INT
		
		[TagDetails(843)]
		public int? DiscretionLimitType { get; set; } // INT
		
		[TagDetails(844)]
		public int? DiscretionRoundDirection { get; set; } // INT
		
		[TagDetails(846)]
		public int? DiscretionScope { get; set; } // INT
		
	}
}
