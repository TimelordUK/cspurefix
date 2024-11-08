using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegStreamTerminationDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40943, Offset = 0, Required = false)]
		public IOINoLegStreamTerminationDateBusinessCenters[]? NoLegStreamTerminationDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStreamTerminationDateBusinessCenters is not null && NoLegStreamTerminationDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40943, NoLegStreamTerminationDateBusinessCenters.Length);
				for (int i = 0; i < NoLegStreamTerminationDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegStreamTerminationDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegStreamTerminationDateBusinessCenters") is IMessageView viewNoLegStreamTerminationDateBusinessCenters)
			{
				var count = viewNoLegStreamTerminationDateBusinessCenters.GroupCount();
				NoLegStreamTerminationDateBusinessCenters = new IOINoLegStreamTerminationDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegStreamTerminationDateBusinessCenters[i] = new();
					((IFixParser)NoLegStreamTerminationDateBusinessCenters[i]).Parse(viewNoLegStreamTerminationDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegStreamTerminationDateBusinessCenters":
					value = NoLegStreamTerminationDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegStreamTerminationDateBusinessCenters = null;
		}
	}
}
