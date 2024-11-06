using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamAssignmentReportNoAsgnReqs : IFixGroup
	{
		[Component(Offset = 0, Required = false)]
		public PartiesComponent? Parties {get; set;}
		
		[Component(Offset = 1, Required = false)]
		public StrmAsgnRptInstrmtGrpComponent? StrmAsgnRptInstrmtGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (Parties is not null) ((IFixEncoder)Parties).Encode(writer);
			if (StrmAsgnRptInstrmtGrp is not null) ((IFixEncoder)StrmAsgnRptInstrmtGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("Parties") is IMessageView viewParties)
			{
				Parties = new();
				((IFixParser)Parties).Parse(viewParties);
			}
			if (view.GetView("StrmAsgnRptInstrmtGrp") is IMessageView viewStrmAsgnRptInstrmtGrp)
			{
				StrmAsgnRptInstrmtGrp = new();
				((IFixParser)StrmAsgnRptInstrmtGrp).Parse(viewStrmAsgnRptInstrmtGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "Parties":
					value = Parties;
					break;
				case "StrmAsgnRptInstrmtGrp":
					value = StrmAsgnRptInstrmtGrp;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			((IFixReset?)Parties)?.Reset();
			((IFixReset?)StrmAsgnRptInstrmtGrp)?.Reset();
		}
	}
}
