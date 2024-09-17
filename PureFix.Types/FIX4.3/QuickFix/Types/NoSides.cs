using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class NoSides : IFixGroup
	{
		[TagDetails(Tag = 54, Type = TagType.String, Offset = 0, Required = true)]
		public string? Side {get; set;}
		
		[TagDetails(Tag = 11, Type = TagType.String, Offset = 1, Required = true)]
		public string? ClOrdID {get; set;}
		
		[TagDetails(Tag = 526, Type = TagType.String, Offset = 2, Required = false)]
		public string? SecondaryClOrdID {get; set;}
		
		[TagDetails(Tag = 583, Type = TagType.String, Offset = 3, Required = false)]
		public string? ClOrdLinkID {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public PartiesComponent? Parties {get; set;}
		
		[TagDetails(Tag = 229, Type = TagType.String, Offset = 5, Required = false)]
		public string? TradeOriginationDate {get; set;}
		
		[TagDetails(Tag = 1, Type = TagType.String, Offset = 6, Required = false)]
		public string? Account {get; set;}
		
		[TagDetails(Tag = 581, Type = TagType.Int, Offset = 7, Required = false)]
		public int? AccountType {get; set;}
		
		[TagDetails(Tag = 589, Type = TagType.String, Offset = 8, Required = false)]
		public string? DayBookingInst {get; set;}
		
		[TagDetails(Tag = 590, Type = TagType.String, Offset = 9, Required = false)]
		public string? BookingUnit {get; set;}
		
		[TagDetails(Tag = 591, Type = TagType.String, Offset = 10, Required = false)]
		public string? PreallocMethod {get; set;}
		
		[Group(NoOfTag = 78, Offset = 11, Required = false)]
		public NoAllocs[]? NoAllocs {get; set;}
		
		[TagDetails(Tag = 465, Type = TagType.Int, Offset = 12, Required = false)]
		public int? QuantityType {get; set;}
		
		[Component(Offset = 13, Required = true)]
		public OrderQtyDataComponent? OrderQtyData {get; set;}
		
		[Component(Offset = 14, Required = false)]
		public CommissionDataComponent? CommissionData {get; set;}
		
		[TagDetails(Tag = 528, Type = TagType.String, Offset = 15, Required = false)]
		public string? OrderCapacity {get; set;}
		
		[TagDetails(Tag = 529, Type = TagType.String, Offset = 16, Required = false)]
		public string? OrderRestrictions {get; set;}
		
		[TagDetails(Tag = 582, Type = TagType.Int, Offset = 17, Required = false)]
		public int? CustOrderCapacity {get; set;}
		
		[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 18, Required = false)]
		public bool? ForexReq {get; set;}
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 19, Required = false)]
		public string? SettlCurrency {get; set;}
		
		[TagDetails(Tag = 58, Type = TagType.String, Offset = 20, Required = false)]
		public string? Text {get; set;}
		
		[TagDetails(Tag = 354, Type = TagType.Length, Offset = 21, Required = false, LinksToTag = 355)]
		public int? EncodedTextLen {get; set;}
		
		[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 22, Required = false, LinksToTag = 354)]
		public byte[]? EncodedText {get; set;}
		
		[TagDetails(Tag = 77, Type = TagType.String, Offset = 23, Required = false)]
		public string? PositionEffect {get; set;}
		
		[TagDetails(Tag = 203, Type = TagType.Int, Offset = 24, Required = false)]
		public int? CoveredOrUncovered {get; set;}
		
		[TagDetails(Tag = 544, Type = TagType.String, Offset = 25, Required = false)]
		public string? CashMargin {get; set;}
		
		[TagDetails(Tag = 635, Type = TagType.String, Offset = 26, Required = false)]
		public string? ClearingFeeIndicator {get; set;}
		
		[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 27, Required = false)]
		public bool? SolicitedFlag {get; set;}
		
		[TagDetails(Tag = 659, Type = TagType.String, Offset = 28, Required = false)]
		public string? SideComplianceID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				Side is not null
				&& ClOrdID is not null
				&& OrderQtyData is not null && ((IFixValidator)OrderQtyData).IsValid(in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Side is not null) writer.WriteString(54, Side);
			if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
			if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
			if (ClOrdLinkID is not null) writer.WriteString(583, ClOrdLinkID);
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (TradeOriginationDate is not null) writer.WriteString(229, TradeOriginationDate);
			if (Account is not null) writer.WriteString(1, Account);
			if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
			if (DayBookingInst is not null) writer.WriteString(589, DayBookingInst);
			if (BookingUnit is not null) writer.WriteString(590, BookingUnit);
			if (PreallocMethod is not null) writer.WriteString(591, PreallocMethod);
			if (NoAllocs is not null && NoAllocs.Length != 0)
			{
				writer.WriteWholeNumber(78, NoAllocs.Length);
				for (int i = 0; i < NoAllocs.Length; i++)
				{
					((IFixEncoder)NoAllocs[i]).Encode(writer);
				}
			}
			if (QuantityType is not null) writer.WriteWholeNumber(465, QuantityType.Value);
			if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
			if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
			if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
			if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
			if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
			if (ForexReq is not null) writer.WriteBoolean(121, ForexReq.Value);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (Text is not null) writer.WriteString(58, Text);
			if (EncodedText is not null)
			{
				writer.WriteWholeNumber(354, EncodedText.Length);
				writer.WriteBuffer(355, EncodedText);
			}
			if (PositionEffect is not null) writer.WriteString(77, PositionEffect);
			if (CoveredOrUncovered is not null) writer.WriteWholeNumber(203, CoveredOrUncovered.Value);
			if (CashMargin is not null) writer.WriteString(544, CashMargin);
			if (ClearingFeeIndicator is not null) writer.WriteString(635, ClearingFeeIndicator);
			if (SolicitedFlag is not null) writer.WriteBoolean(377, SolicitedFlag.Value);
			if (SideComplianceID is not null) writer.WriteString(659, SideComplianceID);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			Side = view.GetString(54);
			ClOrdID = view.GetString(11);
			SecondaryClOrdID = view.GetString(526);
			ClOrdLinkID = view.GetString(583);
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			TradeOriginationDate = view.GetString(229);
			Account = view.GetString(1);
			AccountType = view.GetInt32(581);
			DayBookingInst = view.GetString(589);
			BookingUnit = view.GetString(590);
			PreallocMethod = view.GetString(591);
			if (view.GetView("NoAllocs") is IMessageView viewNoAllocs)
			{
				var count = viewNoAllocs.GroupCount();
				NoAllocs = new NoAllocs[count];
				for (int i = 0; i < count; i++)
				{
					NoAllocs[i] = new();
					((IFixParser)NoAllocs[i]).Parse(viewNoAllocs.GetGroupInstance(i));
				}
			}
			QuantityType = view.GetInt32(465);
			if (view.GetView("OrderQtyData") is IMessageView viewOrderQtyData)
			{
				OrderQtyData = new();
				((IFixParser)OrderQtyData).Parse(viewOrderQtyData);
			}
			if (view.GetView("CommissionData") is IMessageView viewCommissionData)
			{
				CommissionData = new();
				((IFixParser)CommissionData).Parse(viewCommissionData);
			}
			OrderCapacity = view.GetString(528);
			OrderRestrictions = view.GetString(529);
			CustOrderCapacity = view.GetInt32(582);
			ForexReq = view.GetBool(121);
			SettlCurrency = view.GetString(120);
			Text = view.GetString(58);
			EncodedTextLen = view.GetInt32(354);
			EncodedText = view.GetByteArray(355);
			PositionEffect = view.GetString(77);
			CoveredOrUncovered = view.GetInt32(203);
			CashMargin = view.GetString(544);
			ClearingFeeIndicator = view.GetString(635);
			SolicitedFlag = view.GetBool(377);
			SideComplianceID = view.GetString(659);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Side":
					value = Side;
					break;
				case "ClOrdID":
					value = ClOrdID;
					break;
				case "SecondaryClOrdID":
					value = SecondaryClOrdID;
					break;
				case "ClOrdLinkID":
					value = ClOrdLinkID;
					break;
				case "Parties":
					value = Parties;
					break;
				case "TradeOriginationDate":
					value = TradeOriginationDate;
					break;
				case "Account":
					value = Account;
					break;
				case "AccountType":
					value = AccountType;
					break;
				case "DayBookingInst":
					value = DayBookingInst;
					break;
				case "BookingUnit":
					value = BookingUnit;
					break;
				case "PreallocMethod":
					value = PreallocMethod;
					break;
				case "NoAllocs":
					value = NoAllocs;
					break;
				case "QuantityType":
					value = QuantityType;
					break;
				case "OrderQtyData":
					value = OrderQtyData;
					break;
				case "CommissionData":
					value = CommissionData;
					break;
				case "OrderCapacity":
					value = OrderCapacity;
					break;
				case "OrderRestrictions":
					value = OrderRestrictions;
					break;
				case "CustOrderCapacity":
					value = CustOrderCapacity;
					break;
				case "ForexReq":
					value = ForexReq;
					break;
				case "SettlCurrency":
					value = SettlCurrency;
					break;
				case "Text":
					value = Text;
					break;
				case "EncodedTextLen":
					value = EncodedTextLen;
					break;
				case "EncodedText":
					value = EncodedText;
					break;
				case "PositionEffect":
					value = PositionEffect;
					break;
				case "CoveredOrUncovered":
					value = CoveredOrUncovered;
					break;
				case "CashMargin":
					value = CashMargin;
					break;
				case "ClearingFeeIndicator":
					value = ClearingFeeIndicator;
					break;
				case "SolicitedFlag":
					value = SolicitedFlag;
					break;
				case "SideComplianceID":
					value = SideComplianceID;
					break;
				default: return false;
			}
			return true;
		}
	}
}
