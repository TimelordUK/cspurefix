using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AdditionalTermGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40019, Offset = 0, Required = false)]
		public NoAdditionalTerms[]? NoAdditionalTerms {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAdditionalTerms is not null && NoAdditionalTerms.Length != 0)
			{
				writer.WriteWholeNumber(40019, NoAdditionalTerms.Length);
				for (int i = 0; i < NoAdditionalTerms.Length; i++)
				{
					((IFixEncoder)NoAdditionalTerms[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAdditionalTerms") is IMessageView viewNoAdditionalTerms)
			{
				var count = viewNoAdditionalTerms.GroupCount();
				NoAdditionalTerms = new NoAdditionalTerms[count];
				for (int i = 0; i < count; i++)
				{
					NoAdditionalTerms[i] = new();
					((IFixParser)NoAdditionalTerms[i]).Parse(viewNoAdditionalTerms.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAdditionalTerms":
					value = NoAdditionalTerms;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoAdditionalTerms = null;
		}
	}
}
