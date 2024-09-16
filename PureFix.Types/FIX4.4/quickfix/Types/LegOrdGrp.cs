using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class LegOrdGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 555, Offset = 0, Required = true)]
		public LegOrdGrpNoLegs[]? NoLegs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoLegs is not null && FixValidator.IsValid(NoLegs, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegs is not null && NoLegs.Length != 0)
			{
				writer.WriteWholeNumber(555, NoLegs.Length);
				for (int i = 0; i < NoLegs.Length; i++)
				{
					((IFixEncoder)NoLegs[i]).Encode(writer);
				}
			}
		}
	}
}
