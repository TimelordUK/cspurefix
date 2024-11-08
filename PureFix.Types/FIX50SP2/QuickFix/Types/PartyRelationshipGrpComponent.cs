using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class PartyRelationshipGrpComponent : IFixComponent
	{
		[Group(NoOfTag = 1514, Offset = 0, Required = false)]
		public TradeCaptureReportNoPartyRelationships[]? NoPartyRelationships {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (NoPartyRelationships is not null && NoPartyRelationships.Length != 0)
			{
				writer.WriteWholeNumber(1514, NoPartyRelationships.Length);
				for (int i = 0; i < NoPartyRelationships.Length; i++)
				{
					((IFixEncoder)NoPartyRelationships[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoPartyRelationships") is IMessageView viewNoPartyRelationships)
			{
				var count = viewNoPartyRelationships.GroupCount();
				NoPartyRelationships = new TradeCaptureReportNoPartyRelationships[count];
				for (int i = 0; i < count; i++)
				{
					NoPartyRelationships[i] = new();
					((IFixParser)NoPartyRelationships[i]).Parse(viewNoPartyRelationships.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoPartyRelationships":
					value = NoPartyRelationships;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			NoPartyRelationships = null;
		}
	}
}
