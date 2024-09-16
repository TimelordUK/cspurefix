using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class CollateralAmountGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1703, Offset = 0, Required = false)]
		public NoCollateralAmounts[]? NoCollateralAmounts {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoCollateralAmounts is not null && NoCollateralAmounts.Length != 0)
			{
				writer.WriteWholeNumber(1703, NoCollateralAmounts.Length);
				for (int i = 0; i < NoCollateralAmounts.Length; i++)
				{
					((IFixEncoder)NoCollateralAmounts[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoCollateralAmounts") is IMessageView viewNoCollateralAmounts)
			{
				var count = viewNoCollateralAmounts.GroupCount();
				NoCollateralAmounts = new NoCollateralAmounts[count];
				for (int i = 0; i < count; i++)
				{
					NoCollateralAmounts[i] = new();
					((IFixParser)NoCollateralAmounts[i]).Parse(viewNoCollateralAmounts.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoCollateralAmounts":
					value = NoCollateralAmounts;
					break;
				default: return false;
			}
			return true;
		}
	}
}
