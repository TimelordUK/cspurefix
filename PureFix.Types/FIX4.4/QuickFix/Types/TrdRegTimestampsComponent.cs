using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class TrdRegTimestampsComponent : IFixComponent
	{
		[Group(NoOfTag = 768, Offset = 0, Required = false)]
		public NoTrdRegTimestamps[]? NoTrdRegTimestamps {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTrdRegTimestamps is not null && NoTrdRegTimestamps.Length != 0)
			{
				writer.WriteWholeNumber(768, NoTrdRegTimestamps.Length);
				for (int i = 0; i < NoTrdRegTimestamps.Length; i++)
				{
					((IFixEncoder)NoTrdRegTimestamps[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTrdRegTimestamps") is IMessageView viewNoTrdRegTimestamps)
			{
				var count = viewNoTrdRegTimestamps.GroupCount();
				NoTrdRegTimestamps = new NoTrdRegTimestamps[count];
				for (int i = 0; i < count; i++)
				{
					NoTrdRegTimestamps[i] = new();
					((IFixParser)NoTrdRegTimestamps[i]).Parse(viewNoTrdRegTimestamps.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTrdRegTimestamps":
					value = NoTrdRegTimestamps;
					break;
				default: return false;
			}
			return true;
		}
	}
}
