using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegAdditionalTermGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41335, Offset = 0, Required = false)]
		public NoLegAdditionalTerms[]? NoLegAdditionalTerms {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegAdditionalTerms is not null && NoLegAdditionalTerms.Length != 0)
			{
				writer.WriteWholeNumber(41335, NoLegAdditionalTerms.Length);
				for (int i = 0; i < NoLegAdditionalTerms.Length; i++)
				{
					((IFixEncoder)NoLegAdditionalTerms[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegAdditionalTerms") is IMessageView viewNoLegAdditionalTerms)
			{
				var count = viewNoLegAdditionalTerms.GroupCount();
				NoLegAdditionalTerms = new NoLegAdditionalTerms[count];
				for (int i = 0; i < count; i++)
				{
					NoLegAdditionalTerms[i] = new();
					((IFixParser)NoLegAdditionalTerms[i]).Parse(viewNoLegAdditionalTerms.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegAdditionalTerms":
					value = NoLegAdditionalTerms;
					break;
				default: return false;
			}
			return true;
		}
	}
}
