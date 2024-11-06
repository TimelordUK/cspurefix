using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class BidCompRspGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 420, Offset = 0, Required = true)]
		public BidResponseNoBidComponents[]? NoBidComponents {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoBidComponents is not null && FixValidator.IsValid(NoBidComponents, in config);
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
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoBidComponents") is IMessageView viewNoBidComponents)
			{
				var count = viewNoBidComponents.GroupCount();
				NoBidComponents = new BidResponseNoBidComponents[count];
				for (int i = 0; i < count; i++)
				{
					NoBidComponents[i] = new();
					((IFixParser)NoBidComponents[i]).Parse(viewNoBidComponents.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoBidComponents":
					value = NoBidComponents;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoBidComponents = null;
		}
	}
}
