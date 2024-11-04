using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class ContAmtGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 518, Offset = 0, Required = false)]
		public NoContAmts[]? NoContAmts {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoContAmts is not null && NoContAmts.Length != 0)
			{
				writer.WriteWholeNumber(518, NoContAmts.Length);
				for (int i = 0; i < NoContAmts.Length; i++)
				{
					((IFixEncoder)NoContAmts[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoContAmts") is IMessageView viewNoContAmts)
			{
				var count = viewNoContAmts.GroupCount();
				NoContAmts = new NoContAmts[count];
				for (int i = 0; i < count; i++)
				{
					NoContAmts[i] = new();
					((IFixParser)NoContAmts[i]).Parse(viewNoContAmts.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoContAmts":
					value = NoContAmts;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoContAmts = null;
		}
	}
}
