using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NewsRefGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1475, Offset = 0, Required = false)]
		public NoNewsRefIDs[]? NoNewsRefIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoNewsRefIDs is not null && NoNewsRefIDs.Length != 0)
			{
				writer.WriteWholeNumber(1475, NoNewsRefIDs.Length);
				for (int i = 0; i < NoNewsRefIDs.Length; i++)
				{
					((IFixEncoder)NoNewsRefIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoNewsRefIDs") is IMessageView viewNoNewsRefIDs)
			{
				var count = viewNoNewsRefIDs.GroupCount();
				NoNewsRefIDs = new NoNewsRefIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoNewsRefIDs[i] = new();
					((IFixParser)NoNewsRefIDs[i]).Parse(viewNoNewsRefIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoNewsRefIDs":
					value = NoNewsRefIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoNewsRefIDs = null;
		}
	}
}
