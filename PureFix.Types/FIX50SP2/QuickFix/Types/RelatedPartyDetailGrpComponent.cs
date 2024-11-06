using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class RelatedPartyDetailGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1562, Offset = 0, Required = false)]
		public TradeCaptureReportNoRelatedPartyDetailID[]? NoRelatedPartyDetailID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoRelatedPartyDetailID is not null && NoRelatedPartyDetailID.Length != 0)
			{
				writer.WriteWholeNumber(1562, NoRelatedPartyDetailID.Length);
				for (int i = 0; i < NoRelatedPartyDetailID.Length; i++)
				{
					((IFixEncoder)NoRelatedPartyDetailID[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoRelatedPartyDetailID") is IMessageView viewNoRelatedPartyDetailID)
			{
				var count = viewNoRelatedPartyDetailID.GroupCount();
				NoRelatedPartyDetailID = new TradeCaptureReportNoRelatedPartyDetailID[count];
				for (int i = 0; i < count; i++)
				{
					NoRelatedPartyDetailID[i] = new();
					((IFixParser)NoRelatedPartyDetailID[i]).Parse(viewNoRelatedPartyDetailID.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoRelatedPartyDetailID":
					value = NoRelatedPartyDetailID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoRelatedPartyDetailID = null;
		}
	}
}
