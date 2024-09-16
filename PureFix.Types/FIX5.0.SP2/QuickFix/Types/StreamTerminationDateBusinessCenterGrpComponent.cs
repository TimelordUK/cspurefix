using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamTerminationDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40961, Offset = 0, Required = false)]
		public NoStreamTerminationDateBusinessCenters[]? NoStreamTerminationDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStreamTerminationDateBusinessCenters is not null && NoStreamTerminationDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40961, NoStreamTerminationDateBusinessCenters.Length);
				for (int i = 0; i < NoStreamTerminationDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoStreamTerminationDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStreamTerminationDateBusinessCenters") is IMessageView viewNoStreamTerminationDateBusinessCenters)
			{
				var count = viewNoStreamTerminationDateBusinessCenters.GroupCount();
				NoStreamTerminationDateBusinessCenters = new NoStreamTerminationDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoStreamTerminationDateBusinessCenters[i] = new();
					((IFixParser)NoStreamTerminationDateBusinessCenters[i]).Parse(viewNoStreamTerminationDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStreamTerminationDateBusinessCenters":
					value = NoStreamTerminationDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
	}
}
