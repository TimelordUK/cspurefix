using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class StipulationsComponent : IFixComponent
	{
		[Group(NoOfTag = 232, Offset = 0, Required = false)]
		public NoStipulations[]? NoStipulations {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStipulations is not null && NoStipulations.Length != 0)
			{
				writer.WriteWholeNumber(232, NoStipulations.Length);
				for (int i = 0; i < NoStipulations.Length; i++)
				{
					((IFixEncoder)NoStipulations[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStipulations") is IMessageView viewNoStipulations)
			{
				var count = viewNoStipulations.GroupCount();
				NoStipulations = new NoStipulations[count];
				for (int i = 0; i < count; i++)
				{
					NoStipulations[i] = new();
					((IFixParser)NoStipulations[i]).Parse(viewNoStipulations.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStipulations":
					value = NoStipulations;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoStipulations = null;
		}
	}
}
