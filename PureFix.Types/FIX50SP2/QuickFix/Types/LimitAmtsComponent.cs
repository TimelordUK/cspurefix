using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LimitAmtsComponent : IFixComponent
	{
		[Group(NoOfTag = 1630, Offset = 0, Required = false)]
		public NoLimitAmts[]? NoLimitAmts {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLimitAmts is not null && NoLimitAmts.Length != 0)
			{
				writer.WriteWholeNumber(1630, NoLimitAmts.Length);
				for (int i = 0; i < NoLimitAmts.Length; i++)
				{
					((IFixEncoder)NoLimitAmts[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLimitAmts") is IMessageView viewNoLimitAmts)
			{
				var count = viewNoLimitAmts.GroupCount();
				NoLimitAmts = new NoLimitAmts[count];
				for (int i = 0; i < count; i++)
				{
					NoLimitAmts[i] = new();
					((IFixParser)NoLimitAmts[i]).Parse(viewNoLimitAmts.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLimitAmts":
					value = NoLimitAmts;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLimitAmts = null;
		}
	}
}
