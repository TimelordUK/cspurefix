using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SideCrossOrdCxlGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 552, Offset = 0, Required = true)]
		public NoSides[]? NoSides {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoSides is not null && FixValidator.IsValid(NoSides, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSides is not null && NoSides.Length != 0)
			{
				writer.WriteWholeNumber(552, NoSides.Length);
				for (int i = 0; i < NoSides.Length; i++)
				{
					((IFixEncoder)NoSides[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSides") is IMessageView viewNoSides)
			{
				var count = viewNoSides.GroupCount();
				NoSides = new NoSides[count];
				for (int i = 0; i < count; i++)
				{
					NoSides[i] = new();
					((IFixParser)NoSides[i]).Parse(viewNoSides.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSides":
					value = NoSides;
					break;
				default: return false;
			}
			return true;
		}
	}
}
