using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class StreamCommodityAltIDGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41277, Offset = 0, Required = false)]
		public NoStreamCommodityAltIDs[]? NoStreamCommodityAltIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoStreamCommodityAltIDs is not null && NoStreamCommodityAltIDs.Length != 0)
			{
				writer.WriteWholeNumber(41277, NoStreamCommodityAltIDs.Length);
				for (int i = 0; i < NoStreamCommodityAltIDs.Length; i++)
				{
					((IFixEncoder)NoStreamCommodityAltIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoStreamCommodityAltIDs") is IMessageView viewNoStreamCommodityAltIDs)
			{
				var count = viewNoStreamCommodityAltIDs.GroupCount();
				NoStreamCommodityAltIDs = new NoStreamCommodityAltIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoStreamCommodityAltIDs[i] = new();
					((IFixParser)NoStreamCommodityAltIDs[i]).Parse(viewNoStreamCommodityAltIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoStreamCommodityAltIDs":
					value = NoStreamCommodityAltIDs;
					break;
				default: return false;
			}
			return true;
		}
	}
}
