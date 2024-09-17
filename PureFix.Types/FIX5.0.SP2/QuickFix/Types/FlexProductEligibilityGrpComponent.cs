using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class FlexProductEligibilityGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2560, Offset = 0, Required = false)]
		public NoFlexProductEligibilities[]? NoFlexProductEligibilities {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoFlexProductEligibilities is not null && NoFlexProductEligibilities.Length != 0)
			{
				writer.WriteWholeNumber(2560, NoFlexProductEligibilities.Length);
				for (int i = 0; i < NoFlexProductEligibilities.Length; i++)
				{
					((IFixEncoder)NoFlexProductEligibilities[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoFlexProductEligibilities") is IMessageView viewNoFlexProductEligibilities)
			{
				var count = viewNoFlexProductEligibilities.GroupCount();
				NoFlexProductEligibilities = new NoFlexProductEligibilities[count];
				for (int i = 0; i < count; i++)
				{
					NoFlexProductEligibilities[i] = new();
					((IFixParser)NoFlexProductEligibilities[i]).Parse(viewNoFlexProductEligibilities.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoFlexProductEligibilities":
					value = NoFlexProductEligibilities;
					break;
				default: return false;
			}
			return true;
		}
	}
}
