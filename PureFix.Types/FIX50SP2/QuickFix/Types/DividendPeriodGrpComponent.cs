using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class DividendPeriodGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42274, Offset = 0, Required = false)]
		public IOINoDividendPeriods[]? NoDividendPeriods {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoDividendPeriods is not null && NoDividendPeriods.Length != 0)
			{
				writer.WriteWholeNumber(42274, NoDividendPeriods.Length);
				for (int i = 0; i < NoDividendPeriods.Length; i++)
				{
					((IFixEncoder)NoDividendPeriods[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoDividendPeriods") is IMessageView viewNoDividendPeriods)
			{
				var count = viewNoDividendPeriods.GroupCount();
				NoDividendPeriods = new IOINoDividendPeriods[count];
				for (int i = 0; i < count; i++)
				{
					NoDividendPeriods[i] = new();
					((IFixParser)NoDividendPeriods[i]).Parse(viewNoDividendPeriods.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoDividendPeriods":
					value = NoDividendPeriods;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoDividendPeriods = null;
		}
	}
}
