using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("8", FixVersion.FIX42)]
	public sealed partial class ExecutionReport : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = true)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 3, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 41, Type = TagType.String, Offset = 4, Required = false)]
		public string? OrigClOrdID { get; set; }
		
		[TagDetails(Tag = 109, Type = TagType.String, Offset = 5, Required = false)]
		public string? ClientID { get; set; }
		
		[TagDetails(Tag = 76, Type = TagType.String, Offset = 6, Required = false)]
		public string? ExecBroker { get; set; }
		
		[Group(NoOfTag = 382, Offset = 7, Required = false)]
		public ExecutionReportNoContraBrokers[]? NoContraBrokers { get; set; }
		
		[TagDetails(Tag = 66, Type = TagType.String, Offset = 8, Required = false)]
		public string? ListID { get; set; }
		
		[TagDetails(Tag = 17, Type = TagType.String, Offset = 9, Required = true)]
		public string? ExecID { get; set; }
		
		[TagDetails(Tag = 20, Type = TagType.String, Offset = 10, Required = true)]
		public string? ExecTransType { get; set; }
		
		[TagDetails(Tag = 19, Type = TagType.String, Offset = 11, Required = false)]
		public string? ExecRefID { get; set; }
		
		[TagDetails(Tag = 150, Type = TagType.String, Offset = 12, Required = true)]
		public string? ExecType { get; set; }
		
		[TagDetails(Tag = 39, Type = TagType.String, Offset = 13, Required = true)]
		public string? OrdStatus { get; set; }
		
		[TagDetails(Tag = 103, Type = TagType.Int, Offset = 14, Required = false)]
		public int? OrdRejReason { get; set; }
		
		[TagDetails(Tag = 378, Type = TagType.Int, Offset = 15, Required = false)]
		public int? ExecRestatementReason { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 16, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 17, Required = false)]
		public string? SettlmntTyp { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 18, Required = false)]
		public DateOnly? FutSettDate { get; set; }
		
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 19, Required = true)]
		public string? Symbol { get; set; }
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 20, Required = false)]
		public string? SymbolSfx { get; set; }
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 21, Required = false)]
		public string? SecurityID { get; set; }
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 22, Required = false)]
		public string? IDSource { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 23, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 24, Required = false)]
		public MonthYear? MaturityMonthYear { get; set; }
		
		[TagDetails(Tag = 205, Type = TagType.String, Offset = 25, Required = false)]
		public string? MaturityDay { get; set; }
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 26, Required = false)]
		public int? PutOrCall { get; set; }
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 27, Required = false)]
		public double? StrikePrice { get; set; }
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 28, Required = false)]
		public string? OptAttribute { get; set; }
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 29, Required = false)]
		public double? ContractMultiplier { get; set; }
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 30, Required = false)]
		public double? CouponRate { get; set; }
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 31, Required = false)]
		public string? SecurityExchange { get; set; }
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 32, Required = false)]
		public string? Issuer { get; set; }
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 33, Required = false, LinksToTag = 349)]
		public int? EncodedIssuerLen { get; set; }
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 34, Required = false, LinksToTag = 348)]
		public byte[]? EncodedIssuer { get; set; }
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 35, Required = false)]
		public string? SecurityDesc { get; set; }
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 36, Required = false, LinksToTag = 351)]
		public int? EncodedSecurityDescLen { get; set; }
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 37, Required = false, LinksToTag = 350)]
		public byte[]? EncodedSecurityDesc { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 38, Required = true)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 38, Type = TagType.Float, Offset = 39, Required = false)]
		public double? OrderQty { get; set; }
		
		[TagDetails(Tag = 152, Type = TagType.Float, Offset = 40, Required = false)]
		public double? CashOrderQty { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 41, Required = false)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 42, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 43, Required = false)]
		public double? StopPx { get; set; }
		
		[TagDetails(Tag = 211, Type = TagType.Float, Offset = 44, Required = false)]
		public double? PegDifference { get; set; }
		
		[TagDetails(Tag = 388, Type = TagType.String, Offset = 45, Required = false)]
		public string? DiscretionInst { get; set; }
		
		[TagDetails(Tag = 389, Type = TagType.Float, Offset = 46, Required = false)]
		public double? DiscretionOffset { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 47, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 48, Required = false)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 49, Required = false)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 50, Required = false)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 51, Required = false)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 52, Required = false)]
		public DateOnly? ExpireDate { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 53, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 54, Required = false)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 47, Type = TagType.String, Offset = 55, Required = false)]
		public string? Rule80A { get; set; }
		
		[TagDetails(Tag = 32, Type = TagType.Float, Offset = 56, Required = false)]
		public double? LastShares { get; set; }
		
		[TagDetails(Tag = 31, Type = TagType.Float, Offset = 57, Required = false)]
		public double? LastPx { get; set; }
		
		[TagDetails(Tag = 194, Type = TagType.Float, Offset = 58, Required = false)]
		public double? LastSpotRate { get; set; }
		
		[TagDetails(Tag = 195, Type = TagType.Float, Offset = 59, Required = false)]
		public double? LastForwardPoints { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 60, Required = false)]
		public string? LastMkt { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 61, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 29, Type = TagType.String, Offset = 62, Required = false)]
		public string? LastCapacity { get; set; }
		
		[TagDetails(Tag = 151, Type = TagType.Float, Offset = 63, Required = true)]
		public double? LeavesQty { get; set; }
		
		[TagDetails(Tag = 14, Type = TagType.Float, Offset = 64, Required = true)]
		public double? CumQty { get; set; }
		
		[TagDetails(Tag = 6, Type = TagType.Float, Offset = 65, Required = true)]
		public double? AvgPx { get; set; }
		
		[TagDetails(Tag = 424, Type = TagType.Float, Offset = 66, Required = false)]
		public double? DayOrderQty { get; set; }
		
		[TagDetails(Tag = 425, Type = TagType.Float, Offset = 67, Required = false)]
		public double? DayCumQty { get; set; }
		
		[TagDetails(Tag = 426, Type = TagType.Float, Offset = 68, Required = false)]
		public double? DayAvgPx { get; set; }
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 69, Required = false)]
		public int? GTBookingInst { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 70, Required = false)]
		public DateOnly? TradeDate { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 71, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 113, Type = TagType.Boolean, Offset = 72, Required = false)]
		public bool? ReportToExch { get; set; }
		
		[TagDetails(Tag = 12, Type = TagType.Float, Offset = 73, Required = false)]
		public double? Commission { get; set; }
		
		[TagDetails(Tag = 13, Type = TagType.String, Offset = 74, Required = false)]
		public string? CommType { get; set; }
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 75, Required = false)]
		public double? GrossTradeAmt { get; set; }
		
		[TagDetails(Tag = 119, Type = TagType.Float, Offset = 76, Required = false)]
		public double? SettlCurrAmt { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 77, Required = false)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 155, Type = TagType.Float, Offset = 78, Required = false)]
		public double? SettlCurrFxRate { get; set; }
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 79, Required = false)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 80, Required = false)]
		public string? HandlInst { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 81, Required = false)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 82, Required = false)]
		public double? MaxFloor { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 83, Required = false)]
		public string? OpenClose { get; set; }
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 84, Required = false)]
		public double? MaxShow { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 85, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 86, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 87, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 88, Required = false)]
		public DateOnly? FutSettDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 89, Required = false)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 439, Type = TagType.String, Offset = 90, Required = false)]
		public string? ClearingFirm { get; set; }
		
		[TagDetails(Tag = 440, Type = TagType.String, Offset = 91, Required = false)]
		public string? ClearingAccount { get; set; }
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 92, Required = false)]
		public string? MultiLegReportingType { get; set; }
		
		[Component(Offset = 93, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& OrderID is not null
				&& ExecID is not null
				&& ExecTransType is not null
				&& ExecType is not null
				&& OrdStatus is not null
				&& Symbol is not null
				&& Side is not null
				&& LeavesQty is not null
				&& CumQty is not null
				&& AvgPx is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (OrigClOrdID is not null) writer.WriteString(41, OrigClOrdID);
			if (ClientID is not null) writer.WriteString(109, ClientID);
			if (ExecBroker is not null) writer.WriteString(76, ExecBroker);
			if (NoContraBrokers is not null && NoContraBrokers.Length != 0)
			{
				writer.WriteWholeNumber(382, NoContraBrokers.Length);
				for (int i = 0; i < NoContraBrokers.Length; i++)
				{
					((IFixEncoder)NoContraBrokers[i]).Encode(writer);
				}
			}
			if (ListID is not null) writer.WriteString(66, ListID);
			if (ExecID is not null) writer.WriteString(17, ExecID);
			if (ExecTransType is not null) writer.WriteString(20, ExecTransType);
			if (ExecRefID is not null) writer.WriteString(19, ExecRefID);
			if (ExecType is not null) writer.WriteString(150, ExecType);
			if (OrdStatus is not null) writer.WriteString(39, OrdStatus);
			if (OrdRejReason is not null) writer.WriteWholeNumber(103, OrdRejReason.Value);
			if (ExecRestatementReason is not null) writer.WriteWholeNumber(378, ExecRestatementReason.Value);
			if (Account is not null) writer.WriteString(1, Account);
			if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (Symbol is not null) writer.WriteString(55, Symbol);
			if (SymbolSfx is not null) writer.WriteString(65, SymbolSfx);
			if (SecurityID is not null) writer.WriteString(48, SecurityID);
			if (IDSource is not null) writer.WriteString(22, IDSource);
			if (SecurityType is not null) writer.WriteString(167, SecurityType);
			if (MaturityMonthYear is not null) writer.WriteMonthYear(200, MaturityMonthYear.Value);
			if (MaturityDay is not null) writer.WriteString(205, MaturityDay);
			if (PutOrCall is not null) writer.WriteWholeNumber(201, PutOrCall.Value);
			if (StrikePrice is not null) writer.WriteNumber(202, StrikePrice.Value);
			if (OptAttribute is not null) writer.WriteString(206, OptAttribute);
			if (ContractMultiplier is not null) writer.WriteNumber(231, ContractMultiplier.Value);
			if (CouponRate is not null) writer.WriteNumber(223, CouponRate.Value);
			if (SecurityExchange is not null) writer.WriteString(207, SecurityExchange);
			if (Issuer is not null) writer.WriteString(106, Issuer);
			if (EncodedIssuer is not null)
			{
				writer.WriteWholeNumber(348, EncodedIssuer.Length);
				writer.WriteBuffer(349, EncodedIssuer);
			}
			if (SecurityDesc is not null) writer.WriteString(107, SecurityDesc);
			if (EncodedSecurityDesc is not null)
			{
				writer.WriteWholeNumber(350, EncodedSecurityDesc.Length);
				writer.WriteBuffer(351, EncodedSecurityDesc);
			}
			if (Side is not null) writer.WriteString(54, Side);
			if (OrderQty is not null) writer.WriteNumber(38, OrderQty.Value);
			if (CashOrderQty is not null) writer.WriteNumber(152, CashOrderQty.Value);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (StopPx is not null) writer.WriteNumber(99, StopPx.Value);
			if (PegDifference is not null) writer.WriteNumber(211, PegDifference.Value);
			if (DiscretionInst is not null) writer.WriteString(388, DiscretionInst);
			if (DiscretionOffset is not null) writer.WriteNumber(389, DiscretionOffset.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
			if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
			if (TimeInForce is not null) writer.WriteString(59, TimeInForce);
			if (EffectiveTime is not null) writer.WriteUtcTimeStamp(168, EffectiveTime.Value);
			if (ExpireDate is not null) writer.WriteLocalDateOnly(432, ExpireDate.Value);
			if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
			if (ExecInst is not null) writer.WriteString(18, ExecInst);
			if (Rule80A is not null) writer.WriteString(47, Rule80A);
			if (LastShares is not null) writer.WriteNumber(32, LastShares.Value);
			if (LastPx is not null) writer.WriteNumber(31, LastPx.Value);
			if (LastSpotRate is not null) writer.WriteNumber(194, LastSpotRate.Value);
			if (LastForwardPoints is not null) writer.WriteNumber(195, LastForwardPoints.Value);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (LastCapacity is not null) writer.WriteString(29, LastCapacity);
			if (LeavesQty is not null) writer.WriteNumber(151, LeavesQty.Value);
			if (CumQty is not null) writer.WriteNumber(14, CumQty.Value);
			if (AvgPx is not null) writer.WriteNumber(6, AvgPx.Value);
			if (DayOrderQty is not null) writer.WriteNumber(424, DayOrderQty.Value);
			if (DayCumQty is not null) writer.WriteNumber(425, DayCumQty.Value);
			if (DayAvgPx is not null) writer.WriteNumber(426, DayAvgPx.Value);
			if (GTBookingInst is not null) writer.WriteWholeNumber(427, GTBookingInst.Value);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (ReportToExch is not null) writer.WriteBoolean(113, ReportToExch.Value);
			if (Commission is not null) writer.WriteNumber(12, Commission.Value);
			if (CommType is not null) writer.WriteString(13, CommType);
			if (GrossTradeAmt is not null) writer.WriteNumber(381, GrossTradeAmt.Value);
			if (SettlCurrAmt is not null) writer.WriteNumber(119, SettlCurrAmt.Value);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (SettlCurrFxRate is not null) writer.WriteNumber(155, SettlCurrFxRate.Value);
			if (SettlCurrFxRateCalc is not null) writer.WriteString(156, SettlCurrFxRateCalc);
			if (HandlInst is not null) writer.WriteString(21, HandlInst);
			if (MinQty is not null) writer.WriteNumber(110, MinQty.Value);
			if (MaxFloor is not null) writer.WriteNumber(111, MaxFloor.Value);
			if (OpenClose is not null) writer.WriteString(77, OpenClose);
			if (MaxShow is not null) writer.WriteNumber(210, MaxShow.Value);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (FutSettDate2 is not null) writer.WriteLocalDateOnly(193, FutSettDate2.Value);
			if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
			if (ClearingFirm is not null) writer.WriteString(439, ClearingFirm);
			if (ClearingAccount is not null) writer.WriteString(440, ClearingAccount);
			if (MultiLegReportingType is not null) writer.WriteString(442, MultiLegReportingType);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
