using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class NewOrderList : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? ListID { get; set; } // 66 STRING
		public string? BidID { get; set; } // 390 STRING
		public string? ClientBidID { get; set; } // 391 STRING
		public int? ProgRptReqs { get; set; } // 414 INT
		public int? BidType { get; set; } // 394 INT
		public int? ProgPeriodInterval { get; set; } // 415 INT
		public string? CancellationRights { get; set; } // 480 CHAR
		public string? MoneyLaunderingStatus { get; set; } // 481 CHAR
		public string? RegistID { get; set; } // 513 STRING
		public string? ListExecInstType { get; set; } // 433 CHAR
		public string? ListExecInst { get; set; } // 69 STRING
		public int? EncodedListExecInstLen { get; set; } // 352 LENGTH
		public byte[]? EncodedListExecInst { get; set; } // 353 DATA
		public double? AllowableOneSidednessPct { get; set; } // 765 PERCENTAGE
		public double? AllowableOneSidednessValue { get; set; } // 766 AMT
		public string? AllowableOneSidednessCurr { get; set; } // 767 CURRENCY
		public int? TotNoOrders { get; set; } // 68 INT
		public bool? LastFragment { get; set; } // 893 BOOLEAN
		public ListOrdGrp? ListOrdGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
