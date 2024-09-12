using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class NewOrderList : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(66)]
		public string? ListID { get; set; } // STRING
		
		[TagDetails(390)]
		public string? BidID { get; set; } // STRING
		
		[TagDetails(391)]
		public string? ClientBidID { get; set; } // STRING
		
		[TagDetails(414)]
		public int? ProgRptReqs { get; set; } // INT
		
		[TagDetails(394)]
		public int? BidType { get; set; } // INT
		
		[TagDetails(415)]
		public int? ProgPeriodInterval { get; set; } // INT
		
		[TagDetails(480)]
		public string? CancellationRights { get; set; } // CHAR
		
		[TagDetails(481)]
		public string? MoneyLaunderingStatus { get; set; } // CHAR
		
		[TagDetails(513)]
		public string? RegistID { get; set; } // STRING
		
		[TagDetails(433)]
		public string? ListExecInstType { get; set; } // CHAR
		
		[TagDetails(69)]
		public string? ListExecInst { get; set; } // STRING
		
		[TagDetails(352)]
		public int? EncodedListExecInstLen { get; set; } // LENGTH
		
		[TagDetails(353)]
		public byte[]? EncodedListExecInst { get; set; } // DATA
		
		[TagDetails(765)]
		public double? AllowableOneSidednessPct { get; set; } // PERCENTAGE
		
		[TagDetails(766)]
		public double? AllowableOneSidednessValue { get; set; } // AMT
		
		[TagDetails(767)]
		public string? AllowableOneSidednessCurr { get; set; } // CURRENCY
		
		[TagDetails(68)]
		public int? TotNoOrders { get; set; } // INT
		
		[TagDetails(893)]
		public bool? LastFragment { get; set; } // BOOLEAN
		
		public ListOrdGrp? ListOrdGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
