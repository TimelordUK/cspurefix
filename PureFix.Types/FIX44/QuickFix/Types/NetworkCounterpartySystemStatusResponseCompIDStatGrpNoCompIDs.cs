using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NetworkCounterpartySystemStatusResponseCompIDStatGrpNoCompIDs : IFixGroup
	{
		[TagDetails(Tag = 930, Type = TagType.String, Offset = 0, Required = false)]
		public string? RefCompID {get; set;}
		
		[TagDetails(Tag = 931, Type = TagType.String, Offset = 1, Required = false)]
		public string? RefSubID {get; set;}
		
		[TagDetails(Tag = 283, Type = TagType.String, Offset = 2, Required = false)]
		public string? LocationID {get; set;}
		
		[TagDetails(Tag = 284, Type = TagType.String, Offset = 3, Required = false)]
		public string? DeskID {get; set;}
		
		[TagDetails(Tag = 928, Type = TagType.Int, Offset = 4, Required = false)]
		public int? StatusValue {get; set;}
		
		[TagDetails(Tag = 929, Type = TagType.String, Offset = 5, Required = false)]
		public string? StatusText {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RefCompID is not null) writer.WriteString(930, RefCompID);
			if (RefSubID is not null) writer.WriteString(931, RefSubID);
			if (LocationID is not null) writer.WriteString(283, LocationID);
			if (DeskID is not null) writer.WriteString(284, DeskID);
			if (StatusValue is not null) writer.WriteWholeNumber(928, StatusValue.Value);
			if (StatusText is not null) writer.WriteString(929, StatusText);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RefCompID = view.GetString(930);
			RefSubID = view.GetString(931);
			LocationID = view.GetString(283);
			DeskID = view.GetString(284);
			StatusValue = view.GetInt32(928);
			StatusText = view.GetString(929);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RefCompID":
					value = RefCompID;
					break;
				case "RefSubID":
					value = RefSubID;
					break;
				case "LocationID":
					value = LocationID;
					break;
				case "DeskID":
					value = DeskID;
					break;
				case "StatusValue":
					value = StatusValue;
					break;
				case "StatusText":
					value = StatusText;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RefCompID = null;
			RefSubID = null;
			LocationID = null;
			DeskID = null;
			StatusValue = null;
			StatusText = null;
		}
	}
}
