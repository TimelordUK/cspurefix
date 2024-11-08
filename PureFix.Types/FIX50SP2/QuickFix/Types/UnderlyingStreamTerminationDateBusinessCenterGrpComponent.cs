using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamTerminationDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40976, Offset = 0, Required = false)]
		public IOINoUnderlyingStreamTerminationDateBusinessCenters[]? NoUnderlyingStreamTerminationDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreamTerminationDateBusinessCenters is not null && NoUnderlyingStreamTerminationDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(40976, NoUnderlyingStreamTerminationDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingStreamTerminationDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreamTerminationDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreamTerminationDateBusinessCenters") is IMessageView viewNoUnderlyingStreamTerminationDateBusinessCenters)
			{
				var count = viewNoUnderlyingStreamTerminationDateBusinessCenters.GroupCount();
				NoUnderlyingStreamTerminationDateBusinessCenters = new IOINoUnderlyingStreamTerminationDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreamTerminationDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingStreamTerminationDateBusinessCenters[i]).Parse(viewNoUnderlyingStreamTerminationDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreamTerminationDateBusinessCenters":
					value = NoUnderlyingStreamTerminationDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingStreamTerminationDateBusinessCenters = null;
		}
	}
}
