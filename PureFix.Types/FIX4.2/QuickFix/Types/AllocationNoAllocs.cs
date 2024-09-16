using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class AllocationNoAllocs : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 366, Type = TagType.Float, Offset = 1, Required = false)]
		public double? AllocPrice { get; set; }
		
		[TagDetails(Tag = 80, Type = TagType.Float, Offset = 2, Required = true)]
		public double? AllocShares { get; set; }
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 3, Required = false)]
		public string? ProcessCode { get; set; }
		
		[TagDetails(Tag = 92, Type = TagType.String, Offset = 4, Required = false)]
		public string? BrokerOfCredit { get; set; }
		
		[TagDetails(Tag = 208, Type = TagType.Boolean, Offset = 5, Required = false)]
		public bool? NotifyBrokerOfCredit { get; set; }
		
		[TagDetails(Tag = 209, Type = TagType.Int, Offset = 6, Required = false)]
		public int? AllocHandlInst { get; set; }
		
		[TagDetails(Tag = 161, Type = TagType.String, Offset = 7, Required = false)]
		public string? AllocText { get; set; }
		
		[TagDetails(Tag = 360, Type = TagType.Length, Offset = 8, Required = false, LinksToTag = 361)]
		public int? EncodedAllocTextLen { get; set; }
		
		[TagDetails(Tag = 361, Type = TagType.RawData, Offset = 9, Required = false, LinksToTag = 360)]
		public byte[]? EncodedAllocText { get; set; }
		
		[TagDetails(Tag = 76, Type = TagType.String, Offset = 10, Required = false)]
		public string? ExecBroker { get; set; }
		
		[TagDetails(Tag = 109, Type = TagType.String, Offset = 11, Required = false)]
		public string? ClientID { get; set; }
		
		[TagDetails(Tag = 12, Type = TagType.Float, Offset = 12, Required = false)]
		public double? Commission { get; set; }
		
		[TagDetails(Tag = 13, Type = TagType.String, Offset = 13, Required = false)]
		public string? CommType { get; set; }
		
		[TagDetails(Tag = 153, Type = TagType.Float, Offset = 14, Required = false)]
		public double? AllocAvgPx { get; set; }
		
		[TagDetails(Tag = 154, Type = TagType.Float, Offset = 15, Required = false)]
		public double? AllocNetMoney { get; set; }
		
		[TagDetails(Tag = 119, Type = TagType.Float, Offset = 16, Required = false)]
		public double? SettlCurrAmt { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 17, Required = false)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 155, Type = TagType.Float, Offset = 18, Required = false)]
		public double? SettlCurrFxRate { get; set; }
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 19, Required = false)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(Tag = 159, Type = TagType.Float, Offset = 20, Required = false)]
		public double? AccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 160, Type = TagType.String, Offset = 21, Required = false)]
		public string? SettlInstMode { get; set; }
		
		[Group(NoOfTag = 136, Offset = 22, Required = false)]
		public AllocationNoAllocsNoMiscFees[]? NoMiscFees { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				AllocShares is not null;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (AllocAccount is not null) writer.WriteString(79, AllocAccount);
			if (AllocPrice is not null) writer.WriteNumber(366, AllocPrice.Value);
			if (AllocShares is not null) writer.WriteNumber(80, AllocShares.Value);
			if (ProcessCode is not null) writer.WriteString(81, ProcessCode);
			if (BrokerOfCredit is not null) writer.WriteString(92, BrokerOfCredit);
			if (NotifyBrokerOfCredit is not null) writer.WriteBoolean(208, NotifyBrokerOfCredit.Value);
			if (AllocHandlInst is not null) writer.WriteWholeNumber(209, AllocHandlInst.Value);
			if (AllocText is not null) writer.WriteString(161, AllocText);
			if (EncodedAllocText is not null)
			{
				writer.WriteWholeNumber(360, EncodedAllocText.Length);
				writer.WriteBuffer(361, EncodedAllocText);
			}
			if (ExecBroker is not null) writer.WriteString(76, ExecBroker);
			if (ClientID is not null) writer.WriteString(109, ClientID);
			if (Commission is not null) writer.WriteNumber(12, Commission.Value);
			if (CommType is not null) writer.WriteString(13, CommType);
			if (AllocAvgPx is not null) writer.WriteNumber(153, AllocAvgPx.Value);
			if (AllocNetMoney is not null) writer.WriteNumber(154, AllocNetMoney.Value);
			if (SettlCurrAmt is not null) writer.WriteNumber(119, SettlCurrAmt.Value);
			if (SettlCurrency is not null) writer.WriteString(120, SettlCurrency);
			if (SettlCurrFxRate is not null) writer.WriteNumber(155, SettlCurrFxRate.Value);
			if (SettlCurrFxRateCalc is not null) writer.WriteString(156, SettlCurrFxRateCalc);
			if (AccruedInterestAmt is not null) writer.WriteNumber(159, AccruedInterestAmt.Value);
			if (SettlInstMode is not null) writer.WriteString(160, SettlInstMode);
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
