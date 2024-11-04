using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingDividendPeriodBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42882, Offset = 0, Required = false)]
		public NoUnderlyingDividendPeriodBusinessCenters[]? NoUnderlyingDividendPeriodBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingDividendPeriodBusinessCenters is not null && NoUnderlyingDividendPeriodBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42882, NoUnderlyingDividendPeriodBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingDividendPeriodBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingDividendPeriodBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingDividendPeriodBusinessCenters") is IMessageView viewNoUnderlyingDividendPeriodBusinessCenters)
			{
				var count = viewNoUnderlyingDividendPeriodBusinessCenters.GroupCount();
				NoUnderlyingDividendPeriodBusinessCenters = new NoUnderlyingDividendPeriodBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingDividendPeriodBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingDividendPeriodBusinessCenters[i]).Parse(viewNoUnderlyingDividendPeriodBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingDividendPeriodBusinessCenters":
					value = NoUnderlyingDividendPeriodBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingDividendPeriodBusinessCenters = null;
		}
	}
}
