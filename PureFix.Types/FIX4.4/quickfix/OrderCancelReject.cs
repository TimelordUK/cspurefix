using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class OrderCancelReject : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(37)]
		public string? OrderID { get; set; } // STRING
		
		[TagDetails(198)]
		public string? SecondaryOrderID { get; set; } // STRING
		
		[TagDetails(526)]
		public string? SecondaryClOrdID { get; set; } // STRING
		
		[TagDetails(11)]
		public string? ClOrdID { get; set; } // STRING
		
		[TagDetails(583)]
		public string? ClOrdLinkID { get; set; } // STRING
		
		[TagDetails(41)]
		public string? OrigClOrdID { get; set; } // STRING
		
		[TagDetails(39)]
		public string? OrdStatus { get; set; } // CHAR
		
		[TagDetails(636)]
		public bool? WorkingIndicator { get; set; } // BOOLEAN
		
		[TagDetails(586)]
		public DateTime? OrigOrdModTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(66)]
		public string? ListID { get; set; } // STRING
		
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		[TagDetails(229)]
		public DateTime? TradeOriginationDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(75)]
		public DateTime? TradeDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(434)]
		public string? CxlRejResponseTo { get; set; } // CHAR
		
		[TagDetails(102)]
		public int? CxlRejReason { get; set; } // INT
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
