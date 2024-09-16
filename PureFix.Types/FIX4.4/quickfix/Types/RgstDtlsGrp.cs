using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class RgstDtlsGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 473, Offset = 0, Required = false)]
		public RgstDtlsGrpNoRegistDtls[]? NoRegistDtls { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRegistDtls is not null && NoRegistDtls.Length != 0)
			{
				writer.WriteWholeNumber(473, NoRegistDtls.Length);
				for (int i = 0; i < NoRegistDtls.Length; i++)
				{
					((IFixEncoder)NoRegistDtls[i]).Encode(writer);
				}
			}
		}
	}
}
