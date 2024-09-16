using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class TrdRegTimestamps : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 768, Offset = 0, Required = false)]
		public TrdRegTimestampsNoTrdRegTimestamps[]? NoTrdRegTimestamps { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTrdRegTimestamps is not null && NoTrdRegTimestamps.Length != 0)
			{
				writer.WriteWholeNumber(768, NoTrdRegTimestamps.Length);
				for (int i = 0; i < NoTrdRegTimestamps.Length; i++)
				{
					((IFixEncoder)NoTrdRegTimestamps[i]).Encode(writer);
				}
			}
		}
	}
}
