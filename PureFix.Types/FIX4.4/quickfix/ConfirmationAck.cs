using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class ConfirmationAck : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(664)]
		public string? ConfirmID { get; set; } // STRING
		
		[TagDetails(75)]
		public DateTime? TradeDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(940)]
		public int? AffirmStatus { get; set; } // INT
		
		[TagDetails(774)]
		public int? ConfirmRejReason { get; set; } // INT
		
		[TagDetails(573)]
		public string? MatchStatus { get; set; } // CHAR
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
