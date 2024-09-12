using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public class IOI : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		public string? IOIID { get; set; } // 23 STRING
		public string? IOITransType { get; set; } // 28 CHAR
		public string? IOIRefID { get; set; } // 26 STRING
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		public string? Side { get; set; } // 54 CHAR
		public int? QtyType { get; set; } // 854 INT
		public OrderQtyData? OrderQtyData { get; set; }
		public string? IOIQty { get; set; } // 27 STRING
		public string? Currency { get; set; } // 15 CURRENCY
		public Stipulations? Stipulations { get; set; }
		public InstrmtLegIOIGrp? InstrmtLegIOIGrp { get; set; }
		public int? PriceType { get; set; } // 423 INT
		public double? Price { get; set; } // 44 PRICE
		public DateTime? ValidUntilTime { get; set; } // 62 UTCTIMESTAMP
		public string? IOIQltyInd { get; set; } // 25 CHAR
		public bool? IOINaturalFlag { get; set; } // 130 BOOLEAN
		public IOIQualGrp? IOIQualGrp { get; set; }
		public string? Text { get; set; } // 58 STRING
		public int? EncodedTextLen { get; set; } // 354 LENGTH
		public byte[]? EncodedText { get; set; } // 355 DATA
		public DateTime? TransactTime { get; set; } // 60 UTCTIMESTAMP
		public string? URLLink { get; set; } // 149 STRING
		public RoutingGrp? RoutingGrp { get; set; }
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		public YieldData? YieldData { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
