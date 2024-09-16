using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class OrdListStatGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 73, Offset = 0, Required = true)]
		public OrdListStatGrpNoOrders[]? NoOrders { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoOrders is not null && FixValidator.IsValid(NoOrders, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoOrders is not null && NoOrders.Length != 0)
			{
				writer.WriteWholeNumber(73, NoOrders.Length);
				for (int i = 0; i < NoOrders.Length; i++)
				{
					((IFixEncoder)NoOrders[i]).Encode(writer);
				}
			}
		}
	}
}
