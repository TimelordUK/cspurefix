using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SecurityTypeRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(320)]
		public string? SecurityReqID { get; set; } // STRING
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		[TagDetails(336)]
		public string? TradingSessionID { get; set; } // STRING
		
		[TagDetails(625)]
		public string? TradingSessionSubID { get; set; } // STRING
		
		[TagDetails(460)]
		public int? Product { get; set; } // INT
		
		[TagDetails(167)]
		public string? SecurityType { get; set; } // STRING
		
		[TagDetails(762)]
		public string? SecuritySubType { get; set; } // STRING
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
