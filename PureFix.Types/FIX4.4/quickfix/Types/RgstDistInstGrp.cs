using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class RgstDistInstGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 510, Offset = 0, Required = false)]
		public RgstDistInstGrpNoDistribInsts[]? NoDistribInsts { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDistribInsts is not null && NoDistribInsts.Length != 0)
			{
				writer.WriteWholeNumber(510, NoDistribInsts.Length);
				for (int i = 0; i < NoDistribInsts.Length; i++)
				{
					((IFixEncoder)NoDistribInsts[i]).Encode(writer);
				}
			}
		}
	}
}
