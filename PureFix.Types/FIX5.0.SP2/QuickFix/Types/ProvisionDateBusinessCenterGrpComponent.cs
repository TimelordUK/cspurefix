using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProvisionDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40957, Offset = 0, Required = false)]
		public NoProvisionDateBusinessCenters[]? NoProvisionDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProvisionDateBusinessCenters is not null && NoProvisionDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40957, NoProvisionDateBusinessCenters.Length);
				for (int i = 0; i < NoProvisionDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoProvisionDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProvisionDateBusinessCenters") is IMessageView viewNoProvisionDateBusinessCenters)
			{
				var count = viewNoProvisionDateBusinessCenters.GroupCount();
				NoProvisionDateBusinessCenters = new NoProvisionDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoProvisionDateBusinessCenters[i] = new();
					((IFixParser)NoProvisionDateBusinessCenters[i]).Parse(viewNoProvisionDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProvisionDateBusinessCenters":
					value = NoProvisionDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
