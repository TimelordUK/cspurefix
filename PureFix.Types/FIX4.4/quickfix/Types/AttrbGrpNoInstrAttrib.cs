using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class AttrbGrpNoInstrAttrib : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 871, Type = TagType.Int, Offset = 0, Required = false)]
		public int? InstrAttribType { get; set; }
		
		[TagDetails(Tag = 872, Type = TagType.String, Offset = 1, Required = false)]
		public string? InstrAttribValue { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (InstrAttribType is not null) writer.WriteWholeNumber(871, InstrAttribType.Value);
			if (InstrAttribValue is not null) writer.WriteString(872, InstrAttribValue);
		}
	}
}
