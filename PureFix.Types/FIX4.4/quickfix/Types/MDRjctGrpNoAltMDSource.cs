using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class MDRjctGrpNoAltMDSource : IFixValidator, IFixEncoder
	{
		[TagDetails(Tag = 817, Type = TagType.String, Offset = 0, Required = false)]
		public string? AltMDSourceID { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (AltMDSourceID is not null) writer.WriteString(817, AltMDSourceID);
		}
	}
}
