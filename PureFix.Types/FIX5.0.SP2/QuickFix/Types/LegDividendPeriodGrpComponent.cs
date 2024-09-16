using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegDividendPeriodGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42366, Offset = 0, Required = false)]
		public NoLegDividendPeriods[]? NoLegDividendPeriods {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegDividendPeriods is not null && NoLegDividendPeriods.Length != 0)
			{
				writer.WriteWholeNumber(42366, NoLegDividendPeriods.Length);
				for (int i = 0; i < NoLegDividendPeriods.Length; i++)
				{
					((IFixEncoder)NoLegDividendPeriods[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegDividendPeriods") is IMessageView viewNoLegDividendPeriods)
			{
				var count = viewNoLegDividendPeriods.GroupCount();
				NoLegDividendPeriods = new NoLegDividendPeriods[count];
				for (int i = 0; i < count; i++)
				{
					NoLegDividendPeriods[i] = new();
					((IFixParser)NoLegDividendPeriods[i]).Parse(viewNoLegDividendPeriods.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegDividendPeriods":
					value = NoLegDividendPeriods;
					break;
				default: return false;
			}
			return true;
		}
	}
}
