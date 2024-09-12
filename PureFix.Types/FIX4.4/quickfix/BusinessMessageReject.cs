using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class BusinessMessageReject : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(45)]
		public int? RefSeqNum { get; set; } // SEQNUM
		
		[TagDetails(372)]
		public string? RefMsgType { get; set; } // STRING
		
		[TagDetails(379)]
		public string? BusinessRejectRefID { get; set; } // STRING
		
		[TagDetails(380)]
		public int? BusinessRejectReason { get; set; } // INT
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
