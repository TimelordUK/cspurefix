using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingProvisionPartyIDs : IFixGroup
	{
		[TagDetails(Tag = 42174, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingProvisionPartyID {get; set;}
		
		[TagDetails(Tag = 42175, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingProvisionPartyIDSource {get; set;}
		
		[TagDetails(Tag = 42176, Type = TagType.Int, Offset = 2, Required = false)]
		public int? UnderlyingProvisionPartyRole {get; set;}
		
		[TagDetails(Tag = 40918, Type = TagType.Int, Offset = 3, Required = false)]
		public int? UnderlyingProvisionPartyRoleQualifier {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public UnderlyingProvisionPtysSubGrpComponent? UnderlyingProvisionPtysSubGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingProvisionPartyID is not null) writer.WriteString(42174, UnderlyingProvisionPartyID);
			if (UnderlyingProvisionPartyIDSource is not null) writer.WriteString(42175, UnderlyingProvisionPartyIDSource);
			if (UnderlyingProvisionPartyRole is not null) writer.WriteWholeNumber(42176, UnderlyingProvisionPartyRole.Value);
			if (UnderlyingProvisionPartyRoleQualifier is not null) writer.WriteWholeNumber(40918, UnderlyingProvisionPartyRoleQualifier.Value);
			if (UnderlyingProvisionPtysSubGrp is not null) ((IFixEncoder)UnderlyingProvisionPtysSubGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingProvisionPartyID = view.GetString(42174);
			UnderlyingProvisionPartyIDSource = view.GetString(42175);
			UnderlyingProvisionPartyRole = view.GetInt32(42176);
			UnderlyingProvisionPartyRoleQualifier = view.GetInt32(40918);
			if (view.GetView("UnderlyingProvisionPtysSubGrp") is IMessageView viewUnderlyingProvisionPtysSubGrp)
			{
				UnderlyingProvisionPtysSubGrp = new();
				((IFixParser)UnderlyingProvisionPtysSubGrp).Parse(viewUnderlyingProvisionPtysSubGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingProvisionPartyID":
					value = UnderlyingProvisionPartyID;
					break;
				case "UnderlyingProvisionPartyIDSource":
					value = UnderlyingProvisionPartyIDSource;
					break;
				case "UnderlyingProvisionPartyRole":
					value = UnderlyingProvisionPartyRole;
					break;
				case "UnderlyingProvisionPartyRoleQualifier":
					value = UnderlyingProvisionPartyRoleQualifier;
					break;
				case "UnderlyingProvisionPtysSubGrp":
					value = UnderlyingProvisionPtysSubGrp;
					break;
				default: return false;
			}
			return true;
		}
	}
}
