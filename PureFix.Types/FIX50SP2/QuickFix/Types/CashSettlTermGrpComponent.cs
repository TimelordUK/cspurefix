using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class CashSettlTermGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40022, Offset = 0, Required = false)]
		public NoCashSettlTerms[]? NoCashSettlTerms {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoCashSettlTerms is not null && NoCashSettlTerms.Length != 0)
			{
				writer.WriteWholeNumber(40022, NoCashSettlTerms.Length);
				for (int i = 0; i < NoCashSettlTerms.Length; i++)
				{
					((IFixEncoder)NoCashSettlTerms[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoCashSettlTerms") is IMessageView viewNoCashSettlTerms)
			{
				var count = viewNoCashSettlTerms.GroupCount();
				NoCashSettlTerms = new NoCashSettlTerms[count];
				for (int i = 0; i < count; i++)
				{
					NoCashSettlTerms[i] = new();
					((IFixParser)NoCashSettlTerms[i]).Parse(viewNoCashSettlTerms.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoCashSettlTerms":
					value = NoCashSettlTerms;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoCashSettlTerms = null;
		}
	}
}
