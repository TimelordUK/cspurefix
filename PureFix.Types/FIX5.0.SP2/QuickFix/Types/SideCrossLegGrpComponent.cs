using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SideCrossLegGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1829, Offset = 0, Required = false)]
		public NoCrossLegs[]? NoCrossLegs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoCrossLegs is not null && NoCrossLegs.Length != 0)
			{
				writer.WriteWholeNumber(1829, NoCrossLegs.Length);
				for (int i = 0; i < NoCrossLegs.Length; i++)
				{
					((IFixEncoder)NoCrossLegs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoCrossLegs") is IMessageView viewNoCrossLegs)
			{
				var count = viewNoCrossLegs.GroupCount();
				NoCrossLegs = new NoCrossLegs[count];
				for (int i = 0; i < count; i++)
				{
					NoCrossLegs[i] = new();
					((IFixParser)NoCrossLegs[i]).Parse(viewNoCrossLegs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoCrossLegs":
					value = NoCrossLegs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoCrossLegs = null;
		}
	}
}
