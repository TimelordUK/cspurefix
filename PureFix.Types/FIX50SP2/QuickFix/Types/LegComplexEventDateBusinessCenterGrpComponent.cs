using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegComplexEventDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41387, Offset = 0, Required = false)]
		public IOINoLegComplexEventDateBusinessCenters[]? NoLegComplexEventDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegComplexEventDateBusinessCenters is not null && NoLegComplexEventDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(41387, NoLegComplexEventDateBusinessCenters.Length);
				for (int i = 0; i < NoLegComplexEventDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegComplexEventDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegComplexEventDateBusinessCenters") is IMessageView viewNoLegComplexEventDateBusinessCenters)
			{
				var count = viewNoLegComplexEventDateBusinessCenters.GroupCount();
				NoLegComplexEventDateBusinessCenters = new IOINoLegComplexEventDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegComplexEventDateBusinessCenters[i] = new();
					((IFixParser)NoLegComplexEventDateBusinessCenters[i]).Parse(viewNoLegComplexEventDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegComplexEventDateBusinessCenters":
					value = NoLegComplexEventDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegComplexEventDateBusinessCenters = null;
		}
	}
}
