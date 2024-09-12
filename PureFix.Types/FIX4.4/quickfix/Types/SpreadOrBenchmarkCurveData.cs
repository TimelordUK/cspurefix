using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class SpreadOrBenchmarkCurveData
	{
		[TagDetails(218)]
		public double? Spread { get; set; } // PRICEOFFSET
		
		[TagDetails(220)]
		public string? BenchmarkCurveCurrency { get; set; } // CURRENCY
		
		[TagDetails(221)]
		public string? BenchmarkCurveName { get; set; } // STRING
		
		[TagDetails(222)]
		public string? BenchmarkCurvePoint { get; set; } // STRING
		
		[TagDetails(662)]
		public double? BenchmarkPrice { get; set; } // PRICE
		
		[TagDetails(663)]
		public int? BenchmarkPriceType { get; set; } // INT
		
		[TagDetails(699)]
		public string? BenchmarkSecurityID { get; set; } // STRING
		
		[TagDetails(761)]
		public string? BenchmarkSecurityIDSource { get; set; } // STRING
		
	}
}
