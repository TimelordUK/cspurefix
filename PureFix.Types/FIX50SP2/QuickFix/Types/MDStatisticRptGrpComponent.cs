using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MDStatisticRptGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 2474, Offset = 0, Required = false)]
		public NoMDStatistics[]? NoMDStatistics {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMDStatistics is not null && NoMDStatistics.Length != 0)
			{
				writer.WriteWholeNumber(2474, NoMDStatistics.Length);
				for (int i = 0; i < NoMDStatistics.Length; i++)
				{
					((IFixEncoder)NoMDStatistics[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMDStatistics") is IMessageView viewNoMDStatistics)
			{
				var count = viewNoMDStatistics.GroupCount();
				NoMDStatistics = new NoMDStatistics[count];
				for (int i = 0; i < count; i++)
				{
					NoMDStatistics[i] = new();
					((IFixParser)NoMDStatistics[i]).Parse(viewNoMDStatistics.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMDStatistics":
					value = NoMDStatistics;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoMDStatistics = null;
		}
	}
}
