using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class EvntGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 864, Offset = 0, Required = false)]
		public EvntGrpNoEvents[]? NoEvents { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoEvents is not null && NoEvents.Length != 0)
			{
				writer.WriteWholeNumber(864, NoEvents.Length);
				for (int i = 0; i < NoEvents.Length; i++)
				{
					((IFixEncoder)NoEvents[i]).Encode(writer);
				}
			}
		}
	}
}
