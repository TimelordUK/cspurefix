using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class AffectedMarketSegmentGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1791, Offset = 0, Required = false)]
		public NoAffectedMarketSegments[]? NoAffectedMarketSegments {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoAffectedMarketSegments is not null && NoAffectedMarketSegments.Length != 0)
			{
				writer.WriteWholeNumber(1791, NoAffectedMarketSegments.Length);
				for (int i = 0; i < NoAffectedMarketSegments.Length; i++)
				{
					((IFixEncoder)NoAffectedMarketSegments[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoAffectedMarketSegments") is IMessageView viewNoAffectedMarketSegments)
			{
				var count = viewNoAffectedMarketSegments.GroupCount();
				NoAffectedMarketSegments = new NoAffectedMarketSegments[count];
				for (int i = 0; i < count; i++)
				{
					NoAffectedMarketSegments[i] = new();
					((IFixParser)NoAffectedMarketSegments[i]).Parse(viewNoAffectedMarketSegments.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoAffectedMarketSegments":
					value = NoAffectedMarketSegments;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoAffectedMarketSegments = null;
		}
	}
}
