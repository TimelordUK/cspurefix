using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SecurityTypes : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(320)]
		public string? SecurityReqID { get; set; } // STRING
		
		[TagDetails(322)]
		public string? SecurityResponseID { get; set; } // STRING
		
		[TagDetails(323)]
		public int? SecurityResponseType { get; set; } // INT
		
		[TagDetails(557)]
		public int? TotNoSecurityTypes { get; set; } // INT
		
		[TagDetails(893)]
		public bool? LastFragment { get; set; } // BOOLEAN
		
		public SecTypesGrp? SecTypesGrp { get; set; }
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
		
		[TagDetails(263)]
		public string? SubscriptionRequestType { get; set; } // CHAR
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
