using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public class TradingSessionStatus : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? TradSesReqID { get; set; } // 335 STRING
		public string? TradingSessionID { get; set; } // 336 STRING
		public string? TradingSessionSubID { get; set; } // 625 STRING
		public int? TradSesMethod { get; set; } // 338 INT
		public int? TradSesMode { get; set; } // 339 INT
		public bool? UnsolicitedIndicator { get; set; } // 325 BOOLEAN
		public int? TradSesStatus { get; set; } // 340 INT
		public int? TradSesStatusRejReason { get; set; } // 567 INT
		public DateTime? TradSesStartTime { get; set; } // 341 UTCTIMESTAMP
		public DateTime? TradSesOpenTime { get; set; } // 342 UTCTIMESTAMP
		public DateTime? TradSesPreCloseTime { get; set; } // 343 UTCTIMESTAMP
		public DateTime? TradSesCloseTime { get; set; } // 344 UTCTIMESTAMP
		public DateTime? TradSesEndTime { get; set; } // 345 UTCTIMESTAMP
		public double? TotalVolumeTraded { get; set; } // 387 QTY
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
