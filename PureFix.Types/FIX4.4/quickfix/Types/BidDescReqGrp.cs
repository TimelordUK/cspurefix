using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class BidDescReqGrp : IFixValidator, IFixEncoder
	{
		[Group(NoOfTag = 398, Offset = 0, Required = false)]
		public BidDescReqGrpNoBidDescriptors[]? NoBidDescriptors { get; set; }
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoBidDescriptors is not null && NoBidDescriptors.Length != 0)
			{
				writer.WriteWholeNumber(398, NoBidDescriptors.Length);
				for (int i = 0; i < NoBidDescriptors.Length; i++)
				{
					((IFixEncoder)NoBidDescriptors[i]).Encode(writer);
				}
			}
		}
	}
}
