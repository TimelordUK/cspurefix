using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SideCrossOrdModGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 552, Offset = 0, Required = true)]
		public SideCrossOrdModGrpNoSides[]? NoSides { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoSides is not null && FixValidator.IsValid(NoSides, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSides is not null && NoSides.Length != 0)
			{
				writer.WriteWholeNumber(552, NoSides.Length);
				for (int i = 0; i < NoSides.Length; i++)
				{
					((IFixEncoder)NoSides[i]).Encode(writer);
				}
			}
		}
	}
}
