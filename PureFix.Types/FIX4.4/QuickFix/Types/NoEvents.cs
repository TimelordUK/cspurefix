using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed partial class NoEvents : IFixGroup
	{
		[TagDetails(Tag = 865, Type = TagType.Int, Offset = 0, Required = false)]
		public int? EventType {get; set;}
		
		[TagDetails(Tag = 866, Type = TagType.LocalDate, Offset = 1, Required = false)]
		public DateOnly? EventDate {get; set;}
		
		[TagDetails(Tag = 867, Type = TagType.Float, Offset = 2, Required = false)]
		public double? EventPx {get; set;}
		
		[TagDetails(Tag = 868, Type = TagType.String, Offset = 3, Required = false)]
		public string? EventText {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (EventType is not null) writer.WriteWholeNumber(865, EventType.Value);
			if (EventDate is not null) writer.WriteLocalDateOnly(866, EventDate.Value);
			if (EventPx is not null) writer.WriteNumber(867, EventPx.Value);
			if (EventText is not null) writer.WriteString(868, EventText);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			EventType = view.GetInt32(865);
			EventDate = view.GetDateOnly(866);
			EventPx = view.GetDouble(867);
			EventText = view.GetString(868);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "EventType":
					value = EventType;
					break;
				case "EventDate":
					value = EventDate;
					break;
				case "EventPx":
					value = EventPx;
					break;
				case "EventText":
					value = EventText;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			EventType = null;
			EventDate = null;
			EventPx = null;
			EventText = null;
		}
	}
}
