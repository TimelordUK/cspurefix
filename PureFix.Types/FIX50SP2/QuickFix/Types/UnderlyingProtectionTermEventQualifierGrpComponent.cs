using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProtectionTermEventQualifierGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42085, Offset = 0, Required = false)]
		public IOINoUnderlyingProtectionTermEventQualifiers[]? NoUnderlyingProtectionTermEventQualifiers {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProtectionTermEventQualifiers is not null && NoUnderlyingProtectionTermEventQualifiers.Length != 0)
			{
				writer.WriteWholeNumber(42085, NoUnderlyingProtectionTermEventQualifiers.Length);
				for (int i = 0; i < NoUnderlyingProtectionTermEventQualifiers.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProtectionTermEventQualifiers[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProtectionTermEventQualifiers") is IMessageView viewNoUnderlyingProtectionTermEventQualifiers)
			{
				var count = viewNoUnderlyingProtectionTermEventQualifiers.GroupCount();
				NoUnderlyingProtectionTermEventQualifiers = new IOINoUnderlyingProtectionTermEventQualifiers[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProtectionTermEventQualifiers[i] = new();
					((IFixParser)NoUnderlyingProtectionTermEventQualifiers[i]).Parse(viewNoUnderlyingProtectionTermEventQualifiers.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProtectionTermEventQualifiers":
					value = NoUnderlyingProtectionTermEventQualifiers;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingProtectionTermEventQualifiers = null;
		}
	}
}
