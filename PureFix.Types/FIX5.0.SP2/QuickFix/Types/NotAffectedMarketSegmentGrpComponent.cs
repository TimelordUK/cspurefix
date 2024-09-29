using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NotAffectedMarketSegmentGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1793, Offset = 0, Required = false)]
		public NoNotAffectedMarketSegments[]? NoNotAffectedMarketSegments {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNotAffectedMarketSegments is not null && NoNotAffectedMarketSegments.Length != 0)
			{
				writer.WriteWholeNumber(1793, NoNotAffectedMarketSegments.Length);
				for (int i = 0; i < NoNotAffectedMarketSegments.Length; i++)
				{
					((IFixEncoder)NoNotAffectedMarketSegments[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNotAffectedMarketSegments") is IMessageView viewNoNotAffectedMarketSegments)
			{
				var count = viewNoNotAffectedMarketSegments.GroupCount();
				NoNotAffectedMarketSegments = new NoNotAffectedMarketSegments[count];
				for (int i = 0; i < count; i++)
				{
					NoNotAffectedMarketSegments[i] = new();
					((IFixParser)NoNotAffectedMarketSegments[i]).Parse(viewNoNotAffectedMarketSegments.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNotAffectedMarketSegments":
					value = NoNotAffectedMarketSegments;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoNotAffectedMarketSegments = null;
		}
	}
}
