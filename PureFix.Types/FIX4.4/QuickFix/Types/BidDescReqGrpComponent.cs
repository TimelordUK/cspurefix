using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class BidDescReqGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 398, Offset = 0, Required = false)]
		public NoBidDescriptors[]? NoBidDescriptors {get; set;}
		
		
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
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoBidDescriptors") is IMessageView viewNoBidDescriptors)
			{
				var count = viewNoBidDescriptors.GroupCount();
				NoBidDescriptors = new NoBidDescriptors[count];
				for (int i = 0; i < count; i++)
				{
					NoBidDescriptors[i] = new();
					((IFixParser)NoBidDescriptors[i]).Parse(viewNoBidDescriptors.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoBidDescriptors":
					value = NoBidDescriptors;
					break;
				default: return false;
			}
			return true;
		}
	}
}
