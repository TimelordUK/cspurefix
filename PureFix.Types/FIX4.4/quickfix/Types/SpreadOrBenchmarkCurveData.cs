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
		[TagDetails(Tag = 218, Type = TagType.Float, Offset = 0)]
		public double? Spread { get; set; }
		
		[TagDetails(Tag = 220, Type = TagType.String, Offset = 1)]
		public string? BenchmarkCurveCurrency { get; set; }
		
		[TagDetails(Tag = 221, Type = TagType.String, Offset = 2)]
		public string? BenchmarkCurveName { get; set; }
		
		[TagDetails(Tag = 222, Type = TagType.String, Offset = 3)]
		public string? BenchmarkCurvePoint { get; set; }
		
		[TagDetails(Tag = 662, Type = TagType.Float, Offset = 4)]
		public double? BenchmarkPrice { get; set; }
		
		[TagDetails(Tag = 663, Type = TagType.Int, Offset = 5)]
		public int? BenchmarkPriceType { get; set; }
		
		[TagDetails(Tag = 699, Type = TagType.String, Offset = 6)]
		public string? BenchmarkSecurityID { get; set; }
		
		[TagDetails(Tag = 761, Type = TagType.String, Offset = 7)]
		public string? BenchmarkSecurityIDSource { get; set; }
		
	}
}
