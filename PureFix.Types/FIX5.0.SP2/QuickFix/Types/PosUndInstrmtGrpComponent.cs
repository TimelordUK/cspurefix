using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PosUndInstrmtGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 711, Offset = 0, Required = false)]
		public NoUnderlyings[]? NoUnderlyings {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyings is not null && NoUnderlyings.Length != 0)
			{
				writer.WriteWholeNumber(711, NoUnderlyings.Length);
				for (int i = 0; i < NoUnderlyings.Length; i++)
				{
					((IFixEncoder)NoUnderlyings[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyings") is IMessageView viewNoUnderlyings)
			{
				var count = viewNoUnderlyings.GroupCount();
				NoUnderlyings = new NoUnderlyings[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyings[i] = new();
					((IFixParser)NoUnderlyings[i]).Parse(viewNoUnderlyings.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyings":
					value = NoUnderlyings;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyings = null;
		}
	}
}
