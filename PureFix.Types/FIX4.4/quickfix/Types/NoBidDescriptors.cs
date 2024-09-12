using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class NoBidDescriptors
	{
		[TagDetails(399)]
		public int? BidDescriptorType { get; set; } // INT
		
		[TagDetails(400)]
		public string? BidDescriptor { get; set; } // STRING
		
		[TagDetails(401)]
		public int? SideValueInd { get; set; } // INT
		
		[TagDetails(404)]
		public double? LiquidityValue { get; set; } // AMT
		
		[TagDetails(441)]
		public int? LiquidityNumSecurities { get; set; } // INT
		
		[TagDetails(402)]
		public double? LiquidityPctLow { get; set; } // PERCENTAGE
		
		[TagDetails(403)]
		public double? LiquidityPctHigh { get; set; } // PERCENTAGE
		
		[TagDetails(405)]
		public double? EFPTrackingError { get; set; } // PERCENTAGE
		
		[TagDetails(406)]
		public double? FairValue { get; set; } // AMT
		
		[TagDetails(407)]
		public double? OutsideIndexPct { get; set; } // PERCENTAGE
		
		[TagDetails(408)]
		public double? ValueOfFutures { get; set; } // AMT
		
	}
}
