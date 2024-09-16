using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SecTypesGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 558, Offset = 0, Required = false)]
		public SecTypesGrpNoSecurityTypes[]? NoSecurityTypes { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSecurityTypes is not null && NoSecurityTypes.Length != 0)
			{
				writer.WriteWholeNumber(558, NoSecurityTypes.Length);
				for (int i = 0; i < NoSecurityTypes.Length; i++)
				{
					((IFixEncoder)NoSecurityTypes[i]).Encode(writer);
				}
			}
		}
	}
}
