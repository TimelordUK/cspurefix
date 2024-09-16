using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class YieldData : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 235, Type = TagType.String, Offset = 0, Required = false)]
		public string? YieldType { get; set; }
		
		[TagDetails(Tag = 236, Type = TagType.Float, Offset = 1, Required = false)]
		public double? Yield { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (YieldType is not null) writer.WriteString(235, YieldType);
			if (Yield is not null) writer.WriteNumber(236, Yield.Value);
		}
	}
}
