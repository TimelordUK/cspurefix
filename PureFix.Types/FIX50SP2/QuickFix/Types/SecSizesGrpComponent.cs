using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SecSizesGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1177, Offset = 0, Required = false)]
		public MarketDataSnapshotFullRefreshNoOfSecSizes[]? NoOfSecSizes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoOfSecSizes is not null && NoOfSecSizes.Length != 0)
			{
				writer.WriteWholeNumber(1177, NoOfSecSizes.Length);
				for (int i = 0; i < NoOfSecSizes.Length; i++)
				{
					((IFixEncoder)NoOfSecSizes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoOfSecSizes") is IMessageView viewNoOfSecSizes)
			{
				var count = viewNoOfSecSizes.GroupCount();
				NoOfSecSizes = new MarketDataSnapshotFullRefreshNoOfSecSizes[count];
				for (int i = 0; i < count; i++)
				{
					NoOfSecSizes[i] = new();
					((IFixParser)NoOfSecSizes[i]).Parse(viewNoOfSecSizes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoOfSecSizes":
					value = NoOfSecSizes;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoOfSecSizes = null;
		}
	}
}
