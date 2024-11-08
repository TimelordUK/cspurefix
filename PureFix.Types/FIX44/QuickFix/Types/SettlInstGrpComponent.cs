using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class SettlInstGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 778, Offset = 0, Required = false)]
		public SettlementInstructionsNoSettlInst[]? NoSettlInst {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoSettlInst is not null && NoSettlInst.Length != 0)
			{
				writer.WriteWholeNumber(778, NoSettlInst.Length);
				for (int i = 0; i < NoSettlInst.Length; i++)
				{
					((IFixEncoder)NoSettlInst[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoSettlInst") is IMessageView viewNoSettlInst)
			{
				var count = viewNoSettlInst.GroupCount();
				NoSettlInst = new SettlementInstructionsNoSettlInst[count];
				for (int i = 0; i < count; i++)
				{
					NoSettlInst[i] = new();
					((IFixParser)NoSettlInst[i]).Parse(viewNoSettlInst.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoSettlInst":
					value = NoSettlInst;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoSettlInst = null;
		}
	}
}
