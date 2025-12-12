using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44.Components
{
	public sealed partial class SideCrossOrdModGrp : IFixComponent
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
			public Parties? Parties {get; set;}
			
			[TagDetails(Tag = 229, Type = TagType.LocalDate, Offset = 5, Required = false)]
			public DateOnly? TradeOriginationDate {get; set;}
			
			[TagDetails(Tag = 75, Type = TagType.LocalDate, Offset = 6, Required = false)]
			public DateOnly? TradeDate {get; set;}
			
			[TagDetails(Tag = 1, Type = TagType.String, Offset = 7, Required = false)]
			public string? Account {get; set;}
			
			[TagDetails(Tag = 660, Type = TagType.Int, Offset = 8, Required = false)]
			public int? AcctIDSource {get; set;}
			
			[TagDetails(Tag = 581, Type = TagType.Int, Offset = 9, Required = false)]
			public int? AccountType {get; set;}
			
			[TagDetails(Tag = 589, Type = TagType.String, Offset = 10, Required = false)]
			public string? DayBookingInst {get; set;}
			
			[TagDetails(Tag = 590, Type = TagType.String, Offset = 11, Required = false)]
			public string? BookingUnit {get; set;}
			
			[TagDetails(Tag = 591, Type = TagType.String, Offset = 12, Required = false)]
			public string? PreallocMethod {get; set;}
			
			[TagDetails(Tag = 70, Type = TagType.String, Offset = 13, Required = false)]
			public string? AllocID {get; set;}
			
			[Component(Offset = 14, Required = false)]
			public PreAllocGrp? PreAllocGrp {get; set;}
			
			[TagDetails(Tag = 854, Type = TagType.Int, Offset = 15, Required = false)]
			public int? QtyType {get; set;}
			
			[Component(Offset = 16, Required = true)]
			public OrderQtyData? OrderQtyData {get; set;}
			
			[Component(Offset = 17, Required = false)]
			public CommissionData? CommissionData {get; set;}
			
			[TagDetails(Tag = 528, Type = TagType.String, Offset = 18, Required = false)]
			public string? OrderCapacity {get; set;}
			
			[TagDetails(Tag = 529, Type = TagType.String, Offset = 19, Required = false)]
			public string? OrderRestrictions {get; set;}
			
			[TagDetails(Tag = 582, Type = TagType.Int, Offset = 20, Required = false)]
			public int? CustOrderCapacity {get; set;}
			
			[TagDetails(Tag = 121, Type = TagType.Boolean, Offset = 21, Required = false)]
			public bool? ForexReq {get; set;}
			
			[TagDetails(Tag = 120, Type = TagType.String, Offset = 22, Required = false)]
			public string? SettlCurrency {get; set;}
			
			[TagDetails(Tag = 775, Type = TagType.Int, Offset = 23, Required = false)]
			public int? BookingType {get; set;}
			
			[TagDetails(Tag = 58, Type = TagType.String, Offset = 24, Required = false)]
			public string? Text {get; set;}
			
			[TagDetails(Tag = 354, Type = TagType.Length, Offset = 25, Required = false)]
			public int? EncodedTextLen {get; set;}
			
			[TagDetails(Tag = 355, Type = TagType.RawData, Offset = 26, Required = false)]
			public byte[]? EncodedText {get; set;}
			
			[TagDetails(Tag = 77, Type = TagType.String, Offset = 27, Required = false)]
			public string? PositionEffect {get; set;}
			
			[TagDetails(Tag = 203, Type = TagType.Int, Offset = 28, Required = false)]
			public int? CoveredOrUncovered {get; set;}
			
			[TagDetails(Tag = 544, Type = TagType.String, Offset = 29, Required = false)]
			public string? CashMargin {get; set;}
			
			[TagDetails(Tag = 635, Type = TagType.String, Offset = 30, Required = false)]
			public string? ClearingFeeIndicator {get; set;}
			
			[TagDetails(Tag = 377, Type = TagType.Boolean, Offset = 31, Required = false)]
			public bool? SolicitedFlag {get; set;}
			
			[TagDetails(Tag = 659, Type = TagType.String, Offset = 32, Required = false)]
			public string? SideComplianceID {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (Side is not null) writer.WriteString(54, Side);
				if (ClOrdID is not null) writer.WriteString(11, ClOrdID);
				if (SecondaryClOrdID is not null) writer.WriteString(526, SecondaryClOrdID);
				if (ClOrdLinkID is not null) writer.WriteString(583, ClOrdLinkID);
				if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
				if (TradeOriginationDate is not null) writer.WriteLocalDateOnly(229, TradeOriginationDate.Value);
				if (TradeDate is not null) writer.WriteLocalDateOnly(75, TradeDate.Value);
				if (Account is not null) writer.WriteString(1, Account);
				if (AcctIDSource is not null) writer.WriteWholeNumber(660, AcctIDSource.Value);
				if (AccountType is not null) writer.WriteWholeNumber(581, AccountType.Value);
				if (DayBookingInst is not null) writer.WriteString(589, DayBookingInst);
				if (BookingUnit is not null) writer.WriteString(590, BookingUnit);
				if (PreallocMethod is not null) writer.WriteString(591, PreallocMethod);
				if (AllocID is not null) writer.WriteString(70, AllocID);
				if (PreAllocGrp is not null) ((IFixEncoder)PreAllocGrp).Encode(writer);
				if (QtyType is not null) writer.WriteWholeNumber(854, QtyType.Value);
				if (OrderQtyData is not null) ((IFixEncoder)OrderQtyData).Encode(writer);
				if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
				if (OrderCapacity is not null) writer.WriteString(528, OrderCapacity);
				if (OrderRestrictions is not null) writer.WriteString(529, OrderRestrictions);
				if (CustOrderCapacity is not null) writer.WriteWholeNumber(582, CustOrderCapacity.Value);
				if (ForexReq is not null) writer.WriteBoolean(121, ForexReq.Value);
				if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
				if (BookingType is not null) writer.WriteWholeNumber(775, BookingType.Value);
				if (Text is not null) writer.WriteString(58, Text);
				if (EncodedTextLen is not null) writer.WriteWholeNumber(354, EncodedTextLen.Value);
				if (EncodedText is not null) writer.WriteBuffer(355, EncodedText);
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
				TradeOriginationDate = view.GetDateOnly(229);
				TradeDate = view.GetDateOnly(75);
				Account = view.GetString(1);
				AcctIDSource = view.GetInt32(660);
				AccountType = view.GetInt32(581);
				DayBookingInst = view.GetString(589);
				BookingUnit = view.GetString(590);
				PreallocMethod = view.GetString(591);
				AllocID = view.GetString(70);
				if (view.GetView("PreAllocGrp") is IMessageView viewPreAllocGrp)
				{
					PreAllocGrp = new();
					((IFixParser)PreAllocGrp).Parse(viewPreAllocGrp);
				}
				QtyType = view.GetInt32(854);
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
				BookingType = view.GetInt32(775);
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
					{
						value = Side;
						break;
					}
					case "ClOrdID":
					{
						value = ClOrdID;
						break;
					}
					case "SecondaryClOrdID":
					{
						value = SecondaryClOrdID;
						break;
					}
					case "ClOrdLinkID":
					{
						value = ClOrdLinkID;
						break;
					}
					case "Parties":
					{
						value = Parties;
						break;
					}
					case "TradeOriginationDate":
					{
						value = TradeOriginationDate;
						break;
					}
					case "TradeDate":
					{
						value = TradeDate;
						break;
					}
					case "Account":
					{
						value = Account;
						break;
					}
					case "AcctIDSource":
					{
						value = AcctIDSource;
						break;
					}
					case "AccountType":
					{
						value = AccountType;
						break;
					}
					case "DayBookingInst":
					{
						value = DayBookingInst;
						break;
					}
					case "BookingUnit":
					{
						value = BookingUnit;
						break;
					}
					case "PreallocMethod":
					{
						value = PreallocMethod;
						break;
					}
					case "AllocID":
					{
						value = AllocID;
						break;
					}
					case "PreAllocGrp":
					{
						value = PreAllocGrp;
						break;
					}
					case "QtyType":
					{
						value = QtyType;
						break;
					}
					case "OrderQtyData":
					{
						value = OrderQtyData;
						break;
					}
					case "CommissionData":
					{
						value = CommissionData;
						break;
					}
					case "OrderCapacity":
					{
						value = OrderCapacity;
						break;
					}
					case "OrderRestrictions":
					{
						value = OrderRestrictions;
						break;
					}
					case "CustOrderCapacity":
					{
						value = CustOrderCapacity;
						break;
					}
					case "ForexReq":
					{
						value = ForexReq;
						break;
					}
					case "SettlCurrency":
					{
						value = SettlCurrency;
						break;
					}
					case "BookingType":
					{
						value = BookingType;
						break;
					}
					case "Text":
					{
						value = Text;
						break;
					}
					case "EncodedTextLen":
					{
						value = EncodedTextLen;
						break;
					}
					case "EncodedText":
					{
						value = EncodedText;
						break;
					}
					case "PositionEffect":
					{
						value = PositionEffect;
						break;
					}
					case "CoveredOrUncovered":
					{
						value = CoveredOrUncovered;
						break;
					}
					case "CashMargin":
					{
						value = CashMargin;
						break;
					}
					case "ClearingFeeIndicator":
					{
						value = ClearingFeeIndicator;
						break;
					}
					case "SolicitedFlag":
					{
						value = SolicitedFlag;
						break;
					}
					case "SideComplianceID":
					{
						value = SideComplianceID;
						break;
					}
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				Side = null;
				ClOrdID = null;
				SecondaryClOrdID = null;
				ClOrdLinkID = null;
				((IFixReset?)Parties)?.Reset();
				TradeOriginationDate = null;
				TradeDate = null;
				Account = null;
				AcctIDSource = null;
				AccountType = null;
				DayBookingInst = null;
				BookingUnit = null;
				PreallocMethod = null;
				AllocID = null;
				((IFixReset?)PreAllocGrp)?.Reset();
				QtyType = null;
				((IFixReset?)OrderQtyData)?.Reset();
				((IFixReset?)CommissionData)?.Reset();
				OrderCapacity = null;
				OrderRestrictions = null;
				CustOrderCapacity = null;
				ForexReq = null;
				SettlCurrency = null;
				BookingType = null;
				Text = null;
				EncodedTextLen = null;
				EncodedText = null;
				PositionEffect = null;
				CoveredOrUncovered = null;
				CashMargin = null;
				ClearingFeeIndicator = null;
				SolicitedFlag = null;
				SideComplianceID = null;
			}
		}
		[Group(NoOfTag = 552, Offset = 0, Required = true)]
		public NoSides[]? Sides {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Sides is not null && Sides.Length != 0)
			{
				writer.WriteWholeNumber(552, Sides.Length);
				for (int i = 0; i < Sides.Length; i++)
				{
					((IFixEncoder)Sides[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSides") is IMessageView viewNoSides)
			{
				var count = viewNoSides.GroupCount();
				Sides = new NoSides[count];
				for (int i = 0; i < count; i++)
				{
					Sides[i] = new();
					((IFixParser)Sides[i]).Parse(viewNoSides.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSides":
				{
					value = Sides;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			Sides = null;
		}
	}
}
