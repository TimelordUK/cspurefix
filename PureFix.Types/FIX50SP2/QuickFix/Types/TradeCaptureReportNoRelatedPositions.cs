using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class TradeCaptureReportNoRelatedPositions : IFixGroup
	{
		[TagDetails(Tag = 1862, Type = TagType.String, Offset = 0, Required = false)]
		public string? RelatedPositionID {get; set;}
		
		[TagDetails(Tag = 1863, Type = TagType.Int, Offset = 1, Required = false)]
		public int? RelatedPositionIDSource {get; set;}
		
		[TagDetails(Tag = 1864, Type = TagType.LocalDate, Offset = 2, Required = false)]
		public DateOnly? RelatedPositionDate {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (RelatedPositionID is not null) writer.WriteString(1862, RelatedPositionID);
			if (RelatedPositionIDSource is not null) writer.WriteWholeNumber(1863, RelatedPositionIDSource.Value);
			if (RelatedPositionDate is not null) writer.WriteLocalDateOnly(1864, RelatedPositionDate.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			RelatedPositionID = view.GetString(1862);
			RelatedPositionIDSource = view.GetInt32(1863);
			RelatedPositionDate = view.GetDateOnly(1864);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "RelatedPositionID":
					value = RelatedPositionID;
					break;
				case "RelatedPositionIDSource":
					value = RelatedPositionIDSource;
					break;
				case "RelatedPositionDate":
					value = RelatedPositionDate;
					break;
				default: return false;
			}
			return true;
		}
		
		void IFixReset.Reset()
		{
			RelatedPositionID = null;
			RelatedPositionIDSource = null;
			RelatedPositionDate = null;
		}
	}
}
