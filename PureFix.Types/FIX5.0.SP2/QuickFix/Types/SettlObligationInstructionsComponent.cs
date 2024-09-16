using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SettlObligationInstructionsComponent : IFixComponent
	{
		[Group(NoOfTag = 1165, Offset = 0, Required = false)]
		public NoSettlOblig[]? NoSettlOblig {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSettlOblig is not null && NoSettlOblig.Length != 0)
			{
				writer.WriteWholeNumber(1165, NoSettlOblig.Length);
				for (int i = 0; i < NoSettlOblig.Length; i++)
				{
					((IFixEncoder)NoSettlOblig[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSettlOblig") is IMessageView viewNoSettlOblig)
			{
				var count = viewNoSettlOblig.GroupCount();
				NoSettlOblig = new NoSettlOblig[count];
				for (int i = 0; i < count; i++)
				{
					NoSettlOblig[i] = new();
					((IFixParser)NoSettlOblig[i]).Parse(viewNoSettlOblig.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSettlOblig":
					value = NoSettlOblig;
					break;
				default: return false;
			}
			return true;
		}
	}
}
