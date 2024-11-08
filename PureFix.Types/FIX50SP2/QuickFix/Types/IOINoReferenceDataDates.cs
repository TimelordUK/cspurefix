using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class IOINoReferenceDataDates : IFixGroup
	{
		[TagDetails(Tag = 2747, Type = TagType.UtcTimestamp, Offset = 0, Required = false)]
		public DateTime? ReferenceDataDate {get; set;}
		
		[TagDetails(Tag = 2748, Type = TagType.Int, Offset = 1, Required = false)]
		public int? ReferenceDataDateType {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (ReferenceDataDate is not null) writer.WriteUtcTimeStamp(2747, ReferenceDataDate.Value);
			if (ReferenceDataDateType is not null) writer.WriteWholeNumber(2748, ReferenceDataDateType.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			ReferenceDataDate = view.GetDateTime(2747);
			ReferenceDataDateType = view.GetInt32(2748);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "ReferenceDataDate":
					value = ReferenceDataDate;
					break;
				case "ReferenceDataDateType":
					value = ReferenceDataDateType;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			ReferenceDataDate = null;
			ReferenceDataDateType = null;
		}
	}
}
