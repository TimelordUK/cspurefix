using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	[MessageType("AK", FixVersion.FIX44)]
	public sealed partial class Confirmation : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 664, Type = TagType.String, Offset = 1, Required = true)]
		public string? ConfirmID { get; set; }
		
		[TagDetails(Tag = 772, Type = TagType.String, Offset = 2, Required = false)]
		public string? ConfirmRefID { get; set; }
		
		[TagDetails(Tag = 859, Type = TagType.String, Offset = 3, Required = false)]
		public string? ConfirmReqID { get; set; }
		
		[TagDetails(Tag = 666, Type = TagType.Int, Offset = 4, Required = true)]
		public int? ConfirmTransType { get; set; }
		
		[TagDetails(Tag = 773, Type = TagType.Int, Offset = 5, Required = true)]
		public int? ConfirmType { get; set; }
		
		[TagDetails(Tag = 797, Type = TagType.Boolean, Offset = 6, Required = false)]
		public bool? CopyMsgIndicator { get; set; }
		
		[TagDetails(Tag = 650, Type = TagType.Boolean, Offset = 7, Required = false)]
		public bool? LegalConfirm { get; set; }
		
		[TagDetails(Tag = 665, Type = TagType.Int, Offset = 8, Required = true)]
		public int? ConfirmStatus { get; set; }
		
		[Component(Offset = 9, Required = false)]
		public Parties? Parties { get; set; }
		
		[Component(Offset = 10, Required = false)]
		public OrdAllocGrp? OrdAllocGrp { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 11, Required = false)]
		public string? AllocID { get; set; }
		
		[TagDetails(Tag = 793, Type = TagType.String, Offset = 12, Required = false)]
		public string? SecondaryAllocID { get; set; }
		
		[TagDetails(Tag = 467, Type = TagType.String, Offset = 13, Required = false)]
		public string? IndividualAllocID { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 14, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 15, Required = true)]
		public DateOnly? TradeDate { get; set; }
		
		[Component(Offset = 16, Required = false)]
		public TrdRegTimestamps? TrdRegTimestamps { get; set; }
		
		[Component(Offset = 17, Required = true)]
		public Instrument? Instrument { get; set; }
		
		[Component(Offset = 18, Required = false)]
		public InstrumentExtension? InstrumentExtension { get; set; }
		
		[Component(Offset = 19, Required = false)]
		public FinancingDetails? FinancingDetails { get; set; }
		
		[Component(Offset = 20, Required = true)]
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		
		[Component(Offset = 21, Required = true)]
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		
		[Component(Offset = 22, Required = false)]
		public YieldData? YieldData { get; set; }
		
		[TagDetails(Tag = 80, Type = TagType.Float, Offset = 23, Required = true)]
		public double? AllocQty { get; set; }
		
		[TagDetails(Tag = 854, Type = TagType.Int, Offset = 24, Required = false)]
		public int? QtyType { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 25, Required = true)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 26, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 27, Required = false)]
		public string? LastMkt { get; set; }
		
		[Component(Offset = 28, Required = true)]
		public CpctyConfGrp? CpctyConfGrp { get; set; }
		
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 29, Required = true)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 661, Type = TagType.Int, Offset = 30, Required = false)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(Tag = 798, Type = TagType.Int, Offset = 31, Required = false)]
		public int? AllocAccountType { get; set; }
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 32, Required = true)]
		public double? AvgPx { get; set; }
		
		[TagDetails(Tag = 74, Type = TagType.Int, Offset = 33, Required = false)]
		public int? AvgPxPrecision { get; set; }
		
		[TagDetails(Tag = 423, Type = TagType.Int, Offset = 34, Required = false)]
		public int? PriceType { get; set; }
		
		[TagDetails(Tag = 860, Type = TagType.Float, Offset = 35, Required = false)]
		public double? AvgParPx { get; set; }
		
		[Component(Offset = 36, Required = false)]
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		
		[TagDetails(Tag = 861, Type = TagType.Float, Offset = 37, Required = false)]
		public double? ReportedPx { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 38, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 39, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 40, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 41, Required = false)]
		public string? ProcessCode { get; set; }
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 42, Required = true)]
		public double? GrossTradeAmt { get; set; }
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 43, Required = false)]
		public int? NumDaysInterest { get; set; }
		
		[TagDetails(Tag = 230, Type = TagType.LocalDate, Offset = 44, Required = false)]
		public DateOnly? ExDate { get; set; }
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 45, Required = false)]
		public double? AccruedInterestRate { get; set; }
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 46, Required = false)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 738, Type = TagType.Float, Offset = 47, Required = false)]
		public double? InterestAtMaturity { get; set; }
		
		[TagDetails(Tag = 920, Type = TagType.Float, Offset = 48, Required = false)]
		public double? EndAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 921, Type = TagType.Float, Offset = 49, Required = false)]
		public double? StartCash { get; set; }
		
		[TagDetails(Tag = 922, Type = TagType.Float, Offset = 50, Required = false)]
		public double? EndCash { get; set; }
		
		[TagDetails(Tag = 238, Type = TagType.Float, Offset = 51, Required = false)]
		public double? Concession { get; set; }
		
		[TagDetails(Tag = 237, Type = TagType.Float, Offset = 52, Required = false)]
		public double? TotalTakedown { get; set; }
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 53, Required = true)]
		public double? NetMoney { get; set; }
		
		[TagDetails(Tag = 890, Type = TagType.Float, Offset = 54, Required = false)]
		public double? MaturityNetMoney { get; set; }
		
		[TagDetails(Tag = 119, Type = TagType.Float, Offset = 55, Required = false)]
		public double? SettlCurrAmt { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 56, Required = false)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 155, Type = TagType.Float, Offset = 57, Required = false)]
		public double? SettlCurrFxRate { get; set; }
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 58, Required = false)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 59, Required = false)]
		public string? SettlType { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 60, Required = false)]
		public DateOnly? SettlDate { get; set; }
		
		[Component(Offset = 61, Required = false)]
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		
		[Component(Offset = 62, Required = false)]
		public CommissionData? CommissionData { get; set; }
		
		[TagDetails(Tag = 858, Type = TagType.Float, Offset = 63, Required = false)]
		public double? SharedCommission { get; set; }
		
		[Component(Offset = 64, Required = false)]
		public Stipulations? Stipulations { get; set; }
		
		[Component(Offset = 65, Required = false)]
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		
		[Component(Offset = 66, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& ConfirmID is not null
				&& ConfirmTransType is not null
				&& ConfirmType is not null
				&& ConfirmStatus is not null
				&& TransactTime is not null
				&& TradeDate is not null
				&& Instrument is not null && ((IFixValidator)Instrument).IsValid(in config)
				&& UndInstrmtGrp is not null && ((IFixValidator)UndInstrmtGrp).IsValid(in config)
				&& InstrmtLegGrp is not null && ((IFixValidator)InstrmtLegGrp).IsValid(in config)
				&& AllocQty is not null
				&& Side is not null
				&& CpctyConfGrp is not null && ((IFixValidator)CpctyConfGrp).IsValid(in config)
				&& AllocAccount is not null
				&& AvgPx is not null
				&& GrossTradeAmt is not null
				&& NetMoney is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (ConfirmID is not null) writer.WriteString(664, ConfirmID);
			if (ConfirmRefID is not null) writer.WriteString(772, ConfirmRefID);
			if (ConfirmReqID is not null) writer.WriteString(859, ConfirmReqID);
			if (ConfirmTransType is not null) writer.WriteWholeNumber(666, ConfirmTransType.Value);
			if (ConfirmType is not null) writer.WriteWholeNumber(773, ConfirmType.Value);
			if (CopyMsgIndicator is not null) writer.WriteBoolean(797, CopyMsgIndicator.Value);
			if (LegalConfirm is not null) writer.WriteBoolean(650, LegalConfirm.Value);
			if (ConfirmStatus is not null) writer.WriteWholeNumber(665, ConfirmStatus.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (OrdAllocGrp is not null) ((IFixEncoder)OrdAllocGrp).Encode(writer);
			if (AllocID is not null) writer.WriteString(70, AllocID);
			if (SecondaryAllocID is not null) writer.WriteString(793, SecondaryAllocID);
			if (IndividualAllocID is not null) writer.WriteString(467, IndividualAllocID);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TrdRegTimestamps is not null) ((IFixEncoder)TrdRegTimestamps).Encode(writer);
			if (Instrument is not null) ((IFixEncoder)Instrument).Encode(writer);
			if (InstrumentExtension is not null) ((IFixEncoder)InstrumentExtension).Encode(writer);
			if (FinancingDetails is not null) ((IFixEncoder)FinancingDetails).Encode(writer);
			if (UndInstrmtGrp is not null) ((IFixEncoder)UndInstrmtGrp).Encode(writer);
			if (InstrmtLegGrp is not null) ((IFixEncoder)InstrmtLegGrp).Encode(writer);
			if (YieldData is not null) ((IFixEncoder)YieldData).Encode(writer);
			if (AllocQty is not null) writer.WriteNumber(80, AllocQty.Value);
			if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
			if (Side is not null) writer.WriteString(54, Side);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (CpctyConfGrp is not null) ((IFixEncoder)CpctyConfGrp).Encode(writer);
			if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
			if (AllocAcctIDSource is not null) writer.WriteWholeNumber(661, AllocAcctIDSource.Value);
			if (AllocAccountType is not null) writer.WriteWholeNumber(798, AllocAccountType.Value);
			if (AvgPx is not null) writer.WriteNumber(6, AvgPx.Value);
			if (AvgPxPrecision is not null) writer.WriteWholeNumber(74, AvgPxPrecision.Value);
			if (PriceType is not null) writer.WriteWholeNumber(423, PriceType.Value);
			if (AvgParPx is not null) writer.WriteNumber(860, AvgParPx.Value);
			if (SpreadOrBenchmarkCurveData is not null) ((IFixEncoder)SpreadOrBenchmarkCurveData).Encode(writer);
			if (ReportedPx is not null) writer.WriteNumber(861, ReportedPx.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
			if (GrossTradeAmt is not null) writer.WriteNumber(381, GrossTradeAmt.Value);
			if (NumDaysInterest is not null) writer.WriteWholeNumber(157, NumDaysInterest.Value);
			if (ExDate is not null) writer.WriteLocalDateOnly(230, ExDate.Value);
			if (AccruedInterestRate is not null) writer.WriteNumber(158, AccruedInterestRate.Value);
			if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
			if (InterestAtMaturity is not null) writer.WriteNumber(738, InterestAtMaturity.Value);
			if (EndAccruedInterestAmt is not null) writer.WriteNumber(920, EndAccruedInterestAmt.Value);
			if (StartCash is not null) writer.WriteNumber(921, StartCash.Value);
			if (EndCash is not null) writer.WriteNumber(922, EndCash.Value);
			if (Concession is not null) writer.WriteNumber(238, Concession.Value);
			if (TotalTakedown is not null) writer.WriteNumber(237, TotalTakedown.Value);
			if (NetMoney is not null) writer.WriteNumber(118, NetMoney.Value);
			if (MaturityNetMoney is not null) writer.WriteNumber(890, MaturityNetMoney.Value);
			if (SettlCurrAmt is not null) writer.WriteNumber(119, SettlCurrAmt.Value);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (SettlCurrFxRate is not null) writer.WriteNumber(155, SettlCurrFxRate.Value);
			if (SettlCurrFxRateCalc is not null) writer.WriteString(156, SettlCurrFxRateCalc);
			if (SettlType is not null) writer.WriteString(63, SettlType);
			if (SettlDate is not null) writer.WriteLocalDateOnly(64, SettlDate.Value);
			if (SettlInstructionsData is not null) ((IFixEncoder)SettlInstructionsData).Encode(writer);
			if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
			if (SharedCommission is not null) writer.WriteNumber(858, SharedCommission.Value);
			if (Stipulations is not null) ((IFixEncoder)Stipulations).Encode(writer);
			if (MiscFeesGrp is not null) ((IFixEncoder)MiscFeesGrp).Encode(writer);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
