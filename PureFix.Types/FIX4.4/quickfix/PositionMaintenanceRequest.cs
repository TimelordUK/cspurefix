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
		[TagDetails(710)]
		public string? PosReqID { get; set; } // STRING
		
		[TagDetails(709)]
		public int? PosTransType { get; set; } // INT
		
		[TagDetails(712)]
		public int? PosMaintAction { get; set; } // INT
		
		[TagDetails(713)]
		public string? OrigPosReqRefID { get; set; } // STRING
		
		[TagDetails(714)]
		public string? PosMaintRptRefID { get; set; } // STRING
		
		[TagDetails(715)]
		public DateTime? ClearingBusinessDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(716)]
		public string? SettlSessID { get; set; } // STRING
		
		[TagDetails(717)]
		public string? SettlSessSubID { get; set; } // STRING
		
		public Parties? Parties { get; set; }
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(660)]
		public int? AcctIDSource { get; set; } // INT
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		public Instrument? Instrument { get; set; }
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public TrdgSesGrp? TrdgSesGrp { get; set; }
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		public PositionQty? PositionQty { get; set; }
		[TagDetails(718)]
		public int? AdjustmentType { get; set; } // INT
		
		[TagDetails(719)]
		public bool? ContraryInstructionIndicator { get; set; } // BOOLEAN
		
		[TagDetails(720)]
		public bool? PriorSpreadIndicator { get; set; } // BOOLEAN
		
		[TagDetails(834)]
		public double? ThresholdAmount { get; set; } // PRICEOFFSET
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
