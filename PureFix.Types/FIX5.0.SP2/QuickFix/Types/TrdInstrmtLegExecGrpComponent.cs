using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TrdInstrmtLegExecGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1892, Offset = 0, Required = false)]
		public NoLegExecs[]? NoLegExecs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegExecs is not null && NoLegExecs.Length != 0)
			{
				writer.WriteWholeNumber(1892, NoLegExecs.Length);
				for (int i = 0; i < NoLegExecs.Length; i++)
				{
					((IFixEncoder)NoLegExecs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegExecs") is IMessageView viewNoLegExecs)
			{
				var count = viewNoLegExecs.GroupCount();
				NoLegExecs = new NoLegExecs[count];
				for (int i = 0; i < count; i++)
				{
					NoLegExecs[i] = new();
					((IFixParser)NoLegExecs[i]).Parse(viewNoLegExecs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegExecs":
					value = NoLegExecs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
