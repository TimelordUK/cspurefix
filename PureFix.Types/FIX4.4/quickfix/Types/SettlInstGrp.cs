using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SettlInstGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 778, Offset = 0, Required = false)]
		public SettlInstGrpNoSettlInst[]? NoSettlInst { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSettlInst is not null && NoSettlInst.Length != 0)
			{
				writer.WriteWholeNumber(778, NoSettlInst.Length);
				for (int i = 0; i < NoSettlInst.Length; i++)
				{
					((IFixEncoder)NoSettlInst[i]).Encode(writer);
				}
			}
		}
	}
}
