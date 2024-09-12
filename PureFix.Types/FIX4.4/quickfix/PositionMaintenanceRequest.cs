using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class PositionMaintenanceRequest : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? PosReqID { get; set; } // 710 STRING
		public int? PosTransType { get; set; } // 709 INT
		public int? PosMaintAction { get; set; } // 712 INT
		public string? OrigPosReqRefID { get; set; } // 713 STRING
		public string? PosMaintRptRefID { get; set; } // 714 STRING
		public DateTime? ClearingBusinessDate { get; set; } // 715 LOCALMKTDATE
		public string? SettlSessID { get; set; } // 716 STRING
		public string? SettlSessSubID { get; set; } // 717 STRING
		public Parties? Parties { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public Instrument? Instrument { get; set; }
		public string? Currency { get; set; } // 15 CURRENCY
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public PositionQty? PositionQty { get; set; }
		public int? AdjustmentType { get; set; } // 718 INT
		public bool? ContraryInstructionIndicator { get; set; } // 719 BOOLEAN
		public bool? PriorSpreadIndicator { get; set; } // 720 BOOLEAN
		public double? ThresholdAmount { get; set; } // 834 PRICEOFFSET
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
