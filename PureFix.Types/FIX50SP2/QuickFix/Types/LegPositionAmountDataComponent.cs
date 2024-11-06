using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class LegPositionAmountDataComponent : IFixComponent
	{
		[Group(NoOfTag = 1586, Offset = 0, Required = false)]
		public TradeCaptureReportNoLegPosAmt[]? NoLegPosAmt {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoLegPosAmt is not null && NoLegPosAmt.Length != 0)
			{
				writer.WriteWholeNumber(1586, NoLegPosAmt.Length);
				for (int i = 0; i < NoLegPosAmt.Length; i++)
				{
					((IFixEncoder)NoLegPosAmt[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoLegPosAmt") is IMessageView viewNoLegPosAmt)
			{
				var count = viewNoLegPosAmt.GroupCount();
				NoLegPosAmt = new TradeCaptureReportNoLegPosAmt[count];
				for (int i = 0; i < count; i++)
				{
					NoLegPosAmt[i] = new();
					((IFixParser)NoLegPosAmt[i]).Parse(viewNoLegPosAmt.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoLegPosAmt":
					value = NoLegPosAmt;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoLegPosAmt = null;
		}
	}
}
