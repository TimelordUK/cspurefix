using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class MDIncGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 268, Offset = 0, Required = true)]
		public NoMDEntries[]? NoMDEntries {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return
				NoMDEntries is not null && FixValidator.IsValid(NoMDEntries, in config);
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMDEntries is not null && NoMDEntries.Length != 0)
			{
				writer.WriteWholeNumber(268, NoMDEntries.Length);
				for (int i = 0; i < NoMDEntries.Length; i++)
				{
					((IFixEncoder)NoMDEntries[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMDEntries") is IMessageView viewNoMDEntries)
			{
				var count = viewNoMDEntries.GroupCount();
				NoMDEntries = new NoMDEntries[count];
				for (int i = 0; i < count; i++)
				{
					NoMDEntries[i] = new();
					((IFixParser)NoMDEntries[i]).Parse(viewNoMDEntries.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMDEntries":
					value = NoMDEntries;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMDEntries = null;
		}
	}
}
