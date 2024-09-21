using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProtectionTermGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40181, Offset = 0, Required = false)]
		public NoProtectionTerms[]? NoProtectionTerms {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProtectionTerms is not null && NoProtectionTerms.Length != 0)
			{
				writer.WriteWholeNumber(40181, NoProtectionTerms.Length);
				for (int i = 0; i < NoProtectionTerms.Length; i++)
				{
					((IFixEncoder)NoProtectionTerms[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProtectionTerms") is IMessageView viewNoProtectionTerms)
			{
				var count = viewNoProtectionTerms.GroupCount();
				NoProtectionTerms = new NoProtectionTerms[count];
				for (int i = 0; i < count; i++)
				{
					NoProtectionTerms[i] = new();
					((IFixParser)NoProtectionTerms[i]).Parse(viewNoProtectionTerms.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProtectionTerms":
					value = NoProtectionTerms;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoProtectionTerms = null;
		}
	}
}
