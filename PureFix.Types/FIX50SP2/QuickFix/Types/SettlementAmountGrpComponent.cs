using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SettlementAmountGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1700, Offset = 0, Required = false)]
		public AccountSummaryReportNoSettlementAmounts[]? NoSettlementAmounts {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSettlementAmounts is not null && NoSettlementAmounts.Length != 0)
			{
				writer.WriteWholeNumber(1700, NoSettlementAmounts.Length);
				for (int i = 0; i < NoSettlementAmounts.Length; i++)
				{
					((IFixEncoder)NoSettlementAmounts[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSettlementAmounts") is IMessageView viewNoSettlementAmounts)
			{
				var count = viewNoSettlementAmounts.GroupCount();
				NoSettlementAmounts = new AccountSummaryReportNoSettlementAmounts[count];
				for (int i = 0; i < count; i++)
				{
					NoSettlementAmounts[i] = new();
					((IFixParser)NoSettlementAmounts[i]).Parse(viewNoSettlementAmounts.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSettlementAmounts":
					value = NoSettlementAmounts;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoSettlementAmounts = null;
		}
	}
}
