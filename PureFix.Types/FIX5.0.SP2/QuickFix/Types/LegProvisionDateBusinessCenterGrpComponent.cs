using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProvisionDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40939, Offset = 0, Required = false)]
		public NoLegProvisionDateBusinessCenters[]? NoLegProvisionDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProvisionDateBusinessCenters is not null && NoLegProvisionDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40939, NoLegProvisionDateBusinessCenters.Length);
				for (int i = 0; i < NoLegProvisionDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegProvisionDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProvisionDateBusinessCenters") is IMessageView viewNoLegProvisionDateBusinessCenters)
			{
				var count = viewNoLegProvisionDateBusinessCenters.GroupCount();
				NoLegProvisionDateBusinessCenters = new NoLegProvisionDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProvisionDateBusinessCenters[i] = new();
					((IFixParser)NoLegProvisionDateBusinessCenters[i]).Parse(viewNoLegProvisionDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProvisionDateBusinessCenters":
					value = NoLegProvisionDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegProvisionDateBusinessCenters = null;
		}
	}
}
