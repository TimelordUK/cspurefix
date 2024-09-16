using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPreAllocGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 670, Offset = 0, Required = false)]
		public NoLegAllocs[]? NoLegAllocs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegAllocs is not null && NoLegAllocs.Length != 0)
			{
				writer.WriteWholeNumber(670, NoLegAllocs.Length);
				for (int i = 0; i < NoLegAllocs.Length; i++)
				{
					((IFixEncoder)NoLegAllocs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegAllocs") is IMessageView viewNoLegAllocs)
			{
				var count = viewNoLegAllocs.GroupCount();
				NoLegAllocs = new NoLegAllocs[count];
				for (int i = 0; i < count; i++)
				{
					NoLegAllocs[i] = new();
					((IFixParser)NoLegAllocs[i]).Parse(viewNoLegAllocs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegAllocs":
					value = NoLegAllocs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
