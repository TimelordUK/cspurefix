using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class UndInstrmtCollGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 711, Offset = 0, Required = false)]
		public UndInstrmtCollGrpNoUnderlyings[]? NoUnderlyings { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyings is not null && NoUnderlyings.Length != 0)
			{
				writer.WriteWholeNumber(711, NoUnderlyings.Length);
				for (int i = 0; i < NoUnderlyings.Length; i++)
				{
					((IFixEncoder)NoUnderlyings[i]).Encode(writer);
				}
			}
		}
	}
}
