using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MarginAmountComponent : IFixComponent
	{
		[Group(NoOfTag = 1643, Offset = 0, Required = false)]
		public MarginRequirementReportNoMarginAmt[]? NoMarginAmt {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMarginAmt is not null && NoMarginAmt.Length != 0)
			{
				writer.WriteWholeNumber(1643, NoMarginAmt.Length);
				for (int i = 0; i < NoMarginAmt.Length; i++)
				{
					((IFixEncoder)NoMarginAmt[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMarginAmt") is IMessageView viewNoMarginAmt)
			{
				var count = viewNoMarginAmt.GroupCount();
				NoMarginAmt = new MarginRequirementReportNoMarginAmt[count];
				for (int i = 0; i < count; i++)
				{
					NoMarginAmt[i] = new();
					((IFixParser)NoMarginAmt[i]).Parse(viewNoMarginAmt.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMarginAmt":
					value = NoMarginAmt;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMarginAmt = null;
		}
	}
}
