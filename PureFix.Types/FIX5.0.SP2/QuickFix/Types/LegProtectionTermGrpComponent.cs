using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProtectionTermGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41616, Offset = 0, Required = false)]
		public NoLegProtectionTerms[]? NoLegProtectionTerms {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProtectionTerms is not null && NoLegProtectionTerms.Length != 0)
			{
				writer.WriteWholeNumber(41616, NoLegProtectionTerms.Length);
				for (int i = 0; i < NoLegProtectionTerms.Length; i++)
				{
					((IFixEncoder)NoLegProtectionTerms[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProtectionTerms") is IMessageView viewNoLegProtectionTerms)
			{
				var count = viewNoLegProtectionTerms.GroupCount();
				NoLegProtectionTerms = new NoLegProtectionTerms[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProtectionTerms[i] = new();
					((IFixParser)NoLegProtectionTerms[i]).Parse(viewNoLegProtectionTerms.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProtectionTerms":
					value = NoLegProtectionTerms;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegProtectionTerms = null;
		}
	}
}
