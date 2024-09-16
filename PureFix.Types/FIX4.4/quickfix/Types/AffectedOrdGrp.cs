using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class AffectedOrdGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 534, Offset = 0, Required = false)]
		public AffectedOrdGrpNoAffectedOrders[]? NoAffectedOrders { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAffectedOrders is not null && NoAffectedOrders.Length != 0)
			{
				writer.WriteWholeNumber(534, NoAffectedOrders.Length);
				for (int i = 0; i < NoAffectedOrders.Length; i++)
				{
					((IFixEncoder)NoAffectedOrders[i]).Encode(writer);
				}
			}
		}
	}
}
