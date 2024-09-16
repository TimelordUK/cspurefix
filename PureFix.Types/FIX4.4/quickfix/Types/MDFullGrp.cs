using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class MDFullGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 268, Offset = 0, Required = true)]
		public MDFullGrpNoMDEntries[]? NoMDEntries { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoMDEntries is not null && FixValidator.IsValid(NoMDEntries, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMDEntries is not null && NoMDEntries.Length != 0)
			{
				writer.WriteWholeNumber(268, NoMDEntries.Length);
				for (int i = 0; i < NoMDEntries.Length; i++)
				{
					((IFixEncoder)NoMDEntries[i]).Encode(writer);
				}
			}
		}
	}
}
