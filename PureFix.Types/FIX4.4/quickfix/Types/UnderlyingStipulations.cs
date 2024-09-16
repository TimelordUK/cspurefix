using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class UnderlyingStipulations : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 887, Offset = 0, Required = false)]
		public UnderlyingStipulationsNoUnderlyingStips[]? NoUnderlyingStips { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStips is not null && NoUnderlyingStips.Length != 0)
			{
				writer.WriteWholeNumber(887, NoUnderlyingStips.Length);
				for (int i = 0; i < NoUnderlyingStips.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStips[i]).Encode(writer);
				}
			}
		}
	}
}
