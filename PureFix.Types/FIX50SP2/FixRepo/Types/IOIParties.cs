using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public sealed partial class IOIParties : IFixGroup
	{
		[TagDetails(Tag = 448, Type = TagType.String, Offset = 0, Required = false)]
		public string? PartyID {get; set;}
		
		[TagDetails(Tag = 447, Type = TagType.String, Offset = 1, Required = false)]
		public string? PartyIDSource {get; set;}
		
		[TagDetails(Tag = 452, Type = TagType.Int, Offset = 2, Required = false)]
		public int? PartyRole {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PartyID is not null) writer.WriteString(448, PartyID);
			if (PartyIDSource is not null) writer.WriteString(447, PartyIDSource);
			if (PartyRole is not null) writer.WriteWholeNumber(452, PartyRole.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PartyID = view.GetString(448);
			PartyIDSource = view.GetString(447);
			PartyRole = view.GetInt32(452);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PartyID":
					value = PartyID;
					break;
				case "PartyIDSource":
					value = PartyIDSource;
					break;
				case "PartyRole":
					value = PartyRole;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			PartyID = null;
			PartyIDSource = null;
			PartyRole = null;
		}
	}
}
