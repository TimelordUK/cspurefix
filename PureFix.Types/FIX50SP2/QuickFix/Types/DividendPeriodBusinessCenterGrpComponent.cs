using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class DividendPeriodBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42294, Offset = 0, Required = false)]
		public NoDividendPeriodBusinessCenters[]? NoDividendPeriodBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDividendPeriodBusinessCenters is not null && NoDividendPeriodBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42294, NoDividendPeriodBusinessCenters.Length);
				for (int i = 0; i < NoDividendPeriodBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoDividendPeriodBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDividendPeriodBusinessCenters") is IMessageView viewNoDividendPeriodBusinessCenters)
			{
				var count = viewNoDividendPeriodBusinessCenters.GroupCount();
				NoDividendPeriodBusinessCenters = new NoDividendPeriodBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoDividendPeriodBusinessCenters[i] = new();
					((IFixParser)NoDividendPeriodBusinessCenters[i]).Parse(viewNoDividendPeriodBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDividendPeriodBusinessCenters":
					value = NoDividendPeriodBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoDividendPeriodBusinessCenters = null;
		}
	}
}
