using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RiskLimitTypesGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1529, Offset = 0, Required = false)]
		public PartyRiskLimitsReportNoRiskLimitTypes[]? NoRiskLimitTypes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRiskLimitTypes is not null && NoRiskLimitTypes.Length != 0)
			{
				writer.WriteWholeNumber(1529, NoRiskLimitTypes.Length);
				for (int i = 0; i < NoRiskLimitTypes.Length; i++)
				{
					((IFixEncoder)NoRiskLimitTypes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRiskLimitTypes") is IMessageView viewNoRiskLimitTypes)
			{
				var count = viewNoRiskLimitTypes.GroupCount();
				NoRiskLimitTypes = new PartyRiskLimitsReportNoRiskLimitTypes[count];
				for (int i = 0; i < count; i++)
				{
					NoRiskLimitTypes[i] = new();
					((IFixParser)NoRiskLimitTypes[i]).Parse(viewNoRiskLimitTypes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRiskLimitTypes":
					value = NoRiskLimitTypes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoRiskLimitTypes = null;
		}
	}
}
