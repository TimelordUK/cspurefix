using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class UnderlyingStipulationsNoUnderlyingStips : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 888, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingStipType { get; set; }
		
		[TagDetails(Tag = 889, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingStipValue { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingStipType is not null) writer.WriteString(888, UnderlyingStipType);
			if (UnderlyingStipValue is not null) writer.WriteString(889, UnderlyingStipValue);
		}
	}
}
