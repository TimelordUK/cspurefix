using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegStipulationsComponent : IFixComponent
	{
		[Group(NoOfTag = 683, Offset = 0, Required = false)]
		public IOINoLegStipulations[]? NoLegStipulations {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStipulations is not null && NoLegStipulations.Length != 0)
			{
				writer.WriteWholeNumber(683, NoLegStipulations.Length);
				for (int i = 0; i < NoLegStipulations.Length; i++)
				{
					((IFixEncoder)NoLegStipulations[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegStipulations") is IMessageView viewNoLegStipulations)
			{
				var count = viewNoLegStipulations.GroupCount();
				NoLegStipulations = new IOINoLegStipulations[count];
				for (int i = 0; i < count; i++)
				{
					NoLegStipulations[i] = new();
					((IFixParser)NoLegStipulations[i]).Parse(viewNoLegStipulations.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegStipulations":
					value = NoLegStipulations;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegStipulations = null;
		}
	}
}
