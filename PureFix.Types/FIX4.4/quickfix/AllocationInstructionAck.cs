using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class AllocationInstructionAck : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(70)]
		public string? AllocID { get; set; } // STRING
		
		public Parties? Parties { get; set; }
		[TagDetails(793)]
		public string? SecondaryAllocID { get; set; } // STRING
		
		[TagDetails(75)]
		public DateTime? TradeDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(87)]
		public int? AllocStatus { get; set; } // INT
		
		[TagDetails(88)]
		public int? AllocRejCode { get; set; } // INT
		
		[TagDetails(626)]
		public int? AllocType { get; set; } // INT
		
		[TagDetails(808)]
		public int? AllocIntermedReqType { get; set; } // INT
		
		[TagDetails(573)]
		public string? MatchStatus { get; set; } // CHAR
		
		[TagDetails(460)]
		public int? Product { get; set; } // INT
		
		[TagDetails(167)]
		public string? SecurityType { get; set; } // STRING
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public AllocAckGrp? AllocAckGrp { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
