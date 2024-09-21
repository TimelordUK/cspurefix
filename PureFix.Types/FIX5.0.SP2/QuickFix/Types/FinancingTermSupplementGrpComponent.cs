using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class FinancingTermSupplementGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40046, Offset = 0, Required = false)]
		public NoFinancingTermSupplements[]? NoFinancingTermSupplements {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoFinancingTermSupplements is not null && NoFinancingTermSupplements.Length != 0)
			{
				writer.WriteWholeNumber(40046, NoFinancingTermSupplements.Length);
				for (int i = 0; i < NoFinancingTermSupplements.Length; i++)
				{
					((IFixEncoder)NoFinancingTermSupplements[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoFinancingTermSupplements") is IMessageView viewNoFinancingTermSupplements)
			{
				var count = viewNoFinancingTermSupplements.GroupCount();
				NoFinancingTermSupplements = new NoFinancingTermSupplements[count];
				for (int i = 0; i < count; i++)
				{
					NoFinancingTermSupplements[i] = new();
					((IFixParser)NoFinancingTermSupplements[i]).Parse(viewNoFinancingTermSupplements.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoFinancingTermSupplements":
					value = NoFinancingTermSupplements;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoFinancingTermSupplements = null;
		}
	}
}
