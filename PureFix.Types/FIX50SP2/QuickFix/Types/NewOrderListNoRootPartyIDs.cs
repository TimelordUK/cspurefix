using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NewOrderListNoRootPartyIDs : IFixGroup
	{
		[TagDetails(Tag = 1117, Type = TagType.String, Offset = 0, Required = false)]
		public string? RootPartyID {get; set;}
		
		[TagDetails(Tag = 1118, Type = TagType.String, Offset = 1, Required = false)]
		public string? RootPartyIDSource {get; set;}
		
		[TagDetails(Tag = 1119, Type = TagType.Int, Offset = 2, Required = false)]
		public int? RootPartyRole {get; set;}
		
		[TagDetails(Tag = 2388, Type = TagType.Int, Offset = 3, Required = false)]
		public int? RootPartyRoleQualifier {get; set;}
		
		[Component(Offset = 4, Required = false)]
		public RootSubPartiesComponent? RootSubParties {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RootPartyID is not null) writer.WriteString(1117, RootPartyID);
			if (RootPartyIDSource is not null) writer.WriteString(1118, RootPartyIDSource);
			if (RootPartyRole is not null) writer.WriteWholeNumber(1119, RootPartyRole.Value);
			if (RootPartyRoleQualifier is not null) writer.WriteWholeNumber(2388, RootPartyRoleQualifier.Value);
			if (RootSubParties is not null) ((IFixEncoder)RootSubParties).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RootPartyID = view.GetString(1117);
			RootPartyIDSource = view.GetString(1118);
			RootPartyRole = view.GetInt32(1119);
			RootPartyRoleQualifier = view.GetInt32(2388);
			if (view.GetView("RootSubParties") is IMessageView viewRootSubParties)
			{
				RootSubParties = new();
				((IFixParser)RootSubParties).Parse(viewRootSubParties);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RootPartyID":
					value = RootPartyID;
					break;
				case "RootPartyIDSource":
					value = RootPartyIDSource;
					break;
				case "RootPartyRole":
					value = RootPartyRole;
					break;
				case "RootPartyRoleQualifier":
					value = RootPartyRoleQualifier;
					break;
				case "RootSubParties":
					value = RootSubParties;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RootPartyID = null;
			RootPartyIDSource = null;
			RootPartyRole = null;
			RootPartyRoleQualifier = null;
			((IFixReset?)RootSubParties)?.Reset();
		}
	}
}
