using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public class ListStatus : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? ListID { get; set; } // 66 STRING
		public int? ListStatusType { get; set; } // 429 INT
		public int? NoRpts { get; set; } // 82 INT
		public int? ListOrderStatus { get; set; } // 431 INT
		public int? RptSeq { get; set; } // 83 INT
		public string? ListStatusText { get; set; } // 444 STRING
		public int? EncodedListStatusTextLen { get; set; } // 445 LENGTH
		public byte[]? EncodedListStatusText { get; set; } // 446 DATA
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public int? TotNoOrders { get; set; } // 68 INT
		public bool? LastFragment { get; set; } // 893 BOOLEAN
		public OrdListStatGrp? OrdListStatGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
