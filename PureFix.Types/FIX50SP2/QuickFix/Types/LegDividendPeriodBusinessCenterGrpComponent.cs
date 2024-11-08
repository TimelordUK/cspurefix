using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegDividendPeriodBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42386, Offset = 0, Required = false)]
		public IOINoLegDividendPeriodBusinessCenters[]? NoLegDividendPeriodBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegDividendPeriodBusinessCenters is not null && NoLegDividendPeriodBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42386, NoLegDividendPeriodBusinessCenters.Length);
				for (int i = 0; i < NoLegDividendPeriodBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegDividendPeriodBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegDividendPeriodBusinessCenters") is IMessageView viewNoLegDividendPeriodBusinessCenters)
			{
				var count = viewNoLegDividendPeriodBusinessCenters.GroupCount();
				NoLegDividendPeriodBusinessCenters = new IOINoLegDividendPeriodBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegDividendPeriodBusinessCenters[i] = new();
					((IFixParser)NoLegDividendPeriodBusinessCenters[i]).Parse(viewNoLegDividendPeriodBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegDividendPeriodBusinessCenters":
					value = NoLegDividendPeriodBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegDividendPeriodBusinessCenters = null;
		}
	}
}
