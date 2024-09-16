using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PartyRiskLimitsUpdateGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1677, Offset = 0, Required = false)]
		public NoPartyRiskLimits[]? NoPartyRiskLimits {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPartyRiskLimits is not null && NoPartyRiskLimits.Length != 0)
			{
				writer.WriteWholeNumber(1677, NoPartyRiskLimits.Length);
				for (int i = 0; i < NoPartyRiskLimits.Length; i++)
				{
					((IFixEncoder)NoPartyRiskLimits[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPartyRiskLimits") is IMessageView viewNoPartyRiskLimits)
			{
				var count = viewNoPartyRiskLimits.GroupCount();
				NoPartyRiskLimits = new NoPartyRiskLimits[count];
				for (int i = 0; i < count; i++)
				{
					NoPartyRiskLimits[i] = new();
					((IFixParser)NoPartyRiskLimits[i]).Parse(viewNoPartyRiskLimits.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPartyRiskLimits":
					value = NoPartyRiskLimits;
					break;
				default: return false;
			}
			return true;
		}
	}
}
