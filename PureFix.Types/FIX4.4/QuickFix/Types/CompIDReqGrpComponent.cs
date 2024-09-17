using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class CompIDReqGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 936, Offset = 0, Required = false)]
		public NoCompIDs[]? NoCompIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoCompIDs is not null && NoCompIDs.Length != 0)
			{
				writer.WriteWholeNumber(936, NoCompIDs.Length);
				for (int i = 0; i < NoCompIDs.Length; i++)
				{
					((IFixEncoder)NoCompIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoCompIDs") is IMessageView viewNoCompIDs)
			{
				var count = viewNoCompIDs.GroupCount();
				NoCompIDs = new NoCompIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoCompIDs[i] = new();
					((IFixParser)NoCompIDs[i]).Parse(viewNoCompIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoCompIDs":
					value = NoCompIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
