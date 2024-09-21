using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class CpctyConfGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 862, Offset = 0, Required = false)]
		public NoCapacities[]? NoCapacities {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoCapacities is not null && NoCapacities.Length != 0)
			{
				writer.WriteWholeNumber(862, NoCapacities.Length);
				for (int i = 0; i < NoCapacities.Length; i++)
				{
					((IFixEncoder)NoCapacities[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoCapacities") is IMessageView viewNoCapacities)
			{
				var count = viewNoCapacities.GroupCount();
				NoCapacities = new NoCapacities[count];
				for (int i = 0; i < count; i++)
				{
					NoCapacities[i] = new();
					((IFixParser)NoCapacities[i]).Parse(viewNoCapacities.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoCapacities":
					value = NoCapacities;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoCapacities = null;
		}
	}
}
