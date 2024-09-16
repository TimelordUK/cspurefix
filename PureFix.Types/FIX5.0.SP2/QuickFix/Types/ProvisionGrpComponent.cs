using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProvisionGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40090, Offset = 0, Required = false)]
		public NoProvisions[]? NoProvisions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProvisions is not null && NoProvisions.Length != 0)
			{
				writer.WriteWholeNumber(40090, NoProvisions.Length);
				for (int i = 0; i < NoProvisions.Length; i++)
				{
					((IFixEncoder)NoProvisions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProvisions") is IMessageView viewNoProvisions)
			{
				var count = viewNoProvisions.GroupCount();
				NoProvisions = new NoProvisions[count];
				for (int i = 0; i < count; i++)
				{
					NoProvisions[i] = new();
					((IFixParser)NoProvisions[i]).Parse(viewNoProvisions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProvisions":
					value = NoProvisions;
					break;
				default: return false;
			}
			return true;
		}
	}
}
