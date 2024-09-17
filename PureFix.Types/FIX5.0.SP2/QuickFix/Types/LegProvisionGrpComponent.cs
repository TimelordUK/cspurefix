using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProvisionGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40448, Offset = 0, Required = false)]
		public NoLegProvisions[]? NoLegProvisions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProvisions is not null && NoLegProvisions.Length != 0)
			{
				writer.WriteWholeNumber(40448, NoLegProvisions.Length);
				for (int i = 0; i < NoLegProvisions.Length; i++)
				{
					((IFixEncoder)NoLegProvisions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProvisions") is IMessageView viewNoLegProvisions)
			{
				var count = viewNoLegProvisions.GroupCount();
				NoLegProvisions = new NoLegProvisions[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProvisions[i] = new();
					((IFixParser)NoLegProvisions[i]).Parse(viewNoLegProvisions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProvisions":
					value = NoLegProvisions;
					break;
				default: return false;
			}
			return true;
		}
	}
}
