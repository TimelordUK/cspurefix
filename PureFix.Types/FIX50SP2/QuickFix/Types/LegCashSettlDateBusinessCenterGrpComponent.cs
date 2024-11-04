using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegCashSettlDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42306, Offset = 0, Required = false)]
		public NoLegCashSettlDateBusinessCenters[]? NoLegCashSettlDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegCashSettlDateBusinessCenters is not null && NoLegCashSettlDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42306, NoLegCashSettlDateBusinessCenters.Length);
				for (int i = 0; i < NoLegCashSettlDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegCashSettlDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegCashSettlDateBusinessCenters") is IMessageView viewNoLegCashSettlDateBusinessCenters)
			{
				var count = viewNoLegCashSettlDateBusinessCenters.GroupCount();
				NoLegCashSettlDateBusinessCenters = new NoLegCashSettlDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegCashSettlDateBusinessCenters[i] = new();
					((IFixParser)NoLegCashSettlDateBusinessCenters[i]).Parse(viewNoLegCashSettlDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegCashSettlDateBusinessCenters":
					value = NoLegCashSettlDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegCashSettlDateBusinessCenters = null;
		}
	}
}
