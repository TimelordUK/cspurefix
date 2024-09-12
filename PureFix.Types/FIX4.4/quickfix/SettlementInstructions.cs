using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SettlementInstructions : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(777)]
		public string? SettlInstMsgID { get; set; } // STRING
		
		[TagDetails(791)]
		public string? SettlInstReqID { get; set; } // STRING
		
		[TagDetails(160)]
		public string? SettlInstMode { get; set; } // CHAR
		
		[TagDetails(792)]
		public int? SettlInstReqRejCode { get; set; } // INT
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		[TagDetails(11)]
		public string? ClOrdID { get; set; } // STRING
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		public SettlInstGrp? SettlInstGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
