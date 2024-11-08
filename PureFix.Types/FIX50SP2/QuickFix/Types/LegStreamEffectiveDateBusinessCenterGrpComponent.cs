using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegStreamEffectiveDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40942, Offset = 0, Required = false)]
		public IOINoLegStreamEffectiveDateBusinessCenters[]? NoLegStreamEffectiveDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStreamEffectiveDateBusinessCenters is not null && NoLegStreamEffectiveDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40942, NoLegStreamEffectiveDateBusinessCenters.Length);
				for (int i = 0; i < NoLegStreamEffectiveDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegStreamEffectiveDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegStreamEffectiveDateBusinessCenters") is IMessageView viewNoLegStreamEffectiveDateBusinessCenters)
			{
				var count = viewNoLegStreamEffectiveDateBusinessCenters.GroupCount();
				NoLegStreamEffectiveDateBusinessCenters = new IOINoLegStreamEffectiveDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegStreamEffectiveDateBusinessCenters[i] = new();
					((IFixParser)NoLegStreamEffectiveDateBusinessCenters[i]).Parse(viewNoLegStreamEffectiveDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegStreamEffectiveDateBusinessCenters":
					value = NoLegStreamEffectiveDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegStreamEffectiveDateBusinessCenters = null;
		}
	}
}
