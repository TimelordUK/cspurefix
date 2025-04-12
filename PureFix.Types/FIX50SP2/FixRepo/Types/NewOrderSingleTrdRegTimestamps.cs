using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.FixRepo.Types;

namespace PureFix.Types.FIX50SP2.FixRepo.Types
{
	public sealed partial class NewOrderSingleTrdRegTimestamps : IFixGroup
	{
		[TagDetails(Tag = 769, Type = TagType.UtcTimestamp, Offset = 0, Required = false)]
		public DateTime? TrdRegTimestamp {get; set;}
		
		[TagDetails(Tag = 770, Type = TagType.Int, Offset = 1, Required = false)]
		public int? TrdRegTimestampType {get; set;}
		
		[TagDetails(Tag = 771, Type = TagType.String, Offset = 2, Required = false)]
		public string? TrdRegTimestampOrigin {get; set;}
		
		[TagDetails(Tag = 1033, Type = TagType.String, Offset = 3, Required = false)]
		public string? DeskType {get; set;}
		
		[TagDetails(Tag = 1034, Type = TagType.Int, Offset = 4, Required = false)]
		public int? DeskTypeSource {get; set;}
		
		[TagDetails(Tag = 1035, Type = TagType.String, Offset = 5, Required = false)]
		public string? DeskOrderHandlingInst {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TrdRegTimestamp is not null) writer.WriteUtcTimeStamp(769, TrdRegTimestamp.Value);
			if (TrdRegTimestampType is not null) writer.WriteWholeNumber(770, TrdRegTimestampType.Value);
			if (TrdRegTimestampOrigin is not null) writer.WriteString(771, TrdRegTimestampOrigin);
			if (DeskType is not null) writer.WriteString(1033, DeskType);
			if (DeskTypeSource is not null) writer.WriteWholeNumber(1034, DeskTypeSource.Value);
			if (DeskOrderHandlingInst is not null) writer.WriteString(1035, DeskOrderHandlingInst);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			TrdRegTimestamp = view.GetDateTime(769);
			TrdRegTimestampType = view.GetInt32(770);
			TrdRegTimestampOrigin = view.GetString(771);
			DeskType = view.GetString(1033);
			DeskTypeSource = view.GetInt32(1034);
			DeskOrderHandlingInst = view.GetString(1035);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "TrdRegTimestamp":
					value = TrdRegTimestamp;
					break;
				case "TrdRegTimestampType":
					value = TrdRegTimestampType;
					break;
				case "TrdRegTimestampOrigin":
					value = TrdRegTimestampOrigin;
					break;
				case "DeskType":
					value = DeskType;
					break;
				case "DeskTypeSource":
					value = DeskTypeSource;
					break;
				case "DeskOrderHandlingInst":
					value = DeskOrderHandlingInst;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			TrdRegTimestamp = null;
			TrdRegTimestampType = null;
			TrdRegTimestampOrigin = null;
			DeskType = null;
			DeskTypeSource = null;
			DeskOrderHandlingInst = null;
		}
	}
}
