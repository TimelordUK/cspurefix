using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class Hop : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 627, Offset = 0, Required = false)]
		public HopNoHops[]? NoHops { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoHops is not null && NoHops.Length != 0)
			{
				writer.WriteWholeNumber(627, NoHops.Length);
				for (int i = 0; i < NoHops.Length; i++)
				{
					((IFixEncoder)NoHops[i]).Encode(writer);
				}
			}
		}
	}
}
