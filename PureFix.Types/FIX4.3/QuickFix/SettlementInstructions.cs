using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix
{
	[MessageType("T", FixVersion.FIX43)]
	public sealed partial class SettlementInstructions : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeader? StandardHeader { get; set; }
		
		[TagDetails(Tag = 162, Type = TagType.String, Offset = 1, Required = true)]
		public string? SettlInstID { get; set; }
		
		[TagDetails(Tag = 163, Type = TagType.String, Offset = 2, Required = true)]
		public string? SettlInstTransType { get; set; }
		
		[TagDetails(Tag = 214, Type = TagType.String, Offset = 3, Required = true)]
		public string? SettlInstRefID { get; set; }
		
		[TagDetails(Tag = 160, Type = TagType.String, Offset = 4, Required = true)]
		public string? SettlInstMode { get; set; }
		
		[TagDetails(Tag = 165, Type = TagType.String, Offset = 5, Required = true)]
		public string? SettlInstSource { get; set; }
		
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 6, Required = true)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 467, Type = TagType.String, Offset = 7, Required = false)]
		public string? IndividualAllocID { get; set; }
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 8, Required = false)]
		public string? ClOrdID { get; set; }
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 9, Required = false)]
		public DateOnly? TradeDate { get; set; }
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 10, Required = false)]
		public string? AllocID { get; set; }
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 11, Required = false)]
		public string? LastMkt { get; set; }
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 12, Required = false)]
		public string? TradingSessionID { get; set; }
		
		[TagDetails(Tag = 625, Type = TagType.String, Offset = 13, Required = false)]
		public string? TradingSessionSubID { get; set; }
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 14, Required = false)]
		public string? Side { get; set; }
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 15, Required = false)]
		public string? SecurityType { get; set; }
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 16, Required = false)]
		public DateTime? EffectiveTime { get; set; }
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 17, Required = true)]
		public DateTime? TransactTime { get; set; }
		
		[Component(Offset = 18, Required = false)]
		public Parties? Parties { get; set; }
		
		[TagDetails(Tag = 169, Type = TagType.Int, Offset = 19, Required = false)]
		public int? StandInstDbType { get; set; }
		
		[TagDetails(Tag = 170, Type = TagType.String, Offset = 20, Required = false)]
		public string? StandInstDbName { get; set; }
		
		[TagDetails(Tag = 171, Type = TagType.String, Offset = 21, Required = false)]
		public string? StandInstDbID { get; set; }
		
		[TagDetails(Tag = 172, Type = TagType.Int, Offset = 22, Required = false)]
		public int? SettlDeliveryType { get; set; }
		
		[TagDetails(Tag = 173, Type = TagType.String, Offset = 23, Required = false)]
		public string? SettlDepositoryCode { get; set; }
		
		[TagDetails(Tag = 174, Type = TagType.String, Offset = 24, Required = false)]
		public string? SettlBrkrCode { get; set; }
		
		[TagDetails(Tag = 175, Type = TagType.String, Offset = 25, Required = false)]
		public string? SettlInstCode { get; set; }
		
		[TagDetails(Tag = 176, Type = TagType.String, Offset = 26, Required = false)]
		public string? SecuritySettlAgentName { get; set; }
		
		[TagDetails(Tag = 177, Type = TagType.String, Offset = 27, Required = false)]
		public string? SecuritySettlAgentCode { get; set; }
		
		[TagDetails(Tag = 178, Type = TagType.String, Offset = 28, Required = false)]
		public string? SecuritySettlAgentAcctNum { get; set; }
		
		[TagDetails(Tag = 179, Type = TagType.String, Offset = 29, Required = false)]
		public string? SecuritySettlAgentAcctName { get; set; }
		
		[TagDetails(Tag = 180, Type = TagType.String, Offset = 30, Required = false)]
		public string? SecuritySettlAgentContactName { get; set; }
		
		[TagDetails(Tag = 181, Type = TagType.String, Offset = 31, Required = false)]
		public string? SecuritySettlAgentContactPhone { get; set; }
		
		[TagDetails(Tag = 182, Type = TagType.String, Offset = 32, Required = false)]
		public string? CashSettlAgentName { get; set; }
		
		[TagDetails(Tag = 183, Type = TagType.String, Offset = 33, Required = false)]
		public string? CashSettlAgentCode { get; set; }
		
		[TagDetails(Tag = 184, Type = TagType.String, Offset = 34, Required = false)]
		public string? CashSettlAgentAcctNum { get; set; }
		
		[TagDetails(Tag = 185, Type = TagType.String, Offset = 35, Required = false)]
		public string? CashSettlAgentAcctName { get; set; }
		
		[TagDetails(Tag = 186, Type = TagType.String, Offset = 36, Required = false)]
		public string? CashSettlAgentContactName { get; set; }
		
		[TagDetails(Tag = 187, Type = TagType.String, Offset = 37, Required = false)]
		public string? CashSettlAgentContactPhone { get; set; }
		
		[TagDetails(Tag = 492, Type = TagType.Int, Offset = 38, Required = false)]
		public int? PaymentMethod { get; set; }
		
		[TagDetails(Tag = 476, Type = TagType.String, Offset = 39, Required = false)]
		public string? PaymentRef { get; set; }
		
		[TagDetails(Tag = 488, Type = TagType.String, Offset = 40, Required = false)]
		public string? CardHolderName { get; set; }
		
		[TagDetails(Tag = 489, Type = TagType.String, Offset = 41, Required = false)]
		public string? CardNumber { get; set; }
		
		[TagDetails(Tag = 503, Type = TagType.LocalDate, Offset = 42, Required = false)]
		public DateOnly? CardStartDate { get; set; }
		
		[TagDetails(Tag = 490, Type = TagType.LocalDate, Offset = 43, Required = false)]
		public DateOnly? CardExpDate { get; set; }
		
		[TagDetails(Tag = 491, Type = TagType.String, Offset = 44, Required = false)]
		public string? CardIssNo { get; set; }
		
		[TagDetails(Tag = 504, Type = TagType.LocalDate, Offset = 45, Required = false)]
		public DateOnly? PaymentDate { get; set; }
		
		[TagDetails(Tag = 505, Type = TagType.String, Offset = 46, Required = false)]
		public string? PaymentRemitterID { get; set; }
		
		[Component(Offset = 47, Required = true)]
		public StandardTrailer? StandardTrailer { get; set; }
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				(!config.CheckStandardHeader || (StandardHeader is not null && ((IFixValidator)StandardHeader).IsValid(in config)))
				&& SettlInstID is not null
				&& SettlInstTransType is not null
				&& SettlInstRefID is not null
				&& SettlInstMode is not null
				&& SettlInstSource is not null
				&& AllocAccount is not null
				&& TransactTime is not null
				&& (!config.CheckStandardTrailer || (StandardTrailer is not null && ((IFixValidator)StandardTrailer).IsValid(in config)));
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (StandardHeader is not null) ((IFixEncoder)StandardHeader).Encode(writer);
			if (SettlInstID is not null) writer.WriteString(162, SettlInstID);
			if (SettlInstTransType is not null) writer.WriteString(163, SettlInstTransType);
			if (SettlInstRefID is not null) writer.WriteString(214, SettlInstRefID);
			if (SettlInstMode is not null) writer.WriteString(160, SettlInstMode);
			if (SettlInstSource is not null) writer.WriteString(165, SettlInstSource);
			if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
			if (IndividualAllocID is not null) writer.WriteString(467, IndividualAllocID);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (AllocID is not null) writer.WriteString(70, AllocID);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (TradingSessionSubID is not null) writer.WriteString(625, TradingSessionSubID);
			if (Side is not null) writer.WriteString(54, Side);
			if (SecurityType is not null) writer.WriteString(167, SecurityType);
			if (EffectiveTime is not null) writer.WriteUtcTimeStamp(168, EffectiveTime.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (StandInstDbType is not null) writer.WriteWholeNumber(169, StandInstDbType.Value);
			if (StandInstDbName is not null) writer.WriteString(170, StandInstDbName);
			if (StandInstDbID is not null) writer.WriteString(171, StandInstDbID);
			if (SettlDeliveryType is not null) writer.WriteWholeNumber(172, SettlDeliveryType.Value);
			if (SettlDepositoryCode is not null) writer.WriteString(173, SettlDepositoryCode);
			if (SettlBrkrCode is not null) writer.WriteString(174, SettlBrkrCode);
			if (SettlInstCode is not null) writer.WriteString(175, SettlInstCode);
			if (SecuritySettlAgentName is not null) writer.WriteString(176, SecuritySettlAgentName);
			if (SecuritySettlAgentCode is not null) writer.WriteString(177, SecuritySettlAgentCode);
			if (SecuritySettlAgentAcctNum is not null) writer.WriteString(178, SecuritySettlAgentAcctNum);
			if (SecuritySettlAgentAcctName is not null) writer.WriteString(179, SecuritySettlAgentAcctName);
			if (SecuritySettlAgentContactName is not null) writer.WriteString(180, SecuritySettlAgentContactName);
			if (SecuritySettlAgentContactPhone is not null) writer.WriteString(181, SecuritySettlAgentContactPhone);
			if (CashSettlAgentName is not null) writer.WriteString(182, CashSettlAgentName);
			if (CashSettlAgentCode is not null) writer.WriteString(183, CashSettlAgentCode);
			if (CashSettlAgentAcctNum is not null) writer.WriteString(184, CashSettlAgentAcctNum);
			if (CashSettlAgentAcctName is not null) writer.WriteString(185, CashSettlAgentAcctName);
			if (CashSettlAgentContactName is not null) writer.WriteString(186, CashSettlAgentContactName);
			if (CashSettlAgentContactPhone is not null) writer.WriteString(187, CashSettlAgentContactPhone);
			if (PaymentMethod is not null) writer.WriteWholeNumber(492, PaymentMethod.Value);
			if (PaymentRef is not null) writer.WriteString(476, PaymentRef);
			if (CardHolderName is not null) writer.WriteString(488, CardHolderName);
			if (CardNumber is not null) writer.WriteString(489, CardNumber);
			if (CardStartDate is not null) writer.WriteLocalDateOnly(503, CardStartDate.Value);
			if (CardExpDate is not null) writer.WriteLocalDateOnly(490, CardExpDate.Value);
			if (CardIssNo is not null) writer.WriteString(491, CardIssNo);
			if (PaymentDate is not null) writer.WriteLocalDateOnly(504, PaymentDate.Value);
			if (PaymentRemitterID is not null) writer.WriteString(505, PaymentRemitterID);
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
	}
}
