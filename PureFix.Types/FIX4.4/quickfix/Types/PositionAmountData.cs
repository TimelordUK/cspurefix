using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PositionAmountData : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 753, Offset = 0, Required = false)]
		public PositionAmountDataNoPosAmt[]? NoPosAmt { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPosAmt is not null && NoPosAmt.Length != 0)
			{
				writer.WriteWholeNumber(753, NoPosAmt.Length);
				for (int i = 0; i < NoPosAmt.Length; i++)
				{
					((IFixEncoder)NoPosAmt[i]).Encode(writer);
				}
			}
		}
	}
}
