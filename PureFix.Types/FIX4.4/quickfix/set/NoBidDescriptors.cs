using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class NoBidDescriptors
	{
		public int? BidDescriptorType { get; set; } // 399 INT
		public string? BidDescriptor { get; set; } // 400 STRING
		public int? SideValueInd { get; set; } // 401 INT
		public double? LiquidityValue { get; set; } // 404 AMT
		public int? LiquidityNumSecurities { get; set; } // 441 INT
		public double? LiquidityPctLow { get; set; } // 402 PERCENTAGE
		public double? LiquidityPctHigh { get; set; } // 403 PERCENTAGE
		public double? EFPTrackingError { get; set; } // 405 PERCENTAGE
		public double? FairValue { get; set; } // 406 AMT
		public double? OutsideIndexPct { get; set; } // 407 PERCENTAGE
		public double? ValueOfFutures { get; set; } // 408 AMT
	}
}
