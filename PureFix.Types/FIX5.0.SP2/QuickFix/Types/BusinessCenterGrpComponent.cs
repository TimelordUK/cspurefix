using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class BusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40278, Offset = 0, Required = false)]
		public NoBusinessCenters[]? NoBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoBusinessCenters is not null && NoBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40278, NoBusinessCenters.Length);
				for (int i = 0; i < NoBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoBusinessCenters") is IMessageView viewNoBusinessCenters)
			{
				var count = viewNoBusinessCenters.GroupCount();
				NoBusinessCenters = new NoBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoBusinessCenters[i] = new();
					((IFixParser)NoBusinessCenters[i]).Parse(viewNoBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoBusinessCenters":
					value = NoBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoBusinessCenters = null;
		}
	}
}
