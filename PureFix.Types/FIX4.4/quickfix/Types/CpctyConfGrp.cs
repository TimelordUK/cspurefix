using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CpctyConfGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 862, Offset = 0, Required = true)]
		public CpctyConfGrpNoCapacities[]? NoCapacities { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoCapacities is not null && FixValidator.IsValid(NoCapacities, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoCapacities is not null && NoCapacities.Length != 0)
			{
				writer.WriteWholeNumber(862, NoCapacities.Length);
				for (int i = 0; i < NoCapacities.Length; i++)
				{
					((IFixEncoder)NoCapacities[i]).Encode(writer);
				}
			}
		}
	}
}
