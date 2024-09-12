using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class ListStatus : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(66)]
		public string? ListID { get; set; } // STRING
		
		[TagDetails(429)]
		public int? ListStatusType { get; set; } // INT
		
		[TagDetails(82)]
		public int? NoRpts { get; set; } // INT
		
		[TagDetails(431)]
		public int? ListOrderStatus { get; set; } // INT
		
		[TagDetails(83)]
		public int? RptSeq { get; set; } // INT
		
		[TagDetails(444)]
		public string? ListStatusText { get; set; } // STRING
		
		[TagDetails(445)]
		public int? EncodedListStatusTextLen { get; set; } // LENGTH
		
		[TagDetails(446)]
		public byte[]? EncodedListStatusText { get; set; } // DATA
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(68)]
		public int? TotNoOrders { get; set; } // INT
		
		[TagDetails(893)]
		public bool? LastFragment { get; set; } // BOOLEAN
		
		public OrdListStatGrp? OrdListStatGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
