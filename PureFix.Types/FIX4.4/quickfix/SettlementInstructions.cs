using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix
{
	public class SettlementInstructions : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? SettlInstMsgID { get; set; } // 777 STRING
		public string? SettlInstReqID { get; set; } // 791 STRING
		public string? SettlInstMode { get; set; } // 160 CHAR
		public int? SettlInstReqRejCode { get; set; } // 792 INT
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public string? ClOrdID { get; set; } // 11 STRING
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public SettlInstGrp? SettlInstGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
