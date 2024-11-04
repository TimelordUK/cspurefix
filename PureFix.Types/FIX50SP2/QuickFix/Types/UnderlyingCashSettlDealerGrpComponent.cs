using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingCashSettlDealerGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42039, Offset = 0, Required = false)]
		public NoUnderlyingCashSettlDealers[]? NoUnderlyingCashSettlDealers {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingCashSettlDealers is not null && NoUnderlyingCashSettlDealers.Length != 0)
			{
				writer.WriteWholeNumber(42039, NoUnderlyingCashSettlDealers.Length);
				for (int i = 0; i < NoUnderlyingCashSettlDealers.Length; i++)
				{
					((IFixEncoder)NoUnderlyingCashSettlDealers[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingCashSettlDealers") is IMessageView viewNoUnderlyingCashSettlDealers)
			{
				var count = viewNoUnderlyingCashSettlDealers.GroupCount();
				NoUnderlyingCashSettlDealers = new NoUnderlyingCashSettlDealers[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingCashSettlDealers[i] = new();
					((IFixParser)NoUnderlyingCashSettlDealers[i]).Parse(viewNoUnderlyingCashSettlDealers.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingCashSettlDealers":
					value = NoUnderlyingCashSettlDealers;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingCashSettlDealers = null;
		}
	}
}
