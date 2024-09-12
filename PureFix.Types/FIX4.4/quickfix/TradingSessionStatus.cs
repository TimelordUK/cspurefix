using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class TradingSessionStatus : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(335)]
		public string? TradSesReqID { get; set; } // STRING
		
		[TagDetails(336)]
		public string? TradingSessionID { get; set; } // STRING
		
		[TagDetails(625)]
		public string? TradingSessionSubID { get; set; } // STRING
		
		[TagDetails(338)]
		public int? TradSesMethod { get; set; } // INT
		
		[TagDetails(339)]
		public int? TradSesMode { get; set; } // INT
		
		[TagDetails(325)]
		public bool? UnsolicitedIndicator { get; set; } // BOOLEAN
		
		[TagDetails(340)]
		public int? TradSesStatus { get; set; } // INT
		
		[TagDetails(567)]
		public int? TradSesStatusRejReason { get; set; } // INT
		
		[TagDetails(341)]
		public DateTime? TradSesStartTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(342)]
		public DateTime? TradSesOpenTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(343)]
		public DateTime? TradSesPreCloseTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(344)]
		public DateTime? TradSesCloseTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(345)]
		public DateTime? TradSesEndTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(387)]
		public double? TotalVolumeTraded { get; set; } // QTY
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
