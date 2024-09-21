using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegStreamCommodityAltIDGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41674, Offset = 0, Required = false)]
		public NoLegStreamCommodityAltIDs[]? NoLegStreamCommodityAltIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegStreamCommodityAltIDs is not null && NoLegStreamCommodityAltIDs.Length != 0)
			{
				writer.WriteWholeNumber(41674, NoLegStreamCommodityAltIDs.Length);
				for (int i = 0; i < NoLegStreamCommodityAltIDs.Length; i++)
				{
					((IFixEncoder)NoLegStreamCommodityAltIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegStreamCommodityAltIDs") is IMessageView viewNoLegStreamCommodityAltIDs)
			{
				var count = viewNoLegStreamCommodityAltIDs.GroupCount();
				NoLegStreamCommodityAltIDs = new NoLegStreamCommodityAltIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoLegStreamCommodityAltIDs[i] = new();
					((IFixParser)NoLegStreamCommodityAltIDs[i]).Parse(viewNoLegStreamCommodityAltIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegStreamCommodityAltIDs":
					value = NoLegStreamCommodityAltIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegStreamCommodityAltIDs = null;
		}
	}
}
