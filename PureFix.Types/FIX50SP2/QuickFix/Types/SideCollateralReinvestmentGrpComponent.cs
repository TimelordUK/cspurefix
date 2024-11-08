using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SideCollateralReinvestmentGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2864, Offset = 0, Required = false)]
		public TradeCaptureReportNoSideCollateralReinvestments[]? NoSideCollateralReinvestments {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSideCollateralReinvestments is not null && NoSideCollateralReinvestments.Length != 0)
			{
				writer.WriteWholeNumber(2864, NoSideCollateralReinvestments.Length);
				for (int i = 0; i < NoSideCollateralReinvestments.Length; i++)
				{
					((IFixEncoder)NoSideCollateralReinvestments[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSideCollateralReinvestments") is IMessageView viewNoSideCollateralReinvestments)
			{
				var count = viewNoSideCollateralReinvestments.GroupCount();
				NoSideCollateralReinvestments = new TradeCaptureReportNoSideCollateralReinvestments[count];
				for (int i = 0; i < count; i++)
				{
					NoSideCollateralReinvestments[i] = new();
					((IFixParser)NoSideCollateralReinvestments[i]).Parse(viewNoSideCollateralReinvestments.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSideCollateralReinvestments":
					value = NoSideCollateralReinvestments;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoSideCollateralReinvestments = null;
		}
	}
}
