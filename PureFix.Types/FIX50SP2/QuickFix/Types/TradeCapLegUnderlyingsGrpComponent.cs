using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TradeCapLegUnderlyingsGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1342, Offset = 0, Required = false)]
		public TradeCaptureReportNoOfLegUnderlyings[]? NoOfLegUnderlyings {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoOfLegUnderlyings is not null && NoOfLegUnderlyings.Length != 0)
			{
				writer.WriteWholeNumber(1342, NoOfLegUnderlyings.Length);
				for (int i = 0; i < NoOfLegUnderlyings.Length; i++)
				{
					((IFixEncoder)NoOfLegUnderlyings[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoOfLegUnderlyings") is IMessageView viewNoOfLegUnderlyings)
			{
				var count = viewNoOfLegUnderlyings.GroupCount();
				NoOfLegUnderlyings = new TradeCaptureReportNoOfLegUnderlyings[count];
				for (int i = 0; i < count; i++)
				{
					NoOfLegUnderlyings[i] = new();
					((IFixParser)NoOfLegUnderlyings[i]).Parse(viewNoOfLegUnderlyings.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoOfLegUnderlyings":
					value = NoOfLegUnderlyings;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoOfLegUnderlyings = null;
		}
	}
}
