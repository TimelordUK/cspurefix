using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class InstrmtStrkPxGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 428, Offset = 0, Required = true)]
		public InstrmtStrkPxGrpNoStrikes[]? NoStrikes { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoStrikes is not null && FixValidator.IsValid(NoStrikes, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStrikes is not null && NoStrikes.Length != 0)
			{
				writer.WriteWholeNumber(428, NoStrikes.Length);
				for (int i = 0; i < NoStrikes.Length; i++)
				{
					((IFixEncoder)NoStrikes[i]).Encode(writer);
				}
			}
		}
	}
}
