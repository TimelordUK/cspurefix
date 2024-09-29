using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProtectionTermGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42068, Offset = 0, Required = false)]
		public NoUnderlyingProtectionTerms[]? NoUnderlyingProtectionTerms {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProtectionTerms is not null && NoUnderlyingProtectionTerms.Length != 0)
			{
				writer.WriteWholeNumber(42068, NoUnderlyingProtectionTerms.Length);
				for (int i = 0; i < NoUnderlyingProtectionTerms.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProtectionTerms[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProtectionTerms") is IMessageView viewNoUnderlyingProtectionTerms)
			{
				var count = viewNoUnderlyingProtectionTerms.GroupCount();
				NoUnderlyingProtectionTerms = new NoUnderlyingProtectionTerms[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProtectionTerms[i] = new();
					((IFixParser)NoUnderlyingProtectionTerms[i]).Parse(viewNoUnderlyingProtectionTerms.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProtectionTerms":
					value = NoUnderlyingProtectionTerms;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingProtectionTerms = null;
		}
	}
}
