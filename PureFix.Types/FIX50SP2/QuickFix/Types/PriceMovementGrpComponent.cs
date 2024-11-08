using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PriceMovementGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1919, Offset = 0, Required = false)]
		public SecurityListNoPriceMovements[]? NoPriceMovements {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPriceMovements is not null && NoPriceMovements.Length != 0)
			{
				writer.WriteWholeNumber(1919, NoPriceMovements.Length);
				for (int i = 0; i < NoPriceMovements.Length; i++)
				{
					((IFixEncoder)NoPriceMovements[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPriceMovements") is IMessageView viewNoPriceMovements)
			{
				var count = viewNoPriceMovements.GroupCount();
				NoPriceMovements = new SecurityListNoPriceMovements[count];
				for (int i = 0; i < count; i++)
				{
					NoPriceMovements[i] = new();
					((IFixParser)NoPriceMovements[i]).Parse(viewNoPriceMovements.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPriceMovements":
					value = NoPriceMovements;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPriceMovements = null;
		}
	}
}
