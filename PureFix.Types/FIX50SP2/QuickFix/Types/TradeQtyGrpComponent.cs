using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TradeQtyGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1841, Offset = 0, Required = false)]
		public TradeCaptureReportNoTradeQtys[]? NoTradeQtys {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoTradeQtys is not null && NoTradeQtys.Length != 0)
			{
				writer.WriteWholeNumber(1841, NoTradeQtys.Length);
				for (int i = 0; i < NoTradeQtys.Length; i++)
				{
					((IFixEncoder)NoTradeQtys[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTradeQtys") is IMessageView viewNoTradeQtys)
			{
				var count = viewNoTradeQtys.GroupCount();
				NoTradeQtys = new TradeCaptureReportNoTradeQtys[count];
				for (int i = 0; i < count; i++)
				{
					NoTradeQtys[i] = new();
					((IFixParser)NoTradeQtys[i]).Parse(viewNoTradeQtys.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTradeQtys":
					value = NoTradeQtys;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoTradeQtys = null;
		}
	}
}
