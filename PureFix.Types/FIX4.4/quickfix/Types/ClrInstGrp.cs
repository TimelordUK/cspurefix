using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class ClrInstGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 576, Offset = 0, Required = false)]
		public ClrInstGrpNoClearingInstructions[]? NoClearingInstructions { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoClearingInstructions is not null && NoClearingInstructions.Length != 0)
			{
				writer.WriteWholeNumber(576, NoClearingInstructions.Length);
				for (int i = 0; i < NoClearingInstructions.Length; i++)
				{
					((IFixEncoder)NoClearingInstructions[i]).Encode(writer);
				}
			}
		}
	}
}
