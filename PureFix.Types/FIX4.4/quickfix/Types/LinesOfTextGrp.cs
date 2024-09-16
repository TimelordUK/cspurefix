using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class LinesOfTextGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 33, Offset = 0, Required = true)]
		public LinesOfTextGrpNoLinesOfText[]? NoLinesOfText { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoLinesOfText is not null && FixValidator.IsValid(NoLinesOfText, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLinesOfText is not null && NoLinesOfText.Length != 0)
			{
				writer.WriteWholeNumber(33, NoLinesOfText.Length);
				for (int i = 0; i < NoLinesOfText.Length; i++)
				{
					((IFixEncoder)NoLinesOfText[i]).Encode(writer);
				}
			}
		}
	}
}
