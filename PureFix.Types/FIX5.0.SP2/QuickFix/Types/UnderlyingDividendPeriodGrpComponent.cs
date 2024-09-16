using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingDividendPeriodGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42862, Offset = 0, Required = false)]
		public NoUnderlyingDividendPeriods[]? NoUnderlyingDividendPeriods {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingDividendPeriods is not null && NoUnderlyingDividendPeriods.Length != 0)
			{
				writer.WriteWholeNumber(42862, NoUnderlyingDividendPeriods.Length);
				for (int i = 0; i < NoUnderlyingDividendPeriods.Length; i++)
				{
					((IFixEncoder)NoUnderlyingDividendPeriods[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingDividendPeriods") is IMessageView viewNoUnderlyingDividendPeriods)
			{
				var count = viewNoUnderlyingDividendPeriods.GroupCount();
				NoUnderlyingDividendPeriods = new NoUnderlyingDividendPeriods[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingDividendPeriods[i] = new();
					((IFixParser)NoUnderlyingDividendPeriods[i]).Parse(viewNoUnderlyingDividendPeriods.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingDividendPeriods":
					value = NoUnderlyingDividendPeriods;
					break;
				default: return false;
			}
			return true;
		}
	}
}
