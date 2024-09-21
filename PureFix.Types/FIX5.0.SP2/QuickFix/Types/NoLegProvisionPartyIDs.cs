using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoLegProvisionPartyIDs : IFixGroup
	{
		[TagDetails(Tag = 40534, Type = TagType.String, Offset = 0, Required = false)]
		public string? LegProvisionPartyID {get; set;}
		
		[TagDetails(Tag = 40535, Type = TagType.String, Offset = 1, Required = false)]
		public string? LegProvisionPartyIDSource {get; set;}
		
		[TagDetails(Tag = 40536, Type = TagType.Int, Offset = 2, Required = false)]
		public int? LegProvisionPartyRole {get; set;}
		
		[TagDetails(Tag = 2380, Type = TagType.Int, Offset = 3, Required = false)]
		public int? LegProvisionPartyRoleQualifier {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public LegProvisionPtysSubGrpComponent? LegProvisionPtysSubGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (LegProvisionPartyID is not null) writer.WriteString(40534, LegProvisionPartyID);
			if (LegProvisionPartyIDSource is not null) writer.WriteString(40535, LegProvisionPartyIDSource);
			if (LegProvisionPartyRole is not null) writer.WriteWholeNumber(40536, LegProvisionPartyRole.Value);
			if (LegProvisionPartyRoleQualifier is not null) writer.WriteWholeNumber(2380, LegProvisionPartyRoleQualifier.Value);
			if (LegProvisionPtysSubGrp is not null) ((IFixEncoder)LegProvisionPtysSubGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			LegProvisionPartyID = view.GetString(40534);
			LegProvisionPartyIDSource = view.GetString(40535);
			LegProvisionPartyRole = view.GetInt32(40536);
			LegProvisionPartyRoleQualifier = view.GetInt32(2380);
			if (view.GetView("LegProvisionPtysSubGrp") is IMessageView viewLegProvisionPtysSubGrp)
			{
				LegProvisionPtysSubGrp = new();
				((IFixParser)LegProvisionPtysSubGrp).Parse(viewLegProvisionPtysSubGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "LegProvisionPartyID":
					value = LegProvisionPartyID;
					break;
				case "LegProvisionPartyIDSource":
					value = LegProvisionPartyIDSource;
					break;
				case "LegProvisionPartyRole":
					value = LegProvisionPartyRole;
					break;
				case "LegProvisionPartyRoleQualifier":
					value = LegProvisionPartyRoleQualifier;
					break;
				case "LegProvisionPtysSubGrp":
					value = LegProvisionPtysSubGrp;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			LegProvisionPartyID = null;
			LegProvisionPartyIDSource = null;
			LegProvisionPartyRole = null;
			LegProvisionPartyRoleQualifier = null;
			((IFixReset?)LegProvisionPtysSubGrp)?.Reset();
		}
	}
}
