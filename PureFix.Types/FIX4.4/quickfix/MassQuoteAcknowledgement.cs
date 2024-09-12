using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class MassQuoteAcknowledgement : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(131)]
		public string? QuoteReqID { get; set; } // STRING
		
		[TagDetails(117)]
		public string? QuoteID { get; set; } // STRING
		
		[TagDetails(297)]
		public int? QuoteStatus { get; set; } // INT
		
		[TagDetails(300)]
		public int? QuoteRejectReason { get; set; } // INT
		
		[TagDetails(301)]
		public int? QuoteResponseLevel { get; set; } // INT
		
		[TagDetails(537)]
		public int? QuoteType { get; set; } // INT
		
		public Parties? Parties { get; set; }
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public QuotSetAckGrp? QuotSetAckGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
