using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class NewOrderListNoOrders : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 0, Required = true)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 67, Type = TagType.Int, Offset = 1, Required = true)]
		public int? ListSeqNo { get; set; }
		
		[TagDetails(Tag = 160, Type = TagType.String, Offset = 2, Required = false)]
		public string? SettlInstMode { get; set; }
		
		[TagDetails(Tag = 109, Type = TagType.String, Offset = 3, Required = false)]
		public string? ClientID { get; set; }
		
		[TagDetails(Tag = 76, Type = TagType.String, Offset = 4, Required = false)]
		public string? ExecBroker { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 5, Required = false)]
		public string? Account { get; set; }
		
		[Group(NoOfTag = 78, Offset = 6, Required = false)]
		public NewOrderListNoOrdersNoAllocs[]? NoAllocs { get; set; }
		
		[TagDetails(Tag = 63, Type = TagType.String, Offset = 7, Required = false)]
		public string? SettlmntTyp { get; set; }
		
		[TagDetails(Tag = 64, Type = TagType.LocalDate, Offset = 8, Required = false)]
		public DateOnly? FutSettDate { get; set; }
		
		[TagDetails(Tag = 21, Type = TagType.String, Offset = 9, Required = false)]
		public string? HandlInst { get; set; }
		
		[TagDetails(Tag = 18, Type = TagType.String, Offset = 10, Required = false)]
		public string? ExecInst { get; set; }
		
		[TagDetails(Tag = 110, Type = TagType.Float, Offset = 11, Required = false)]
		public double? MinQty { get; set; }
		
		[TagDetails(Tag = 111, Type = TagType.Float, Offset = 12, Required = false)]
		public double? MaxFloor { get; set; }
		
		[TagDetails(Tag = 100, Type = TagType.String, Offset = 13, Required = false)]
		public string? ExDestination { get; set; }
		
		[Group(NoOfTag = 386, Offset = 14, Required = false)]
		public NewOrderListNoOrdersNoTradingSessions[]? NoTradingSessions { get; set; }
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 15, Required = false)]
		public string? ProcessCode { get; set; }
		
		[TagDetails(Tag = 55, Type = TagType.String, Offset = 16, Required = true)]
		public string? Symbol { get; set; }
		
		[TagDetails(Tag = 65, Type = TagType.String, Offset = 17, Required = false)]
		public string? SymbolSfx { get; set; }
		
		[TagDetails(Tag = 48, Type = TagType.String, Offset = 18, Required = false)]
		public string? SecurityID { get; set; }
		
		[TagDetails(Tag = 22, Type = TagType.String, Offset = 19, Required = false)]
		public string? IDSource { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 20, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 200, Type = TagType.MonthYear, Offset = 21, Required = false)]
		public MonthYear? MaturityMonthYear { get; set; }
		
		[TagDetails(Tag = 205, Type = TagType.String, Offset = 22, Required = false)]
		public string? MaturityDay { get; set; }
		
		[TagDetails(Tag = 201, Type = TagType.Int, Offset = 23, Required = false)]
		public int? PutOrCall { get; set; }
		
		[TagDetails(Tag = 202, Type = TagType.Float, Offset = 24, Required = false)]
		public double? StrikePrice { get; set; }
		
		[TagDetails(Tag = 206, Type = TagType.String, Offset = 25, Required = false)]
		public string? OptAttribute { get; set; }
		
		[TagDetails(Tag = 231, Type = TagType.Float, Offset = 26, Required = false)]
		public double? ContractMultiplier { get; set; }
		
		[TagDetails(Tag = 223, Type = TagType.Float, Offset = 27, Required = false)]
		public double? CouponRate { get; set; }
		
		[TagDetails(Tag = 207, Type = TagType.String, Offset = 28, Required = false)]
		public string? SecurityExchange { get; set; }
		
		[TagDetails(Tag = 106, Type = TagType.String, Offset = 29, Required = false)]
		public string? Issuer { get; set; }
		
		[TagDetails(Tag = 348, Type = TagType.Length, Offset = 30, Required = false, LinksToTag = 349)]
		public int? EncodedIssuerLen { get; set; }
		
		[TagDetails(Tag = 349, Type = TagType.RawData, Offset = 31, Required = false, LinksToTag = 348)]
		public byte[]? EncodedIssuer { get; set; }
		
		[TagDetails(Tag = 107, Type = TagType.String, Offset = 32, Required = false)]
		public string? SecurityDesc { get; set; }
		
		[TagDetails(Tag = 350, Type = TagType.Length, Offset = 33, Required = false, LinksToTag = 351)]
		public int? EncodedSecurityDescLen { get; set; }
		
		[TagDetails(Tag = 351, Type = TagType.RawData, Offset = 34, Required = false, LinksToTag = 350)]
		public byte[]? EncodedSecurityDesc { get; set; }
		
		[TagDetails(Tag = 140, Type = TagType.Float, Offset = 35, Required = false)]
		public double? PrevClosePx { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 36, Required = true)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 401, Type = TagType.Int, Offset = 37, Required = false)]
		public int? SideValueInd { get; set; }
		
		[TagDetails(Tag = 114, Type = TagType.Boolean, Offset = 38, Required = false)]
		public bool? LocateReqd { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 39, Required = false)]
		public DateTime? TransactTime { get; set; }
		
		[TagDetails(Tag = 38, Type = TagType.Float, Offset = 40, Required = false)]
		public double? OrderQty { get; set; }
		
		[TagDetails(Tag = 152, Type = TagType.Float, Offset = 41, Required = false)]
		public double? CashOrderQty { get; set; }
		
		[TagDetails(Tag = 40, Type = TagType.String, Offset = 42, Required = false)]
		public string? OrdType { get; set; }
		
		[TagDetails(Tag = 44, Type = TagType.Float, Offset = 43, Required = false)]
		public double? Price { get; set; }
		
		[TagDetails(Tag = 99, Type = TagType.Float, Offset = 44, Required = false)]
		public double? StopPx { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 45, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 46, Required = false)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 47, Required = false)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(Tag = 23, Type = TagType.String, Offset = 48, Required = false)]
		public string? IOIid { get; set; }
		
		[TagDetails(Tag = 117, Type = TagType.String, Offset = 49, Required = false)]
		public string? QuoteID { get; set; }
		
		[TagDetails(Tag = 59, Type = TagType.String, Offset = 50, Required = false)]
		public string? TimeInForce { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 51, Required = false)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 432, Type = TagType.LocalDate, Offset = 52, Required = false)]
		public DateOnly? ExpireDate { get; set; }
		
		[TagDetails(Tag = 126, Type = TagType.UtcTimestamp, Offset = 53, Required = false)]
		public DateTime? ExpireTime { get; set; }
		
		[TagDetails(Tag = 427, Type = TagType.Int, Offset = 54, Required = false)]
		public int? GTBookingInst { get; set; }
		
		[TagDetails(Tag = 12, Type = TagType.Float, Offset = 55, Required = false)]
		public double? Commission { get; set; }
		
		[TagDetails(Tag = 13, Type = TagType.String, Offset = 56, Required = false)]
		public string? CommType { get; set; }
		
		[TagDetails(Tag = 47, Type = TagType.String, Offset = 57, Required = false)]
		public string? Rule80A { get; set; }
		
		[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 58, Required = false)]
		public bool? ForexReq { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 59, Required = false)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 60, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 61, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 62, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 193, Type = TagType.LocalDate, Offset = 63, Required = false)]
		public DateOnly? FutSettDate2 { get; set; }
		
		[TagDetails(Tag = 192, Type = TagType.Float, Offset = 64, Required = false)]
		public double? OrderQty2 { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 65, Required = false)]
		public string? OpenClose { get; set; }
		
		[TagDetails(Tag = 203, Type = TagType.Int, Offset = 66, Required = false)]
		public int? CoveredOrUncovered { get; set; }
		
		[TagDetails(Tag = 204, Type = TagType.Int, Offset = 67, Required = false)]
		public int? CustomerOrFirm { get; set; }
		
		[TagDetails(Tag = 210, Type = TagType.Float, Offset = 68, Required = false)]
		public double? MaxShow { get; set; }
		
		[TagDetails(Tag = 211, Type = TagType.Float, Offset = 69, Required = false)]
		public double? PegDifference { get; set; }
		
		[TagDetails(Tag = 388, Type = TagType.String, Offset = 70, Required = false)]
		public string? DiscretionInst { get; set; }
		
		[TagDetails(Tag = 389, Type = TagType.Float, Offset = 71, Required = false)]
		public double? DiscretionOffset { get; set; }
		
		[TagDetails(Tag = 439, Type = TagType.String, Offset = 72, Required = false)]
		public string? ClearingFirm { get; set; }
		
		[TagDetails(Tag = 440, Type = TagType.String, Offset = 73, Required = false)]
		public string? ClearingAccount { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				ClOrdID is not null
				&& ListSeqNo is not null
				&& Symbol is not null
				&& Side is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (ListSeqNo is not null) writer.WriteWholeNumber(67, ListSeqNo.Value);
			if (SettlInstMode is not null) writer.WriteString(160, SettlInstMode);
			if (ClientID is not null) writer.WriteString(109, ClientID);
			if (ExecBroker is not null) writer.WriteString(76, ExecBroker);
			if (Account is not null) writer.WriteString(1, Account);
			if (NoAllocs is not null && NoAllocs.Length != 0)
			{
				writer.WriteWholeNumber(78, NoAllocs.Length);
				for (int i = 0; i < NoAllocs.Length; i++)
				{
					((IFixEncoder)NoAllocs[i]).Encode(writer);
				}
			}
			if (SettlmntTyp is not null) writer.WriteString(63, SettlmntTyp);
			if (FutSettDate is not null) writer.WriteLocalDateOnly(64, FutSettDate.Value);
			if (HandlInst is not null) writer.WriteString(21, HandlInst);
			if (ExecInst is not null) writer.WriteString(18, ExecInst);
			if (MinQty is not null) writer.WriteNumber(110, MinQty.Value);
			if (MaxFloor is not null) writer.WriteNumber(111, MaxFloor.Value);
			if (ExDestination is not null) writer.WriteString(100, ExDestination);
			if (NoTradingSessions is not null && NoTradingSessions.Length != 0)
			{
				writer.WriteWholeNumber(386, NoTradingSessions.Length);
				for (int i = 0; i < NoTradingSessions.Length; i++)
				{
					((IFixEncoder)NoTradingSessions[i]).Encode(writer);
				}
			}
			if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
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
			if (PrevClosePx is not null) writer.WriteNumber(140, PrevClosePx.Value);
			if (Side is not null) writer.WriteString(54, Side);
			if (SideValueInd is not null) writer.WriteWholeNumber(401, SideValueInd.Value);
			if (LocateReqd is not null) writer.WriteBoolean(114, LocateReqd.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (OrderQty is not null) writer.WriteNumber(38, OrderQty.Value);
			if (CashOrderQty is not null) writer.WriteNumber(152, CashOrderQty.Value);
			if (OrdType is not null) writer.WriteString(40, OrdType);
			if (Price is not null) writer.WriteNumber(44, Price.Value);
			if (StopPx is not null) writer.WriteNumber(99, StopPx.Value);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
			if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
			if (IOIid is not null) writer.WriteString(23, IOIid);
			if (QuoteID is not null) writer.WriteString(117, QuoteID);
			if (TimeInForce is not null) writer.WriteString(59, TimeInForce);
			if (EffectiveTime is not null) writer.WriteUtcTimeStamp(168, EffectiveTime.Value);
			if (ExpireDate is not null) writer.WriteLocalDateOnly(432, ExpireDate.Value);
			if (ExpireTime is not null) writer.WriteUtcTimeStamp(126, ExpireTime.Value);
			if (GTBookingInst is not null) writer.WriteWholeNumber(427, GTBookingInst.Value);
			if (Commission is not null) writer.WriteNumber(12, Commission.Value);
			if (CommType is not null) writer.WriteString(13, CommType);
			if (Rule80A is not null) writer.WriteString(47, Rule80A);
			if (ForexReq is not null) writer.WriteBoolean(121, ForexReq.Value);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (FutSettDate2 is not null) writer.WriteLocalDateOnly(193, FutSettDate2.Value);
			if (OrderQty2 is not null) writer.WriteNumber(192, OrderQty2.Value);
			if (OpenClose is not null) writer.WriteString(77, OpenClose);
			if (CoveredOrUncovered is not null) writer.WriteWholeNumber(203, CoveredOrUncovered.Value);
			if (CustomerOrFirm is not null) writer.WriteWholeNumber(204, CustomerOrFirm.Value);
			if (MaxShow is not null) writer.WriteNumber(210, MaxShow.Value);
			if (PegDifference is not null) writer.WriteNumber(211, PegDifference.Value);
			if (DiscretionInst is not null) writer.WriteString(388, DiscretionInst);
			if (DiscretionOffset is not null) writer.WriteNumber(389, DiscretionOffset.Value);
			if (ClearingFirm is not null) writer.WriteString(439, ClearingFirm);
			if (ClearingAccount is not null) writer.WriteString(440, ClearingAccount);
		}
	}
}
