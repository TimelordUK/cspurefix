using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44.Components;

namespace PureFix.Types.FIX44.Components
{
	public sealed partial class TrdRegTimestamps : IFixComponent
	{
		public sealed partial class NoTrdRegTimestamps : IFixGroup
		{
			[TagDetails(Tag = 769, Type = TagType.UtcTimestamp, Offset = 0, Required = false)]
			public DateTime? TrdRegTimestamp {get; set;}
			
			[TagDetails(Tag = 770, Type = TagType.Int, Offset = 1, Required = false)]
			public int? TrdRegTimestampType {get; set;}
			
			[TagDetails(Tag = 771, Type = TagType.String, Offset = 2, Required = false)]
			public string? TrdRegTimestampOrigin {get; set;}
			
			
			bool IFixValidator.IsValid(in FixValidatorConfig config)
			{
				return true;
			}
			
			void IFixEncoder.Encode(IFixWriter writer)
			{
				if (TrdRegTimestamp is not null) writer.WriteUtcTimeStamp(769, TrdRegTimestamp.Value);
				if (TrdRegTimestampType is not null) writer.WriteWholeNumber(770, TrdRegTimestampType.Value);
				if (TrdRegTimestampOrigin is not null) writer.WriteString(771, TrdRegTimestampOrigin);
			}
			
			void IFixParser.Parse(IMessageView? view)
			{
				if (view is null) return;
				
				TrdRegTimestamp = view.GetDateTime(769);
				TrdRegTimestampType = view.GetInt32(770);
				TrdRegTimestampOrigin = view.GetString(771);
			}
			
			bool IFixLookup.TryGetByTag(string name, out object? value)
			{
				value = null;
				switch (name)
				{
					case "TrdRegTimestamp":
					{
						value = TrdRegTimestamp;
						break;
					}
					case "TrdRegTimestampType":
					{
						value = TrdRegTimestampType;
						break;
					}
					case "TrdRegTimestampOrigin":
					{
						value = TrdRegTimestampOrigin;
						break;
					}
					default:
					{
						return false;
					}
				}
				return true;
			}
			
			void IFixReset.Reset()
			{
				TrdRegTimestamp = null;
				TrdRegTimestampType = null;
				TrdRegTimestampOrigin = null;
			}
		}
		[Group(NoOfTag = 768, Offset = 0, Required = false)]
		public NoTrdRegTimestamps[]? TrdRegTimestampsItems {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (TrdRegTimestampsItems is not null && TrdRegTimestampsItems.Length != 0)
			{
				writer.WriteWholeNumber(768, TrdRegTimestampsItems.Length);
				for (int i = 0; i < TrdRegTimestampsItems.Length; i++)
				{
					((IFixEncoder)TrdRegTimestampsItems[i]).Encode(writer);
				}
			}
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			if (view.GetView("NoTrdRegTimestamps") is IMessageView viewNoTrdRegTimestamps)
			{
				var count = viewNoTrdRegTimestamps.GroupCount();
				TrdRegTimestampsItems = new NoTrdRegTimestamps[count];
				for (int i = 0; i < count; i++)
				{
					TrdRegTimestampsItems[i] = new();
					((IFixParser)TrdRegTimestampsItems[i]).Parse(viewNoTrdRegTimestamps.GetGroupInstance(i));
				}
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "NoTrdRegTimestamps":
				{
					value = TrdRegTimestampsItems;
					break;
				}
				default:
				{
					return false;
				}
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			TrdRegTimestampsItems = null;
		}
	}
}
