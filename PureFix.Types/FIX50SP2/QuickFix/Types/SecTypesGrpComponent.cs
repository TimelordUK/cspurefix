using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SecTypesGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 558, Offset = 0, Required = false)]
		public NoSecurityTypes[]? NoSecurityTypes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSecurityTypes is not null && NoSecurityTypes.Length != 0)
			{
				writer.WriteWholeNumber(558, NoSecurityTypes.Length);
				for (int i = 0; i < NoSecurityTypes.Length; i++)
				{
					((IFixEncoder)NoSecurityTypes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSecurityTypes") is IMessageView viewNoSecurityTypes)
			{
				var count = viewNoSecurityTypes.GroupCount();
				NoSecurityTypes = new NoSecurityTypes[count];
				for (int i = 0; i < count; i++)
				{
					NoSecurityTypes[i] = new();
					((IFixParser)NoSecurityTypes[i]).Parse(viewNoSecurityTypes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSecurityTypes":
					value = NoSecurityTypes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoSecurityTypes = null;
		}
	}
}
