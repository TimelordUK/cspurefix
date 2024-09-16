using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class TrdCollGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 897, Offset = 0, Required = false)]
		public TrdCollGrpNoTrades[]? NoTrades { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTrades is not null && NoTrades.Length != 0)
			{
				writer.WriteWholeNumber(897, NoTrades.Length);
				for (int i = 0; i < NoTrades.Length; i++)
				{
					((IFixEncoder)NoTrades[i]).Encode(writer);
				}
			}
		}
	}
}
