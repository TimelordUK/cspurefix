using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class OrderEntryAckGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2428, Offset = 0, Required = false)]
		public NoOrderEntries[]? NoOrderEntries {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoOrderEntries is not null && NoOrderEntries.Length != 0)
			{
				writer.WriteWholeNumber(2428, NoOrderEntries.Length);
				for (int i = 0; i < NoOrderEntries.Length; i++)
				{
					((IFixEncoder)NoOrderEntries[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoOrderEntries") is IMessageView viewNoOrderEntries)
			{
				var count = viewNoOrderEntries.GroupCount();
				NoOrderEntries = new NoOrderEntries[count];
				for (int i = 0; i < count; i++)
				{
					NoOrderEntries[i] = new();
					((IFixParser)NoOrderEntries[i]).Parse(viewNoOrderEntries.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoOrderEntries":
					value = NoOrderEntries;
					break;
				default: return false;
			}
			return true;
		}
	}
}
