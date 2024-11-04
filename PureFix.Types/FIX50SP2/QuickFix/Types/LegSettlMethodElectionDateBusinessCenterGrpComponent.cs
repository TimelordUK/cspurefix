using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegSettlMethodElectionDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42581, Offset = 0, Required = false)]
		public NoLegSettlMethodElectionDateBusinessCenters[]? NoLegSettlMethodElectionDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegSettlMethodElectionDateBusinessCenters is not null && NoLegSettlMethodElectionDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(42581, NoLegSettlMethodElectionDateBusinessCenters.Length);
				for (int i = 0; i < NoLegSettlMethodElectionDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoLegSettlMethodElectionDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegSettlMethodElectionDateBusinessCenters") is IMessageView viewNoLegSettlMethodElectionDateBusinessCenters)
			{
				var count = viewNoLegSettlMethodElectionDateBusinessCenters.GroupCount();
				NoLegSettlMethodElectionDateBusinessCenters = new NoLegSettlMethodElectionDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoLegSettlMethodElectionDateBusinessCenters[i] = new();
					((IFixParser)NoLegSettlMethodElectionDateBusinessCenters[i]).Parse(viewNoLegSettlMethodElectionDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegSettlMethodElectionDateBusinessCenters":
					value = NoLegSettlMethodElectionDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegSettlMethodElectionDateBusinessCenters = null;
		}
	}
}
