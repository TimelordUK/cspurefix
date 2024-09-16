using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SecAltIDGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 454, Offset = 0, Required = false)]
		public SecAltIDGrpNoSecurityAltID[]? NoSecurityAltID { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSecurityAltID is not null && NoSecurityAltID.Length != 0)
			{
				writer.WriteWholeNumber(454, NoSecurityAltID.Length);
				for (int i = 0; i < NoSecurityAltID.Length; i++)
				{
					((IFixEncoder)NoSecurityAltID[i]).Encode(writer);
				}
			}
		}
	}
}
