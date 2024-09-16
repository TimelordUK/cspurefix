using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoRelatedPartyDetailAltID : IFixGroup
	{
		[TagDetails(Tag = 1570, Type = TagType.String, Offset = 0, Required = false)]
		public string? RelatedPartyDetailAltID {get; set;}
		
		[TagDetails(Tag = 1571, Type = TagType.String, Offset = 1, Required = false)]
		public string? RelatedPartyDetailAltIDSource {get; set;}
		
		[Component(Offset = 2, Required = false)]
		public RelatedPartyDetailAltSubGrpComponent? RelatedPartyDetailAltSubGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RelatedPartyDetailAltID is not null) writer.WriteString(1570, RelatedPartyDetailAltID);
			if (RelatedPartyDetailAltIDSource is not null) writer.WriteString(1571, RelatedPartyDetailAltIDSource);
			if (RelatedPartyDetailAltSubGrp is not null) ((IFixEncoder)RelatedPartyDetailAltSubGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RelatedPartyDetailAltID = view.GetString(1570);
			RelatedPartyDetailAltIDSource = view.GetString(1571);
			if (view.GetView("RelatedPartyDetailAltSubGrp") is IMessageView viewRelatedPartyDetailAltSubGrp)
			{
				RelatedPartyDetailAltSubGrp = new();
				((IFixParser)RelatedPartyDetailAltSubGrp).Parse(viewRelatedPartyDetailAltSubGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RelatedPartyDetailAltID":
					value = RelatedPartyDetailAltID;
					break;
				case "RelatedPartyDetailAltIDSource":
					value = RelatedPartyDetailAltIDSource;
					break;
				case "RelatedPartyDetailAltSubGrp":
					value = RelatedPartyDetailAltSubGrp;
					break;
				default: return false;
			}
			return true;
		}
	}
}
