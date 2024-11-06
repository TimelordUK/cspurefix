using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX42.QuickFix.Types;

namespace PureFix.Types.FIX42.QuickFix.Types
{
	public sealed partial class NewsNoRoutingIDs : IFixGroup
	{
		[TagDetails(Tag = 216, Type = TagType.Int, Offset = 0, Required = false)]
		public int? RoutingType {get; set;}
		
		[TagDetails(Tag = 217, Type = TagType.String, Offset = 1, Required = false)]
		public string? RoutingID {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RoutingType is not null) writer.WriteWholeNumber(216, RoutingType.Value);
			if (RoutingID is not null) writer.WriteString(217, RoutingID);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RoutingType = view.GetInt32(216);
			RoutingID = view.GetString(217);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RoutingType":
					value = RoutingType;
					break;
				case "RoutingID":
					value = RoutingID;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RoutingType = null;
			RoutingID = null;
		}
	}
}
