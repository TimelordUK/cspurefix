using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("h")]
	public sealed class TradingSessionStatus : FixMsg
	{
		[Component]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(335, TagType.String)]
		public string? TradSesReqID { get; set; }
		
		[TagDetails(336, TagType.String)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(625, TagType.String)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(338, TagType.Int)]
		public int? TradSesMethod { get; set; }
		
		[TagDetails(339, TagType.Int)]
		public int? TradSesMode { get; set; }
		
		[TagDetails(325, TagType.Boolean)]
		public bool? UnsolicitedIndicator { get; set; }
		
		[TagDetails(340, TagType.Int)]
		public int? TradSesStatus { get; set; }
		
		[TagDetails(567, TagType.Int)]
		public int? TradSesStatusRejReason { get; set; }
		
		[TagDetails(341, TagType.UtcTimestamp)]
		public DateTime? TradSesStartTime { get; set; }
		
		[TagDetails(342, TagType.UtcTimestamp)]
		public DateTime? TradSesOpenTime { get; set; }
		
		[TagDetails(343, TagType.UtcTimestamp)]
		public DateTime? TradSesPreCloseTime { get; set; }
		
		[TagDetails(344, TagType.UtcTimestamp)]
		public DateTime? TradSesCloseTime { get; set; }
		
		[TagDetails(345, TagType.UtcTimestamp)]
		public DateTime? TradSesEndTime { get; set; }
		
		[TagDetails(387, TagType.Float)]
		public double? TotalVolumeTraded { get; set; }
		
		[TagDetails(58, TagType.String)]
		public string? Text { get; set; }
		
		[TagDetails(354, TagType.Length)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(355, TagType.RawData)]
		public byte[]? EncodedText { get; set; }
		
		[Component]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
