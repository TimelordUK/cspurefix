using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public sealed partial class QuoteStatusReportTargetParties : IFixGroup
	{
		[TagDetails(Tag = 1462, Type = TagType.String, Offset = 0, Required = false)]
		public string? TargetPartyID {get; set;}
		
		[TagDetails(Tag = 1463, Type = TagType.String, Offset = 1, Required = false)]
		public string? TargetPartyIDSource {get; set;}
		
		[TagDetails(Tag = 1464, Type = TagType.Int, Offset = 2, Required = false)]
		public int? TargetPartyRole {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TargetPartyID is not null) writer.WriteString(1462, TargetPartyID);
			if (TargetPartyIDSource is not null) writer.WriteString(1463, TargetPartyIDSource);
			if (TargetPartyRole is not null) writer.WriteWholeNumber(1464, TargetPartyRole.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			TargetPartyID = view.GetString(1462);
			TargetPartyIDSource = view.GetString(1463);
			TargetPartyRole = view.GetInt32(1464);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "TargetPartyID":
					value = TargetPartyID;
					break;
				case "TargetPartyIDSource":
					value = TargetPartyIDSource;
					break;
				case "TargetPartyRole":
					value = TargetPartyRole;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			TargetPartyID = null;
			TargetPartyIDSource = null;
			TargetPartyRole = null;
		}
	}
}
