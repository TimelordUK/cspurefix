using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class FinancingContractualMatrixGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 40042, Offset = 0, Required = false)]
		public NoContractualMatrices[]? NoContractualMatrices {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoContractualMatrices is not null && NoContractualMatrices.Length != 0)
			{
				writer.WriteWholeNumber(40042, NoContractualMatrices.Length);
				for (int i = 0; i < NoContractualMatrices.Length; i++)
				{
					((IFixEncoder)NoContractualMatrices[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoContractualMatrices") is IMessageView viewNoContractualMatrices)
			{
				var count = viewNoContractualMatrices.GroupCount();
				NoContractualMatrices = new NoContractualMatrices[count];
				for (int i = 0; i < count; i++)
				{
					NoContractualMatrices[i] = new();
					((IFixParser)NoContractualMatrices[i]).Parse(viewNoContractualMatrices.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoContractualMatrices":
					value = NoContractualMatrices;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoContractualMatrices = null;
		}
	}
}
