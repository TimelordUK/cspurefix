using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class LegPreAllocGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 670, Offset = 0, Required = false)]
		public LegPreAllocGrpNoLegAllocs[]? NoLegAllocs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegAllocs is not null && NoLegAllocs.Length != 0)
			{
				writer.WriteWholeNumber(670, NoLegAllocs.Length);
				for (int i = 0; i < NoLegAllocs.Length; i++)
				{
					((IFixEncoder)NoLegAllocs[i]).Encode(writer);
				}
			}
		}
	}
}
