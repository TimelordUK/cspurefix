using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class MDReqGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 267, Offset = 0, Required = true)]
		public MDReqGrpNoMDEntryTypes[]? NoMDEntryTypes { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoMDEntryTypes is not null && FixValidator.IsValid(NoMDEntryTypes, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMDEntryTypes is not null && NoMDEntryTypes.Length != 0)
			{
				writer.WriteWholeNumber(267, NoMDEntryTypes.Length);
				for (int i = 0; i < NoMDEntryTypes.Length; i++)
				{
					((IFixEncoder)NoMDEntryTypes[i]).Encode(writer);
				}
			}
		}
	}
}
