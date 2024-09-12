using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class RequestForPositionsAck : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? PosMaintRptID { get; set; } // 721 STRING
		public string? PosReqID { get; set; } // 710 STRING
		public int? TotalNumPosReports { get; set; } // 727 INT
		public bool? UnsolicitedIndicator { get; set; } // 325 BOOLEAN
		public int? PosReqResult { get; set; } // 728 INT
		public int? PosReqStatus { get; set; } // 729 INT
		public Parties? Parties { get; set; }
		public string? Account { get; set; } // 1 STRING
		public int? AcctIDSource { get; set; } // 660 INT
		public int? AccountType { get; set; } // 581 INT
		public Instrument? Instrument { get; set; }
		public string? Currency { get; set; } // 15 CURRENCY
		public InstrmtLegGrp? InstrmtLegGrp { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public int? ResponseTransportType { get; set; } // 725 INT
		public string? ResponseDestination { get; set; } // 726 STRING
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
