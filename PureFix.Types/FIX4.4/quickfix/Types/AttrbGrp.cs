using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class AttrbGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 870, Offset = 0, Required = false)]
		public AttrbGrpNoInstrAttrib[]? NoInstrAttrib { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoInstrAttrib is not null && NoInstrAttrib.Length != 0)
			{
				writer.WriteWholeNumber(870, NoInstrAttrib.Length);
				for (int i = 0; i < NoInstrAttrib.Length; i++)
				{
					((IFixEncoder)NoInstrAttrib[i]).Encode(writer);
				}
			}
		}
	}
}
