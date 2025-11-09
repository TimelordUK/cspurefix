using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44UnitTest.Components;

namespace PureFix.Types.FIX44UnitTest.Components
{
	public sealed partial class AllocGrp : IFixComponent
	{
		public sealed partial class NoAllocs : IFixGroup
		{
			[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
			public string? AllocAccount {get; set;}
			
			[TagDetails(Tag = 661, Type = TagType.Int, Offset = 1, Required = false)]
			public int? AllocAcctIDSource {get; set;}
			
			[TagDetails(Tag = 573, Type = TagType.String, Offset = 2, Required = false)]
			public string? MatchStatus {get; set;}
			
			[TagDetails(Tag = 366, Type = TagType.Float, Offset = 3, Required = false)]
			public double? AllocPrice {get; set;}
			
			[TagDetails(Tag = 80, Type = TagType.Float, Offset = 4, Required = false)]
			public double? AllocQty {get; set;}
			
			[TagDetails(Tag = 467, Type = TagType.String, Offset = 5, Required = false)]
			public string? IndividualAllocID {get; set;}
			
			[TagDetails(Tag = 81, Type = TagType.String, Offset = 6, Required = false)]
			public string? ProcessCode {get; set;}
			
			[Component(Offset = 7, Required = false)]
			public NestedParties? NestedParties {get; set;}
			
			[TagDetails(Tag = 208, Type = TagType.Boolean, Offset = 8, Required = false)]
			public bool? NotifyBrokerOfCredit {get; set;}
			
			[TagDetails(Tag = 209, Type = TagType.Int, Offset = 9, Required = false)]
			public int? AllocHandlInst {get; set;}
			
			[TagDetails(Tag = 161, Type = TagType.String, Offset = 10, Required = false)]
			public string? AllocText {get; set;}
			
			[TagDetails(Tag = 360, Type = TagType.Length, Offset = 11, Required = false)]
			public int? EncodedAllocTextLen {get; set;}
			
			[TagDetails(Tag = 361, Type = TagType.RawData, Offset = 12, Required = false)]
			public byte[]? EncodedAllocText {get; set;}
			
			[Component(Offset = 13, Required = false)]
			public CommissionData? CommissionData {get; set;}
			
			[TagDetails(Tag = 153, Type = TagType.Float, Offset = 14, Required = false)]
			public double? AllocAvgPx {get; set;}
			
			[TagDetails(Tag = 154, Type = TagType.Float, Offset = 15, Required = false)]
			public double? AllocNetMoney {get; set;}
			
			[TagDetails(Tag = 119, Type = TagType.Float, Offset = 16, Required = false)]
			public double? SettlCurrAmt {get; set;}
			
			[TagDetails(Tag = 737, Type = TagType.Float, Offset = 17, Required = false)]
			public double? AllocSettlCurrAmt {get; set;}
			
			[TagDetails(Tag = 120, Type = TagType.String, Offset = 18, Required = false)]
			public string? SettlCurrency {get; set;}
			
			[TagDetails(Tag = 736, Type = TagType.String, Offset = 19, Required = false)]
			public string? AllocSettlCurrency {get; set;}
			
			[TagDetails(Tag = 155, Type = TagType.Float, Offset = 20, Required = false)]
			public double? SettlCurrFxRate {get; set;}
			
			[TagDetails(Tag = 156, Type = TagType.String, Offset = 21, Required = false)]
			public string? SettlCurrFxRateCalc {get; set;}
			
			[TagDetails(Tag = 742, Type = TagType.Float, Offset = 22, Required = false)]
			public double? AllocAccruedInterestAmt {get; set;}
			
			[TagDetails(Tag = 741, Type = TagType.Float, Offset = 23, Required = false)]
			public double? AllocInterestAtMaturity {get; set;}
			
			[Component(Offset = 24, Required = false)]
			public MiscFeesGrp? MiscFeesGrp {get; set;}
			
			[Component(Offset = 25, Required = false)]
			public ClrInstGrp? ClrInstGrp {get; set;}
			
			[TagDetails(Tag = 780, Type = TagType.Int, Offset = 26, Required = false)]
			public int? AllocSettlInstType {get; set;}
			
			[Component(Offset = 27, Required = false)]
			public SettlInstructionsData? SettlInstructionsData {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
				if (AllocAcctIDSource is not null) writer.WriteWholeNumber(661, AllocAcctIDSource.Value);
				if (MatchStatus is not null) writer.WriteString(573, MatchStatus);
				if (AllocPrice is not null) writer.WriteNumber(366, AllocPrice.Value);
				if (AllocQty is not null) writer.WriteNumber(80, AllocQty.Value);
				if (IndividualAllocID is not null) writer.WriteString(467, IndividualAllocID);
				if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
				if (NestedParties is not null) ((IFixEncoder)NestedParties).Encode(writer);
				if (NotifyBrokerOfCredit is not null) writer.WriteBoolean(208, NotifyBrokerOfCredit.Value);
				if (AllocHandlInst is not null) writer.WriteWholeNumber(209, AllocHandlInst.Value);
				if (AllocText is not null) writer.WriteString(161, AllocText);
				if (EncodedAllocTextLen is not null) writer.WriteWholeNumber(360, EncodedAllocTextLen.Value);
				if (EncodedAllocText is not null) writer.WriteBuffer(361, EncodedAllocText);
				if (CommissionData is not null) ((IFixEncoder)CommissionData).Encode(writer);
				if (AllocAvgPx is not null) writer.WriteNumber(153, AllocAvgPx.Value);
				if (AllocNetMoney is not null) writer.WriteNumber(154, AllocNetMoney.Value);
				if (SettlCurrAmt is not null) writer.WriteNumber(119, SettlCurrAmt.Value);
				if (AllocSettlCurrAmt is not null) writer.WriteNumber(737, AllocSettlCurrAmt.Value);
				if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
				if (AllocSettlCurrency is not null) writer.WriteString(736, AllocSettlCurrency);
				if (SettlCurrFxRate is not null) writer.WriteNumber(155, SettlCurrFxRate.Value);
				if (SettlCurrFxRateCalc is not null) writer.WriteString(156, SettlCurrFxRateCalc);
				if (AllocAccruedInterestAmt is not null) writer.WriteNumber(742, AllocAccruedInterestAmt.Value);
				if (AllocInterestAtMaturity is not null) writer.WriteNumber(741, AllocInterestAtMaturity.Value);
				if (MiscFeesGrp is not null) ((IFixEncoder)MiscFeesGrp).Encode(writer);
				if (ClrInstGrp is not null) ((IFixEncoder)ClrInstGrp).Encode(writer);
				if (AllocSettlInstType is not null) writer.WriteWholeNumber(780, AllocSettlInstType.Value);
				if (SettlInstructionsData is not null) ((IFixEncoder)SettlInstructionsData).Encode(writer);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				AllocAccount = view.GetString(79);
				AllocAcctIDSource = view.GetInt32(661);
				MatchStatus = view.GetString(573);
				AllocPrice = view.GetDouble(366);
				AllocQty = view.GetDouble(80);
				IndividualAllocID = view.GetString(467);
				ProcessCode = view.GetString(81);
				if (view.GetView("NestedParties") is IMessageView viewNestedParties)
				{
					NestedParties = new();
					((IFixParser)NestedParties).Parse(viewNestedParties);
				}
				NotifyBrokerOfCredit = view.GetBool(208);
				AllocHandlInst = view.GetInt32(209);
				AllocText = view.GetString(161);
				EncodedAllocTextLen = view.GetInt32(360);
				EncodedAllocText = view.GetByteArray(361);
				if (view.GetView("CommissionData") is IMessageView viewCommissionData)
				{
					CommissionData = new();
					((IFixParser)CommissionData).Parse(viewCommissionData);
				}
				AllocAvgPx = view.GetDouble(153);
				AllocNetMoney = view.GetDouble(154);
				SettlCurrAmt = view.GetDouble(119);
				AllocSettlCurrAmt = view.GetDouble(737);
				SettlCurrency = view.GetString(120);
				AllocSettlCurrency = view.GetString(736);
				SettlCurrFxRate = view.GetDouble(155);
				SettlCurrFxRateCalc = view.GetString(156);
				AllocAccruedInterestAmt = view.GetDouble(742);
				AllocInterestAtMaturity = view.GetDouble(741);
				if (view.GetView("MiscFeesGrp") is IMessageView viewMiscFeesGrp)
				{
					MiscFeesGrp = new();
					((IFixParser)MiscFeesGrp).Parse(viewMiscFeesGrp);
				}
				if (view.GetView("ClrInstGrp") is IMessageView viewClrInstGrp)
				{
					ClrInstGrp = new();
					((IFixParser)ClrInstGrp).Parse(viewClrInstGrp);
				}
				AllocSettlInstType = view.GetInt32(780);
				if (view.GetView("SettlInstructionsData") is IMessageView viewSettlInstructionsData)
				{
					SettlInstructionsData = new();
					((IFixParser)SettlInstructionsData).Parse(viewSettlInstructionsData);
				}
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "AllocAccount":
					{
						value = AllocAccount;
						break;
					}
					case "AllocAcctIDSource":
					{
						value = AllocAcctIDSource;
						break;
					}
					case "MatchStatus":
					{
						value = MatchStatus;
						break;
					}
					case "AllocPrice":
					{
						value = AllocPrice;
						break;
					}
					case "AllocQty":
					{
						value = AllocQty;
						break;
					}
					case "IndividualAllocID":
					{
						value = IndividualAllocID;
						break;
					}
					case "ProcessCode":
					{
						value = ProcessCode;
						break;
					}
					case "NestedParties":
					{
						value = NestedParties;
						break;
					}
					case "NotifyBrokerOfCredit":
					{
						value = NotifyBrokerOfCredit;
						break;
					}
					case "AllocHandlInst":
					{
						value = AllocHandlInst;
						break;
					}
					case "AllocText":
					{
						value = AllocText;
						break;
					}
					case "EncodedAllocTextLen":
					{
						value = EncodedAllocTextLen;
						break;
					}
					case "EncodedAllocText":
					{
						value = EncodedAllocText;
						break;
					}
					case "CommissionData":
					{
						value = CommissionData;
						break;
					}
					case "AllocAvgPx":
					{
						value = AllocAvgPx;
						break;
					}
					case "AllocNetMoney":
					{
						value = AllocNetMoney;
						break;
					}
					case "SettlCurrAmt":
					{
						value = SettlCurrAmt;
						break;
					}
					case "AllocSettlCurrAmt":
					{
						value = AllocSettlCurrAmt;
						break;
					}
					case "SettlCurrency":
					{
						value = SettlCurrency;
						break;
					}
					case "AllocSettlCurrency":
					{
						value = AllocSettlCurrency;
						break;
					}
					case "SettlCurrFxRate":
					{
						value = SettlCurrFxRate;
						break;
					}
					case "SettlCurrFxRateCalc":
					{
						value = SettlCurrFxRateCalc;
						break;
					}
					case "AllocAccruedInterestAmt":
					{
						value = AllocAccruedInterestAmt;
						break;
					}
					case "AllocInterestAtMaturity":
					{
						value = AllocInterestAtMaturity;
						break;
					}
					case "MiscFeesGrp":
					{
						value = MiscFeesGrp;
						break;
					}
					case "ClrInstGrp":
					{
						value = ClrInstGrp;
						break;
					}
					case "AllocSettlInstType":
					{
						value = AllocSettlInstType;
						break;
					}
					case "SettlInstructionsData":
					{
						value = SettlInstructionsData;
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
				AllocAccount = null;
				AllocAcctIDSource = null;
				MatchStatus = null;
				AllocPrice = null;
				AllocQty = null;
				IndividualAllocID = null;
				ProcessCode = null;
				((IFixReset?)NestedParties)?.Reset();
				NotifyBrokerOfCredit = null;
				AllocHandlInst = null;
				AllocText = null;
				EncodedAllocTextLen = null;
				EncodedAllocText = null;
				((IFixReset?)CommissionData)?.Reset();
				AllocAvgPx = null;
				AllocNetMoney = null;
				SettlCurrAmt = null;
				AllocSettlCurrAmt = null;
				SettlCurrency = null;
				AllocSettlCurrency = null;
				SettlCurrFxRate = null;
				SettlCurrFxRateCalc = null;
				AllocAccruedInterestAmt = null;
				AllocInterestAtMaturity = null;
				((IFixReset?)MiscFeesGrp)?.Reset();
				((IFixReset?)ClrInstGrp)?.Reset();
				AllocSettlInstType = null;
				((IFixReset?)SettlInstructionsData)?.Reset();
			}
		}
		[Group(NoOfTag = 78, Offset = 0, Required = false)]
		public NoAllocs[]? Allocs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Allocs is not null && Allocs.Length != 0)
			{
				writer.WriteWholeNumber(78, Allocs.Length);
				for (int i = 0; i < Allocs.Length; i++)
				{
					((IFixEncoder)Allocs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAllocs") is IMessageView viewNoAllocs)
			{
				var count = viewNoAllocs.GroupCount();
				Allocs = new NoAllocs[count];
				for (int i = 0; i < count; i++)
				{
					Allocs[i] = new();
					((IFixParser)Allocs[i]).Parse(viewNoAllocs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAllocs":
				{
					value = Allocs;
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
			Allocs = null;
		}
	}
}
