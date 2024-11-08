using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingCashSettlTermGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42041, Offset = 0, Required = false)]
		public IOINoUnderlyingCashSettlTerms[]? NoUnderlyingCashSettlTerms {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingCashSettlTerms is not null && NoUnderlyingCashSettlTerms.Length != 0)
			{
				writer.WriteWholeNumber(42041, NoUnderlyingCashSettlTerms.Length);
				for (int i = 0; i < NoUnderlyingCashSettlTerms.Length; i++)
				{
					((IFixEncoder)NoUnderlyingCashSettlTerms[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingCashSettlTerms") is IMessageView viewNoUnderlyingCashSettlTerms)
			{
				var count = viewNoUnderlyingCashSettlTerms.GroupCount();
				NoUnderlyingCashSettlTerms = new IOINoUnderlyingCashSettlTerms[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingCashSettlTerms[i] = new();
					((IFixParser)NoUnderlyingCashSettlTerms[i]).Parse(viewNoUnderlyingCashSettlTerms.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingCashSettlTerms":
					value = NoUnderlyingCashSettlTerms;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingCashSettlTerms = null;
		}
	}
}
