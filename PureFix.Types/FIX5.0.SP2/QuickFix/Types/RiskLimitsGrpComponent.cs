using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RiskLimitsGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1669, Offset = 0, Required = false)]
		public NoRiskLimits[]? NoRiskLimits {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRiskLimits is not null && NoRiskLimits.Length != 0)
			{
				writer.WriteWholeNumber(1669, NoRiskLimits.Length);
				for (int i = 0; i < NoRiskLimits.Length; i++)
				{
					((IFixEncoder)NoRiskLimits[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRiskLimits") is IMessageView viewNoRiskLimits)
			{
				var count = viewNoRiskLimits.GroupCount();
				NoRiskLimits = new NoRiskLimits[count];
				for (int i = 0; i < count; i++)
				{
					NoRiskLimits[i] = new();
					((IFixParser)NoRiskLimits[i]).Parse(viewNoRiskLimits.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRiskLimits":
					value = NoRiskLimits;
					break;
				default: return false;
			}
			return true;
		}
	}
}
