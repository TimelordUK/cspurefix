using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class FinancingContractualDefinitionGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40040, Offset = 0, Required = false)]
		public NoContractualDefinitions[]? NoContractualDefinitions {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoContractualDefinitions is not null && NoContractualDefinitions.Length != 0)
			{
				writer.WriteWholeNumber(40040, NoContractualDefinitions.Length);
				for (int i = 0; i < NoContractualDefinitions.Length; i++)
				{
					((IFixEncoder)NoContractualDefinitions[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoContractualDefinitions") is IMessageView viewNoContractualDefinitions)
			{
				var count = viewNoContractualDefinitions.GroupCount();
				NoContractualDefinitions = new NoContractualDefinitions[count];
				for (int i = 0; i < count; i++)
				{
					NoContractualDefinitions[i] = new();
					((IFixParser)NoContractualDefinitions[i]).Parse(viewNoContractualDefinitions.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoContractualDefinitions":
					value = NoContractualDefinitions;
					break;
				default: return false;
			}
			return true;
		}
	}
}
