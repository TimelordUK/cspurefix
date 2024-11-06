using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AdditionalTermBondRefGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40000, Offset = 0, Required = false)]
		public IOINoAdditionalTermBondRefs[]? NoAdditionalTermBondRefs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAdditionalTermBondRefs is not null && NoAdditionalTermBondRefs.Length != 0)
			{
				writer.WriteWholeNumber(40000, NoAdditionalTermBondRefs.Length);
				for (int i = 0; i < NoAdditionalTermBondRefs.Length; i++)
				{
					((IFixEncoder)NoAdditionalTermBondRefs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAdditionalTermBondRefs") is IMessageView viewNoAdditionalTermBondRefs)
			{
				var count = viewNoAdditionalTermBondRefs.GroupCount();
				NoAdditionalTermBondRefs = new IOINoAdditionalTermBondRefs[count];
				for (int i = 0; i < count; i++)
				{
					NoAdditionalTermBondRefs[i] = new();
					((IFixParser)NoAdditionalTermBondRefs[i]).Parse(viewNoAdditionalTermBondRefs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAdditionalTermBondRefs":
					value = NoAdditionalTermBondRefs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoAdditionalTermBondRefs = null;
		}
	}
}
