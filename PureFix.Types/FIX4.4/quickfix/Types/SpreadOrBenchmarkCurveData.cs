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
		[TagDetails(218, TagType.Float)]
		public double? Spread { get; set; }
		
		[TagDetails(220, TagType.String)]
		public string? BenchmarkCurveCurrency { get; set; }
		
		[TagDetails(221, TagType.String)]
		public string? BenchmarkCurveName { get; set; }
		
		[TagDetails(222, TagType.String)]
		public string? BenchmarkCurvePoint { get; set; }
		
		[TagDetails(662, TagType.Float)]
		public double? BenchmarkPrice { get; set; }
		
		[TagDetails(663, TagType.Int)]
		public int? BenchmarkPriceType { get; set; }
		
		[TagDetails(699, TagType.String)]
		public string? BenchmarkSecurityID { get; set; }
		
		[TagDetails(761, TagType.String)]
		public string? BenchmarkSecurityIDSource { get; set; }
		
	}
}
