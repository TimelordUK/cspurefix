using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class IOIQualGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 199, Offset = 0, Required = false)]
		public IOIQualGrpNoIOIQualifiers[]? NoIOIQualifiers { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoIOIQualifiers is not null && NoIOIQualifiers.Length != 0)
			{
				writer.WriteWholeNumber(199, NoIOIQualifiers.Length);
				for (int i = 0; i < NoIOIQualifiers.Length; i++)
				{
					((IFixEncoder)NoIOIQualifiers[i]).Encode(writer);
				}
			}
		}
	}
}
