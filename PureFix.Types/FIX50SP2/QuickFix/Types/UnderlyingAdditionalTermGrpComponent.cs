using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingAdditionalTermGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42036, Offset = 0, Required = false)]
		public NoUnderlyingAdditionalTerms[]? NoUnderlyingAdditionalTerms {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingAdditionalTerms is not null && NoUnderlyingAdditionalTerms.Length != 0)
			{
				writer.WriteWholeNumber(42036, NoUnderlyingAdditionalTerms.Length);
				for (int i = 0; i < NoUnderlyingAdditionalTerms.Length; i++)
				{
					((IFixEncoder)NoUnderlyingAdditionalTerms[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingAdditionalTerms") is IMessageView viewNoUnderlyingAdditionalTerms)
			{
				var count = viewNoUnderlyingAdditionalTerms.GroupCount();
				NoUnderlyingAdditionalTerms = new NoUnderlyingAdditionalTerms[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingAdditionalTerms[i] = new();
					((IFixParser)NoUnderlyingAdditionalTerms[i]).Parse(viewNoUnderlyingAdditionalTerms.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingAdditionalTerms":
					value = NoUnderlyingAdditionalTerms;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingAdditionalTerms = null;
		}
	}
}
