using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class BidCompReqGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 420, Offset = 0, Required = false)]
		public BidCompReqGrpNoBidComponents[]? NoBidComponents { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoBidComponents is not null && NoBidComponents.Length != 0)
			{
				writer.WriteWholeNumber(420, NoBidComponents.Length);
				for (int i = 0; i < NoBidComponents.Length; i++)
				{
					((IFixEncoder)NoBidComponents[i]).Encode(writer);
				}
			}
		}
	}
}
