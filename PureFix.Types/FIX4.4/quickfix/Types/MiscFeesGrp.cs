using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class MiscFeesGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 136, Offset = 0, Required = false)]
		public MiscFeesGrpNoMiscFees[]? NoMiscFees { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMiscFees is not null && NoMiscFees.Length != 0)
			{
				writer.WriteWholeNumber(136, NoMiscFees.Length);
				for (int i = 0; i < NoMiscFees.Length; i++)
				{
					((IFixEncoder)NoMiscFees[i]).Encode(writer);
				}
			}
		}
	}
}
