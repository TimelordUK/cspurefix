using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class UnderlyingStreamCommodityAltIDGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 41990, Offset = 0, Required = false)]
		public IOINoUnderlyingStreamCommodityAltIDs[]? NoUnderlyingStreamCommodityAltIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoUnderlyingStreamCommodityAltIDs is not null && NoUnderlyingStreamCommodityAltIDs.Length != 0)
			{
				writer.WriteWholeNumber(41990, NoUnderlyingStreamCommodityAltIDs.Length);
				for (int i = 0; i < NoUnderlyingStreamCommodityAltIDs.Length; i++)
				{
					((IFixEncoder)NoUnderlyingStreamCommodityAltIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoUnderlyingStreamCommodityAltIDs") is IMessageView viewNoUnderlyingStreamCommodityAltIDs)
			{
				var count = viewNoUnderlyingStreamCommodityAltIDs.GroupCount();
				NoUnderlyingStreamCommodityAltIDs = new IOINoUnderlyingStreamCommodityAltIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoUnderlyingStreamCommodityAltIDs[i] = new();
					((IFixParser)NoUnderlyingStreamCommodityAltIDs[i]).Parse(viewNoUnderlyingStreamCommodityAltIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoUnderlyingStreamCommodityAltIDs":
					value = NoUnderlyingStreamCommodityAltIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoUnderlyingStreamCommodityAltIDs = null;
		}
	}
}
