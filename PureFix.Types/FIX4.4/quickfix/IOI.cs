using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix
{
	public sealed class IOI : FixMsg
	{
		public override StandardHeader? StandardHeader { get; set; }
		[TagDetails(23)]
		public string? IOIID { get; set; } // STRING
		
		[TagDetails(28)]
		public string? IOITransType { get; set; } // CHAR
		
		[TagDetails(26)]
		public string? IOIRefID { get; set; } // STRING
		
		public Instrument? Instrument { get; set; }
		public FinancingDetails? FinancingDetails { get; set; }
		public UndInstrmtGrp? UndInstrmtGrp { get; set; }
		[TagDetails(54)]
		public string? Side { get; set; } // CHAR
		
		[TagDetails(854)]
		public int? QtyType { get; set; } // INT
		
		public OrderQtyData? OrderQtyData { get; set; }
		[TagDetails(27)]
		public string? IOIQty { get; set; } // STRING
		
		[TagDetails(15)]
		public string? Currency { get; set; } // CURRENCY
		
		public Stipulations? Stipulations { get; set; }
		public InstrmtLegIOIGrp? InstrmtLegIOIGrp { get; set; }
		[TagDetails(423)]
		public int? PriceType { get; set; } // INT
		
		[TagDetails(44)]
		public double? Price { get; set; } // PRICE
		
		[TagDetails(62)]
		public DateTime? ValidUntilTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(25)]
		public string? IOIQltyInd { get; set; } // CHAR
		
		[TagDetails(130)]
		public bool? IOINaturalFlag { get; set; } // BOOLEAN
		
		public IOIQualGrp? IOIQualGrp { get; set; }
		[TagDetails(58)]
		public string? Text { get; set; } // STRING
		
		[TagDetails(354)]
		public int? EncodedTextLen { get; set; } // LENGTH
		
		[TagDetails(355)]
		public byte[]? EncodedText { get; set; } // DATA
		
		[TagDetails(60)]
		public DateTime? TransactTime { get; set; } // UTCTIMESTAMP
		
		[TagDetails(149)]
		public string? URLLink { get; set; } // STRING
		
		public RoutingGrp? RoutingGrp { get; set; }
		public SpreadOrBenchmarkCurveData? SpreadOrBenchmarkCurveData { get; set; }
		public YieldData? YieldData { get; set; }
		public override StandardTrailer? StandardTrailer { get; set; }
	}
}
