using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class UndSecAltIDGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 457, Offset = 0, Required = false)]
		public UndSecAltIDGrpNoUnderlyingSecurityAltID[]? NoUnderlyingSecurityAltID { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingSecurityAltID is not null && NoUnderlyingSecurityAltID.Length != 0)
			{
				writer.WriteWholeNumber(457, NoUnderlyingSecurityAltID.Length);
				for (int i = 0; i < NoUnderlyingSecurityAltID.Length; i++)
				{
					((IFixEncoder)NoUnderlyingSecurityAltID[i]).Encode(writer);
				}
			}
		}
	}
}
