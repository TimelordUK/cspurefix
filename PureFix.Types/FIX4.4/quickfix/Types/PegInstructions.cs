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
		[TagDetails(211)]
		public double? PegOffsetValue { get; set; } // FLOAT
		
		[TagDetails(835)]
		public int? PegMoveType { get; set; } // INT
		
		[TagDetails(836)]
		public int? PegOffsetType { get; set; } // INT
		
		[TagDetails(837)]
		public int? PegLimitType { get; set; } // INT
		
		[TagDetails(838)]
		public int? PegRoundDirection { get; set; } // INT
		
		[TagDetails(840)]
		public int? PegScope { get; set; } // INT
		
	}
}
