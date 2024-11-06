using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40923, Offset = 0, Required = false)]
		public IOINoLegBusinessCenters[]? NoLegBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegBusinessCenters is not null && NoLegBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40923, NoLegBusinessCenters.Length);
				for (int i = 0; i < NoLegBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegBusinessCenters") is IMessageView viewNoLegBusinessCenters)
			{
				var count = viewNoLegBusinessCenters.GroupCount();
				NoLegBusinessCenters = new IOINoLegBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegBusinessCenters[i] = new();
					((IFixParser)NoLegBusinessCenters[i]).Parse(viewNoLegBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegBusinessCenters":
					value = NoLegBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegBusinessCenters = null;
		}
	}
}
