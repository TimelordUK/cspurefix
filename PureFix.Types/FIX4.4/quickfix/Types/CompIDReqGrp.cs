using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CompIDReqGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 936, Offset = 0, Required = false)]
		public CompIDReqGrpNoCompIDs[]? NoCompIDs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoCompIDs is not null && NoCompIDs.Length != 0)
			{
				writer.WriteWholeNumber(936, NoCompIDs.Length);
				for (int i = 0; i < NoCompIDs.Length; i++)
				{
					((IFixEncoder)NoCompIDs[i]).Encode(writer);
				}
			}
		}
	}
}
