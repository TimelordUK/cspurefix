using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class RoutingGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 215, Offset = 0, Required = false)]
		public RoutingGrpNoRoutingIDs[]? NoRoutingIDs { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRoutingIDs is not null && NoRoutingIDs.Length != 0)
			{
				writer.WriteWholeNumber(215, NoRoutingIDs.Length);
				for (int i = 0; i < NoRoutingIDs.Length; i++)
				{
					((IFixEncoder)NoRoutingIDs[i]).Encode(writer);
				}
			}
		}
	}
}
