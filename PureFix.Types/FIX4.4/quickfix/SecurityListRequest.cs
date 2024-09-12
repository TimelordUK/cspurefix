using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SecurityListRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(320)]
		public string? SecurityReqID { get; set; } // STRING
		
		[TagDetails(559)]
		public int? SecurityListRequestType { get; set; } // INT
		
		public Instrument? Instrument { get; set; }
		public InstrumentExtension? InstrumentExtension { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
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
