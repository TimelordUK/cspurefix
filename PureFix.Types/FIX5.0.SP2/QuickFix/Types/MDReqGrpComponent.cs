using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class MDReqGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 267, Offset = 0, Required = false)]
		public NoMDEntryTypes[]? NoMDEntryTypes {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoMDEntryTypes is not null && NoMDEntryTypes.Length != 0)
			{
				writer.WriteWholeNumber(267, NoMDEntryTypes.Length);
				for (int i = 0; i < NoMDEntryTypes.Length; i++)
				{
					((IFixEncoder)NoMDEntryTypes[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoMDEntryTypes") is IMessageView viewNoMDEntryTypes)
			{
				var count = viewNoMDEntryTypes.GroupCount();
				NoMDEntryTypes = new NoMDEntryTypes[count];
				for (int i = 0; i < count; i++)
				{
					NoMDEntryTypes[i] = new();
					((IFixParser)NoMDEntryTypes[i]).Parse(viewNoMDEntryTypes.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoMDEntryTypes":
					value = NoMDEntryTypes;
					break;
				default: return false;
			}
			return true;
		}
	}
}
