using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PureFix.Types.FIX44.QuickFix.Types;

namespace PureFix.Types.FIX44.QuickFix.Types
{
	public sealed class FinancingDetails
	{
		[TagDetails(Tag = 913, Type = TagType.String, Offset = 0, Required = false)]
		public string? AgreementDesc { get; set; }
		
		[TagDetails(Tag = 914, Type = TagType.String, Offset = 1, Required = false)]
		public string? AgreementID { get; set; }
		
		[TagDetails(Tag = 915, Type = TagType.LocalDate, Offset = 2, Required = false)]
		public DateOnly? AgreementDate { get; set; }
		
		[TagDetails(Tag = 918, Type = TagType.String, Offset = 3, Required = false)]
		public string? AgreementCurrency { get; set; }
		
		[TagDetails(Tag = 788, Type = TagType.Int, Offset = 4, Required = false)]
		public int? TerminationType { get; set; }
		
		[TagDetails(Tag = 916, Type = TagType.LocalDate, Offset = 5, Required = false)]
		public DateOnly? StartDate { get; set; }
		
		[TagDetails(Tag = 917, Type = TagType.LocalDate, Offset = 6, Required = false)]
		public DateOnly? EndDate { get; set; }
		
		[TagDetails(Tag = 919, Type = TagType.Int, Offset = 7, Required = false)]
		public int? DeliveryType { get; set; }
		
		[TagDetails(Tag = 898, Type = TagType.Float, Offset = 8, Required = false)]
		public double? MarginRatio { get; set; }
		
	}
}
