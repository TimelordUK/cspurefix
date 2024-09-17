using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingProtectionTermObligationGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42087, Offset = 0, Required = false)]
		public NoUnderlyingProtectionTermObligations[]? NoUnderlyingProtectionTermObligations {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingProtectionTermObligations is not null && NoUnderlyingProtectionTermObligations.Length != 0)
			{
				writer.WriteWholeNumber(42087, NoUnderlyingProtectionTermObligations.Length);
				for (int i = 0; i < NoUnderlyingProtectionTermObligations.Length; i++)
				{
					((IFixEncoder)NoUnderlyingProtectionTermObligations[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingProtectionTermObligations") is IMessageView viewNoUnderlyingProtectionTermObligations)
			{
				var count = viewNoUnderlyingProtectionTermObligations.GroupCount();
				NoUnderlyingProtectionTermObligations = new NoUnderlyingProtectionTermObligations[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingProtectionTermObligations[i] = new();
					((IFixParser)NoUnderlyingProtectionTermObligations[i]).Parse(viewNoUnderlyingProtectionTermObligations.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingProtectionTermObligations":
					value = NoUnderlyingProtectionTermObligations;
					break;
				default: return false;
			}
			return true;
		}
	}
}
