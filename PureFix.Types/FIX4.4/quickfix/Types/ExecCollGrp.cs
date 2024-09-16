using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class ExecCollGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 124, Offset = 0, Required = false)]
		public ExecCollGrpNoExecs[]? NoExecs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoExecs is not null && NoExecs.Length != 0)
			{
				writer.WriteWholeNumber(124, NoExecs.Length);
				for (int i = 0; i < NoExecs.Length; i++)
				{
					((IFixEncoder)NoExecs[i]).Encode(writer);
				}
			}
		}
	}
}
