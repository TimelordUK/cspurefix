using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class SpreadOrBenchmarkCurveDataComponent : IFixComponent
	{
		[TagDetails(Tag = 218, Type = TagType.Float, Offset = 0, Required = false)]
		public double? Spread {get; set;}
		
		[TagDetails(Tag = 220, Type = TagType.String, Offset = 1, Required = false)]
		public string? BenchmarkCurveCurrency {get; set;}
		
		[TagDetails(Tag = 221, Type = TagType.String, Offset = 2, Required = false)]
		public string? BenchmarkCurveName {get; set;}
		
		[TagDetails(Tag = 222, Type = TagType.String, Offset = 3, Required = false)]
		public string? BenchmarkCurvePoint {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Spread is not null) writer.WriteNumber(218, Spread.Value);
			if (BenchmarkCurveCurrency is not null) writer.WriteString(220, BenchmarkCurveCurrency);
			if (BenchmarkCurveName is not null) writer.WriteString(221, BenchmarkCurveName);
			if (BenchmarkCurvePoint is not null) writer.WriteString(222, BenchmarkCurvePoint);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			Spread = view.GetDouble(218);
			BenchmarkCurveCurrency = view.GetString(220);
			BenchmarkCurveName = view.GetString(221);
			BenchmarkCurvePoint = view.GetString(222);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Spread":
					value = Spread;
					break;
				case "BenchmarkCurveCurrency":
					value = BenchmarkCurveCurrency;
					break;
				case "BenchmarkCurveName":
					value = BenchmarkCurveName;
					break;
				case "BenchmarkCurvePoint":
					value = BenchmarkCurvePoint;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			Spread = null;
			BenchmarkCurveCurrency = null;
			BenchmarkCurveName = null;
			BenchmarkCurvePoint = null;
		}
	}
}
