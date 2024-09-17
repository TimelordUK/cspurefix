using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProtectionTermEventQualifierGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41633, Offset = 0, Required = false)]
		public NoLegProtectionTermEventQualifiers[]? NoLegProtectionTermEventQualifiers {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProtectionTermEventQualifiers is not null && NoLegProtectionTermEventQualifiers.Length != 0)
			{
				writer.WriteWholeNumber(41633, NoLegProtectionTermEventQualifiers.Length);
				for (int i = 0; i < NoLegProtectionTermEventQualifiers.Length; i++)
				{
					((IFixEncoder)NoLegProtectionTermEventQualifiers[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProtectionTermEventQualifiers") is IMessageView viewNoLegProtectionTermEventQualifiers)
			{
				var count = viewNoLegProtectionTermEventQualifiers.GroupCount();
				NoLegProtectionTermEventQualifiers = new NoLegProtectionTermEventQualifiers[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProtectionTermEventQualifiers[i] = new();
					((IFixParser)NoLegProtectionTermEventQualifiers[i]).Parse(viewNoLegProtectionTermEventQualifiers.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProtectionTermEventQualifiers":
					value = NoLegProtectionTermEventQualifiers;
					break;
				default: return false;
			}
			return true;
		}
	}
}
