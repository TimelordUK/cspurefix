using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class AssignmentReport : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(833)]
		public string? AsgnRptID { get; set; } // STRING
		
		[TagDetails(832)]
		public int? TotNumAssignmentReports { get; set; } // INT
		
		[TagDetails(912)]
		public bool? LastRptRequested { get; set; } // BOOLEAN
		
		public Parties? Parties { get; set; }
		[TagDetails(1)]
		public string? Account { get; set; } // STRING
		
		[TagDetails(581)]
		public int? AccountType { get; set; } // INT
		
		public Instrument? Instrument { get; set; }
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public PositionQty? PositionQty { get; set; }
		public PositionAmountData? PositionAmountData { get; set; }
		[TagDetails(834)]
		public double? ThresholdAmount { get; set; } // PRICEOFFSET
		
		[TagDetails(730)]
		public double? SettlPrice { get; set; } // PRICE
		
		[TagDetails(731)]
		public int? SettlPriceType { get; set; } // INT
		
		[TagDetails(732)]
		public double? UnderlyingSettlPrice { get; set; } // PRICE
		
		[TagDetails(432)]
		public DateTime? ExpireDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(744)]
		public string? AssignmentMethod { get; set; } // CHAR
		
		[TagDetails(745)]
		public double? AssignmentUnit { get; set; } // QTY
		
		[TagDetails(746)]
		public double? OpenInterest { get; set; } // AMT
		
		[TagDetails(747)]
		public string? ExerciseMethod { get; set; } // CHAR
		
		[TagDetails(716)]
		public string? SettlSessID { get; set; } // STRING
		
		[TagDetails(717)]
		public string? SettlSessSubID { get; set; } // STRING
		
		[TagDetails(715)]
		public DateTime? ClearingBusinessDate { get; set; } // LOCALMKTDATE
		
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
