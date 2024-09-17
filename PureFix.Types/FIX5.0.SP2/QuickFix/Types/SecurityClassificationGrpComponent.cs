using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SecurityClassificationGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1582, Offset = 0, Required = false)]
		public NoSecurityClassifications[]? NoSecurityClassifications {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSecurityClassifications is not null && NoSecurityClassifications.Length != 0)
			{
				writer.WriteWholeNumber(1582, NoSecurityClassifications.Length);
				for (int i = 0; i < NoSecurityClassifications.Length; i++)
				{
					((IFixEncoder)NoSecurityClassifications[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSecurityClassifications") is IMessageView viewNoSecurityClassifications)
			{
				var count = viewNoSecurityClassifications.GroupCount();
				NoSecurityClassifications = new NoSecurityClassifications[count];
				for (int i = 0; i < count; i++)
				{
					NoSecurityClassifications[i] = new();
					((IFixParser)NoSecurityClassifications[i]).Parse(viewNoSecurityClassifications.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSecurityClassifications":
					value = NoSecurityClassifications;
					break;
				default: return false;
			}
			return true;
		}
	}
}
