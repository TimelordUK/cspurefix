using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TargetMarketSegmentGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1789, Offset = 0, Required = false)]
		public OrderMassActionReportNoTargetMarketSegments[]? NoTargetMarketSegments {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTargetMarketSegments is not null && NoTargetMarketSegments.Length != 0)
			{
				writer.WriteWholeNumber(1789, NoTargetMarketSegments.Length);
				for (int i = 0; i < NoTargetMarketSegments.Length; i++)
				{
					((IFixEncoder)NoTargetMarketSegments[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTargetMarketSegments") is IMessageView viewNoTargetMarketSegments)
			{
				var count = viewNoTargetMarketSegments.GroupCount();
				NoTargetMarketSegments = new OrderMassActionReportNoTargetMarketSegments[count];
				for (int i = 0; i < count; i++)
				{
					NoTargetMarketSegments[i] = new();
					((IFixParser)NoTargetMarketSegments[i]).Parse(viewNoTargetMarketSegments.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTargetMarketSegments":
					value = NoTargetMarketSegments;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoTargetMarketSegments = null;
		}
	}
}
