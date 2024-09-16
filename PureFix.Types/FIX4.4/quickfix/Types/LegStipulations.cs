using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class LegStipulations : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 683, Offset = 0, Required = false)]
		public LegStipulationsNoLegStipulations[]? NoLegStipulations { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStipulations is not null && NoLegStipulations.Length != 0)
			{
				writer.WriteWholeNumber(683, NoLegStipulations.Length);
				for (int i = 0; i < NoLegStipulations.Length; i++)
				{
					((IFixEncoder)NoLegStipulations[i]).Encode(writer);
				}
			}
		}
	}
}
