using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class ProtectionTermObligationGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40201, Offset = 0, Required = false)]
		public IOINoProtectionTermObligations[]? NoProtectionTermObligations {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoProtectionTermObligations is not null && NoProtectionTermObligations.Length != 0)
			{
				writer.WriteWholeNumber(40201, NoProtectionTermObligations.Length);
				for (int i = 0; i < NoProtectionTermObligations.Length; i++)
				{
					((IFixEncoder)NoProtectionTermObligations[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoProtectionTermObligations") is IMessageView viewNoProtectionTermObligations)
			{
				var count = viewNoProtectionTermObligations.GroupCount();
				NoProtectionTermObligations = new IOINoProtectionTermObligations[count];
				for (int i = 0; i < count; i++)
				{
					NoProtectionTermObligations[i] = new();
					((IFixParser)NoProtectionTermObligations[i]).Parse(viewNoProtectionTermObligations.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoProtectionTermObligations":
					value = NoProtectionTermObligations;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoProtectionTermObligations = null;
		}
	}
}
