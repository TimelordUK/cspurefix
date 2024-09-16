using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RiskWarningLevelGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1559, Offset = 0, Required = false)]
		public NoRiskWarningLevels[]? NoRiskWarningLevels {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRiskWarningLevels is not null && NoRiskWarningLevels.Length != 0)
			{
				writer.WriteWholeNumber(1559, NoRiskWarningLevels.Length);
				for (int i = 0; i < NoRiskWarningLevels.Length; i++)
				{
					((IFixEncoder)NoRiskWarningLevels[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRiskWarningLevels") is IMessageView viewNoRiskWarningLevels)
			{
				var count = viewNoRiskWarningLevels.GroupCount();
				NoRiskWarningLevels = new NoRiskWarningLevels[count];
				for (int i = 0; i < count; i++)
				{
					NoRiskWarningLevels[i] = new();
					((IFixParser)NoRiskWarningLevels[i]).Parse(viewNoRiskWarningLevels.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRiskWarningLevels":
					value = NoRiskWarningLevels;
					break;
				default: return false;
			}
			return true;
		}
	}
}
