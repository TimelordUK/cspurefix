using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class LegSecAltIDGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 604, Offset = 0, Required = false)]
		public LegSecAltIDGrpNoLegSecurityAltID[]? NoLegSecurityAltID { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegSecurityAltID is not null && NoLegSecurityAltID.Length != 0)
			{
				writer.WriteWholeNumber(604, NoLegSecurityAltID.Length);
				for (int i = 0; i < NoLegSecurityAltID.Length; i++)
				{
					((IFixEncoder)NoLegSecurityAltID[i]).Encode(writer);
				}
			}
		}
	}
}
