using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegReturnRateValuationDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42569, Offset = 0, Required = false)]
		public NoLegReturnRateValuationDateBusinessCenters[]? NoLegReturnRateValuationDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegReturnRateValuationDateBusinessCenters is not null && NoLegReturnRateValuationDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42569, NoLegReturnRateValuationDateBusinessCenters.Length);
				for (int i = 0; i < NoLegReturnRateValuationDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegReturnRateValuationDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegReturnRateValuationDateBusinessCenters") is IMessageView viewNoLegReturnRateValuationDateBusinessCenters)
			{
				var count = viewNoLegReturnRateValuationDateBusinessCenters.GroupCount();
				NoLegReturnRateValuationDateBusinessCenters = new NoLegReturnRateValuationDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegReturnRateValuationDateBusinessCenters[i] = new();
					((IFixParser)NoLegReturnRateValuationDateBusinessCenters[i]).Parse(viewNoLegReturnRateValuationDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegReturnRateValuationDateBusinessCenters":
					value = NoLegReturnRateValuationDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegReturnRateValuationDateBusinessCenters = null;
		}
	}
}
