using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX50SP2.QuickFix.Types;

namespace PureFix.Types.FIX50SP2.QuickFix.Types
{
	public sealed partial class NoUnderlyingProtectionTermEvents : IFixGroup
	{
		[TagDetails(Tag = 42078, Type = TagType.String, Offset = 0, Required = false)]
		public string? UnderlyingProtectionTermEventType {get; set;}
		
		[TagDetails(Tag = 42079, Type = TagType.String, Offset = 1, Required = false)]
		public string? UnderlyingProtectionTermEventValue {get; set;}
		
		[TagDetails(Tag = 42080, Type = TagType.String, Offset = 2, Required = false)]
		public string? UnderlyingProtectionTermEventCurrency {get; set;}
		
		[TagDetails(Tag = 42081, Type = TagType.Int, Offset = 3, Required = false)]
		public int? UnderlyingProtectionTermEventPeriod {get; set;}
		
		[TagDetails(Tag = 42082, Type = TagType.String, Offset = 4, Required = false)]
		public string? UnderlyingProtectionTermEventUnit {get; set;}
		
		[TagDetails(Tag = 42083, Type = TagType.Int, Offset = 5, Required = false)]
		public int? UnderlyingProtectionTermEventDayType {get; set;}
		
		[TagDetails(Tag = 42084, Type = TagType.String, Offset = 6, Required = false)]
		public string? UnderlyingProtectionTermEventRateSource {get; set;}
		
		[Component(Offset = 7, Required = false)]
		public UnderlyingProtectionTermEventQualifierGrpComponent? UnderlyingProtectionTermEventQualifierGrp {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (UnderlyingProtectionTermEventType is not null) writer.WriteString(42078, UnderlyingProtectionTermEventType);
			if (UnderlyingProtectionTermEventValue is not null) writer.WriteString(42079, UnderlyingProtectionTermEventValue);
			if (UnderlyingProtectionTermEventCurrency is not null) writer.WriteString(42080, UnderlyingProtectionTermEventCurrency);
			if (UnderlyingProtectionTermEventPeriod is not null) writer.WriteWholeNumber(42081, UnderlyingProtectionTermEventPeriod.Value);
			if (UnderlyingProtectionTermEventUnit is not null) writer.WriteString(42082, UnderlyingProtectionTermEventUnit);
			if (UnderlyingProtectionTermEventDayType is not null) writer.WriteWholeNumber(42083, UnderlyingProtectionTermEventDayType.Value);
			if (UnderlyingProtectionTermEventRateSource is not null) writer.WriteString(42084, UnderlyingProtectionTermEventRateSource);
			if (UnderlyingProtectionTermEventQualifierGrp is not null) ((IFixEncoder)UnderlyingProtectionTermEventQualifierGrp).Encode(writer);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			UnderlyingProtectionTermEventType = view.GetString(42078);
			UnderlyingProtectionTermEventValue = view.GetString(42079);
			UnderlyingProtectionTermEventCurrency = view.GetString(42080);
			UnderlyingProtectionTermEventPeriod = view.GetInt32(42081);
			UnderlyingProtectionTermEventUnit = view.GetString(42082);
			UnderlyingProtectionTermEventDayType = view.GetInt32(42083);
			UnderlyingProtectionTermEventRateSource = view.GetString(42084);
			if (view.GetView("UnderlyingProtectionTermEventQualifierGrp") is IMessageView viewUnderlyingProtectionTermEventQualifierGrp)
			{
				UnderlyingProtectionTermEventQualifierGrp = new();
				((IFixParser)UnderlyingProtectionTermEventQualifierGrp).Parse(viewUnderlyingProtectionTermEventQualifierGrp);
			}
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "UnderlyingProtectionTermEventType":
					value = UnderlyingProtectionTermEventType;
					break;
				case "UnderlyingProtectionTermEventValue":
					value = UnderlyingProtectionTermEventValue;
					break;
				case "UnderlyingProtectionTermEventCurrency":
					value = UnderlyingProtectionTermEventCurrency;
					break;
				case "UnderlyingProtectionTermEventPeriod":
					value = UnderlyingProtectionTermEventPeriod;
					break;
				case "UnderlyingProtectionTermEventUnit":
					value = UnderlyingProtectionTermEventUnit;
					break;
				case "UnderlyingProtectionTermEventDayType":
					value = UnderlyingProtectionTermEventDayType;
					break;
				case "UnderlyingProtectionTermEventRateSource":
					value = UnderlyingProtectionTermEventRateSource;
					break;
				case "UnderlyingProtectionTermEventQualifierGrp":
					value = UnderlyingProtectionTermEventQualifierGrp;
					break;
				default: return false;
			}
			return true;
		}
	}
}
