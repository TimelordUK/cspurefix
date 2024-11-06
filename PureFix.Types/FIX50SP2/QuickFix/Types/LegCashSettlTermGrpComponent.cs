using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegCashSettlTermGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41344, Offset = 0, Required = false)]
		public IOINoLegCashSettlTerms[]? NoLegCashSettlTerms {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegCashSettlTerms is not null && NoLegCashSettlTerms.Length != 0)
			{
				writer.WriteWholeNumber(41344, NoLegCashSettlTerms.Length);
				for (int i = 0; i < NoLegCashSettlTerms.Length; i++)
				{
					((IFixEncoder)NoLegCashSettlTerms[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegCashSettlTerms") is IMessageView viewNoLegCashSettlTerms)
			{
				var count = viewNoLegCashSettlTerms.GroupCount();
				NoLegCashSettlTerms = new IOINoLegCashSettlTerms[count];
				for (int i = 0; i < count; i++)
				{
					NoLegCashSettlTerms[i] = new();
					((IFixParser)NoLegCashSettlTerms[i]).Parse(viewNoLegCashSettlTerms.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegCashSettlTerms":
					value = NoLegCashSettlTerms;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegCashSettlTerms = null;
		}
	}
}
