using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SideCollateralAmountGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2691, Offset = 0, Required = false)]
		public TradeCaptureReportNoSideCollateralAmounts[]? NoSideCollateralAmounts {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSideCollateralAmounts is not null && NoSideCollateralAmounts.Length != 0)
			{
				writer.WriteWholeNumber(2691, NoSideCollateralAmounts.Length);
				for (int i = 0; i < NoSideCollateralAmounts.Length; i++)
				{
					((IFixEncoder)NoSideCollateralAmounts[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSideCollateralAmounts") is IMessageView viewNoSideCollateralAmounts)
			{
				var count = viewNoSideCollateralAmounts.GroupCount();
				NoSideCollateralAmounts = new TradeCaptureReportNoSideCollateralAmounts[count];
				for (int i = 0; i < count; i++)
				{
					NoSideCollateralAmounts[i] = new();
					((IFixParser)NoSideCollateralAmounts[i]).Parse(viewNoSideCollateralAmounts.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSideCollateralAmounts":
					value = NoSideCollateralAmounts;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoSideCollateralAmounts = null;
		}
	}
}
