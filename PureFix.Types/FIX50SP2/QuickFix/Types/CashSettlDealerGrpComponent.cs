using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class CashSettlDealerGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40277, Offset = 0, Required = false)]
		public NoCashSettlDealers[]? NoCashSettlDealers {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoCashSettlDealers is not null && NoCashSettlDealers.Length != 0)
			{
				writer.WriteWholeNumber(40277, NoCashSettlDealers.Length);
				for (int i = 0; i < NoCashSettlDealers.Length; i++)
				{
					((IFixEncoder)NoCashSettlDealers[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoCashSettlDealers") is IMessageView viewNoCashSettlDealers)
			{
				var count = viewNoCashSettlDealers.GroupCount();
				NoCashSettlDealers = new NoCashSettlDealers[count];
				for (int i = 0; i < count; i++)
				{
					NoCashSettlDealers[i] = new();
					((IFixParser)NoCashSettlDealers[i]).Parse(viewNoCashSettlDealers.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoCashSettlDealers":
					value = NoCashSettlDealers;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoCashSettlDealers = null;
		}
	}
}
