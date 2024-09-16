using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class MDRjctGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 816, Offset = 0, Required = false)]
		public MDRjctGrpNoAltMDSource[]? NoAltMDSource { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAltMDSource is not null && NoAltMDSource.Length != 0)
			{
				writer.WriteWholeNumber(816, NoAltMDSource.Length);
				for (int i = 0; i < NoAltMDSource.Length; i++)
				{
					((IFixEncoder)NoAltMDSource[i]).Encode(writer);
				}
			}
		}
	}
}
