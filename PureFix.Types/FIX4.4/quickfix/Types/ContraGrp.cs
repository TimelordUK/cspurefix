using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class ContraGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 382, Offset = 0, Required = false)]
		public ContraGrpNoContraBrokers[]? NoContraBrokers { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoContraBrokers is not null && NoContraBrokers.Length != 0)
			{
				writer.WriteWholeNumber(382, NoContraBrokers.Length);
				for (int i = 0; i < NoContraBrokers.Length; i++)
				{
					((IFixEncoder)NoContraBrokers[i]).Encode(writer);
				}
			}
		}
	}
}
