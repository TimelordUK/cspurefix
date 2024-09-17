using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix
{
	[MessageType("T", FixVersion.FIX42)]
	public sealed partial class SettlementInstructions : IFixMessage
	{
		[Component(Offset = 0, Required = true)]
		public StandardHeaderComponent? StandardHeader {get; set;}
		
		[TagDetails(Tag = 162, Type = TagType.String, Offset = 1, Required = true)]
		public string? SettlInstID {get; set;}
		
		[TagDetails(Tag = 163, Type = TagType.String, Offset = 2, Required = true)]
		public string? SettlInstTransType {get; set;}
		
		[TagDetails(Tag = 214, Type = TagType.String, Offset = 3, Required = true)]
		public string? SettlInstRefID {get; set;}
		
		[TagDetails(Tag = 160, Type = TagType.String, Offset = 4, Required = true)]
		public string? SettlInstMode {get; set;}
		
		[TagDetails(Tag = 165, Type = TagType.String, Offset = 5, Required = true)]
		public string? SettlInstSource {get; set;}
		
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 6, Required = true)]
		public string? AllocAccount {get; set;}
		
		[TagDetails(Tag = 166, Type = TagType.String, Offset = 7, Required = false)]
		public string? SettlLocation {get; set;}
		
		[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 8, Required = false)]
		public DateOnly? TradeDate {get; set;}
		
		[TagDetails(Tag = 70, Type = TagType.String, Offset = 9, Required = false)]
		public string? AllocID {get; set;}
		
		[TagDetails(Tag = 30, Type = TagType.String, Offset = 10, Required = false)]
		public string? LastMkt {get; set;}
		
		[TagDetails(Tag = 336, Type = TagType.String, Offset = 11, Required = false)]
		public string? TradingSessionID {get; set;}
		
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 12, Required = false)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 167, Type = TagType.String, Offset = 13, Required = false)]
		public string? SecurityType {get; set;}
		
		[TagDetails(Tag = 168, Type = TagType.UtcTimestamp, Offset = 14, Required = false)]
		public DateTime? EffectiveTime {get; set;}
		
		[TagDetails(Tag = 60, Type = TagType.UtcTimestamp, Offset = 15, Required = true)]
		public DateTime? TransactTime {get; set;}
		
		[TagDetails(Tag = 109, Type = TagType.String, Offset = 16, Required = false)]
		public string? ClientID {get; set;}
		
		[TagDetails(Tag = 76, Type = TagType.String, Offset = 17, Required = false)]
		public string? ExecBroker {get; set;}
		
		[TagDetails(Tag = 169, Type = TagType.Int, Offset = 18, Required = false)]
		public int? StandInstDbType {get; set;}
		
		[TagDetails(Tag = 170, Type = TagType.String, Offset = 19, Required = false)]
		public string? StandInstDbName {get; set;}
		
		[TagDetails(Tag = 171, Type = TagType.String, Offset = 20, Required = false)]
		public string? StandInstDbID {get; set;}
		
		[TagDetails(Tag = 172, Type = TagType.Int, Offset = 21, Required = false)]
		public int? SettlDeliveryType {get; set;}
		
		[TagDetails(Tag = 173, Type = TagType.String, Offset = 22, Required = false)]
		public string? SettlDepositoryCode {get; set;}
		
		[TagDetails(Tag = 174, Type = TagType.String, Offset = 23, Required = false)]
		public string? SettlBrkrCode {get; set;}
		
		[TagDetails(Tag = 175, Type = TagType.String, Offset = 24, Required = false)]
		public string? SettlInstCode {get; set;}
		
		[TagDetails(Tag = 176, Type = TagType.String, Offset = 25, Required = false)]
		public string? SecuritySettlAgentName {get; set;}
		
		[TagDetails(Tag = 177, Type = TagType.String, Offset = 26, Required = false)]
		public string? SecuritySettlAgentCode {get; set;}
		
		[TagDetails(Tag = 178, Type = TagType.String, Offset = 27, Required = false)]
		public string? SecuritySettlAgentAcctNum {get; set;}
		
		[TagDetails(Tag = 179, Type = TagType.String, Offset = 28, Required = false)]
		public string? SecuritySettlAgentAcctName {get; set;}
		
		[TagDetails(Tag = 180, Type = TagType.String, Offset = 29, Required = false)]
		public string? SecuritySettlAgentContactName {get; set;}
		
		[TagDetails(Tag = 181, Type = TagType.String, Offset = 30, Required = false)]
		public string? SecuritySettlAgentContactPhone {get; set;}
		
		[TagDetails(Tag = 182, Type = TagType.String, Offset = 31, Required = false)]
		public string? CashSettlAgentName {get; set;}
		
		[TagDetails(Tag = 183, Type = TagType.String, Offset = 32, Required = false)]
		public string? CashSettlAgentCode {get; set;}
		
		[TagDetails(Tag = 184, Type = TagType.String, Offset = 33, Required = false)]
		public string? CashSettlAgentAcctNum {get; set;}
		
		[TagDetails(Tag = 185, Type = TagType.String, Offset = 34, Required = false)]
		public string? CashSettlAgentAcctName {get; set;}
		
		[TagDetails(Tag = 186, Type = TagType.String, Offset = 35, Required = false)]
		public string? CashSettlAgentContactName {get; set;}
		
		[TagDetails(Tag = 187, Type = TagType.String, Offset = 36, Required = false)]
		public string? CashSettlAgentContactPhone {get; set;}
		
		[Component(Offset = 37, Required = true)]
		public StandardTrailerComponent? StandardTrailer {get; set;}
		
		IStandardHeader? IFixMessage.StandardHeader => StandardHeader;
		
		IStandardTrailer? IFixMessage.StandardTrailer => StandardTrailer;
		
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
			if (SettlLocation is not null) writer.WriteString(166, SettlLocation);
			if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
			if (AllocID is not null) writer.WriteString(70, AllocID);
			if (LastMkt is not null) writer.WriteString(30, LastMkt);
			if (TradingSessionID is not null) writer.WriteString(336, TradingSessionID);
			if (Side is not null) writer.WriteString(54, Side);
			if (SecurityType is not null) writer.WriteString(167, SecurityType);
			if (EffectiveTime is not null) writer.WriteUtcTimeStamp(168, EffectiveTime.Value);
			if (TransactTime is not null) writer.WriteUtcTimeStamp(60, TransactTime.Value);
			if (ClientID is not null) writer.WriteString(109, ClientID);
			if (ExecBroker is not null) writer.WriteString(76, ExecBroker);
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
			if (StandardTrailer is not null) ((IFixEncoder)StandardTrailer).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("StandardHeader") is IMessageView viewStandardHeader)
			{
				StandardHeader = new();
				((IFixParser)StandardHeader).Parse(viewStandardHeader);
			}
			SettlInstID = view.GetString(162);
			SettlInstTransType = view.GetString(163);
			SettlInstRefID = view.GetString(214);
			SettlInstMode = view.GetString(160);
			SettlInstSource = view.GetString(165);
			AllocAccount = view.GetString(79);
			SettlLocation = view.GetString(166);
			TradeDate = view.GetDateOnly(75);
			AllocID = view.GetString(70);
			LastMkt = view.GetString(30);
			TradingSessionID = view.GetString(336);
			Side = view.GetString(54);
			SecurityType = view.GetString(167);
			EffectiveTime = view.GetDateTime(168);
			TransactTime = view.GetDateTime(60);
			ClientID = view.GetString(109);
			ExecBroker = view.GetString(76);
			StandInstDbType = view.GetInt32(169);
			StandInstDbName = view.GetString(170);
			StandInstDbID = view.GetString(171);
			SettlDeliveryType = view.GetInt32(172);
			SettlDepositoryCode = view.GetString(173);
			SettlBrkrCode = view.GetString(174);
			SettlInstCode = view.GetString(175);
			SecuritySettlAgentName = view.GetString(176);
			SecuritySettlAgentCode = view.GetString(177);
			SecuritySettlAgentAcctNum = view.GetString(178);
			SecuritySettlAgentAcctName = view.GetString(179);
			SecuritySettlAgentContactName = view.GetString(180);
			SecuritySettlAgentContactPhone = view.GetString(181);
			CashSettlAgentName = view.GetString(182);
			CashSettlAgentCode = view.GetString(183);
			CashSettlAgentAcctNum = view.GetString(184);
			CashSettlAgentAcctName = view.GetString(185);
			CashSettlAgentContactName = view.GetString(186);
			CashSettlAgentContactPhone = view.GetString(187);
			if (view.GetView("StandardTrailer") is IMessageView viewStandardTrailer)
			{
				StandardTrailer = new();
				((IFixParser)StandardTrailer).Parse(viewStandardTrailer);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "StandardHeader":
					value = StandardHeader;
					break;
				case "SettlInstID":
					value = SettlInstID;
					break;
				case "SettlInstTransType":
					value = SettlInstTransType;
					break;
				case "SettlInstRefID":
					value = SettlInstRefID;
					break;
				case "SettlInstMode":
					value = SettlInstMode;
					break;
				case "SettlInstSource":
					value = SettlInstSource;
					break;
				case "AllocAccount":
					value = AllocAccount;
					break;
				case "SettlLocation":
					value = SettlLocation;
					break;
				case "TradeDate":
					value = TradeDate;
					break;
				case "AllocID":
					value = AllocID;
					break;
				case "LastMkt":
					value = LastMkt;
					break;
				case "TradingSessionID":
					value = TradingSessionID;
					break;
				case "Side":
					value = Side;
					break;
				case "SecurityType":
					value = SecurityType;
					break;
				case "EffectiveTime":
					value = EffectiveTime;
					break;
				case "TransactTime":
					value = TransactTime;
					break;
				case "ClientID":
					value = ClientID;
					break;
				case "ExecBroker":
					value = ExecBroker;
					break;
				case "StandInstDbType":
					value = StandInstDbType;
					break;
				case "StandInstDbName":
					value = StandInstDbName;
					break;
				case "StandInstDbID":
					value = StandInstDbID;
					break;
				case "SettlDeliveryType":
					value = SettlDeliveryType;
					break;
				case "SettlDepositoryCode":
					value = SettlDepositoryCode;
					break;
				case "SettlBrkrCode":
					value = SettlBrkrCode;
					break;
				case "SettlInstCode":
					value = SettlInstCode;
					break;
				case "SecuritySettlAgentName":
					value = SecuritySettlAgentName;
					break;
				case "SecuritySettlAgentCode":
					value = SecuritySettlAgentCode;
					break;
				case "SecuritySettlAgentAcctNum":
					value = SecuritySettlAgentAcctNum;
					break;
				case "SecuritySettlAgentAcctName":
					value = SecuritySettlAgentAcctName;
					break;
				case "SecuritySettlAgentContactName":
					value = SecuritySettlAgentContactName;
					break;
				case "SecuritySettlAgentContactPhone":
					value = SecuritySettlAgentContactPhone;
					break;
				case "CashSettlAgentName":
					value = CashSettlAgentName;
					break;
				case "CashSettlAgentCode":
					value = CashSettlAgentCode;
					break;
				case "CashSettlAgentAcctNum":
					value = CashSettlAgentAcctNum;
					break;
				case "CashSettlAgentAcctName":
					value = CashSettlAgentAcctName;
					break;
				case "CashSettlAgentContactName":
					value = CashSettlAgentContactName;
					break;
				case "CashSettlAgentContactPhone":
					value = CashSettlAgentContactPhone;
					break;
				case "StandardTrailer":
					value = StandardTrailer;
					break;
				default: return false;
			}
			return true;
		}
	}
}
