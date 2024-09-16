using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class TradeCaptureReportNoSides : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 0, Required = true)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 37, Type = TagType.String, Offset = 1, Required = true)]
		public string? OrderID { get; set; }
		
		[TagDetails(Tag = 198, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryOrderID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 3, Required = false)]
		public string? ClOrdID { get; set; }
		
		[Component(Offset = 4, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 5, Required = false)]
		public string? Account { get; set; }
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 6, Required = false)]
		public int? AccountType { get; set; }
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 7, Required = false)]
		public string? ProcessCode { get; set; }
		
		[TagDetails(Tag = 575, Type = TagType.Boolean, Offset = 8, Required = false)]
		public bool? OddLot { get; set; }
		
		[Group(NoOfTag = 576, Offset = 9, Required = false)]
		public TradeCaptureReportNoSidesNoClearingInstructions[]? NoClearingInstructions { get; set; }
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 10, Required = false)]
		public string? ClearingFeeIndicator { get; set; }
		
		[TagDetails(Tag = 578, Type = TagType.String, Offset = 11, Required = false)]
		public string? TradeInputSource { get; set; }
		
		[TagDetails(Tag = 579, Type = TagType.String, Offset = 12, Required = false)]
		public string? TradeInputDevice { get; set; }
		
		[TagDetails(Tag = 15, Type = TagType.String, Offset = 13, Required = false)]
		public string? Currency { get; set; }
		
		[TagDetails(Tag = 376, Type = TagType.String, Offset = 14, Required = false)]
		public string? ComplianceID { get; set; }
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 15, Required = false)]
		public bool? SolicitedFlag { get; set; }
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 16, Required = false)]
		public string? OrderCapacity { get; set; }
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 17, Required = false)]
		public string? OrderRestrictions { get; set; }
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 18, Required = false)]
		public int? CustOrderCapacity { get; set; }
		
		[TagDetails(Tag = 483, Type = TagType.UtcTimestamp, Offset = 19, Required = false)]
		public DateTime? TransBkdTime { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 20, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 21, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[Component(Offset = 22, Required = false)]
		public CommissionData? CommissionData { get; set; }
		
		[TagDetails(Tag = 381, Type = TagType.Float, Offset = 23, Required = false)]
		public double? GrossTradeAmt { get; set; }
		
		[TagDetails(Tag = 157, Type = TagType.Int, Offset = 24, Required = false)]
		public int? NumDaysInterest { get; set; }
		
		[TagDetails(Tag = 230, Type = TagType.String, Offset = 25, Required = false)]
		public string? ExDate { get; set; }
		
		[TagDetails(Tag = 158, Type = TagType.Float, Offset = 26, Required = false)]
		public double? AccruedInterestRate { get; set; }
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 27, Required = false)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 238, Type = TagType.Float, Offset = 28, Required = false)]
		public double? Concession { get; set; }
		
		[TagDetails(Tag = 237, Type = TagType.Float, Offset = 29, Required = false)]
		public double? TotalTakedown { get; set; }
		
		[TagDetails(Tag = 118, Type = TagType.Float, Offset = 30, Required = false)]
		public double? NetMoney { get; set; }
		
		[TagDetails(Tag = 119, Type = TagType.Float, Offset = 31, Required = false)]
		public double? SettlCurrAmt { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 32, Required = false)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 155, Type = TagType.Float, Offset = 33, Required = false)]
		public double? SettlCurrFxRate { get; set; }
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 34, Required = false)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 35, Required = false)]
		public string? PositionEffect { get; set; }
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 36, Required = false)]
		public string? Text { get; set; }
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 37, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen { get; set; }
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 38, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText { get; set; }
		
		[TagDetails(Tag = 442, Type = TagType.String, Offset = 39, Required = false)]
		public string? MultiLegReportingType { get; set; }
		
		[Group(NoOfTag = 518, Offset = 40, Required = false)]
		public TradeCaptureReportNoSidesNoContAmts[]? NoContAmts { get; set; }
		
		[Group(NoOfTag = 136, Offset = 41, Required = false)]
		public TradeCaptureReportNoSidesNoMiscFees[]? NoMiscFees { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				Side is not null
				&& OrderID is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Side is not null) writer.WriteString(54, Side);
			if (OrderID is not null) writer.WriteString(37, OrderID);
			if (SecondaryOrderID is not null) writer.WriteString(198, SecondaryOrderID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (Account is not null) writer.WriteString(1, Account);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
			if (OddLot is not null) writer.WriteBoolean(575, OddLot.Value);
			if (NoClearingInstructions is not null && NoClearingInstructions.Length != 0)
			{
				writer.WriteWholeNumber(576, NoClearingInstructions.Length);
				for (int i = 0; i < NoClearingInstructions.Length; i++)
				{
					((IFixEncoder)NoClearingInstructions[i]).Encode(writer);
				}
			}
			if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
			if (TradeInputSource is not null) writer.WriteString(578, TradeInputSource);
			if (TradeInputDevice is not null) writer.WriteString(579, TradeInputDevice);
			if (Currency is not null) writer.WriteString(15, Currency);
			if (ComplianceID is not null) writer.WriteString(376, ComplianceID);
			if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
			if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
			if (TransBkdTime is not null) writer.WriteUtcTimeStamp(483, TransBkdTime.Value);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
			if (GrossTradeAmt is not null) writer.WriteNumber(381, GrossTradeAmt.Value);
			if (NumDaysInterest is not null) writer.WriteWholeNumber(157, NumDaysInterest.Value);
			if (ExDate is not null) writer.WriteString(230, ExDate);
			if (AccruedInterestRate is not null) writer.WriteNumber(158, AccruedInterestRate.Value);
			if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
			if (Concession is not null) writer.WriteNumber(238, Concession.Value);
			if (TotalTakedown is not null) writer.WriteNumber(237, TotalTakedown.Value);
			if (NetMoney is not null) writer.WriteNumber(118, NetMoney.Value);
			if (SettlCurrAmt is not null) writer.WriteNumber(119, SettlCurrAmt.Value);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (SettlCurrFxRate is not null) writer.WriteNumber(155, SettlCurrFxRate.Value);
			if (SettlCurrFxRateCalc is not null) writer.WriteString(156, SettlCurrFxRateCalc);
			if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (MultiLegReportingType is not null) writer.WriteString(442, MultiLegReportingType);
			if (NoContAmts is not null && NoContAmts.Length != 0)
			{
				writer.WriteWholeNumber(518, NoContAmts.Length);
				for (int i = 0; i < NoContAmts.Length; i++)
				{
					((IFixEncoder)NoContAmts[i]).Encode(writer);
				}
			}
			if (NoMiscFees is not null && NoMiscFees.Length != 0)
			{
				writer.WriteWholeNumber(136, NoMiscFees.Length);
				for (int i = 0; i < NoMiscFees.Length; i++)
				{
					((IFixEncoder)NoMiscFees[i]).Encode(writer);
				}
			}
		}
	}
}
