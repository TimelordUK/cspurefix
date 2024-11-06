using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegFinancingContractualMatrixGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 42203, Offset = 0, Required = false)]
		public AdvertisementNoLegContractualMatrices[]? NoLegContractualMatrices {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegContractualMatrices is not null && NoLegContractualMatrices.Length != 0)
			{
				writer.WriteWholeNumber(42203, NoLegContractualMatrices.Length);
				for (int i = 0; i < NoLegContractualMatrices.Length; i++)
				{
					((IFixEncoder)NoLegContractualMatrices[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegContractualMatrices") is IMessageView viewNoLegContractualMatrices)
			{
				var count = viewNoLegContractualMatrices.GroupCount();
				NoLegContractualMatrices = new AdvertisementNoLegContractualMatrices[count];
				for (int i = 0; i < count; i++)
				{
					NoLegContractualMatrices[i] = new();
					((IFixParser)NoLegContractualMatrices[i]).Parse(viewNoLegContractualMatrices.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegContractualMatrices":
					value = NoLegContractualMatrices;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegContractualMatrices = null;
		}
	}
}
