using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SettlMethodElectionDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42775, Offset = 0, Required = false)]
		public NoSettlMethodElectionDateBusinessCenters[]? NoSettlMethodElectionDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSettlMethodElectionDateBusinessCenters is not null && NoSettlMethodElectionDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42775, NoSettlMethodElectionDateBusinessCenters.Length);
				for (int i = 0; i < NoSettlMethodElectionDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoSettlMethodElectionDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSettlMethodElectionDateBusinessCenters") is IMessageView viewNoSettlMethodElectionDateBusinessCenters)
			{
				var count = viewNoSettlMethodElectionDateBusinessCenters.GroupCount();
				NoSettlMethodElectionDateBusinessCenters = new NoSettlMethodElectionDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoSettlMethodElectionDateBusinessCenters[i] = new();
					((IFixParser)NoSettlMethodElectionDateBusinessCenters[i]).Parse(viewNoSettlMethodElectionDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSettlMethodElectionDateBusinessCenters":
					value = NoSettlMethodElectionDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoSettlMethodElectionDateBusinessCenters = null;
		}
	}
}
