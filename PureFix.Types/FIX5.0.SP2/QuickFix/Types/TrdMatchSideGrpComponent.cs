using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TrdMatchSideGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1890, Offset = 0, Required = false)]
		public NoTrdMatchSides[]? NoTrdMatchSides {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTrdMatchSides is not null && NoTrdMatchSides.Length != 0)
			{
				writer.WriteWholeNumber(1890, NoTrdMatchSides.Length);
				for (int i = 0; i < NoTrdMatchSides.Length; i++)
				{
					((IFixEncoder)NoTrdMatchSides[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTrdMatchSides") is IMessageView viewNoTrdMatchSides)
			{
				var count = viewNoTrdMatchSides.GroupCount();
				NoTrdMatchSides = new NoTrdMatchSides[count];
				for (int i = 0; i < count; i++)
				{
					NoTrdMatchSides[i] = new();
					((IFixParser)NoTrdMatchSides[i]).Parse(viewNoTrdMatchSides.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTrdMatchSides":
					value = NoTrdMatchSides;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoTrdMatchSides = null;
		}
	}
}
