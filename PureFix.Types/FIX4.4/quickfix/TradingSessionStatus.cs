using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("h", FixVersion.FIX44)]
	public sealed class TradingSessionStatus : FixMsg
	{
		[Component(Offset = 0, Required = true)]
		public override StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 335, Type = TagType.String, Offset = 1, Required = false)]
		public string? TradSesReqID { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 2, Required = true)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 3, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 338, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TradSesMethod { get; set; }
		
		[TagDetails(Tag = 339, Type = TagType.Int, Offset = 5, Required = false)]
		public int? TradSesMode { get; set; }
		
		[TagDetails(Tag = 325, Type = TagType.Boolean, Offset = 6, Required = false)]
		public bool? UnsolicitedIndicator { get; set; }
		
		[TagDetails(Tag = 340, Type = TagType.Int, Offset = 7, Required = true)]
		public int? TradSesStatus { get; set; }
		
		[TagDetails(Tag = 567, Type = TagType.Int, Offset = 8, Required = false)]
		public int? TradSesStatusRejReason { get; set; }
		
		[TagDetails(Tag = 341, Type = TagType.UtcTimestamp, Offset = 9, Required = false)]
		public DateTime? TradSesStartTime { get; set; }
		
		[TagDetails(Tag = 342, Type = TagType.UtcTimestamp, Offset = 10, Required = false)]
		public DateTime? TradSesOpenTime { get; set; }
		
		[TagDetails(Tag = 343, Type = TagType.UtcTimestamp, Offset = 11, Required = false)]
		public DateTime? TradSesPreCloseTime { get; set; }
		
		[TagDetails(Tag = 344, Type = TagType.UtcTimestamp, Offset = 12, Required = false)]
		public DateTime? TradSesCloseTime { get; set; }
		
		[TagDetails(Tag = 345, Type = TagType.UtcTimestamp, Offset = 13, Required = false)]
		public DateTime? TradSesEndTime { get; set; }
		
		[TagDetails(Tag = 387, Type = TagType.Float, Offset = 14, Required = false)]
		public double? TotalVolumeTraded { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 15, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 16, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 17, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[Component(Offset = 18, Required = true)]
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
