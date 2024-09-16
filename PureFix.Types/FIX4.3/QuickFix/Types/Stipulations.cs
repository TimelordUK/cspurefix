using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX43.QuickFix.Types;

namespace PureFix.Types.FIX43.QuickFix.Types
{
	public sealed partial class Stipulations : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 232, Offset = 0, Required = false)]
		public StipulationsNoStipulations[]? NoStipulations { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStipulations is not null && NoStipulations.Length != 0)
			{
				writer.WriteWholeNumber(232, NoStipulations.Length);
				for (int i = 0; i < NoStipulations.Length; i++)
				{
					((IFixEncoder)NoStipulations[i]).Encode(writer);
				}
			}
		}
	}
}
