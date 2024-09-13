using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class AllocGrpNoAllocs
	{
		[TagDetails(Tag = 79, Type = TagType.String, Offset = 0, Required = false)]
		public string? AllocAccount { get; set; }
		
		[TagDetails(Tag = 661, Type = TagType.Int, Offset = 1, Required = false)]
		public int? AllocAcctIDSource { get; set; }
		
		[TagDetails(Tag = 573, Type = TagType.String, Offset = 2, Required = false)]
		public string? MatchStatus { get; set; }
		
		[TagDetails(Tag = 366, Type = TagType.Float, Offset = 3, Required = false)]
		public double? AllocPrice { get; set; }
		
		[TagDetails(Tag = 80, Type = TagType.Float, Offset = 4, Required = false)]
		public double? AllocQty { get; set; }
		
		[TagDetails(Tag = 467, Type = TagType.String, Offset = 5, Required = false)]
		public string? IndividualAllocID { get; set; }
		
		[TagDetails(Tag = 81, Type = TagType.String, Offset = 6, Required = false)]
		public string? ProcessCode { get; set; }
		
		[Component(Offset = 7, Required = false)]
		public NestedParties? NestedParties { get; set; }
		
		[TagDetails(Tag = 208, Type = TagType.Boolean, Offset = 8, Required = false)]
		public bool? NotifyBrokerOfCredit { get; set; }
		
		[TagDetails(Tag = 209, Type = TagType.Int, Offset = 9, Required = false)]
		public int? AllocHandlInst { get; set; }
		
		[TagDetails(Tag = 161, Type = TagType.String, Offset = 10, Required = false)]
		public string? AllocText { get; set; }
		
		[TagDetails(Tag = 360, Type = TagType.Length, Offset = 11, Required = false, LinksToTag = 361)]
		public int? EncodedAllocTextLen { get; set; }
		
		[TagDetails(Tag = 361, Type = TagType.RawData, Offset = 12, Required = false, LinksToTag = 360)]
		public byte[]? EncodedAllocText { get; set; }
		
		[Component(Offset = 13, Required = false)]
		public CommissionData? CommissionData { get; set; }
		
		[TagDetails(Tag = 153, Type = TagType.Float, Offset = 14, Required = false)]
		public double? AllocAvgPx { get; set; }
		
		[TagDetails(Tag = 154, Type = TagType.Float, Offset = 15, Required = false)]
		public double? AllocNetMoney { get; set; }
		
		[TagDetails(Tag = 119, Type = TagType.Float, Offset = 16, Required = false)]
		public double? SettlCurrAmt { get; set; }
		
		[TagDetails(Tag = 737, Type = TagType.Float, Offset = 17, Required = false)]
		public double? AllocSettlCurrAmt { get; set; }
		
		[TagDetails(Tag = 120, Type = TagType.String, Offset = 18, Required = false)]
		public string? SettlCurrency { get; set; }
		
		[TagDetails(Tag = 736, Type = TagType.String, Offset = 19, Required = false)]
		public string? AllocSettlCurrency { get; set; }
		
		[TagDetails(Tag = 155, Type = TagType.Float, Offset = 20, Required = false)]
		public double? SettlCurrFxRate { get; set; }
		
		[TagDetails(Tag = 156, Type = TagType.String, Offset = 21, Required = false)]
		public string? SettlCurrFxRateCalc { get; set; }
		
		[TagDetails(Tag = 742, Type = TagType.Float, Offset = 22, Required = false)]
		public double? AllocAccruedInterestAmt { get; set; }
		
		[TagDetails(Tag = 741, Type = TagType.Float, Offset = 23, Required = false)]
		public double? AllocInterestAtMaturity { get; set; }
		
		[Component(Offset = 24, Required = false)]
		public MiscFeesGrp? MiscFeesGrp { get; set; }
		
		[Component(Offset = 25, Required = false)]
		public ClrInstGrp? ClrInstGrp { get; set; }
		
		[TagDetails(Tag = 780, Type = TagType.Int, Offset = 26, Required = false)]
		public int? AllocSettlInstType { get; set; }
		
		[Component(Offset = 27, Required = false)]
		public SettlInstructionsData? SettlInstructionsData { get; set; }
		
	}
}
