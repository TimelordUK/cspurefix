using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class UnderlyingInstrumentNoUnderlyingSecurityAltID : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 458, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingSecurityAltID { get; set; }
		
		[TagDetails(Tag = 459, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingSecurityAltIDSource { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingSecurityAltID is not null) writer.WriteString(458, UnderlyingSecurityAltID);
			if (UnderlyingSecurityAltIDSource is not null) writer.WriteString(459, UnderlyingSecurityAltIDSource);
		}
	}
}
