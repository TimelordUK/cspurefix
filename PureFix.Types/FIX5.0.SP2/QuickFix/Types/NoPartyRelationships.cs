using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoPartyRelationships : IFixGroup
	{
		[TagDetails(Tag = 1515, Type = TagType.Int, Offset = 0, Required = false)]
		public int? PartyRelationship {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (PartyRelationship is not null) writer.WriteWholeNumber(1515, PartyRelationship.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			PartyRelationship = view.GetInt32(1515);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "PartyRelationship":
					value = PartyRelationship;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			PartyRelationship = null;
		}
	}
}
