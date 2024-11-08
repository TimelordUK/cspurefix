using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class PositionAmountDataComponent : IFixComponent
	{
		[Group(NoOfTag = 753, Offset = 0, Required = false)]
		public TradeCaptureReportNoPosAmt[]? NoPosAmt {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPosAmt is not null && NoPosAmt.Length != 0)
			{
				writer.WriteWholeNumber(753, NoPosAmt.Length);
				for (int i = 0; i < NoPosAmt.Length; i++)
				{
					((IFixEncoder)NoPosAmt[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPosAmt") is IMessageView viewNoPosAmt)
			{
				var count = viewNoPosAmt.GroupCount();
				NoPosAmt = new TradeCaptureReportNoPosAmt[count];
				for (int i = 0; i < count; i++)
				{
					NoPosAmt[i] = new();
					((IFixParser)NoPosAmt[i]).Parse(viewNoPosAmt.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPosAmt":
					value = NoPosAmt;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPosAmt = null;
		}
	}
}
