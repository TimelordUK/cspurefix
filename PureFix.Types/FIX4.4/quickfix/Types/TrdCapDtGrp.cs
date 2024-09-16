using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class TrdCapDtGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 580, Offset = 0, Required = false)]
		public TrdCapDtGrpNoDates[]? NoDates { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDates is not null && NoDates.Length != 0)
			{
				writer.WriteWholeNumber(580, NoDates.Length);
				for (int i = 0; i < NoDates.Length; i++)
				{
					((IFixEncoder)NoDates[i]).Encode(writer);
				}
			}
		}
	}
}
