using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegProtectionTermObligationGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41635, Offset = 0, Required = false)]
		public IOINoLegProtectionTermObligations[]? NoLegProtectionTermObligations {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegProtectionTermObligations is not null && NoLegProtectionTermObligations.Length != 0)
			{
				writer.WriteWholeNumber(41635, NoLegProtectionTermObligations.Length);
				for (int i = 0; i < NoLegProtectionTermObligations.Length; i++)
				{
					((IFixEncoder)NoLegProtectionTermObligations[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegProtectionTermObligations") is IMessageView viewNoLegProtectionTermObligations)
			{
				var count = viewNoLegProtectionTermObligations.GroupCount();
				NoLegProtectionTermObligations = new IOINoLegProtectionTermObligations[count];
				for (int i = 0; i < count; i++)
				{
					NoLegProtectionTermObligations[i] = new();
					((IFixParser)NoLegProtectionTermObligations[i]).Parse(viewNoLegProtectionTermObligations.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegProtectionTermObligations":
					value = NoLegProtectionTermObligations;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegProtectionTermObligations = null;
		}
	}
}
