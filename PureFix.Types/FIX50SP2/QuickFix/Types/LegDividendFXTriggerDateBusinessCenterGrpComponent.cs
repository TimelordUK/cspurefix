using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegDividendFXTriggerDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42364, Offset = 0, Required = false)]
		public IOINoLegDividendFXTriggerDateBusinessCenters[]? NoLegDividendFXTriggerDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegDividendFXTriggerDateBusinessCenters is not null && NoLegDividendFXTriggerDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42364, NoLegDividendFXTriggerDateBusinessCenters.Length);
				for (int i = 0; i < NoLegDividendFXTriggerDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegDividendFXTriggerDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegDividendFXTriggerDateBusinessCenters") is IMessageView viewNoLegDividendFXTriggerDateBusinessCenters)
			{
				var count = viewNoLegDividendFXTriggerDateBusinessCenters.GroupCount();
				NoLegDividendFXTriggerDateBusinessCenters = new IOINoLegDividendFXTriggerDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegDividendFXTriggerDateBusinessCenters[i] = new();
					((IFixParser)NoLegDividendFXTriggerDateBusinessCenters[i]).Parse(viewNoLegDividendFXTriggerDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegDividendFXTriggerDateBusinessCenters":
					value = NoLegDividendFXTriggerDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegDividendFXTriggerDateBusinessCenters = null;
		}
	}
}
