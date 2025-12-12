using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types;
using PureFix.Types.FIX44UnitTest.Components;

namespace PureFix.Types.FIX44UnitTest.Components
{
	public sealed partial class FinancingDetails : IFixComponent
	{
		[TagDetails(Tag = 913, Type = TagType.String, Offset = 0, Required = false)]
		public string? AgreementDesc {get; set;}
		
		[TagDetails(Tag = 914, Type = TagType.String, Offset = 1, Required = false)]
		public string? AgreementID {get; set;}
		
		[TagDetails(Tag = 915, Type = TagType.LocalDate, Offset = 2, Required = false)]
		public DateOnly? AgreementDate {get; set;}
		
		[TagDetails(Tag = 918, Type = TagType.String, Offset = 3, Required = false)]
		public string? AgreementCurrency {get; set;}
		
		[TagDetails(Tag = 788, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TerminationType {get; set;}
		
		[TagDetails(Tag = 916, Type = TagType.LocalDate, Offset = 5, Required = false)]
		public DateOnly? StartDate {get; set;}
		
		[TagDetails(Tag = 917, Type = TagType.LocalDate, Offset = 6, Required = false)]
		public DateOnly? EndDate {get; set;}
		
		[TagDetails(Tag = 919, Type = TagType.Int, Offset = 7, Required = false)]
		public int? DeliveryType {get; set;}
		
		[TagDetails(Tag = 898, Type = TagType.Float, Offset = 8, Required = false)]
		public double? MarginRatio {get; set;}
		
		
		bool IFixValidator.IsValid(in FixValidatorConfig config)
		{
			return true;
		}
		
		void IFixEncoder.Encode(IFixWriter writer)
		{
			if (AgreementDesc is not null) writer.WriteString(913, AgreementDesc);
			if (AgreementID is not null) writer.WriteString(914, AgreementID);
			if (AgreementDate is not null) writer.WriteLocalDateOnly(915, AgreementDate.Value);
			if (AgreementCurrency is not null) writer.WriteString(918, AgreementCurrency);
			if (TerminationType is not null) writer.WriteWholeNumber(788, TerminationType.Value);
			if (StartDate is not null) writer.WriteLocalDateOnly(916, StartDate.Value);
			if (EndDate is not null) writer.WriteLocalDateOnly(917, EndDate.Value);
			if (DeliveryType is not null) writer.WriteWholeNumber(919, DeliveryType.Value);
			if (MarginRatio is not null) writer.WriteNumber(898, MarginRatio.Value);
		}
		
		void IFixParser.Parse(IMessageView? view)
		{
			if (view is null) return;
			
			AgreementDesc = view.GetString(913);
			AgreementID = view.GetString(914);
			AgreementDate = view.GetDateOnly(915);
			AgreementCurrency = view.GetString(918);
			TerminationType = view.GetInt32(788);
			StartDate = view.GetDateOnly(916);
			EndDate = view.GetDateOnly(917);
			DeliveryType = view.GetInt32(919);
			MarginRatio = view.GetDouble(898);
		}
		
		bool IFixLookup.TryGetByTag(string name, out object? value)
		{
			value = null;
			switch (name)
			{
				case "AgreementDesc":
				{
					value = AgreementDesc;
					break;
				}
				case "AgreementID":
				{
					value = AgreementID;
					break;
				}
				case "AgreementDate":
				{
					value = AgreementDate;
					break;
				}
				case "AgreementCurrency":
				{
					value = AgreementCurrency;
					break;
				}
				case "TerminationType":
				{
					value = TerminationType;
					break;
				}
				case "StartDate":
				{
					value = StartDate;
					break;
				}
				case "EndDate":
				{
					value = EndDate;
					break;
				}
				case "DeliveryType":
				{
					value = DeliveryType;
					break;
				}
				case "MarginRatio":
				{
					value = MarginRatio;
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
			AgreementDesc = null;
			AgreementID = null;
			AgreementDate = null;
			AgreementCurrency = null;
			TerminationType = null;
			StartDate = null;
			EndDate = null;
			DeliveryType = null;
			MarginRatio = null;
		}
	}
}
