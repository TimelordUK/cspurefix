using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class SideRegulatoryTradeIDGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1971, Offset = 0, Required = false)]
		public TradeCaptureReportNoSideRegulatoryTradeIDs[]? NoSideRegulatoryTradeIDs {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSideRegulatoryTradeIDs is not null && NoSideRegulatoryTradeIDs.Length != 0)
			{
				writer.WriteWholeNumber(1971, NoSideRegulatoryTradeIDs.Length);
				for (int i = 0; i < NoSideRegulatoryTradeIDs.Length; i++)
				{
					((IFixEncoder)NoSideRegulatoryTradeIDs[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSideRegulatoryTradeIDs") is IMessageView viewNoSideRegulatoryTradeIDs)
			{
				var count = viewNoSideRegulatoryTradeIDs.GroupCount();
				NoSideRegulatoryTradeIDs = new TradeCaptureReportNoSideRegulatoryTradeIDs[count];
				for (int i = 0; i < count; i++)
				{
					NoSideRegulatoryTradeIDs[i] = new();
					((IFixParser)NoSideRegulatoryTradeIDs[i]).Parse(viewNoSideRegulatoryTradeIDs.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSideRegulatoryTradeIDs":
					value = NoSideRegulatoryTradeIDs;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoSideRegulatoryTradeIDs = null;
		}
	}
}
