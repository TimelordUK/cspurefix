using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class LinesOfTextGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 33, Offset = 0, Required = true)]
		public NewsNoLinesOfText[]? NoLinesOfText {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoLinesOfText is not null && FixValidator.IsValid(NoLinesOfText, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLinesOfText is not null && NoLinesOfText.Length != 0)
			{
				writer.WriteWholeNumber(33, NoLinesOfText.Length);
				for (int i = 0; i < NoLinesOfText.Length; i++)
				{
					((IFixEncoder)NoLinesOfText[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLinesOfText") is IMessageView viewNoLinesOfText)
			{
				var count = viewNoLinesOfText.GroupCount();
				NoLinesOfText = new NewsNoLinesOfText[count];
				for (int i = 0; i < count; i++)
				{
					NoLinesOfText[i] = new();
					((IFixParser)NoLinesOfText[i]).Parse(viewNoLinesOfText.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLinesOfText":
					value = NoLinesOfText;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLinesOfText = null;
		}
	}
}
