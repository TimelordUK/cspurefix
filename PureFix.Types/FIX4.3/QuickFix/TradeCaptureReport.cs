using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("AE", FixVersion.FIX43)]
	public sealed partial class TradeCaptureReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 571, Type = TagType.String, Offset = 1, Required = true)]
		public string? TradeReportID { get; set; }
		
		[TagDetails(Tag = 487, Type = TagType.String, Offset = 2, Required = false)]
		public string? TradeReportTransType { get; set; }
		
		[TagDetails(Tag = 568, Type = TagType.String, Offset = 3, Required = false)]
		public string? TradeRequestID { get; set; }
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 4, Required = true)]
		public string? ExecType { get; set; }
		
		[TagDetails(Tag = 572, Type = TagType.String, Offset = 5, Required = false)]
		public string? TradeReportRefID { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 6, Required = false)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 527, Type = TagType.String, Offset = 7, Required = false)]
		public string? SecondaryExecID { get; set; }
		
		[TagDetails(Tag = 378, Type = TagType.Int, Offset = 8, Required = false)]
		public int? ExecRestatementReason { get; set; }
		
		[TagDetails(Tag = 570, Type = TagType.Boolean, Offset = 9, Required = true)]
		public bool? PreviouslyReported { get; set; }
		
		[Component(Offset = 10, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 11, Required = false)]
		public OrderQtyData? OrderQtyData { get; set; }
		
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 12, Required = true)]
		public double? LastQty { get; set; }
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 13, Required = true)]
		public double? LastPx { get; set; }
		
		[TagDetails(Tag = 194, Type = TagType.Float, Offset = 14, Required = false)]
		public double? LastSpotRate { get; set; }
		
		[TagDetails(Tag = 195, Type = TagType.Float, Offset = 15, Required = false)]
		public double? LastForwardPoints { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 16, Required = false)]
		public string? LastMkt { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 17, Required = true)]
		public DateOnly? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 18, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 19, Required = false)]
		public string? SettlmntTyp { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 20, Required = false)]
		public DateOnly? FutSettDate { get; set; }
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 21, Required = false)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(Tag = 574, Type = TagType.String, Offset = 22, Required = false)]
		public string? MatchType { get; set; }
		
		[Group(NoOfTag = 552, Offset = 23, Required = true)]
		public TradeCaptureReportNoSides[]? NoSides { get; set; }
		
		[Component(Offset = 24, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& TradeReportID is not null
				&& ExecType is not null
				&& PreviouslyReported is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& LastQty is not null
				&& LastPx is not null
				&& TradeDate is not null
				&& TransactTime is not null
				&& NoSides is not null && FixValidator.IsValid(NoSides, in config)
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (TradeReportID is not null) writer.WriteString(571, TradeReportID);
			if (TradeReportTransType is not null) writer.WriteString(487, TradeReportTransType);
			if (TradeRequestID is not null) writer.WriteString(568, TradeRequestID);
			if (ExecType is not null) writer.WriteString(150, ExecType);
			if (TradeReportRefID is not null) writer.WriteString(572, TradeReportRefID);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (SecondaryExecID is not null) writer.WriteString(527, SecondaryExecID);
			if (ExecRestatementReason is not null) writer.WriteWholeNumber(378, ExecRestatementReason.Value);
			if (PreviouslyReported is not null) writer.WriteBoolean(570, PreviouslyReported.Value);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (LastQty is not null) writer.WriteNumber(32, LastQty.Value);
			if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
			if (LastSpotRate is not null) writer.WriteNumber(194, LastSpotRate.Value);
			if (LastForwardPoints is not null) writer.WriteNumber(195, LastForwardPoints.Value);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (MatchStatus is not null) writer.WriteString(573, MatchStatus);
			if (MatchType is not null) writer.WriteString(574, MatchType);
			if (NoSides is not null && NoSides.Length != 0)
			{
				writer.WriteWholeNumber(552, NoSides.Length);
				for (int i = 0; i < NoSides.Length; i++)
				{
					((IFixEncoder)NoSides[i]).Encode(writer);
				}
			}
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
