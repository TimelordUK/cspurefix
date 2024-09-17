using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPhysicalSettlTermGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41599, Offset = 0, Required = false)]
		public NoLegPhysicalSettlTerms[]? NoLegPhysicalSettlTerms {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPhysicalSettlTerms is not null && NoLegPhysicalSettlTerms.Length != 0)
			{
				writer.WriteWholeNumber(41599, NoLegPhysicalSettlTerms.Length);
				for (int i = 0; i < NoLegPhysicalSettlTerms.Length; i++)
				{
					((IFixEncoder)NoLegPhysicalSettlTerms[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPhysicalSettlTerms") is IMessageView viewNoLegPhysicalSettlTerms)
			{
				var count = viewNoLegPhysicalSettlTerms.GroupCount();
				NoLegPhysicalSettlTerms = new NoLegPhysicalSettlTerms[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPhysicalSettlTerms[i] = new();
					((IFixParser)NoLegPhysicalSettlTerms[i]).Parse(viewNoLegPhysicalSettlTerms.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPhysicalSettlTerms":
					value = NoLegPhysicalSettlTerms;
					break;
				default: return false;
			}
			return true;
		}
	}
}
