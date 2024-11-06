using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class InstrmtMatchSideGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1889, Offset = 0, Required = false)]
		public TradeMatchReportNoInstrmtMatchSides[]? NoInstrmtMatchSides {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoInstrmtMatchSides is not null && NoInstrmtMatchSides.Length != 0)
			{
				writer.WriteWholeNumber(1889, NoInstrmtMatchSides.Length);
				for (int i = 0; i < NoInstrmtMatchSides.Length; i++)
				{
					((IFixEncoder)NoInstrmtMatchSides[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoInstrmtMatchSides") is IMessageView viewNoInstrmtMatchSides)
			{
				var count = viewNoInstrmtMatchSides.GroupCount();
				NoInstrmtMatchSides = new TradeMatchReportNoInstrmtMatchSides[count];
				for (int i = 0; i < count; i++)
				{
					NoInstrmtMatchSides[i] = new();
					((IFixParser)NoInstrmtMatchSides[i]).Parse(viewNoInstrmtMatchSides.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoInstrmtMatchSides":
					value = NoInstrmtMatchSides;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoInstrmtMatchSides = null;
		}
	}
}
