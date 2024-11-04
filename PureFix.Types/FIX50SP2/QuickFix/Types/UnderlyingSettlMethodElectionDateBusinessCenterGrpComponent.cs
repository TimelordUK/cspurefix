using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingSettlMethodElectionDateBusinessCenterGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 43074, Offset = 0, Required = false)]
		public NoUnderlyingSettlMethodElectionDateBusinessCenters[]? NoUnderlyingSettlMethodElectionDateBusinessCenters {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingSettlMethodElectionDateBusinessCenters is not null && NoUnderlyingSettlMethodElectionDateBusinessCenters.Length != 0)
			{
				writer.WriteWholeNumber(43074, NoUnderlyingSettlMethodElectionDateBusinessCenters.Length);
				for (int i = 0; i < NoUnderlyingSettlMethodElectionDateBusinessCenters.Length; i++)
				{
					((IFixEncoder)NoUnderlyingSettlMethodElectionDateBusinessCenters[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingSettlMethodElectionDateBusinessCenters") is IMessageView viewNoUnderlyingSettlMethodElectionDateBusinessCenters)
			{
				var count = viewNoUnderlyingSettlMethodElectionDateBusinessCenters.GroupCount();
				NoUnderlyingSettlMethodElectionDateBusinessCenters = new NoUnderlyingSettlMethodElectionDateBusinessCenters[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingSettlMethodElectionDateBusinessCenters[i] = new();
					((IFixParser)NoUnderlyingSettlMethodElectionDateBusinessCenters[i]).Parse(viewNoUnderlyingSettlMethodElectionDateBusinessCenters.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingSettlMethodElectionDateBusinessCenters":
					value = NoUnderlyingSettlMethodElectionDateBusinessCenters;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingSettlMethodElectionDateBusinessCenters = null;
		}
	}
}
