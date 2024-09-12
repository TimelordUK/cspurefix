using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class SecurityStatus : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(324)]
		public string? SecurityStatusReqID { get; set; } // STRING
		
		public Instrument? Instrument { get; set; }
		public InstrumentExtension? InstrumentExtension { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		[TagDetails(336)]
		public string? TradingSessionID { get; set; } // STRING
		
		[TagDetails(625)]
		public string? TradingSessionSubID { get; set; } // STRING
		
		[TagDetails(325)]
		public bool? UnsolicitedIndicator { get; set; } // BOOLEAN
		
		[TagDetails(326)]
		public int? SecurityTradingStatus { get; set; } // INT
		
		[TagDetails(291)]
		public string? FinancialStatus { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(292)]
		public string? CorporateAction { get; set; } // MULTIPLEVALUESTRING
		
		[TagDetails(327)]
		public string? HaltReasonChar { get; set; } // CHAR
		
		[TagDetails(328)]
		public bool? InViewOfCommon { get; set; } // BOOLEAN
		
		[TagDetails(329)]
		public bool? DueToRelated { get; set; } // BOOLEAN
		
		[TagDetails(330)]
		public double? BuyVolume { get; set; } // QTY
		
		[TagDetails(331)]
		public double? SellVolume { get; set; } // QTY
		
		[TagDetails(332)]
		public double? HighPx { get; set; } // PRICE
		
		[TagDetails(333)]
		public double? LowPx { get; set; } // PRICE
		
		[TagDetails(31)]
		public double? LastPx { get; set; } // PRICE
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(334)]
		public int? Adjustment { get; set; } // INT
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
