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
		[TagDetails(Tag = 218, Type = TagType.Float, Offset = 0, Required = false)]
		public double? Spread { get; set; }
		
		[TagDetails(Tag = 220, Type = TagType.String, Offset = 1, Required = false)]
		public string? BenchmarkCurveCurrency { get; set; }
		
		[TagDetails(Tag = 221, Type = TagType.String, Offset = 2, Required = false)]
		public string? BenchmarkCurveName { get; set; }
		
		[TagDetails(Tag = 222, Type = TagType.String, Offset = 3, Required = false)]
		public string? BenchmarkCurvePoint { get; set; }
		
		[TagDetails(Tag = 662, Type = TagType.Float, Offset = 4, Required = false)]
		public double? BenchmarkPrice { get; set; }
		
		[TagDetails(Tag = 663, Type = TagType.Int, Offset = 5, Required = false)]
		public int? BenchmarkPriceType { get; set; }
		
		[TagDetails(Tag = 699, Type = TagType.String, Offset = 6, Required = false)]
		public string? BenchmarkSecurityID { get; set; }
		
		[TagDetails(Tag = 761, Type = TagType.String, Offset = 7, Required = false)]
		public string? BenchmarkSecurityIDSource { get; set; }
		
	}
}
