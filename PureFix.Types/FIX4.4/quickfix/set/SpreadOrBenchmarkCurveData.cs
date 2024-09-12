using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX4._4.quickfix.set;
namespace PureFix.Types.FIX4._4.quickfix.set
{
	public class SpreadOrBenchmarkCurveData
	{
		public double? Spread { get; set; } // 218 PRICEOFFSET
		public string? BenchmarkCurveCurrency { get; set; } // 220 CURRENCY
		public string? BenchmarkCurveName { get; set; } // 221 STRING
		public string? BenchmarkCurvePoint { get; set; } // 222 STRING
		public double? BenchmarkPrice { get; set; } // 662 PRICE
		public int? BenchmarkPriceType { get; set; } // 663 INT
		public string? BenchmarkSecurityID { get; set; } // 699 STRING
		public string? BenchmarkSecurityIDSource { get; set; } // 761 STRING
	}
}
