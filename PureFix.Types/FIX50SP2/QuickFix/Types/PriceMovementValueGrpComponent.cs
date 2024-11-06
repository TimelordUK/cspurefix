using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PriceMovementValueGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1920, Offset = 0, Required = false)]
		public SecurityListNoPriceMovementValues[]? NoPriceMovementValues {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPriceMovementValues is not null && NoPriceMovementValues.Length != 0)
			{
				writer.WriteWholeNumber(1920, NoPriceMovementValues.Length);
				for (int i = 0; i < NoPriceMovementValues.Length; i++)
				{
					((IFixEncoder)NoPriceMovementValues[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPriceMovementValues") is IMessageView viewNoPriceMovementValues)
			{
				var count = viewNoPriceMovementValues.GroupCount();
				NoPriceMovementValues = new SecurityListNoPriceMovementValues[count];
				for (int i = 0; i < count; i++)
				{
					NoPriceMovementValues[i] = new();
					((IFixParser)NoPriceMovementValues[i]).Parse(viewNoPriceMovementValues.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPriceMovementValues":
					value = NoPriceMovementValues;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPriceMovementValues = null;
		}
	}
}
