using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class DlvyInstGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 85, Offset = 0, Required = false)]
		public DlvyInstGrpNoDlvyInst[]? NoDlvyInst { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDlvyInst is not null && NoDlvyInst.Length != 0)
			{
				writer.WriteWholeNumber(85, NoDlvyInst.Length);
				for (int i = 0; i < NoDlvyInst.Length; i++)
				{
					((IFixEncoder)NoDlvyInst[i]).Encode(writer);
				}
			}
		}
	}
}
