using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MatchingDataPointGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2781, Offset = 0, Required = false)]
		public NoMatchingDataPoints[]? NoMatchingDataPoints {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMatchingDataPoints is not null && NoMatchingDataPoints.Length != 0)
			{
				writer.WriteWholeNumber(2781, NoMatchingDataPoints.Length);
				for (int i = 0; i < NoMatchingDataPoints.Length; i++)
				{
					((IFixEncoder)NoMatchingDataPoints[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMatchingDataPoints") is IMessageView viewNoMatchingDataPoints)
			{
				var count = viewNoMatchingDataPoints.GroupCount();
				NoMatchingDataPoints = new NoMatchingDataPoints[count];
				for (int i = 0; i < count; i++)
				{
					NoMatchingDataPoints[i] = new();
					((IFixParser)NoMatchingDataPoints[i]).Parse(viewNoMatchingDataPoints.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMatchingDataPoints":
					value = NoMatchingDataPoints;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMatchingDataPoints = null;
		}
	}
}
