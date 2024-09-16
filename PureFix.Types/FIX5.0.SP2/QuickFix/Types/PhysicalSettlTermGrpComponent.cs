using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PhysicalSettlTermGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40204, Offset = 0, Required = false)]
		public NoPhysicalSettlTerms[]? NoPhysicalSettlTerms {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPhysicalSettlTerms is not null && NoPhysicalSettlTerms.Length != 0)
			{
				writer.WriteWholeNumber(40204, NoPhysicalSettlTerms.Length);
				for (int i = 0; i < NoPhysicalSettlTerms.Length; i++)
				{
					((IFixEncoder)NoPhysicalSettlTerms[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPhysicalSettlTerms") is IMessageView viewNoPhysicalSettlTerms)
			{
				var count = viewNoPhysicalSettlTerms.GroupCount();
				NoPhysicalSettlTerms = new NoPhysicalSettlTerms[count];
				for (int i = 0; i < count; i++)
				{
					NoPhysicalSettlTerms[i] = new();
					((IFixParser)NoPhysicalSettlTerms[i]).Parse(viewNoPhysicalSettlTerms.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPhysicalSettlTerms":
					value = NoPhysicalSettlTerms;
					break;
				default: return false;
			}
			return true;
		}
	}
}
