using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class LegBenchmarkCurveData : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 676, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegBenchmarkCurveCurrency { get; set; }
		
		[TagDetails(Tag = 677, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegBenchmarkCurveName { get; set; }
		
		[TagDetails(Tag = 678, Type = TagType.String, Offset = 2, Required = false)]
		public string? LegBenchmarkCurvePoint { get; set; }
		
		[TagDetails(Tag = 679, Type = TagType.Float, Offset = 3, Required = false)]
		public double? LegBenchmarkPrice { get; set; }
		
		[TagDetails(Tag = 680, Type = TagType.Int, Offset = 4, Required = false)]
		public int? LegBenchmarkPriceType { get; set; }
		
	}
}
